// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Services.Characters.Loading;

/// <summary>
/// Defines the contract for a service that handles character loading operations.
/// </summary>
public interface ICharacterLoadingService
{
    /// <summary>
    /// Asynchronously loads character data for the specified character.
    /// </summary>
    /// <param name="character">The character whose character data is to be loaded.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task LoadCharacterAsync(CharacterActor character);
}
