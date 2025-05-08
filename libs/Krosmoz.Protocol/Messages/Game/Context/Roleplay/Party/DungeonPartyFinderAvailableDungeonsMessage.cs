// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class DungeonPartyFinderAvailableDungeonsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6242;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DungeonPartyFinderAvailableDungeonsMessage Empty =>
		new() { DungeonIds = [] };

	public required IEnumerable<short> DungeonIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var dungeonIdsBefore = writer.Position;
		var dungeonIdsCount = 0;
		writer.WriteShort(0);
		foreach (var item in DungeonIds)
		{
			writer.WriteShort(item);
			dungeonIdsCount++;
		}
		var dungeonIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, dungeonIdsBefore);
		writer.WriteShort((short)dungeonIdsCount);
		writer.Seek(SeekOrigin.Begin, dungeonIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var dungeonIdsCount = reader.ReadShort();
		var dungeonIds = new short[dungeonIdsCount];
		for (var i = 0; i < dungeonIdsCount; i++)
		{
			dungeonIds[i] = reader.ReadShort();
		}
		DungeonIds = dungeonIds;
	}
}
