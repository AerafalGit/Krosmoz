// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Enums;

public enum ListAddFailures
{
	ListAddFailureUnknown = 0,
	ListAddFailureOverQuota = 1,
	ListAddFailureNotFound = 2,
	ListAddFailureEgocentric = 3,
	ListAddFailureIsDouble = 4
}
