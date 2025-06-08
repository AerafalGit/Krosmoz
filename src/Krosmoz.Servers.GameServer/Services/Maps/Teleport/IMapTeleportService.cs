// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Servers.GameServer.Models.Actors;
using Krosmoz.Servers.GameServer.Models.World;
using Krosmoz.Servers.GameServer.Models.World.Maps;

namespace Krosmoz.Servers.GameServer.Services.Maps.Teleport;

/// <summary>
/// Interface for a service that handles map teleportation operations.
/// </summary>
public interface IMapTeleportService
{
    /// <summary>
    /// Changes the map of the specified actor asynchronously.
    /// </summary>
    /// <param name="actor">The actor to change the map for.</param>
    /// <param name="mapId">The unique identifier of the target map.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ChangeMapAsync(Actor actor, int mapId);

    /// <summary>
    /// Teleports the specified actor to a map asynchronously.
    /// </summary>
    /// <param name="actor">The actor to teleport.</param>
    /// <param name="mapId">The unique identifier of the target map.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task TeleportAsync(Actor actor, int mapId);

    /// <summary>
    /// Teleports the specified actor to a specific cell on a map asynchronously.
    /// </summary>
    /// <param name="actor">The actor to teleport.</param>
    /// <param name="map">The target map.</param>
    /// <param name="cellId">The cell identifier on the map.</param>
    /// <param name="force">Whether to force the teleportation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task TeleportAsync(Actor actor, Map map, short cellId, bool force = false);

    /// <summary>
    /// Teleports the specified actor to a specific cell on a map with a specified direction asynchronously.
    /// </summary>
    /// <param name="actor">The actor to teleport.</param>
    /// <param name="map">The target map.</param>
    /// <param name="cellId">The cell identifier on the map.</param>
    /// <param name="direction">The direction the actor will face after teleportation.</param>
    /// <param name="force">Whether to force the teleportation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task TeleportAsync(Actor actor, Map map, short cellId, Directions direction, bool force = false);

    /// <summary>
    /// Teleports the specified actor to a specific position on a map asynchronously.
    /// </summary>
    /// <param name="actor">The actor to teleport.</param>
    /// <param name="map">The target map.</param>
    /// <param name="position">The world position to teleport the actor to.</param>
    /// <param name="fromFight">Whether the teleportation is from a fight.</param>
    /// <param name="force">Whether to force the teleportation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task TeleportAsync(Actor actor, Map map, WorldPosition position, bool fromFight = false, bool force = false);
}
