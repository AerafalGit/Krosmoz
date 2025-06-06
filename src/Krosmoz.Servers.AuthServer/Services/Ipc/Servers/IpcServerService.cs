// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Ipc;
using Krosmoz.Protocol.Ipc.Messages.Servers;
using Krosmoz.Servers.AuthServer.Services.Servers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;

namespace Krosmoz.Servers.AuthServer.Services.Ipc.Servers;

/// <summary>
/// Represents the IPC server service for handling server-related operations.
/// </summary>
public sealed class IpcServerService : BackgroundService
{
    private readonly INatsConnection _natsConnection;
    private readonly IServerService _serverService;
    private readonly ILogger<IpcServerService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="IpcServerService"/> class.
    /// </summary>
    /// <param name="natsConnection">The NATS connection used for IPC communication.</param>
    /// <param name="serverService">The server service used to manage server operations.</param>
    /// <param name="logger">The logger used for logging messages.</param>
    public IpcServerService(INatsConnection natsConnection, IServerService serverService, ILogger<IpcServerService> logger)
    {
        _natsConnection = natsConnection;
        _serverService = serverService;
        _logger = logger;
    }

    /// <summary>
    /// Executes the background service tasks for handling server-related IPC operations.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        return Task.WhenAll(
        [
            ServerRegister(cancellationToken),
            ServerStatusUpdate(cancellationToken)
        ]);
    }

    /// <summary>
    /// Handles requests to register a server.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    private async Task ServerRegister(CancellationToken cancellationToken)
    {
        await foreach (var request in _natsConnection.SubscribeAsync<IpcServerRegisterRequest>(IpcSubjects.ServerRegister, cancellationToken: cancellationToken))
        {
            try
            {
                if (request.Data is null)
                {
                    await request.ReplyAsync(new IpcServerRegisterResponse { Success = false }, cancellationToken: cancellationToken);
                    continue;
                }

                var success = await _serverService.RegisterServerAsync(
                    request.Data.ServerId,
                    request.Data.ServerName,
                    request.Data.ServerAddress,
                    request.Data.ServerPort,
                    cancellationToken);

                var response = new IpcServerRegisterResponse { Success = success };

                await request.ReplyAsync(response, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while processing server registration request");
            }
        }
    }

    /// <summary>
    /// Handles requests to update the status of a server.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    private async Task ServerStatusUpdate(CancellationToken cancellationToken)
    {
        await foreach (var request in _natsConnection.SubscribeAsync<IpcServerStatusUpdateEvent>(IpcSubjects.ServerStatusUpdate, cancellationToken: cancellationToken))
        {
            try
            {
                if (request.Data is null)
                    continue;

                await _serverService.UpdateServerStatusAsync(request.Data.ServerId, request.Data.ServerStatus, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while processing server status update request");
            }
        }
    }
}
