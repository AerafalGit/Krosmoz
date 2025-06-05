// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Chat.Smiley;

public class ChatSmileyMessage : DofusMessage
{
	public new const uint StaticProtocolId = 801;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ChatSmileyMessage Empty =>
		new() { EntityId = 0, SmileyId = 0, AccountId = 0 };

	public required int EntityId { get; set; }

	public required sbyte SmileyId { get; set; }

	public required int AccountId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(EntityId);
		writer.WriteInt8(SmileyId);
		writer.WriteInt32(AccountId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		EntityId = reader.ReadInt32();
		SmileyId = reader.ReadInt8();
		AccountId = reader.ReadInt32();
	}
}
