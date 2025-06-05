// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Enums;

public enum TaxCollectorErrorReasons
{
	TaxCollectorErrorUnknown = 0,
	TaxCollectorNotFound = 1,
	TaxCollectorNotOwned = 2,
	TaxCollectorNoRights = 3,
	TaxCollectorMaxReached = 4,
	TaxCollectorAlreadyOne = 5,
	TaxCollectorCantHireYet = 6,
	TaxCollectorCantHireHere = 7,
	TaxCollectorNotEnoughKamas = 8
}
