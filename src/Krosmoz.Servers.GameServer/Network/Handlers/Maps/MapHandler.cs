// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Dispatcher.Attributes;
using Krosmoz.Protocol.Messages.Game.Context.Roleplay;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Maps;
using Krosmoz.Servers.GameServer.Services.Maps.Teleport;

namespace Krosmoz.Servers.GameServer.Network.Handlers.Maps;

/// <summary>
/// Handles map-related network messages.
/// </summary>
public sealed class MapHandler
{
    private readonly IMapService _mapService;
    private readonly IMapTeleportService _mapTeleportService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapHandler"/> class.
    /// </summary>
    /// <param name="mapService">Service for handling map-related operations.</param>
    /// <param name="mapTeleportService">Service for handling map teleportation operations.</param>
    public MapHandler(IMapService mapService, IMapTeleportService mapTeleportService)
    {
        _mapService = mapService;
        _mapTeleportService = mapTeleportService;
    }

    /// <summary>
    /// Handles a request for map information asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client.</param>
    /// <param name="message">The message containing the map information request.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleMapInformationRequestAsync(DofusConnection connection, MapInformationsRequestMessage message)
    {
        return _mapService.SendMapInformationsAsync(connection.Heroes.Master, message.MapId);
    }

    /// <summary>
    /// Handles a request to change the map asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client.</param>
    /// <param name="message">The message containing the map change request.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleChangeMapAsync(DofusConnection connection, ChangeMapMessage message)
    {
        return _mapTeleportService.ChangeMapAsync(connection.Heroes.Master, message.MapId);
    }
}
