// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Krosmoz.Protocol.Enums;
using Krosmoz.Servers.GameServer.Models.World;
using Krosmoz.Servers.GameServer.Services.Maps;

namespace Krosmoz.Servers.GameServer.Factories.World;

/// <summary>
/// Factory class responsible for creating world positions.
/// Implements <see cref="IWorldPositionFactory"/>.
/// </summary>
public sealed class WorldPositionFactory : IWorldPositionFactory
{
    private readonly IMapService _mapService;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorldPositionFactory"/> class.
    /// </summary>
    /// <param name="mapService">The map service used to retrieve map data.</param>
    public WorldPositionFactory(IMapService mapService)
    {
        _mapService = mapService;
    }

    /// <summary>
    /// Attempts to create a world position based on the provided map ID, cell ID, and orientation.
    /// </summary>
    /// <param name="mapId">The ID of the map where the position is located.</param>
    /// <param name="cellId">The ID of the cell within the map.</param>
    /// <param name="orientation">The orientation of the position.</param>
    /// <param name="worldPosition">
    /// When this method returns, contains the created <see cref="WorldPosition"/> if the creation was successful;
    /// otherwise, <c>null</c>.
    /// </param>
    /// <returns><c>true</c> if the world position was successfully created; otherwise, <c>false</c>.</returns>
    public bool TryCreateWorldPosition(int mapId, short cellId, Directions orientation, [NotNullWhen(true)] out WorldPosition? worldPosition)
    {
        if (!_mapService.TryGetMap(mapId, out var map))
        {
            worldPosition = null;
            return false;
        }

        worldPosition = new WorldPosition(map, map.GetCell(cellId), orientation);
        return true;
    }
}
