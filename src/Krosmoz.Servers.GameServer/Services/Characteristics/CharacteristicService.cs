// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Frozen;
using Krosmoz.Core.Services;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Messages.Game.Character.Stats;
using Krosmoz.Protocol.Messages.Game.Context.Roleplay.Stats;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;
using Krosmoz.Servers.GameServer.Services.Breeds;
using Krosmoz.Servers.GameServer.Services.Datacenter;
using Characteristic = Krosmoz.Servers.GameServer.Models.Characteristics.Characteristic;
using DatacenterCharacteristic = Krosmoz.Protocol.Datacenter.Characteristics.Characteristic;

namespace Krosmoz.Servers.GameServer.Services.Characteristics;

/// <summary>
/// Provides services for managing and sending character characteristics.
/// </summary>
public sealed class CharacteristicService : ICharacteristicService, IInitializableService
{
    private readonly IDatacenterService _datacenterService;
    private readonly IBreedService _breedService;

    private FrozenDictionary<CharacteristicIds, DatacenterCharacteristic> _characteristics;

    public CharacteristicService(IDatacenterService datacenterService, IBreedService breedService)
    {
        _datacenterService = datacenterService;
        _breedService = breedService;
        _characteristics = FrozenDictionary<CharacteristicIds, DatacenterCharacteristic>.Empty;
    }

    /// <summary>
    /// Sends the characteristics of a character asynchronously.
    /// </summary>
    /// <param name="character">The character actor whose characteristics will be sent.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask SendCharacterCharacteristicsAsync(CharacterActor character)
    {
        return character.Connection.SendAsync(new CharacterStatsListMessage
        {
            Stats = character.GetCharacterCharacteristicsInformations()
        });
    }

    /// <summary>
    /// Upgrades a specific characteristic of a character asynchronously.
    /// </summary>
    /// <param name="character">The character actor whose characteristic will be upgraded.</param>
    /// <param name="characteristicId">The ID of the characteristic to upgrade.</param>
    /// <param name="boostPoint">The number of boost points to apply to the characteristic.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task UpgradeCharacteristicAsync(CharacterActor character, CharacteristicIds characteristicId, ushort boostPoint)
    {
        if (character.Characteristics.StatsPoints.Total < boostPoint || boostPoint is 0)
        {
            await SendStatsUpgradeResultAsync(character, StatsUpgradeResults.NotEnoughPoint, boostPoint);
            return;
        }

        if (!_characteristics.TryGetValue(characteristicId, out var datacenterCharacteristic) || !datacenterCharacteristic.Upgradable)
            return;

        if (!character.Characteristics.Characteristics.TryGetValue(characteristicId, out var characteristic))
            return;

        if (!TryGetCharacteristicUpgradeAmount(character, boostPoint, characteristic, out var actualPoints, out var points))
            return;

        characteristic.Base = actualPoints;

        character.Characteristics.StatsPoints.Base -= boostPoint - points;

        await SendStatsUpgradeResultAsync(character, StatsUpgradeResults.Success, boostPoint);

        await SendCharacterCharacteristicsAsync(character);
    }

    /// <summary>
    /// Begins the asynchronous update of a character's life points.
    /// </summary>
    /// <param name="character">The character actor whose life points update will begin.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask BeginLifePointsUpdateAsync(CharacterActor character)
    {
        return ValueTask.CompletedTask;

        // TODO: Implement the logic to begin life points update.
    }

    /// <summary>
    /// Ends the asynchronous update of a character's life points.
    /// </summary>
    /// <param name="character">The character actor whose life points update will end.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask EndLifePointsUpdateAsync(CharacterActor character)
    {
        return ValueTask.CompletedTask;

        // TODO: Implement the logic to end life points update.
    }

    /// <summary>
    /// Initializes the service by loading characteristics data from the datacenter.
    /// </summary>
    public void Initialize()
    {
        _characteristics = _datacenterService.GetObjects<DatacenterCharacteristic>().ToFrozenDictionary(static x => (CharacteristicIds)x.Id);
    }

    /// <summary>
    /// Attempts to calculate the upgrade amount for a characteristic.
    /// </summary>
    /// <param name="character">The character actor whose characteristic is being upgraded.</param>
    /// <param name="boostPoint">The number of boost points to apply.</param>
    /// <param name="characteristic">The characteristic being upgraded.</param>
    /// <param name="actualPoints">The resulting base value of the characteristic after the upgrade.</param>
    /// <param name="points">The remaining boost points after the upgrade.</param>
    /// <returns><c>true</c> if the upgrade calculation was successful; otherwise, <c>false</c>.</returns>
    private bool TryGetCharacteristicUpgradeAmount(
        CharacterActor character,
        ushort boostPoint,
        Characteristic characteristic,
        out int actualPoints,
        out ushort points)
    {
        actualPoints = characteristic.Base;
        points = boostPoint;

        if (!_breedService.TryGetBreed(character.Breed, out var breed))
            return false;

        var thresholds = characteristic.Id switch
        {
            CharacteristicIds.Vitalite => breed.StatsPointsForVitality,
            CharacteristicIds.Chance => breed.StatsPointsForChance,
            CharacteristicIds.Force => breed.StatsPointsForStrength,
            CharacteristicIds.Sagesse => breed.StatsPointsForWisdom,
            CharacteristicIds.Intelligence => breed.StatsPointsForIntelligence,
            CharacteristicIds.Agilite => breed.StatsPointsForAgility,
            _ => []
        };

        for (var i = GetThresholdIndex(actualPoints, thresholds); points >= thresholds[i][1]; i = GetThresholdIndex(actualPoints, thresholds))
        {
            var boost = i < thresholds.Count - 1 && points / (double)thresholds[i][1] > thresholds[i + 1][0] - actualPoints
                ? (short)(thresholds[i + 1][0] - actualPoints)
                : (short)Math.Floor(points / (double)thresholds[i][1]);

            var pointsUsed = (short)(boost * thresholds[i][1]);

            if (thresholds[i].Count > 2)
                boost = (short)(boost * thresholds[i][2]);

            actualPoints += boost;
            points -= (ushort)pointsUsed;
        }

        return true;
    }

    /// <summary>
    /// Gets the index of the threshold for the given characteristic points.
    /// </summary>
    /// <param name="actualPoints">The current base value of the characteristic.</param>
    /// <param name="thresholds">The list of thresholds for the characteristic.</param>
    /// <returns>The index of the threshold.</returns>
    private static int GetThresholdIndex(long actualPoints, List<List<uint>> thresholds)
    {
        for (var i = 0; i < thresholds.Count - 1; i++)
        {
            if (thresholds[i][0] <= actualPoints && thresholds[i + 1][0] > actualPoints)
                return i;
        }

        return thresholds.Count - 1;
    }

    /// <summary>
    /// Sends the result of a stats upgrade operation to the character asynchronously.
    /// </summary>
    /// <param name="character">The character actor to send the result to.</param>
    /// <param name="result">The result of the stats upgrade operation.</param>
    /// <param name="boostPoint">The number of boost points used in the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private static ValueTask SendStatsUpgradeResultAsync(CharacterActor character, StatsUpgradeResults result, ushort boostPoint)
    {
        return character.Connection.SendAsync(new StatsUpgradeResultMessage { Result = (sbyte)result, NbCharacBoost = boostPoint });
    }
}
