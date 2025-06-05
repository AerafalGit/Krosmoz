// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Enums;

public enum CharacterCreationResults
{
	Ok = 0,
	ErrNoReason = 1,
	ErrInvalidName = 2,
	ErrNameAlreadyExists = 3,
	ErrTooManyCharacters = 4,
	ErrNotAllowed = 5,
	ErrNewPlayerNotAllowed = 6,
	ErrRestricedZone = 7
}
