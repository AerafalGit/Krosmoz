// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Enums;

public enum PresetUseResults
{
	PresetUseOk = 1,
	PresetUseOkPartial = 2,
	PresetUseErrUnknown = 3,
	PresetUseErrCriterion = 4,
	PresetUseErrBadPresetId = 5,
	PresetUseErrCooldown = 6
}
