// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Services.Social;

/// <summary>
/// Defines the contract for a service that provides social-related operations for characters.
/// </summary>
public interface ISocialService
{
    /// <summary>
    /// Loads the social relations (e.g., friends, ignored players) of a character asynchronously.
    /// </summary>
    /// <param name="character">The character actor whose relations will be loaded.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task LoadRelationsAsync(CharacterActor character);

    /// <summary>
    /// Sends the friend list of a character asynchronously.
    /// </summary>
    /// <param name="character">The character actor whose friend list will be sent.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask SendFriendListAsync(CharacterActor character);

    /// <summary>
    /// Sends the ignored list of a character asynchronously.
    /// </summary>
    /// <param name="character">The character actor whose ignored list will be sent.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask SendIgnoredListAsync(CharacterActor character);

    /// <summary>
    /// Sends the spouse information of a character asynchronously.
    /// </summary>
    /// <param name="character">The character actor whose spouse information will be sent.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask SendSpouseInformationsAsync(CharacterActor character);
}
