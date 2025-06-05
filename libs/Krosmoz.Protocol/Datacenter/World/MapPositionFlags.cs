// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.World;

[Flags]
public enum MapPositionFlags : uint
{
    None = 0,
    AllowChallenge = 1,
    AllowAggression = 2,
    AllowTeleportTo = 4,
    AllowTeleportFrom = 8,
    AllowExchangesBetweenPlayers = 16,
    AllowHumanVendor = 32,
    AllowCollector = 64,
    AllowSoulCapture = 128,
    AllowSoulSummon = 256,
    AllowTavernRegen = 512,
    AllowTombMode = 1024,
    AllowTeleportEverywhere = 2048,
    AllowFightChallenges = 4096
}
