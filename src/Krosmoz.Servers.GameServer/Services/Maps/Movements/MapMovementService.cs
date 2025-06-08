// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Game.Context;
using Krosmoz.Protocol.Types.Game.Context;
using Krosmoz.Servers.GameServer.Models.Actors;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;
using Krosmoz.Servers.GameServer.Models.World;
using Krosmoz.Servers.GameServer.Models.World.Maps;
using Krosmoz.Servers.GameServer.Models.World.Paths;
using Krosmoz.Servers.GameServer.Services.Characteristics;

namespace Krosmoz.Servers.GameServer.Services.Maps.Movements;

/// <summary>
/// Represents a service for handling map movements and orientation changes for actors.
/// </summary>
public sealed class MapMovementService : IMapMovementService
{
    private readonly IMapService _mapService;
    private readonly ICharacteristicService _characteristicService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapMovementService"/> class.
    /// </summary>
    /// <param name="mapService">The map service used for map-related operations.</param>
    /// <param name="characteristicService">The characteristic service used for actor characteristics.</param>
    public MapMovementService(IMapService mapService, ICharacteristicService characteristicService)
    {
        _mapService = mapService;
        _characteristicService = characteristicService;
    }

    /// <summary>
    /// Handles the change of orientation for an actor on the specified map asynchronously.
    /// </summary>
    /// <param name="map">The map where the orientation change occurs.</param>
    /// <param name="actorId">The unique identifier of the actor whose orientation is changing.</param>
    /// <param name="direction">The new direction of the actor.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task OnChangeOrientationAsync(Map map, int actorId, Directions direction)
    {
        return !map.TryGetActor(actorId, out var actor)
            ? Task.CompletedTask
            : OnChangeOrientationAsync(map, actor, direction);
    }

    /// <summary>
    /// Changes the orientation of the specified actor on the map asynchronously.
    /// </summary>
    /// <param name="map">The map where the orientation change occurs.</param>
    /// <param name="actor">The actor whose orientation is changing.</param>
    /// <param name="direction">The new direction of the actor.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task OnChangeOrientationAsync(Map map, Actor actor, Directions direction)
    {
        if ((sbyte)direction < 0 || (sbyte)direction > 7)
            return Task.CompletedTask;

        actor.Orientation = direction;

        return _mapService.SendAsync(map, new GameMapChangeOrientationMessage
        {
            Orientation = new ActorOrientation { Id = actor.Id, Direction = (sbyte)direction }
        });
    }

    /// <summary>
    /// Handles a map movement request for the specified actor asynchronously.
    /// </summary>
    /// <param name="actor">The actor making the movement request.</param>
    /// <param name="mapId">The identifier of the map to which the actor is moving.</param>
    /// <param name="keyMovements">The sequence of key movements for the actor.</param>
    /// <param name="cautious">Indicates whether the movement is cautious.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task OnMapMovementRequestAsync(Actor actor, int mapId, IEnumerable<short> keyMovements, bool cautious)
    {
        if (!_mapService.TryGetMap(mapId, out var map))
            return;

        if (actor.Map.Id != mapId)
        {
            await SendGameMapNoMovementAsync(actor);
            return;
        }

        var movementPath = MovementPath.BuildFromCompressedPath(map, keyMovements);

        // TODO: fights

        if (!actor.CanMove())
        {
            await SendGameMapNoMovementAsync(actor);
            return;
        }

        await MoveActorAsync(actor, movementPath, cautious);
    }

    /// <summary>
    /// Confirms the map movement for the specified character asynchronously.
    /// </summary>
    /// <param name="character">The character actor confirming the movement.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task OnMapMovementConfirmAsync(CharacterActor character)
    {
        return !character.IsMoving
            ? Task.CompletedTask
            : StopMoveAsync(character, character.MovementPath.EndPathPosition);
    }

    /// <summary>
    /// Cancels the map movement for the specified character asynchronously.
    /// </summary>
    /// <param name="character">The character actor canceling the movement.</param>
    /// <param name="cellId">The cell identifier where the movement is canceled.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task OnMapMovementCancelAsync(CharacterActor character, short cellId)
    {
        if (!character.IsMoving)
            return Task.CompletedTask;

        if (!character.MovementPath.Contains(cellId))
            return Task.CompletedTask;

        return StopMoveAsync(character, new WorldPosition(character.Map, character.Map.GetCell(cellId), character.Orientation));
    }

    /// <summary>
    /// Moves the specified actor along the given movement path asynchronously.
    /// </summary>
    /// <param name="actor">The actor to move.</param>
    /// <param name="movementPath">The movement path to follow.</param>
    /// <param name="cautious">Indicates whether the movement is cautious.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task MoveActorAsync(Actor actor, MovementPath movementPath, bool cautious)
    {
        if (movementPath.IsEmpty)
        {
            await SendGameMapNoMovementAsync(actor);
            return;
        }

        // TODO: clip path at triggers

        if (actor.IsMoving)
            await StopMoveAsync(actor, movementPath.EndPathPosition);

        var movementKeys = movementPath.GetServerPathKeys();

        await _mapService.SendAsync(actor.Map, cautious
            ? new GameCautiousMapMovementMessage
            {
                ActorId = actor.Id,
                KeyMovements = movementKeys
            }
            : new GameMapMovementMessage
            {
                ActorId = actor.Id,
                KeyMovements = movementKeys
            });

        // TODO: stop emote when moving

        if (actor is CharacterActor character)
        {
            character.MovementPath = movementPath;
            return;
        }

        // For non-character actors, we directly set the position, don't need to wait for confirmation
        actor.Position = movementPath.EndPathPosition;
    }

    /// <summary>
    /// Stops the movement of the specified actor and updates its position asynchronously.
    /// </summary>
    /// <param name="actor">The actor whose movement is being stopped.</param>
    /// <param name="position">The new position of the actor.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task StopMoveAsync(Actor actor, WorldPosition position)
    {
        actor.Position = position;
        actor.MovementPath = null;

        // TODO: execute triggers if any

        if (actor is CharacterActor character)
        {
            await _characteristicService.EndLifePointsUpdateAsync(character);
            await _characteristicService.BeginLifePointsUpdateAsync(character);
        }
    }

    /// <summary>
    /// Sends a "no movement" message to the specified actor asynchronously.
    /// </summary>
    /// <param name="actor">The actor to send the message to.</param>
    /// <returns>A value task that represents the asynchronous operation.</returns>
    private static ValueTask SendGameMapNoMovementAsync(Actor actor)
    {
        return actor is CharacterActor character
            ? character.Connection.SendAsync<GameMapNoMovementMessage>()
            : ValueTask.CompletedTask;
    }
}
