// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Net;
using Krosmoz.Core.Network.Framing;
using Krosmoz.Core.Network.Framing.Factory;
using Krosmoz.Core.Network.Metadata;
using Krosmoz.Servers.AuthServer.Database.Models.Accounts;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Servers.AuthServer.Network.Transport;

/// <summary>
/// Represents a connection to a Dofus client, managing message sending, connection state.
/// </summary>
public sealed class DofusConnection : IAsyncDisposable
{
    private readonly ConnectionContext _connection;
    private readonly FrameWriter _writer;
    private readonly IMessageFactory _messageFactory;
    private readonly ILogger<DofusConnection> _logger;

    private bool _disposed;

    /// <summary>
    /// Gets or sets the account record associated with the connection.
    /// </summary>
    public AccountRecord Account { get; set; } = null!;

    /// <summary>
    /// Gets or sets the server ID to auto-select for the connection.
    /// </summary>
    public ushort ServerIdAutoSelect { get; set; }

    /// <summary>
    /// Gets the unique identifier for the connection.
    /// </summary>
    public string ConnectionId =>
        _connection.ConnectionId;

    /// <summary>
    /// Gets the remote endpoint of the connection.
    /// </summary>
    public IPEndPoint? RemoteEndPoint =>
        _connection.RemoteEndPoint as IPEndPoint;

    /// <summary>
    /// Gets the cancellation token that signals when the connection is closed.
    /// </summary>
    public CancellationToken ConnectionClosed =>
        _connection.ConnectionClosed;

    /// <summary>
    /// Gets a value indicating whether the connection is active.
    /// </summary>
    public bool IsConnected =>
        !_disposed && !_connection.ConnectionClosed.IsCancellationRequested;

    /// <summary>
    /// Initializes a new instance of the <see cref="DofusConnection"/> class.
    /// </summary>
    /// <param name="connection">The connection context.</param>
    /// <param name="writer">The frame writer for sending messages.</param>
    /// <param name="logger">The logger for logging connection events.</param>
    public DofusConnection(
        ConnectionContext connection,
        FrameWriter writer,
        ILogger<DofusConnection> logger)
    {
        _connection = connection;
        _writer = writer;
        _messageFactory = connection.Features.Get<IMessageFactory>()!;
        _logger = logger;
    }

    /// <summary>
    /// Sends a new message of the specified type asynchronously.
    /// </summary>
    /// <typeparam name="TNewMessage">The type of the message to send.</typeparam>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask SendAsync<TNewMessage>()
        where TNewMessage : DofusMessage, new()
    {
        return SendAsync(new TNewMessage());
    }

    /// <summary>
    /// Sends the specified message asynchronously.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask SendAsync(DofusMessage message)
    {
        if (!IsConnected)
            return ValueTask.CompletedTask;

        var messageName = _messageFactory.CreateMessageName(message.ProtocolId);

        _logger.LogDebug("DofusConnection {ConnectionName} sending message {MessageName} ({MessageId})", this, messageName, message.ProtocolId);

        return _writer.WriteAsync(message, _connection.ConnectionClosed);
    }

    /// <summary>
    /// Aborts the connection immediately.
    /// </summary>
    public void Abort()
    {
        _connection.Abort();
    }

    /// <summary>
    /// Aborts the connection with a specified reason.
    /// </summary>
    /// <param name="reason">The reason for aborting the connection.</param>
    public void Abort(string reason)
    {
        Abort(new ConnectionAbortedException(reason));
    }

    /// <summary>
    /// Aborts the connection with a specified exception.
    /// </summary>
    /// <param name="reason">The exception describing the reason for aborting the connection.</param>
    public void Abort(ConnectionAbortedException reason)
    {
        _connection.Abort(reason);
    }

    /// <summary>
    /// Aborts the connection asynchronously after a specified delay.
    /// </summary>
    /// <param name="delay">The delay before aborting the connection.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async ValueTask AbortAsync(TimeSpan delay)
    {
        if (delay > TimeSpan.Zero)
            await Task.Delay(delay, ConnectionClosed).ConfigureAwait(false);

        Abort();
    }

    /// <summary>
    /// Aborts the connection asynchronously with a specified reason and delay.
    /// </summary>
    /// <param name="reason">The reason for aborting the connection.</param>
    /// <param name="delay">The delay before aborting the connection.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async ValueTask AbortAsync(string reason, TimeSpan delay)
    {
        if (delay > TimeSpan.Zero)
            await Task.Delay(delay, ConnectionClosed).ConfigureAwait(false);

        Abort(reason);
    }

    /// <summary>
    /// Aborts the connection asynchronously with a specified exception and delay.
    /// </summary>
    /// <param name="reason">The exception describing the reason for aborting the connection.</param>
    /// <param name="delay">The delay before aborting the connection.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async ValueTask AbortAsync(ConnectionAbortedException reason, TimeSpan delay)
    {
        if (delay > TimeSpan.Zero)
            await Task.Delay(delay, ConnectionClosed).ConfigureAwait(false);

        Abort(reason);
    }

    /// <summary>
    /// Disposes the connection asynchronously, releasing all resources.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async ValueTask DisposeAsync()
    {
        if (_disposed)
            return;

        _disposed = true;

        if (!_connection.ConnectionClosed.IsCancellationRequested)
            _connection.Abort();

        await _writer.DisposeAsync().ConfigureAwait(false);
        await _connection.DisposeAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Returns a string representation of the connection, including the remote endpoint or connection ID.
    /// </summary>
    /// <returns>A string representation of the connection.</returns>
    public override string ToString()
    {
        return RemoteEndPoint?.ToString() ?? ConnectionId;
    }
}
