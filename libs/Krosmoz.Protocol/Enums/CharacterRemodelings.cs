// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Enums;

[Flags]
public enum CharacterRemodelings
{
	CharacterRemodelingNotApplicable = 0,
	CharacterRemodelingName = 1,
	CharacterRemodelingColors = 2,
	CharacterRemodelingCosmetic = 4,
	CharacterRemodelingBreed = 8,
	CharacterRemodelingGender = 16
}
