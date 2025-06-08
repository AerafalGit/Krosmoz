// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Servers.GameServer.Models.Actors;
using Krosmoz.Servers.GameServer.Models.World;
using Krosmoz.Servers.GameServer.Models.World.Maps;

namespace Krosmoz.Servers.GameServer.Services.Maps.Teleport;

/// <summary>
/// Service responsible for handling map teleportation operations.
/// </summary>
public sealed class MapTeleportService : IMapTeleportService
{
    private readonly IMapService _mapService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapTeleportService"/> class.
    /// </summary>
    /// <param name="mapService">The service used for map-related operations.</param>
    public MapTeleportService(IMapService mapService)
    {
        _mapService = mapService;
    }

    /// <summary>
    /// Changes the map of the specified actor asynchronously.
    /// </summary>
    /// <param name="actor">The actor to change the map for.</param>
    /// <param name="mapId">The unique identifier of the target map.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task ChangeMapAsync(Actor actor, int mapId)
    {
        if (!actor.CanMove())
            return Task.CompletedTask;

        var mapDirection = GetMapNeighbour(actor.Map, mapId);

        if (mapDirection is MapNeighbours.None)
            return Task.CompletedTask;

        var nextMap = GetMapNeighbourOverride(actor.Map, mapDirection, mapId);

        if (nextMap is null)
            return Task.CompletedTask;

        var nextCellId = GetCellAfterChangeMap(nextMap, actor.Position.Cell.Id, mapDirection);

        return TeleportAsync(actor, nextMap, nextCellId);
    }

    /// <summary>
    /// Teleports the specified actor to a map asynchronously.
    /// </summary>
    /// <param name="actor">The actor to teleport.</param>
    /// <param name="mapId">The unique identifier of the target map.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task TeleportAsync(Actor actor, int mapId)
    {
        return !_mapService.TryGetMap(mapId, out var map)
            ? Task.CompletedTask
            : TeleportAsync(actor, map, map.GetRandomFreeCell().Id);
    }

    /// <summary>
    /// Teleports the specified actor to a specific cell on a map asynchronously.
    /// </summary>
    /// <param name="actor">The actor to teleport.</param>
    /// <param name="map">The target map.</param>
    /// <param name="cellId">The cell identifier on the map.</param>
    /// <param name="force">Whether to force the teleportation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task TeleportAsync(Actor actor, Map map, short cellId, bool force = false)
    {
        return TeleportAsync(actor, map, cellId, actor.Orientation, force);
    }

    /// <summary>
    /// Teleports the specified actor to a specific cell on a map with a specified direction asynchronously.
    /// </summary>
    /// <param name="actor">The actor to teleport.</param>
    /// <param name="map">The target map.</param>
    /// <param name="cellId">The cell identifier on the map.</param>
    /// <param name="direction">The direction the actor will face after teleportation.</param>
    /// <param name="force">Whether to force the teleportation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task TeleportAsync(Actor actor, Map map, short cellId, Directions direction, bool force = false)
    {
        return TeleportAsync(actor, map, force ? new WorldPosition(map, map.GetCell(cellId), direction) : new WorldPosition(map, map.GetCellWalkableOrDefault(cellId), actor.Orientation));
    }

    /// <summary>
    /// Teleports the specified actor to a specific position on a map asynchronously.
    /// </summary>
    /// <param name="actor">The actor to teleport.</param>
    /// <param name="map">The target map.</param>
    /// <param name="position">The world position to teleport the actor to.</param>
    /// <param name="fromFight">Whether the teleportation is from a fight.</param>
    /// <param name="force">Whether to force the teleportation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task TeleportAsync(Actor actor, Map map, WorldPosition position, bool fromFight = false, bool force = false)
    {
        if (!actor.CanMove() && !force)
            return;

        if ((sbyte)position.Orientation < 0 || (sbyte)position.Orientation > 7)
            position.Orientation = actor.Orientation;

        if (!fromFight && actor.Map.Id == position.Map.Id)
        {
            actor.Map.AddActor(actor);
            actor.Position = position;
            await _mapService.RefreshActorAsync(actor);
            return;
        }

        await _mapService.RemoveActorAsync(actor);

        actor.Position = position;

        await _mapService.AddActorAsync(actor);
    }

    /// <summary>
    /// Retrieves the neighboring map based on the current map and the specified direction.
    /// </summary>
    /// <param name="map">The current map.</param>
    /// <param name="mapNeighbour">The direction of the neighboring map.</param>
    /// <param name="fallbackMapId">The fallback map identifier if no neighbor exists.</param>
    /// <returns>The neighboring map or null if not found.</returns>
    private Map? GetMapNeighbourOverride(Map map, MapNeighbours mapNeighbour, int fallbackMapId)
    {
        if (!_mapService.TryGetMapScrollAction(map.Id, out var scrollAction))
            return _mapService.TryGetMap(fallbackMapId, out var fallbackMap) ? fallbackMap : null;

        return mapNeighbour switch
        {
            MapNeighbours.Bottom when scrollAction.BottomExists => _mapService.TryGetMap(scrollAction.BottomMapId, out var bottomMap) ? bottomMap : null,
            MapNeighbours.Left when scrollAction.LeftExists => _mapService.TryGetMap(scrollAction.LeftMapId, out var leftMap) ? leftMap : null,
            MapNeighbours.Right when scrollAction.RightExists => _mapService.TryGetMap(scrollAction.RightMapId, out var rightMap) ? rightMap : null,
            MapNeighbours.Top when scrollAction.TopExists => _mapService.TryGetMap(scrollAction.TopMapId, out var topMap) ? topMap : null,
            _ => null
        };
    }

    /// <summary>
    /// Calculates the cell identifier after changing the map based on the direction.
    /// </summary>
    /// <param name="map">The target map.</param>
    /// <param name="cellId">The current cell identifier.</param>
    /// <param name="mapNeighbour">The direction of the neighboring map.</param>
    /// <returns>The cell identifier after the map change.</returns>
    private short GetCellAfterChangeMap(Map map, short cellId, MapNeighbours mapNeighbour)
    {
        return mapNeighbour switch
        {
            MapNeighbours.Top => map.TopCellId ?? (short)(cellId + 532),
            MapNeighbours.Bottom => map.BottomCellId ?? (short)(cellId - 532),
            MapNeighbours.Left => map.LeftCellId ?? (short)(cellId + 13),
            MapNeighbours.Right => map.RightCellId ?? (short)(cellId - 13),
            _ => 0
        };
    }

    /// <summary>
    /// Determines the neighboring map direction based on the current map and target map identifier.
    /// </summary>
    /// <param name="fromMap">The current map.</param>
    /// <param name="toMapId">The target map identifier.</param>
    /// <returns>The direction of the neighboring map.</returns>
    private static MapNeighbours GetMapNeighbour(Map fromMap, int toMapId)
    {
        return fromMap switch
        {
            _ when fromMap.LeftNeighbourId == toMapId => MapNeighbours.Left,
            _ when fromMap.RightNeighbourId == toMapId => MapNeighbours.Right,
            _ when fromMap.TopNeighbourId == toMapId => MapNeighbours.Top,
            _ when fromMap.BottomNeighbourId == toMapId => MapNeighbours.Bottom,
            _ => MapNeighbours.None
        };
    }
}
