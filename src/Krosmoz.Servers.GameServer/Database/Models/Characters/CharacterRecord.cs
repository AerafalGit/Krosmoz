// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Servers.GameServer.Database.Models.Characteristics;
using Krosmoz.Servers.GameServer.Models.Appearances;

namespace Krosmoz.Servers.GameServer.Database.Models.Characters;

public sealed class CharacterRecord
{
    public int Id { get; init; }

    public required string Name { get; set; }

    public required int AccountId { get; set; }

    public required ulong Experience { get; set; }

    public required BreedIds Breed { get; set; }

    public required ushort Head { get; set; }

    public required bool Sex { get; set; }

    public required CharacterPosition Position { get; set; }

    public required PlayerStatuses Status { get; set; }

    public required ActorLook Look { get; set; }

    public required uint Kamas { get; set; }

    public required List<EmoticonIds> Emotes { get; set; }

    public required List<SpellIds> Spells { get; set; }

    public required DateTime CreatedAt { get; init; }

    public required DateTime UpdatedAt { get; set; }

    public required ushort DeathCount { get; set; }

    public required byte DeathMaxLevel { get; set; }

    public required HardcoreOrEpicDeathStates DeathState { get; set; }

    public required Dictionary<CharacteristicIds, CharacteristicData> Characteristics { get; init; }

    public required CharacterRemodelings PossibleChanges { get; set; }

    public required CharacterRemodelings MandatoryChanges { get; set; }

    public required PlayerCapabilities Capabilities { get; set; }

    public DateTime? DeletedAt { get; set; }
}
