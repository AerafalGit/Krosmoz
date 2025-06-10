// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Server;
using Microsoft.Extensions.Hosting;

namespace Krosmoz.Core.Network.Hosting;

/// <summary>
/// Represents a hosted service for managing the lifecycle of a composite server.
/// Implements the <see cref="IHostedService"/> interface to integrate with the hosting environment.
/// </summary>
public sealed class ServerHostedService : IHostedService
{
    private readonly Server.Server _server;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServerHostedService"/> class.
    /// </summary>
    /// <param name="server">The composite server instance to manage.</param>
    public ServerHostedService(Server.Server server)
    {
        _server = server;
    }

    /// <summary>
    /// Starts the server asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return _server.StartAsync(cancellationToken);
    }

    /// <summary>
    /// Stops the server asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return _server.StopAsync(cancellationToken);
    }
}
