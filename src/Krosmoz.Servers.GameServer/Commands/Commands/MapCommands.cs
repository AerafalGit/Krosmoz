// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Commands.Attributes;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Maps.Teleport;

namespace Krosmoz.Servers.GameServer.Commands.Commands;

/// <summary>
/// Represents a set of commands related to map operations, such as teleporting players to specific maps.
/// </summary>
[CommandGroup("maps")]
public sealed class MapCommands
{
    private readonly IMapTeleportService _mapTeleportService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapCommands"/> class.
    /// </summary>
    /// <param name="mapTeleportService">The service responsible for handling map teleportation operations.</param>
    public MapCommands(IMapTeleportService mapTeleportService)
    {
        _mapTeleportService = mapTeleportService;
    }

    /// <summary>
    /// Teleports the player to a specified map by its ID asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the player.</param>
    /// <param name="mapId">The ID of the map to teleport the player to.</param>
    /// <returns>A task that represents the asynchronous teleportation operation.</returns>
    [Command("go")]
    [CommandDescription("Teleports the player to a specified map by its ID.")]
    public Task GoToMapAsync(DofusConnection connection, int mapId)
    {
        return _mapTeleportService.TeleportAsync(connection.Heroes.Master, mapId);
    }
}
