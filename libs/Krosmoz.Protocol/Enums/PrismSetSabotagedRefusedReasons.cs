// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Enums;

public enum PrismSetSabotagedRefusedReasons
{
	SabotageRefused = -1,
	SabotageInsufficientRights = 0,
	SabotageMemberAccountNeeded = 1,
	SabotageRestrictedAccount = 2,
	SabotageWrongAlliance = 3,
	SabotageNoPrism = 4,
	SabotageWrongState = 5
}
