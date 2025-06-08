// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Models.Context;

/// <summary>
/// Defines the contract for a context service that manages game contexts for character.
/// </summary>
public interface IContextService
{
    /// <summary>
    /// Asynchronously creates a game context for the specified character with the given player state.
    /// </summary>
    /// <param name="character">The character for whom the game context is to be created.</param>
    /// <param name="state">The state of the player in the game context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CreateContextAsync(CharacterActor character, PlayerStates state);
}
