// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Enums;

public enum ExchangeErrors : int
{
	RequestImpossible = 1,
	RequestCharacterOccupied = 2,
	RequestCharacterJobNotEquiped = 3,
	RequestCharacterToolTooFar = 4,
	RequestCharacterOverloaded = 5,
	RequestCharacterNotSuscriber = 6,
	RequestCharacterRestricted = 7,
	BuyError = 8,
	SellError = 9,
	MountPaddockError = 10,
	BidSearchError = 11
}
