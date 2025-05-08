// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Quest;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Npc;

public sealed class MapNpcsQuestStatusUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5642;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MapNpcsQuestStatusUpdateMessage Empty =>
		new() { MapId = 0, NpcsIdsWithQuest = [], QuestFlags = [], NpcsIdsWithoutQuest = [] };

	public required int MapId { get; set; }

	public required IEnumerable<int> NpcsIdsWithQuest { get; set; }

	public required IEnumerable<GameRolePlayNpcQuestFlag> QuestFlags { get; set; }

	public required IEnumerable<int> NpcsIdsWithoutQuest { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(MapId);
		var npcsIdsWithQuestBefore = writer.Position;
		var npcsIdsWithQuestCount = 0;
		writer.WriteShort(0);
		foreach (var item in NpcsIdsWithQuest)
		{
			writer.WriteInt(item);
			npcsIdsWithQuestCount++;
		}
		var npcsIdsWithQuestAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, npcsIdsWithQuestBefore);
		writer.WriteShort((short)npcsIdsWithQuestCount);
		writer.Seek(SeekOrigin.Begin, npcsIdsWithQuestAfter);
		var questFlagsBefore = writer.Position;
		var questFlagsCount = 0;
		writer.WriteShort(0);
		foreach (var item in QuestFlags)
		{
			item.Serialize(writer);
			questFlagsCount++;
		}
		var questFlagsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, questFlagsBefore);
		writer.WriteShort((short)questFlagsCount);
		writer.Seek(SeekOrigin.Begin, questFlagsAfter);
		var npcsIdsWithoutQuestBefore = writer.Position;
		var npcsIdsWithoutQuestCount = 0;
		writer.WriteShort(0);
		foreach (var item in NpcsIdsWithoutQuest)
		{
			writer.WriteInt(item);
			npcsIdsWithoutQuestCount++;
		}
		var npcsIdsWithoutQuestAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, npcsIdsWithoutQuestBefore);
		writer.WriteShort((short)npcsIdsWithoutQuestCount);
		writer.Seek(SeekOrigin.Begin, npcsIdsWithoutQuestAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MapId = reader.ReadInt();
		var npcsIdsWithQuestCount = reader.ReadShort();
		var npcsIdsWithQuest = new int[npcsIdsWithQuestCount];
		for (var i = 0; i < npcsIdsWithQuestCount; i++)
		{
			npcsIdsWithQuest[i] = reader.ReadInt();
		}
		NpcsIdsWithQuest = npcsIdsWithQuest;
		var questFlagsCount = reader.ReadShort();
		var questFlags = new GameRolePlayNpcQuestFlag[questFlagsCount];
		for (var i = 0; i < questFlagsCount; i++)
		{
			var entry = GameRolePlayNpcQuestFlag.Empty;
			entry.Deserialize(reader);
			questFlags[i] = entry;
		}
		QuestFlags = questFlags;
		var npcsIdsWithoutQuestCount = reader.ReadShort();
		var npcsIdsWithoutQuest = new int[npcsIdsWithoutQuestCount];
		for (var i = 0; i < npcsIdsWithoutQuestCount; i++)
		{
			npcsIdsWithoutQuest[i] = reader.ReadInt();
		}
		NpcsIdsWithoutQuest = npcsIdsWithoutQuest;
	}
}
