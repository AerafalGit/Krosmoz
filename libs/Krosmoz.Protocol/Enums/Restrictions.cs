// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Enums;

[Flags]
public enum Restrictions
{
    None = 0,
    CantAggress = 1 << 0,
    CantBeAggressed = 1 << 1,
    CantTrade = 1 << 2,
    CantUseObject = 1 << 3,
    CantUseTaxCollector = 1 << 4,
    CantUseInteractive = 1 << 5,
    CantChat = 1 << 6,
    CantBeChallenged = 1 << 7,
    CantBeAttackedByMutant = 1 << 8,
    CantAttack = 1 << 9,
    CantAttackMonster = 1 << 10,
    CantBeMerchant = 1 << 11,
    CantChallenge = 1 << 12,
    CantChangeZone = 1 << 13,
    CantExchange = 1 << 14,
    CantMinimize = 1 << 15,
    CantMove = 1 << 16,
    CantRun = 1 << 17,
    CantSpeakToNpc = 1 << 18,
    CantWalk8Directions = 1 << 19,
    ForceSlowWalk = 1 << 20
}
