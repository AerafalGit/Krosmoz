// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Models.GameActions;

/// <summary>
/// Represents the base class for game actions.
/// A game action defines a specific operation or behavior that can be executed
/// by a character in the game world.
/// </summary>
public abstract class GameAction
{
    /// <summary>
    /// Gets the type of the game action.
    /// The type is represented by the <see cref="GameActionTypes"/> enumeration.
    /// </summary>
    public abstract GameActionTypes Type { get; }

    /// <summary>
    /// Determines whether the game action can be executed by the specified character.
    /// </summary>
    /// <param name="character">The character attempting to execute the action.</param>
    /// <returns>
    /// <c>true</c> if the action can be executed by the character; otherwise, <c>false</c>.
    /// </returns>
    public abstract bool CanBeExecuted(CharacterActor character);
}
