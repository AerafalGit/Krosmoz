// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Dungeon;

public sealed class DungeonKeyRingUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6296;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DungeonKeyRingUpdateMessage Empty =>
		new() { DungeonId = 0, Available = false };

	public required ushort DungeonId { get; set; }

	public required bool Available { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(DungeonId);
		writer.WriteBoolean(Available);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		DungeonId = reader.ReadVarUInt16();
		Available = reader.ReadBoolean();
	}
}
