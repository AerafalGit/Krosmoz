// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Concurrent;
using System.Net;
using Krosmoz.Core.Extensions;
using Krosmoz.Core.Network.Internal;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Core.Network.Server;

/// <summary>
/// Represents a composite server that manages multiple listeners and connections.
/// Provides functionality for starting, stopping, and managing server connections.
/// </summary>
public sealed class CompositeServer
{
    private readonly CompositeServerBuilder _builder;
    private readonly ILogger<CompositeServer> _logger;
    private readonly List<RunningListener> _listeners;
    private readonly TaskCompletionSource<object?> _shutdownTcs;
    private readonly PeriodicTimer _timer;

    private Task _timerTask;

    /// <summary>
    /// Gets the collection of endpoints associated with the server listeners.
    /// </summary>
    public IEnumerable<EndPoint> EndPoints =>
        _listeners.Select(static listener => listener.Listener.EndPoint);

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeServer"/> class.
    /// </summary>
    /// <param name="builder">The builder used to configure the server.</param>
    internal CompositeServer(CompositeServerBuilder builder)
    {
        _builder = builder;
        _logger = builder.ApplicationServices.GetLoggerFactory().CreateLogger<CompositeServer>();
        _listeners = [];
        _shutdownTcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        _timer = new PeriodicTimer(builder.HeartBeatInterval);
        _timerTask = Task.CompletedTask;
    }

    /// <summary>
    /// Starts the server asynchronously, binding listeners and initializing connections.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            foreach (var binding in _builder.Bindings)
            {
                await foreach (var listener in binding.BindAsync(cancellationToken).ConfigureAwait(false))
                {
                    var runningListener = new RunningListener(this, binding, listener);

                    _listeners.Add(runningListener);
                    _logger.LogInformation("Now listening on: {EndPoint}", listener.EndPoint);

                    runningListener.Start();
                }
            }
        }
        catch
        {
            await StopAsync(CancellationToken.None).ConfigureAwait(false);
            throw;
        }

        _timerTask = StartTimerAsync();
    }

    /// <summary>
    /// Stops the server asynchronously, unbinding listeners and shutting down connections.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var tasks = new Task[_listeners.Count];

        for (var i = 0; i < _listeners.Count; i++)
            tasks[i] = _listeners[i].Listener.UnbindAsync(cancellationToken).AsTask();

        await Task.WhenAll(tasks).ConfigureAwait(false);

        _shutdownTcs.TrySetResult(null);

        for (var i = 0; i < _listeners.Count; i++)
            tasks[i] = _listeners[i].ExecutionTask;

        var shutdownTask = Task.WhenAll(tasks);

        if (cancellationToken.CanBeCanceled)
            await shutdownTask.WithCancellation(cancellationToken).ConfigureAwait(false);
        else
            await shutdownTask.ConfigureAwait(false);

        if (_timer is not null)
        {
            _timer.Dispose();
            await _timerTask.ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Starts the periodic timer for heartbeat checks asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task StartTimerAsync()
    {
        using (_timer)
        {
            while (await _timer.WaitForNextTickAsync().ConfigureAwait(false))
            {
                foreach (var listener in _listeners)
                    listener.TickHeartbeat();
            }
        }
    }

    /// <summary>
    /// Represents a running listener that manages connections for a specific binding.
    /// </summary>
    private sealed class RunningListener
    {
        private readonly ConcurrentDictionary<long, (ServerConnection Connection, Task ExecutionTask)> _connections;
        private readonly CompositeServer _server;
        private readonly ServerBinding _binding;

        /// <summary>
        /// Gets the connection listener associated with this running listener.
        /// </summary>
        public IConnectionListener Listener { get; }

        /// <summary>
        /// Gets the task representing the execution of the listener.
        /// </summary>
        public Task ExecutionTask { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RunningListener"/> class.
        /// </summary>
        /// <param name="server">The composite server managing this listener.</param>
        /// <param name="binding">The server binding associated with this listener.</param>
        /// <param name="listener">The connection listener.</param>
        public RunningListener(CompositeServer server, ServerBinding binding, IConnectionListener listener)
        {
            _server = server;
            _binding = binding;
            _connections = [];

            Listener = listener;
            ExecutionTask = Task.CompletedTask;
        }

        /// <summary>
        /// Starts the listener asynchronously.
        /// </summary>
        public void Start()
        {
            ExecutionTask = RunListenerAsync();
        }

        /// <summary>
        /// Executes heartbeat checks for all connections managed by this listener.
        /// </summary>
        public void TickHeartbeat()
        {
            foreach (var (_, (connection, _)) in _connections)
                connection.TickHeartbeat();
        }

        /// <summary>
        /// Runs the listener asynchronously, accepting and managing connections.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task RunListenerAsync()
        {
            var connectionDelegate = _binding.Application;

            long id = 0;

            while (true)
            {
                try
                {
                    var connection = await Listener.AcceptAsync().ConfigureAwait(false);

                    if (connection is null)
                        break;

                    var serverConnection = new ServerConnection(id, connection, _server._logger);

                    _connections[id] = (serverConnection, ExecuteConnectionAsync(serverConnection));
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _server._logger.LogCritical(ex, "Stopped accepting connections on {endpoint}", Listener.EndPoint);
                    break;
                }

                id++;
            }

            await _server._shutdownTcs.Task.ConfigureAwait(false);

            var tasks = new List<Task>(_connections.Count);

            foreach (var (_, (connection, executionTask)) in _connections)
            {
                connection.RequestClose();
                tasks.Add(executionTask);
            }

            if (!await Task.WhenAll(tasks).TimeoutAfter(_server._builder.ShutdownTimeout).ConfigureAwait(false))
            {
                foreach (var (_, (connection, _)) in _connections)
                    connection.TransportConnection.Abort();

                await Task.WhenAll(tasks).ConfigureAwait(false);
            }

            await Listener.DisposeAsync().ConfigureAwait(false);
            return;

            async Task ExecuteConnectionAsync(ServerConnection serverConnection)
            {
                await Task.Yield();

                var connection = serverConnection.TransportConnection;

                try
                {
                    using var scope = BeginConnectionScope(serverConnection);

                    await connectionDelegate(connection).ConfigureAwait(false);
                }
                catch (ConnectionResetException)
                {
                    // Don't let connection aborted exceptions out
                }
                catch (ConnectionAbortedException)
                {
                    // Don't let connection aborted exceptions out
                }
                catch (Exception ex)
                {
                    _server._logger.LogError(ex, "Unexpected exception from connection {ConnectionId}.", connection.ConnectionId);
                }
                finally
                {
                    await serverConnection.FireOnCompletedAsync().ConfigureAwait(false);

                    await connection.DisposeAsync().ConfigureAwait(false);

                    _connections.TryRemove(serverConnection.Id, out _);
                }
            }
        }

        /// <summary>
        /// Begins a logging scope for the specified connection.
        /// </summary>
        /// <param name="connection">The server connection.</param>
        /// <returns>An <see cref="IDisposable"/> representing the logging scope.</returns>
        private IDisposable BeginConnectionScope(ServerConnection connection)
        {
            if (_server._logger.IsEnabled(LogLevel.Critical))
                return _server._logger.BeginScope(connection) ?? EmptyDisposable.Instance;

            return EmptyDisposable.Instance;
        }
    }
}
