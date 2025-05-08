// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Enums;

public enum CrafterDirectoryParamBits : uint
{
	CraftOptionNone = 0,
	CraftOptionNotFree = 1,
	CraftOptionNotFreeExceptOnFail = 2,
	CraftOptionResourcesRequired = 4
}
