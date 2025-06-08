// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Dispatcher.Attributes;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Game.Context;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Maps.Movements;

namespace Krosmoz.Servers.GameServer.Network.Handlers.Maps.Movements;

/// <summary>
/// Handles map movement-related operations.
/// </summary>
public sealed class MapMovementHandler
{
    private readonly IMapMovementService _mapMovementService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapMovementHandler"/> class.
    /// </summary>
    /// <param name="mapMovementService">The service used for handling map movement operations.</param>
    public MapMovementHandler(IMapMovementService mapMovementService)
    {
        _mapMovementService = mapMovementService;
    }

    /// <summary>
    /// Handles a request to change the orientation of a character on the map asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client making the request.</param>
    /// <param name="message">The message containing the orientation change details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleGameMapChangeOrientationRequestAsync(DofusConnection connection, GameMapChangeOrientationRequestMessage message)
    {
        return _mapMovementService.OnChangeOrientationAsync(connection.Heroes.Master.Map, connection.Heroes.Master, (Directions)message.Direction);
    }

    /// <summary>
    /// Handles a cautious map movement request asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client making the request.</param>
    /// <param name="message">The message containing the cautious map movement details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleGameCautiousMapMovementRequestAsync(DofusConnection connection, GameCautiousMapMovementRequestMessage message)
    {
        return _mapMovementService.OnMapMovementRequestAsync(connection.Heroes.Master, message.MapId, message.KeyMovements, true);
    }

    /// <summary>
    /// Handles a standard map movement request asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client making the request.</param>
    /// <param name="message">The message containing the map movement details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleGameMapMovementRequestAsync(DofusConnection connection, GameMapMovementRequestMessage message)
    {
        return _mapMovementService.OnMapMovementRequestAsync(connection.Heroes.Master, message.MapId, message.KeyMovements, false);
    }

    /// <summary>
    /// Handles a map movement confirmation asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client making the confirmation.</param>
    /// <param name="_">The message confirming the map movement.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleGameMapMovementConfirmAsync(DofusConnection connection, GameMapMovementConfirmMessage _)
    {
        return _mapMovementService.OnMapMovementConfirmAsync(connection.Heroes.Master);
    }

    /// <summary>
    /// Handles a map movement cancellation request asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client making the request.</param>
    /// <param name="message">The message containing the map movement cancellation details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleGameMapMovementCancelAsync(DofusConnection connection, GameMapMovementCancelMessage message)
    {
        return _mapMovementService.OnMapMovementCancelAsync(connection.Heroes.Master, (short)message.CellId);
    }
}
