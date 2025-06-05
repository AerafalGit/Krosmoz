// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Core.Network.Server;

/// <summary>
/// Represents a server connection that implements multiple connection features.
/// Provides functionality for connection heartbeat, completion, lifetime notifications, and endpoint management.
/// </summary>
internal sealed class ServerConnection :
    IConnectionHeartbeatFeature,
    IConnectionCompleteFeature,
    IConnectionLifetimeNotificationFeature,
    IConnectionEndPointFeature
{
    private readonly CancellationTokenSource _connectionClosingCts;
    private readonly Lock _heartbeatLock;
    private readonly ILogger _logger;

    private List<(Action<object> handler, object state)>? _heartbeatHandlers;
    private Stack<KeyValuePair<Func<object, Task>, object>>? _onCompleted;
    private bool _completed;
    private string? _cachedToString;

    /// <summary>
    /// Gets the unique identifier for the connection.
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Gets the transport connection context associated with this server connection.
    /// </summary>
    public ConnectionContext TransportConnection { get; }

    /// <summary>
    /// Gets or sets the cancellation token that signals when the connection is requested to close.
    /// </summary>
    public CancellationToken ConnectionClosedRequested { get; set; }

    /// <summary>
    /// Gets or sets the local endpoint of the connection.
    /// </summary>
    public EndPoint? LocalEndPoint
    {
        get => TransportConnection.LocalEndPoint;
        set => TransportConnection.LocalEndPoint = value;
    }

    /// <summary>
    /// Gets or sets the remote endpoint of the connection.
    /// </summary>
    public EndPoint? RemoteEndPoint
    {
        get => TransportConnection.RemoteEndPoint;
        set => TransportConnection.RemoteEndPoint = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServerConnection"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the connection.</param>
    /// <param name="transportConnection">The transport connection context.</param>
    /// <param name="logger">The logger instance for logging connection events.</param>
    public ServerConnection(long id, ConnectionContext transportConnection, ILogger logger)
    {
        _logger = logger;
        _connectionClosingCts = new CancellationTokenSource();
        _heartbeatLock = new Lock();

        Id = id;
        TransportConnection = transportConnection;
        ConnectionClosedRequested = _connectionClosingCts.Token;

        transportConnection.Features.Set<IConnectionHeartbeatFeature>(this);
        transportConnection.Features.Set<IConnectionCompleteFeature>(this);
        transportConnection.Features.Set<IConnectionLifetimeNotificationFeature>(this);

        if (transportConnection.Features.Get<IConnectionEndPointFeature>() is null)
            transportConnection.Features.Set<IConnectionEndPointFeature>(this);
    }

    /// <summary>
    /// Registers a callback to be invoked when the connection is completed.
    /// </summary>
    /// <param name="callback">The callback function.</param>
    /// <param name="state">The state object to pass to the callback.</param>
    /// <exception cref="InvalidOperationException">Thrown if the connection is already complete.</exception>
    void IConnectionCompleteFeature.OnCompleted(Func<object, Task> callback, object state)
    {
        if (_completed)
            throw new InvalidOperationException("The connection is already complete.");

        _onCompleted ??= new Stack<KeyValuePair<Func<object, Task>, object>>();
        _onCompleted.Push(new KeyValuePair<Func<object, Task>, object>(callback, state));
    }

    /// <summary>
    /// Executes all registered heartbeat handlers.
    /// </summary>
    public void TickHeartbeat()
    {
        lock (_heartbeatLock)
        {
            if (_heartbeatHandlers is null)
                return;

            foreach (var (handler, state) in _heartbeatHandlers)
                handler(state);
        }
    }

    /// <summary>
    /// Registers a heartbeat handler to be invoked periodically.
    /// </summary>
    /// <param name="action">The action to invoke.</param>
    /// <param name="state">The state object to pass to the action.</param>
    public void OnHeartbeat(Action<object> action, object state)
    {
        lock (_heartbeatLock)
        {
            _heartbeatHandlers ??= [];
            _heartbeatHandlers.Add((action, state));
        }
    }

    /// <summary>
    /// Fires all registered completion callbacks asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the connection is already complete.</exception>
    public Task FireOnCompletedAsync()
    {
        if (_completed)
            throw new InvalidOperationException("The connection is already complete.");

        _completed = true;

        var onCompleted = _onCompleted;

        if (onCompleted is null || onCompleted.Count is 0)
            return Task.CompletedTask;

        return CompleteAsyncMayAwait(onCompleted);
    }

    /// <summary>
    /// Executes completion callbacks, potentially awaiting tasks.
    /// </summary>
    /// <param name="onCompleted">The stack of completion callbacks.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private Task CompleteAsyncMayAwait(Stack<KeyValuePair<Func<object, Task>, object>> onCompleted)
    {
        while (onCompleted.TryPop(out var entry))
        {
            try
            {
                var task = entry.Key.Invoke(entry.Value);

                if (!task.IsCompletedSuccessfully)
                    return CompleteAsyncAwaited(task, onCompleted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured running an IConnectionCompleteFeature.OnCompleted callback.");
            }
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Awaits a task and continues executing completion callbacks.
    /// </summary>
    /// <param name="currentTask">The current task to await.</param>
    /// <param name="onCompleted">The stack of completion callbacks.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task CompleteAsyncAwaited(Task currentTask, Stack<KeyValuePair<Func<object, Task>, object>> onCompleted)
    {
        try
        {
            await currentTask.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured running an IConnectionCompleteFeature.OnCompleted callback.");
        }

        while (onCompleted.TryPop(out var entry))
        {
            try
            {
                await entry.Key.Invoke(entry.Value).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured running an IConnectionCompleteFeature.OnCompleted callback.");
            }
        }
    }

    /// <summary>
    /// Requests the connection to close by signaling the cancellation token.
    /// </summary>
    public void RequestClose()
    {
        _connectionClosingCts.Cancel();
    }

    /// <summary>
    /// Returns a string representation of the connection.
    /// </summary>
    /// <returns>A string in the format "ConnectionId:{ConnectionId}".</returns>
    public override string ToString()
    {
        return _cachedToString ??= string.Format(CultureInfo.InvariantCulture, "ConnectionId:{0}", TransportConnection.ConnectionId);
    }
}
