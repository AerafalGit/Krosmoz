// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Enums;

public enum SocialGroupCreationResults
{
	SocialGroupCreateOk = 1,
	SocialGroupCreateErrorNameInvalid = 2,
	SocialGroupCreateErrorAlreadyInGroup = 3,
	SocialGroupCreateErrorNameAlreadyExists = 4,
	SocialGroupCreateErrorEmblemAlreadyExists = 5,
	SocialGroupCreateErrorLeave = 6,
	SocialGroupCreateErrorCancel = 7,
	SocialGroupCreateErrorRequirementUnmet = 8,
	SocialGroupCreateErrorEmblemInvalid = 9,
	SocialGroupCreateErrorTagInvalid = 10,
	SocialGroupCreateErrorTagAlreadyExists = 11,
	SocialGroupCreateErrorNeedsSubgroup = 12,
	SocialGroupCreateErrorUnknown = 99
}
