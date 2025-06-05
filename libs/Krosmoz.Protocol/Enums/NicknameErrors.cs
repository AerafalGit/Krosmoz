// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Enums;

public enum NicknameErrors
{
	AlreadyUsed = 1,
	SameAsLogin = 2,
	TooSimilarToLogin = 3,
	InvalidNick = 4,
	UnknownNickError = 99
}
