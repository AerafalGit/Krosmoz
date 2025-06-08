// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Dispatcher.Attributes;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Authorized;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Commands;
using Krosmoz.Servers.GameServer.Services.Maps.Teleport;

namespace Krosmoz.Servers.GameServer.Network.Handlers.Admin;

/// <summary>
/// Handles administrative commands and operations.
/// </summary>
public sealed class AdminHandler
{
    private readonly ICommandService _commandService;
    private readonly IMapTeleportService _mapTeleportService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdminHandler"/> class.
    /// </summary>
    /// <param name="commandService">Service for executing commands.</param>
    /// <param name="mapTeleportService">Service for handling map teleportation operations.</param>
    public AdminHandler(ICommandService commandService, IMapTeleportService mapTeleportService)
    {
        _commandService = commandService;
        _mapTeleportService = mapTeleportService;
    }

    /// <summary>
    /// Handles an administrative command sent by a client asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client.</param>
    /// <param name="message">The message containing the administrative command.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleAdminCommandAsync(DofusConnection connection, AdminCommandMessage message)
    {
        return connection.Heroes.Account.Hierarchy >= GameHierarchies.Moderator
            ? _commandService.ExecuteCommandAsync(connection, message.Content)
            : Task.CompletedTask;
    }

    /// <summary>
    /// Handles a quiet administrative command sent by a client asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client.</param>
    /// <param name="message">The message containing the quiet administrative command.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleAdminCommandQuietAsync(DofusConnection connection, AdminQuietCommandMessage message)
    {
        if (connection.Heroes.Account.Hierarchy < GameHierarchies.Moderator)
            return Task.CompletedTask;

        var data = message.Content.Split(' ');

        if (data.Length is 2 && data[0] is "moveto" && int.TryParse(data[1], out var mapId))
        {
            return connection.Heroes.Master.Map.Id == mapId
                ? Task.CompletedTask
                : _mapTeleportService.TeleportAsync(connection.Heroes.Master, mapId);
        }

        return Task.CompletedTask;
    }
}
