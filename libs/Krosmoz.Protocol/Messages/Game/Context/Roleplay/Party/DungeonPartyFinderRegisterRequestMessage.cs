// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class DungeonPartyFinderRegisterRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6249;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DungeonPartyFinderRegisterRequestMessage Empty =>
		new() { DungeonIds = [] };

	public required IEnumerable<ushort> DungeonIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var dungeonIdsBefore = writer.Position;
		var dungeonIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in DungeonIds)
		{
			writer.WriteVarUInt16(item);
			dungeonIdsCount++;
		}
		var dungeonIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, dungeonIdsBefore);
		writer.WriteInt16((short)dungeonIdsCount);
		writer.Seek(SeekOrigin.Begin, dungeonIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var dungeonIdsCount = reader.ReadInt16();
		var dungeonIds = new ushort[dungeonIdsCount];
		for (var i = 0; i < dungeonIdsCount; i++)
		{
			dungeonIds[i] = reader.ReadVarUInt16();
		}
		DungeonIds = dungeonIds;
	}
}
