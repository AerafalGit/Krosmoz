// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using Krosmoz.Core.Network.Framing.Serialization;
using Krosmoz.Core.Network.Metadata;
using Krosmoz.Core.Network.Transport;

namespace Krosmoz.Core.Network.Framing;

/// <summary>
/// Represents a reader for processing frames from a stream or pipe.
/// Provides methods for reading and decoding messages asynchronously.
/// </summary>
public sealed class FrameReader : IAsyncDisposable
{
    private static readonly ActivitySource s_activitySource = new("Boufbowl.Framing");

    private readonly PipeReader _reader;
    private readonly MessageDecoder _decoder;
    private readonly IMessageFactory _factory;
    private readonly SocketConnectionMetrics _socketConnectionMetrics;

    private ReadOnlySequence<byte> _buffer;
    private SequencePosition _consumed;
    private bool _disposed;
    private SequencePosition _examined;
    private bool _hasMessage;
    private bool _isCanceled;
    private bool _isCompleted;

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameReader"/> class using a pipe reader.
    /// </summary>
    /// <param name="reader">The pipe reader to read frames from.</param>
    /// <param name="decoder">The decoder to use for decoding messages.</param>
    /// <param name="factory">The factory used to create messages.</param>
    /// <param name="socketConnectionMetrics">Metrics for tracking socket connection performance.</param>
    public FrameReader(
        PipeReader reader,
        MessageDecoder decoder,
        IMessageFactory factory,
        SocketConnectionMetrics socketConnectionMetrics)
    {
        _reader = reader;
        _decoder = decoder;
        _factory = factory;
        _socketConnectionMetrics = socketConnectionMetrics;
    }

    /// <summary>
    /// Reads all messages asynchronously using the specified decoder.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous enumerable of decoded messages.</returns>
    public async IAsyncEnumerable<FrameReadResult> ReadAllAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var readResult = await ReadAsync(cancellationToken).ConfigureAwait(false);

                if (readResult.IsCanceled)
                    throw new OperationCanceledException("The read operation was canceled.", cancellationToken);

                if (readResult.IsCompleted)
                    yield break;

                if (readResult.Message is null || readResult.MessageName is null)
                    throw new InvalidDataException("The read operation returned a null message.");

                using var activity = s_activitySource.CreateActivity("message.read", ActivityKind.Internal);

                activity?.SetTag("connection.id", _socketConnectionMetrics.ConnectionId);
                activity?.SetTag("connection.endpoint", _socketConnectionMetrics.RemoteEndPoint);

                activity?.SetTag("message.id", readResult.Message.ProtocolId);
                activity?.SetTag("message.name", readResult.MessageName);
                activity?.SetTag("message.length", readResult.MessageLength);

                _socketConnectionMetrics.IncrementMessageReceived(readResult.MessageName, readResult.MessageLength);

