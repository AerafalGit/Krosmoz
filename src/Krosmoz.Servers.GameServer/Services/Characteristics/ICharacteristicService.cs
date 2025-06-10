// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Services.Characteristics;

/// <summary>
/// Defines the contract for a service that provides operations related to characteristics.
/// </summary>
public interface ICharacteristicService
{
    /// <summary>
    /// Sends the characteristics of a character asynchronously.
    /// </summary>
    /// <param name="character">The character actor whose characteristics will be sent.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask SendCharacterCharacteristicsAsync(CharacterActor character);

    /// <summary>
    /// Upgrades a specific characteristic of a character asynchronously.
    /// </summary>
    /// <param name="character">The character actor whose characteristic will be upgraded.</param>
    /// <param name="characteristicId">The ID of the characteristic to upgrade.</param>
    /// <param name="boostPoint">The number of boost points to apply to the characteristic.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpgradeCharacteristicAsync(CharacterActor character, CharacteristicIds characteristicId, ushort boostPoint);

    /// <summary>
    /// Begins the asynchronous update of a character's life points.
    /// </summary>
    /// <param name="character">The character actor whose life points update will begin.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask BeginLifePointsUpdateAsync(CharacterActor character);

    /// <summary>
    /// Ends the asynchronous update of a character's life points.
    /// </summary>
    /// <param name="character">The character actor whose life points update will end.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask EndLifePointsUpdateAsync(CharacterActor character);
}
