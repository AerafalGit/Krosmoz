// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Concurrent;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Ipc;
using Krosmoz.Protocol.Ipc.Messages.Heartbeat;
using Krosmoz.Servers.AuthServer.Services.Servers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;

namespace Krosmoz.Servers.AuthServer.Services.Ipc.Heartbeat;

/// <summary>
/// Represents the IPC heartbeat service for managing server keep-alive signals and offline server detection.
/// </summary>
public sealed class IpcHeartbeatService : BackgroundService
{
    /// <summary>
    /// The timeout duration in seconds for determining if a server is offline.
    /// </summary>
    private const int KeepAliveTimeoutSeconds = 15;

    /// <summary>
    /// The interval in seconds for checking offline servers.
    /// </summary>
    private const int KeepAliveIntervalSeconds = 5;

    private readonly ConcurrentDictionary<ushort, DateTime> _serverKeepAliveTimestamps;
    private readonly INatsConnection _natsConnection;
    private readonly IServerService _serverService;
    private readonly ILogger<IpcHeartbeatService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="IpcHeartbeatService"/> class.
    /// </summary>
    /// <param name="natsConnection">The NATS connection instance.</param>
    /// <param name="serverService">The server service instance.</param>
    /// <param name="logger">The logger instance.</param>
    public IpcHeartbeatService(INatsConnection natsConnection, IServerService serverService, ILogger<IpcHeartbeatService> logger)
    {
        _natsConnection = natsConnection;
        _serverService = serverService;
        _logger = logger;
        _serverKeepAliveTimestamps = new ConcurrentDictionary<ushort, DateTime>();
    }

    /// <summary>
    /// Executes the background service tasks for processing server keep-alive signals.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _ = RemoveOfflineServersAsync(cancellationToken);

        ushort serverId = 0;

        await foreach (var request in _natsConnection.SubscribeAsync<IpcHeartbeatRequest>(IpcSubjects.Heartbeat, cancellationToken: cancellationToken))
        {
            try
            {
                if (request.Data is null)
                    continue;

                serverId = request.Data.ServerId;

                _serverKeepAliveTimestamps.AddOrUpdate(serverId, DateTime.UtcNow, static (_, _) => DateTime.UtcNow);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing KeepAlive request for server id {ServerId}", serverId);
            }
        }
    }

    /// <summary>
    /// Periodically removes servers that are offline based on their last keep-alive timestamps.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task RemoveOfflineServersAsync(CancellationToken cancellationToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(KeepAliveIntervalSeconds));

        while (await timer.WaitForNextTickAsync(cancellationToken))
        {
            try
            {
                var now = DateTime.UtcNow;

                var offlineServers = _serverKeepAliveTimestamps
                    .Where(x => (now - x.Value).TotalSeconds > KeepAliveTimeoutSeconds)
                    .Select(static x => x.Key)
                    .ToArray();

                foreach (var serverId in offlineServers)
                {
                    _serverKeepAliveTimestamps.TryRemove(serverId, out _);

                    await _serverService.UpdateServerStatusAsync(serverId, ServerStatuses.Offline, cancellationToken);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while removing offline servers");
            }
        }
    }
}
