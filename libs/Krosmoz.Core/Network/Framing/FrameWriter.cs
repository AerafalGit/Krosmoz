// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.IO.Pipelines;
using Krosmoz.Core.Network.Framing.Serialization;

namespace Krosmoz.Core.Network.Framing;

/// <summary>
/// Represents a writer for encoding and writing frames to a stream or pipe.
/// Provides methods for writing single or multiple messages asynchronously.
/// </summary>
/// <typeparam name="TMessage">The type of the message to encode and write.</typeparam>
public sealed class FrameWriter<TMessage> : IAsyncDisposable
    where TMessage : class
{
    private readonly PipeWriter _writer;
    private readonly SemaphoreSlim _semaphore;
    private readonly IMessageEncoder<TMessage> _encoder;

    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameWriter{TMessage}"/> class using a stream.
    /// </summary>
    /// <param name="stream">The stream to write frames to.</param>
    /// <param name="encoder">The encoder to use for encoding messages.</param>
    public FrameWriter(Stream stream, IMessageEncoder<TMessage> encoder) : this(PipeWriter.Create(stream), encoder)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameWriter{TMessage}"/> class using a pipe writer.
    /// </summary>
    /// <param name="writer">The pipe writer to write frames to.</param>
    /// <param name="encoder">The encoder to use for encoding messages.</param>
    public FrameWriter(PipeWriter writer, IMessageEncoder<TMessage> encoder)
    {
        _writer = writer;
        _encoder = encoder;
        _semaphore = new SemaphoreSlim(1);
    }

    /// <summary>
    /// Writes a single message asynchronously to the underlying writer.
    /// </summary>
    /// <param name="message">The message to write.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous write operation.</returns>
    public async ValueTask WriteAsync(TMessage message, CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            if (_disposed)
                return;

            _encoder.EncodeMessage(_writer, message);

            var result = await _writer.FlushAsync(cancellationToken).ConfigureAwait(false);

            if (result.IsCanceled)
                throw new OperationCanceledException("The write operation was canceled.", cancellationToken);

            if (result.IsCompleted)
                _disposed = true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Writes multiple messages asynchronously to the underlying writer.
    /// </summary>
    /// <param name="messages">The collection of messages to write.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous write operation.</returns>
    public async ValueTask WriteManyAsync(IEnumerable<TMessage> messages, CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            if (_disposed)
                return;

            foreach (var message in messages)
                _encoder.EncodeMessage(_writer, message);

            var result = await _writer.FlushAsync(cancellationToken).ConfigureAwait(false);

            if (result.IsCanceled)
                throw new OperationCanceledException("The write operation was canceled.", cancellationToken);

            if (result.IsCompleted)
                _disposed = true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Disposes the writer asynchronously, releasing resources.
    /// </summary>
    /// <returns>A task representing the asynchronous dispose operation.</returns>
    public async ValueTask DisposeAsync()
    {
        await _semaphore.WaitAsync().ConfigureAwait(false);

        try
        {
            if (_disposed)
                return;

            _disposed = true;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
