// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.IO.Pipelines;

namespace Krosmoz.Core.Network.Framing;

/// <summary>
/// Represents a writer for encoding and writing network messages asynchronously to a pipe writer.
/// </summary>
/// <typeparam name="TMessage">The type of the network message to write.</typeparam>
internal sealed class MessageWriter<TMessage> : IAsyncDisposable
    where TMessage : class
{
    private readonly PipeWriter _writer;
    private readonly IMessageEncoder<TMessage> _encoder;
    private readonly SemaphoreSlim _semaphore;

    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageWriter{TMessage}"/> class.
    /// </summary>
    /// <param name="writer">The pipe writer to write data to.</param>
    /// <param name="encoder">The encoder to encode messages into the data.</param>
    public MessageWriter(PipeWriter writer, IMessageEncoder<TMessage> encoder)
    {
        _writer = writer;
        _encoder = encoder;
        _semaphore = new SemaphoreSlim(1);
    }

    /// <summary>
    /// Writes a network message asynchronously to the pipe writer.
    /// </summary>
    /// <param name="message">The network message to write.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// <exception cref="OperationCanceledException">Thrown if the write operation is canceled.</exception>
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
                throw new OperationCanceledException("The write operation was canceled.");

            if (result.IsCompleted)
                _disposed = true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Disposes the writer asynchronously, releasing any resources held.
    /// </summary>
    /// <returns>A task that represents the asynchronous dispose operation.</returns>
    public async ValueTask DisposeAsync()
    {
        await _semaphore.WaitAsync().ConfigureAwait(false);

        try
        {
            if (_disposed)
                return;

            _disposed = true;

            await _writer.CompleteAsync().ConfigureAwait(false);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
