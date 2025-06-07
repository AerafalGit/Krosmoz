// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Services.Inventory;

/// <summary>
/// Defines the contract for a service that handles inventory-related operations for character.
/// </summary>
public interface IInventoryService
{
    /// <summary>
    /// Asynchronously sends the inventory content of the specified character.
    /// </summary>
    /// <param name="character">The character whose inventory content is to be sent.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    ValueTask SendInventoryContentAsync(CharacterActor character);

    /// <summary>
    /// Asynchronously sends the inventory weight of the specified character.
    /// </summary>
    /// <param name="character">The character whose inventory weight is to be sent.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    ValueTask SendInventoryWeightAsync(CharacterActor character);
}
