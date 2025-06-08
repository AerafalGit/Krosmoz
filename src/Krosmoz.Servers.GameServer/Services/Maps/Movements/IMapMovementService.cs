// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Servers.GameServer.Models.Actors;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;
using Krosmoz.Servers.GameServer.Models.World.Maps;

namespace Krosmoz.Servers.GameServer.Services.Maps.Movements;

/// <summary>
/// Defines the contract for handling map movement and orientation changes for actors.
/// </summary>
public interface IMapMovementService
{
    /// <summary>
    /// Handles the change of orientation for an actor on the specified map asynchronously.
    /// </summary>
    /// <param name="map">The map where the orientation change occurs.</param>
    /// <param name="actorId">The unique identifier of the actor whose orientation is changing.</param>
    /// <param name="direction">The new direction of the actor.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task OnChangeOrientationAsync(Map map, int actorId, Directions direction);

    /// <summary>
    /// Changes the orientation of the specified actor on the map asynchronously.
    /// </summary>
    /// <param name="map">The map where the orientation change occurs.</param>
    /// <param name="actor">The actor whose orientation is changing.</param>
    /// <param name="direction">The new direction of the actor.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task OnChangeOrientationAsync(Map map, Actor actor, Directions direction);

    /// <summary>
    /// Handles a map movement request for the specified actor asynchronously.
    /// </summary>
    /// <param name="actor">The actor making the movement request.</param>
    /// <param name="mapId">The identifier of the map to which the actor is moving.</param>
    /// <param name="keyMovements">The sequence of key movements for the actor.</param>
    /// <param name="cautious">Indicates whether the movement is cautious.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task OnMapMovementRequestAsync(Actor actor, int mapId, IEnumerable<short> keyMovements, bool cautious);

    /// <summary>
    /// Confirms the map movement for the specified character asynchronously.
    /// </summary>
    /// <param name="character">The character actor confirming the movement.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task OnMapMovementConfirmAsync(CharacterActor character);

    /// <summary>
    /// Cancels the map movement for the specified character asynchronously.
    /// </summary>
    /// <param name="character">The character actor canceling the movement.</param>
    /// <param name="cellId">The cell identifier where the movement is canceled.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task OnMapMovementCancelAsync(CharacterActor character, short cellId);
}
