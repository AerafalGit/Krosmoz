// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Enums;

public enum ServerConnectionErrors
{
	ServerConnectionErrorDueToStatus = 0,
	ServerConnectionErrorNoReason = 1,
	ServerConnectionErrorAccountRestricted = 2,
	ServerConnectionErrorCommunityRestricted = 3,
	ServerConnectionErrorLocationRestricted = 4,
	ServerConnectionErrorSubscribersOnly = 5,
	ServerConnectionErrorRegularPlayersOnly = 6
}
