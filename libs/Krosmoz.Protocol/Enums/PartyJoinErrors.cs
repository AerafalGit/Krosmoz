// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Enums;

public enum PartyJoinErrors
{
	PartyJoinErrorUnknown = 0,
	PartyJoinErrorPlayerNotFound = 1,
	PartyJoinErrorPartyNotFound = 2,
	PartyJoinErrorPartyFull = 3,
	PartyJoinErrorPlayerBusy = 4,
	PartyJoinErrorPlayerAlreadyInvited = 6,
	PartyJoinErrorPlayerTooSollicited = 7,
	PartyJoinErrorPlayerLoyal = 8,
	PartyJoinErrorUnmodifiable = 9,
	PartyJoinErrorUnmetCriterion = 10,
	PartyJoinErrorNotEnoughRoom = 11,
	PartyJoinErrorCompositionChanged = 12
}
