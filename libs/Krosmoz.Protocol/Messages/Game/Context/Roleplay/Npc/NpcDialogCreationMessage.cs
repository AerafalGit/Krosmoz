// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Npc;

public sealed class NpcDialogCreationMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5618;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static NpcDialogCreationMessage Empty =>
		new() { MapId = 0, NpcId = 0 };

	public required int MapId { get; set; }

	public required int NpcId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(MapId);
		writer.WriteInt32(NpcId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MapId = reader.ReadInt32();
		NpcId = reader.ReadInt32();
	}
}
