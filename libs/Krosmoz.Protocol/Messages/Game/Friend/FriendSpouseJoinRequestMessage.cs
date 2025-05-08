// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class FriendSpouseJoinRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5604;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static FriendSpouseJoinRequestMessage Empty =>
		new();
}
