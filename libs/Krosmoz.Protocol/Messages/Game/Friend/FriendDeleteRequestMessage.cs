// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class FriendDeleteRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5603;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static FriendDeleteRequestMessage Empty =>
		new() { AccountId = 0 };

	public required int AccountId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(AccountId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AccountId = reader.ReadInt32();
	}
}
