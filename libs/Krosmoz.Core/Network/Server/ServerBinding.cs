// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Connections;

namespace Krosmoz.Core.Network.Server;

/// <summary>
/// Represents a server binding for a specific port.
/// Provides functionality to bind a connection listener to the loopback address.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public sealed class ServerBinding
{
    private readonly int _port;
    private readonly ConnectionDelegate _application;
    private readonly IConnectionListenerFactory _connectionListenerFactory;

    /// <summary>
    /// Gets the application delegate that processes connections.
    /// </summary>
    public ConnectionDelegate Application =>
        _application;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServerBinding"/> class.
    /// </summary>
    /// <param name="port">The port number to bind the listener to.</param>
    /// <param name="application">The delegate representing the application middleware.</param>
    /// <param name="connectionListenerFactory">The factory used to create connection listeners.</param>
    public ServerBinding(int port, ConnectionDelegate application, IConnectionListenerFactory connectionListenerFactory)
    {
        _port = port;
        _application = application;
        _connectionListenerFactory = connectionListenerFactory;
    }

    /// <summary>
    /// Asynchronously binds a connection listener to the loopback address and specified port.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the binding operation.</param>
    /// <returns>
    /// An asynchronous enumerable of <see cref="IConnectionListener"/> instances representing the bound listeners.
    /// </returns>
    public async IAsyncEnumerable<IConnectionListener> BindAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        yield return await _connectionListenerFactory.BindAsync(new IPEndPoint(IPAddress.Loopback, _port), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Returns a string representation of the server binding.
    /// </summary>
    /// <returns>A string in the format "0.0.0.0:{port}".</returns>
    public override string ToString()
    {
        return $"0.0.0.0:{_port}";
    }
}
