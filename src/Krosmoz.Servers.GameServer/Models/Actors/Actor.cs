// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Types.Game.Context;
using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;
using Krosmoz.Servers.GameServer.Models.Appearances;
using Krosmoz.Servers.GameServer.Models.World;
using Krosmoz.Servers.GameServer.Models.World.Cells;
using Krosmoz.Servers.GameServer.Models.World.Maps;
using Krosmoz.Servers.GameServer.Models.World.Paths;

namespace Krosmoz.Servers.GameServer.Models.Actors;

/// <summary>
/// Represents a base class for all actors in the game world.
/// An actor is an entity that has a position, orientation, appearance, and can interact with the game world.
/// </summary>
public abstract class Actor
{
    /// <summary>
    /// Gets or sets the unique identifier of the actor.
    /// </summary>
    public abstract int Id { get; }

    /// <summary>
    /// Gets or sets the position of the actor in the game world.
    /// </summary>
    public abstract WorldPosition Position { get; set; }

    /// <summary>
    /// Gets or sets the appearance of the actor.
    /// </summary>
    public abstract ActorLook Look { get; set; }

    /// <summary>
    /// Gets or sets the current movement path of the actor.
    /// </summary>
    public MovementPath? MovementPath { get; set; }

    /// <summary>
    /// Gets a value indicating whether the actor is currently moving.
    /// This property returns true if the <see cref="MovementPath"/> is not null, indicating the actor has a movement path.
    /// </summary>
    [MemberNotNullWhen(true, nameof(MovementPath))]
    public bool IsMoving =>
        MovementPath is not null;

    /// <summary>
    /// Gets the map associated with the actor's current position.
    /// </summary>
    public Map Map =>
        Position.Map;

    /// <summary>
    /// Gets or sets the cell where the actor is located.
    /// </summary>
    public Cell Cell
    {
        get => Position.Cell;
        set => Position.Cell = value;
    }

    /// <summary>
    /// Gets or sets the orientation of the actor.
    /// </summary>
    public Directions Orientation
    {
        get => Position.Orientation;
        set => Position.Orientation = value;
    }

    /// <summary>
    /// Determines whether the actor can move.
    /// </summary>
    /// <returns>True if the actor can move; otherwise, false.</returns>
    public virtual bool CanMove()
    {
        return true;
    }

    /// <summary>
    /// Determines whether the actor can be seen by a specified character.
    /// </summary>
    /// <param name="fromCharacter">The character attempting to see the actor.</param>
    /// <returns>True if the actor can be seen; otherwise, false.</returns>
    public virtual bool CanBeeSeen(CharacterActor fromCharacter)
    {
        return true;
    }

    /// <summary>
    /// Retrieves the game roleplay actor information, including its ID, disposition, and appearance.
    /// </summary>
    /// <returns>An instance of <see cref="GameRolePlayActorInformations"/> containing the actor's roleplay data.</returns>
    public virtual GameRolePlayActorInformations GetGameRolePlayActorInformations()
    {
        return new GameRolePlayActorInformations
        {
            ContextualId = Id,
            Disposition = GetEntityDispositionInformations(),
            Look = Look.GetEntityLook()
        };
    }

    /// <summary>
    /// Retrieves the disposition information of the actor, including its cell ID and orientation.
    /// </summary>
    /// <returns>An instance of <see cref="EntityDispositionInformations"/> containing the actor's disposition data.</returns>
    public virtual EntityDispositionInformations GetEntityDispositionInformations()
    {
        return new EntityDispositionInformations
        {
            CellId = Cell.Id,
            Direction = (sbyte)Orientation,
        };
    }
}
