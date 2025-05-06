// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Krosmoz.Core.Network.Transport;

/// <summary>
/// Represents an abstract TCP server for managing network communication sessions.
/// </summary>
/// <typeparam name="TMessage">The type of the network message to handle.</typeparam>
/// <typeparam name="TSession">The type of the TCP session associated with the server.</typeparam>
public abstract class TcpServer<TMessage, TSession> : IHostedService, IDisposable
    where TSession : TcpSession<TMessage>
    where TMessage : class
{
    private readonly Socket _socket;
    private readonly IServiceProvider _services;
    private readonly CancellationTokenSource _cts;
    private readonly ILogger<TcpServer<TMessage, TSession>> _logger;
    private readonly ConcurrentDictionary<string, TSession> _sessions;
    private readonly TcpServerOptions _options;

    /// <summary>
    /// Gets the local endpoint of the server.
    /// </summary>
    public IPEndPoint LocalEndPoint =>
        (IPEndPoint)_socket.LocalEndPoint!;

    /// <summary>
    /// Gets the local IP address of the server.
    /// </summary>
    public IPAddress LocalAddress =>
        LocalEndPoint.Address;

    /// <summary>
    /// Gets the local port number of the server.
    /// </summary>
    public int LocalPort =>
        LocalEndPoint.Port;

    /// <summary>
    /// Initializes a new instance of the <see cref="TcpServer{TMessage, TSession}"/> class.
    /// </summary>
    /// <param name="services">The service provider for dependency injection.</param>
    /// <param name="logger">The logger for logging server events.</param>
    /// <param name="options">The configuration options for the server.</param>
    protected TcpServer(IServiceProvider services, ILogger<TcpServer<TMessage, TSession>> logger, IOptions<TcpServerOptions> options)
    {
        _services = services;
        _options = options.Value;
        _logger = logger;
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _cts = new CancellationTokenSource();
        _sessions = new ConcurrentDictionary<string, TSession>();
    }

    /// <summary>
    /// Starts the server and begins listening for incoming connections asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        cancellationToken.Register(() => _cts.Cancel());

        _ = Task.Run(async () =>
        {
            _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            _socket.Bind(new IPEndPoint(IPAddress.Parse(_options.Host), _options.Port));

            _socket.Listen(_options.MaxConnections);

            _logger.LogInformation("Now listening on: {EndPoint}", _socket.LocalEndPoint);

            while (!_cts.IsCancellationRequested)
            {
                var socket = await _socket.AcceptAsync(_cts.Token).ConfigureAwait(false);

                socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);

                var session = ActivatorUtilities.CreateInstance<TSession>(_services, socket);

                session.ConnectionClosed.Register(() => { _sessions.TryRemove(session.SessionId, out _); });

                _sessions.TryAdd(session.SessionId, session);

                _ = session.ListenAsync().ConfigureAwait(false);
            }
        }, _cts.Token);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Stops the server and disconnects all active sessions asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (!_cts.IsCancellationRequested)
            await _cts.CancelAsync().ConfigureAwait(false);

        await Task.WhenAll(_sessions.Values.Select(static s => s.DisconnectAsync())).ConfigureAwait(false);

        try
        {
            _socket.Shutdown(SocketShutdown.Both);
        }
        catch (SocketException)
        {
            // ignore
        }
        finally
        {
            _socket.Close();
        }
    }

    /// <summary>
    /// Releases the resources used by the server.
    /// </summary>
    public void Dispose()
    {
        _cts.Dispose();
        _socket.Dispose();
        GC.SuppressFinalize(this);
    }
}
