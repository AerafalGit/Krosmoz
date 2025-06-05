// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Chat.Smiley;

public sealed class LocalizedChatSmileyMessage : ChatSmileyMessage
{
	public new const uint StaticProtocolId = 6185;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new LocalizedChatSmileyMessage Empty =>
		new() { AccountId = 0, SmileyId = 0, EntityId = 0, CellId = 0 };

	public required ushort CellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(CellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CellId = reader.ReadVarUInt16();
	}
}
