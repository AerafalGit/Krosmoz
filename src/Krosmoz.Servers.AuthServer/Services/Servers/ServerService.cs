// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Diagnostics;
using System.Net;
using System.Text;
using Krosmoz.Core.Extensions;
using Krosmoz.Core.Services;
using Krosmoz.Protocol.Constants;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Connection;
using Krosmoz.Protocol.Types.Connection;
using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.AuthServer.Database.Models.Accounts;
using Krosmoz.Servers.AuthServer.Database.Models.Servers;
using Krosmoz.Servers.AuthServer.Network.Transport;
using Microsoft.Extensions.DependencyInjection;

namespace Krosmoz.Servers.AuthServer.Services.Servers;

/// <summary>
/// Service for managing server-related operations.
/// </summary>
public sealed class ServerService : IServerService, IAsyncInitializableService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ConcurrentDictionary<string, DofusConnection> _connectionsWaiting;

    private FrozenDictionary<ushort, ServerRecord> _servers;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServerService"/> class.
    /// </summary>
    /// <param name="serviceScopeFactory">The service scope factory used to create database contexts.</param>
    public ServerService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _connectionsWaiting = [];
        _servers = new Dictionary<ushort, ServerRecord>().ToFrozenDictionary();
    }

    /// <summary>
    /// Registers a server asynchronously with the specified details.
    /// </summary>
    /// <param name="id">The unique identifier of the server.</param>
    /// <param name="name">The name of the server.</param>
    /// <param name="ipAddress">The IP address of the server.</param>
    /// <param name="port">The port number of the server.</param>
    /// <param name="cancellationToken">A token to signal the operation should be canceled.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a boolean value
    /// indicating whether the server was successfully registered.
    /// </returns>
    public async Task<bool> RegisterServerAsync(ushort id, string name, IPAddress ipAddress, ushort port, CancellationToken cancellationToken)
    {
        if (!_servers.TryGetValue(id, out var server))
            return false;

        if (!string.IsNullOrEmpty(name))
            server.Name = name;

        server.IpAddress = ipAddress;
        server.Port = port;
        server.OpenedAt ??= DateTime.UtcNow;

        if (server.Status is ServerStatuses.Offline)
            server.Status = ServerStatuses.Starting;

        await UpdateServerAsync(server, cancellationToken);

        await Parallel.ForEachAsync(_connectionsWaiting.Values, cancellationToken, async (connection, token) =>
        {
            if (!connection.IsConnected)
            {
                _connectionsWaiting.Remove(connection.ConnectionId, out _);
                return;
            }

            await connection.SendAsync(new ServerStatusUpdateMessage { Server = GetGameServerInformations(server, connection.Account) });
        });

        return true;
    }

    /// <summary>
    /// Updates the status of a server asynchronously.
    /// </summary>
    /// <param name="serverId">The unique identifier of the server to update.</param>
    /// <param name="status">The new status to set for the server.</param>
    /// <param name="cancellationToken">A token to signal the operation should be canceled.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a boolean value
    /// indicating whether the server status was successfully updated.
    /// </returns>
    public async Task<bool> UpdateServerStatusAsync(ushort serverId, ServerStatuses status, CancellationToken cancellationToken)
    {
        if (!_servers.TryGetValue(serverId, out var server))
            return false;

        server.Status = status;

        await UpdateServerAsync(server, cancellationToken);

        await Parallel.ForEachAsync(_connectionsWaiting.Values, cancellationToken, async (connection, token) =>
        {
            if (!connection.IsConnected)
            {
                _connectionsWaiting.Remove(connection.ConnectionId, out _);
                return;
            }

            await connection.SendAsync(new ServerStatusUpdateMessage { Server = GetGameServerInformations(server, connection.Account) });
        });

        return true;
    }

    /// <summary>
    /// Handles the event when a user is successfully authenticated.
    /// </summary>
    /// <param name="connection">The authentication connection of the user.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task OnSuccessfullyAuthenticatedAsync(DofusConnection connection)
    {
        foreach (var dofusConnection in _connectionsWaiting.Values.Where(x => x.Account.Id == connection.Account.Id))
            dofusConnection.Abort("Already connected to the server.");

        _connectionsWaiting.TryAdd(connection.ConnectionId, connection);

        connection.ConnectionClosed.Register(() => _connectionsWaiting.Remove(connection.ConnectionId, out _));

        var visibleServers = _servers.Values.Where(x => x.IpAddress is not null && x.VisibleHierarchy <= connection.Account.Hierarchy);

        await connection.SendAsync(new ServersListMessage
        {
            Servers = visibleServers.Select(x => GetGameServerInformations(x, connection.Account)),
            AlreadyConnectedToServerId = connection.ServerIdAutoSelect > 0 ? connection.ServerIdAutoSelect : (ushort)0,
            CanCreateNewCharacter = true
        });

        if (connection.ServerIdAutoSelect > 0)
            await SelectGameServerAsync(connection, connection.ServerIdAutoSelect);
    }

    /// <summary>
    /// Selects a game server for the user asynchronously.
    /// </summary>
    /// <param name="connection">The authentication connection of the user.</param>
    /// <param name="serverId">The unique identifier of the server to select.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SelectGameServerAsync(DofusConnection connection, ushort serverId)
    {
        if (!_servers.TryGetValue(serverId, out var server))
            return;

        if (server.Status is not ServerStatuses.Online)
        {
            await SendSelectServerRefusedAsync(connection, server, ServerConnectionErrors.ServerConnectionErrorDueToStatus);
            return;
        }

        if (server.JoinableHierarchy > connection.Account.Hierarchy)
        {
            await SendSelectServerRefusedAsync(connection, server, ServerConnectionErrors.ServerConnectionErrorAccountRestricted);
            return;
        }

        Debug.Assert(server.IpAddress is not null);
        Debug.Assert(server.Port is not null);
        Debug.Assert(!string.IsNullOrEmpty(connection.Account.Ticket));

        var charactersCount = connection.Account.Characters.Count(x => x.ServerId == server.Id && !x.DeletedAt.HasValue);

        await connection.SendAsync(new SelectedServerDataMessage
        {
            ServerId = serverId,
            Address = server.IpAddress.ToString(),
            Port = server.Port.Value,
            Ticket = Encoding.ASCII.GetBytes(connection.Account.Ticket).Select(static x => (sbyte)x).ToArray(),
            CanCreateNewCharacter = charactersCount < ProtocolConstants.MaxPlayersPerTeam
        });

        connection.Abort("Switching to game server.");
    }

    /// <summary>
    /// Initializes the service asynchronously by loading server data from the database.
    /// </summary>
    /// <param name="cancellationToken">A token to signal the operation should be canceled.</param>
    /// <returns>A task that represents the asynchronous initialization operation.</returns>
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        _servers = await dbContext.Servers.ToFrozenDictionaryAsync(static x => x.Id, cancellationToken);
    }

    /// <summary>
    /// Updates the specified server record in the database asynchronously.
    /// </summary>
    /// <param name="server">The server record to update.</param>
    /// <param name="cancellationToken">A token to signal the operation should be canceled.</param>
    /// <returns>A task that represents the asynchronous update operation.</returns>
    private async Task UpdateServerAsync(ServerRecord server, CancellationToken cancellationToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        dbContext.Servers.Update(server);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Sends a message to the user indicating that the server selection was refused.
    /// </summary>
    /// <param name="connection">The authentication connection of the user.</param>
    /// <param name="server">The server record associated with the refusal.</param>
    /// <param name="error">The error code indicating the reason for refusal.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private static ValueTask SendSelectServerRefusedAsync(DofusConnection connection, ServerRecord server, ServerConnectionErrors error)
    {
        return connection.SendAsync(new SelectedServerRefusedMessage
        {
            ServerId = server.Id,
            ServerStatus = (sbyte)server.Status,
            Error = (sbyte)error
        });
    }

    /// <summary>
    /// Retrieves game server information for the specified server and account.
    /// </summary>
    /// <param name="server">The server record to retrieve information for.</param>
    /// <param name="account">The account record associated with the user.</param>
    /// <returns>A <see cref="GameServerInformations"/> object containing the server details.</returns>
    private static GameServerInformations GetGameServerInformations(ServerRecord server, AccountRecord account)
    {
        var charactersCount = account.Characters.Count(x => x.ServerId == server.Id && !x.DeletedAt.HasValue);

        Debug.Assert(server.OpenedAt is not null);

        return new GameServerInformations
        {
            Id = server.Id,
            CharactersCount = (sbyte)charactersCount,
            Completion = 0,
            Date = server.OpenedAt.Value.GetUnixTimestampMilliseconds(),
            IsSelectable = server.Status is ServerStatuses.Online,
            Status = (sbyte)server.Status
        };
    }
}