                yield return readResult;
            }
            finally
            {
                Advance();
            }
        }
    }

    /// <summary>
    /// Reads a single frame asynchronously using the decoder.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the result of the read operation.</returns>
    private ValueTask<FrameReadResult> ReadAsync(CancellationToken cancellationToken)
    {
        ObjectDisposedException.ThrowIf(_disposed, GetType());

        if (_hasMessage)
            throw new InvalidOperationException($"{nameof(Advance)} must be called before calling {nameof(ReadAsync)}.");

        if (_consumed.GetObject() is null)
            return DoAsyncRead(cancellationToken);

        if (_decoder.TryDecodeMessage(_buffer, ref _consumed, ref _examined, out var messageLength, out var message))
        {
            _hasMessage = true;
            return new ValueTask<FrameReadResult>(new FrameReadResult(messageLength, _factory.CreateMessageName(message.ProtocolId), message, _isCanceled, false));
        }

        _reader.AdvanceTo(_consumed, _examined);

        _buffer = default;
        _consumed = default;
        _examined = default;

        if (_isCompleted)
        {
            _consumed = default;
            _examined = default;

            if (!_buffer.IsEmpty)
                throw new InvalidDataException("Connection terminated while reading a message.");

            return new ValueTask<FrameReadResult>(new FrameReadResult(_isCanceled, _isCompleted));
        }

        return DoAsyncRead(cancellationToken);
    }

    /// <summary>
    /// Performs an asynchronous read operation to decode a message.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the result of the read operation.</returns>
    private ValueTask<FrameReadResult> DoAsyncRead(CancellationToken cancellationToken)
    {
        while (true)
        {
            var readTask = _reader.ReadAsync(cancellationToken);

            ReadResult result;

            if (readTask.IsCompletedSuccessfully)
                result = readTask.Result;
            else
                return ContinueDoAsyncRead(readTask, cancellationToken);

            var (shouldContinue, hasMessage) = TrySetMessage(result, out var protocolReadResult);

            if (hasMessage)
                return new ValueTask<FrameReadResult>(protocolReadResult);

            if (!shouldContinue)
                break;
        }

        return new ValueTask<FrameReadResult>(new FrameReadResult(_isCanceled, _isCompleted));
    }

    /// <summary>
    /// Continues an asynchronous read operation to decode a message.
    /// </summary>
    /// <param name="readTask">The task representing the read operation.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the result of the read operation.</returns>
    private async ValueTask<FrameReadResult> ContinueDoAsyncRead(ValueTask<ReadResult> readTask, CancellationToken cancellationToken)
    {
        while (true)
        {
            var result = await readTask.ConfigureAwait(false);

            var (shouldContinue, hasMessage) = TrySetMessage(result, out var readResult);

            if (hasMessage)
                return readResult;

            if (!shouldContinue)
            {
                break;
            }

            readTask = _reader.ReadAsync(cancellationToken);
        }

        return new FrameReadResult(_isCanceled, _isCompleted);
    }

    /// <summary>
    /// Attempts to set the decoded message from the read result.
    /// </summary>
    /// <param name="result">The result of the read operation.</param>
    /// <param name="readResult">The result of the frame read operation.</param>
    /// <returns>A tuple indicating whether to continue and whether a message was decoded.</returns>
    private (bool ShouldContinue, bool HasMessage) TrySetMessage(ReadResult result, out FrameReadResult readResult)
    {
        _buffer = result.Buffer;
        _isCanceled = result.IsCanceled;
        _isCompleted = result.IsCompleted;
        _consumed = _buffer.Start;
        _examined = _buffer.End;

        if (_isCanceled)
        {
            readResult = FrameReadResult.Empty;
            return (false, false);
        }

        if (_decoder.TryDecodeMessage(_buffer, ref _consumed, ref _examined, out var messageLength, out var message))
        {
            _hasMessage = true;
            readResult = new FrameReadResult(messageLength, _factory.CreateMessageName(message.ProtocolId), message, _isCanceled, false);
            return (false, true);
        }

        _reader.AdvanceTo(_consumed, _examined);

        _buffer = default;
        _consumed = default;
        _examined = default;

        if (_isCompleted)
        {
            _consumed = default;
            _examined = default;

            if (!_buffer.IsEmpty)
                throw new InvalidDataException("Connection terminated while reading a message.");

            readResult = FrameReadResult.Empty;
            return (false, false);
        }

        readResult = FrameReadResult.Empty;
        return (true, false);
    }

    /// <summary>
    /// Advances the reader to the next frame.
    /// </summary>
    public void Advance()
    {
        ObjectDisposedException.ThrowIf(_disposed, GetType());

        _isCanceled = false;

        if (!_hasMessage)
            return;

        _buffer = _buffer.Slice(_consumed);

        _hasMessage = false;
    }

    /// <summary>
    /// Disposes the reader asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous dispose operation.</returns>
    public ValueTask DisposeAsync()
    {
        _disposed = true;

        return ValueTask.CompletedTask;
    }
}
