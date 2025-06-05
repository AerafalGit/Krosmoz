// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Npc;

public sealed class NpcDialogReplyMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5616;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static NpcDialogReplyMessage Empty =>
		new() { ReplyId = 0 };

	public required ushort ReplyId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ReplyId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ReplyId = reader.ReadVarUInt16();
	}
}
