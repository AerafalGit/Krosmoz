// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Npc;

public sealed class NpcGenericActionRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5898;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static NpcGenericActionRequestMessage Empty =>
		new() { NpcId = 0, NpcActionId = 0, NpcMapId = 0 };

	public required int NpcId { get; set; }

	public required sbyte NpcActionId { get; set; }

	public required int NpcMapId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(NpcId);
		writer.WriteInt8(NpcActionId);
		writer.WriteInt32(NpcMapId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		NpcId = reader.ReadInt32();
		NpcActionId = reader.ReadInt8();
		NpcMapId = reader.ReadInt32();
	}
}
