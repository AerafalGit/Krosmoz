// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Messages.Game.Inventory.Items;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Services.Inventory;

/// <summary>
/// Provides services for managing and sending inventory-related data for characters.
/// </summary>
public sealed class InventoryService : IInventoryService
{
    /// <summary>
    /// Sends the inventory content of a character asynchronously.
    /// </summary>
    /// <param name="character">The character actor whose inventory content will be sent.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask SendInventoryContentAsync(CharacterActor character)
    {
        return character.Connection.SendAsync(new InventoryContentMessage
        {
            Kamas = (uint)character.Kamas,
            Objects = []
        });
    }

    /// <summary>
    /// Sends the inventory weight of a character asynchronously.
    /// </summary>
    /// <param name="character">The character actor whose inventory weight will be sent.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask SendInventoryWeightAsync(CharacterActor character)
    {
        return character.Connection.SendAsync(new InventoryWeightMessage
        {
            Weight = 0,
            WeightMax = 1_000 +
                        (uint)(character.Characteristics.Strength.Total * 5) +
                        (uint)character.Characteristics.Weight.Total
            // + (uint)character.JobInventory.Jobs.Sum(static x => x.Level * 5)
        });
    }
}
