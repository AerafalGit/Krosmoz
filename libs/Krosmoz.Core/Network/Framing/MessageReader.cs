// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers;
using System.IO.Pipelines;
using Krosmoz.Core.Network.Metadata;

namespace Krosmoz.Core.Network.Framing;

/// <summary>
/// Represents a reader for processing network messages asynchronously from a pipe reader.
/// </summary>
/// <typeparam name="TMessage">The type of the network message to read.</typeparam>
internal sealed class MessageReader<TMessage> : IAsyncDisposable
    where TMessage : class
{
    private readonly PipeReader _reader;
    private readonly IMessageDecoder<TMessage> _decoder;

    private ReadOnlySequence<byte> _buffer;
    private SequencePosition _examined;
    private SequencePosition _consumed;
    private bool _isCanceled;
    private bool _isCompleted;
    private bool _hasMessage;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageReader{TMessage}"/> class.
    /// </summary>
    /// <param name="reader">The pipe reader to read data from.</param>
    /// <param name="decoder">The decoder to decode messages from the data.</param>
    public MessageReader(PipeReader reader, IMessageDecoder<TMessage> decoder)
    {
        _reader = reader;
        _decoder = decoder;
    }

    /// <summary>
    /// Reads a network message asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous read operation, containing the network metadata.</returns>
    public ValueTask<MessageMetadata<TMessage>> ReadAsync(CancellationToken cancellationToken)
    {
        if (_disposed)
            return new ValueTask<MessageMetadata<TMessage>>(new MessageMetadata<TMessage>(true, true));

        if (_hasMessage)
            throw new InvalidOperationException($"{nameof(Advance)} must be called before calling {nameof(ReadAsync)}.");

        if (_consumed.GetObject() is null)
            return DoAsyncRead(cancellationToken);

        if (_decoder.TryDecodeMessage(_buffer, ref _consumed, ref _examined, out var message))
        {
            _hasMessage = true;
            return new ValueTask<MessageMetadata<TMessage>>(new MessageMetadata<TMessage>(message, _isCanceled, false));
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
                throw new OutOfMemoryException("The buffer should be empty when the reader is completed.");

            return new ValueTask<MessageMetadata<TMessage>>(new MessageMetadata<TMessage>(_isCanceled, true));
        }

        return DoAsyncRead(cancellationToken);
    }

    /// <summary>
    /// Performs the asynchronous read operation.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous read operation, containing the network metadata.</returns>
    private ValueTask<MessageMetadata<TMessage>> DoAsyncRead(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var readTask = _reader.ReadAsync(cancellationToken);

            ReadResult result;

            if (readTask.IsCompletedSuccessfully)
                result = readTask.Result;
            else
                return ReadAsyncCore(readTask, cancellationToken);

            var (shouldContinue, hasMessage) = TrySetMessage(result, out var readResult);

            if (hasMessage)
                return new ValueTask<MessageMetadata<TMessage>>(readResult);

            if (!shouldContinue)
                break;
        }

        return new ValueTask<MessageMetadata<TMessage>>(new MessageMetadata<TMessage>(_isCanceled, _isCompleted));
    }

    /// <summary>
    /// Performs the asynchronous read operation when the read task is not completed successfully.
    /// </summary>
    /// <param name="readTask">The read task to await.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous read operation, containing the network metadata.</returns>
    private async ValueTask<MessageMetadata<TMessage>> ReadAsyncCore(ValueTask<ReadResult> readTask, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var result = await readTask.ConfigureAwait(false);

            var (shouldContinue, hasMessage) = TrySetMessage(result, out var readResult);

            if (hasMessage)
                return readResult;

            if (!shouldContinue)
                break;

            readTask = _reader.ReadAsync(cancellationToken);
        }

        return new MessageMetadata<TMessage>(_isCanceled, _isCompleted);
    }

    /// <summary>
    /// Attempts to set the message from the read result.
    /// </summary>
    /// <param name="result">The result of the read operation.</param>
    /// <param name="metadata">The network metadata to set if a message is decoded.</param>
    /// <returns>A tuple indicating whether to continue reading and whether a message was decoded.</returns>
    private (bool ShouldContinue, bool HasMessage) TrySetMessage(ReadResult result, out MessageMetadata<TMessage> metadata)
    {
        _buffer = result.Buffer;
        _isCanceled = result.IsCanceled;
        _isCompleted = result.IsCompleted;
        _consumed = _buffer.Start;
        _examined = _buffer.End;

        if (_isCanceled)
        {
            metadata = default;
            return (false, false);
        }

        if (_decoder.TryDecodeMessage(_buffer, ref _consumed, ref _examined, out var message))
        {
            _hasMessage = true;
            metadata = new MessageMetadata<TMessage>(message, _isCanceled, false);
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
                throw new InvalidOperationException("The buffer should be empty when the reader is completed.");

            metadata = default;
            return (false, false);
        }

        metadata = default;
        return (true, false);
    }

    /// <summary>
    /// Advances the reader to the next message.
    /// </summary>
    public void Advance()
    {
        if (_disposed)
            return;

        _isCanceled = false;

        if (!_hasMessage)
            return;

        _buffer = _buffer.Slice(_consumed);
        _hasMessage = false;
    }

    /// <summary>
    /// Disposes the reader asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous dispose operation.</returns>
    public async ValueTask DisposeAsync()
    {
        if (_disposed)
            return;

        _disposed = true;

        await _reader.CompleteAsync().ConfigureAwait(false);
    }
}
