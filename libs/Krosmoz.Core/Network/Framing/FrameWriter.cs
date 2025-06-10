// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;
using System.IO.Pipelines;
using Krosmoz.Core.Network.Framing.Serialization;
using Krosmoz.Core.Network.Metadata;
using Krosmoz.Core.Network.Transport;

namespace Krosmoz.Core.Network.Framing;

/// <summary>
/// Represents a writer for encoding and writing frames to a stream or pipe.
/// Provides methods for writing single or multiple messages asynchronously.
/// </summary>
public sealed class FrameWriter : IAsyncDisposable
{
    private static readonly ActivitySource s_activitySource = new("Boufbowl.Framing");

    private readonly PipeWriter _writer;
    private readonly SemaphoreSlim _semaphore;
    private readonly IMessageFactory _factory;
    private readonly SocketConnectionMetrics _socketConnectionMetrics;

    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameWriter"/> class using a pipe writer.
    /// </summary>
    /// <param name="writer">The pipe writer to write frames to.</param>
    /// <param name="factory">The factory used to create messages.</param>
    /// <param name="socketConnectionMetrics">Metrics for tracking socket connection performance.</param>
    public FrameWriter(
        PipeWriter writer,
        IMessageFactory factory,
        SocketConnectionMetrics socketConnectionMetrics)
    {
        _writer = writer;
        _factory = factory;
        _socketConnectionMetrics = socketConnectionMetrics;
        _semaphore = new SemaphoreSlim(1);
    }

    /// <summary>
    /// Writes a single message asynchronously to the underlying writer.
    /// </summary>
    /// <param name="message">The message to write.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous write operation.</returns>
    public async ValueTask WriteAsync(DofusMessage message, CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            if (_disposed)
                return;

            var messageName = _factory.CreateMessageName(message.ProtocolId);

            var messageLength = MessageEncoder.EncodeMessage(_writer, message);

            using var activity = s_activitySource.CreateActivity("message.write", ActivityKind.Internal);

            activity?.SetTag("connection.id", _socketConnectionMetrics.ConnectionId);
            activity?.SetTag("connection.endpoint", _socketConnectionMetrics.RemoteEndPoint);

            activity?.SetTag("message.id", message.ProtocolId);
            activity?.SetTag("message.name", messageName);
            activity?.SetTag("message.length", messageLength);

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

