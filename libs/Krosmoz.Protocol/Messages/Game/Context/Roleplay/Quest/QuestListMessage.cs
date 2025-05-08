// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Quest;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Quest;

public sealed class QuestListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5626;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static QuestListMessage Empty =>
		new() { FinishedQuestsIds = [], FinishedQuestsCounts = [], ActiveQuests = [] };

	public required IEnumerable<short> FinishedQuestsIds { get; set; }

	public required IEnumerable<short> FinishedQuestsCounts { get; set; }

	public required IEnumerable<QuestActiveInformations> ActiveQuests { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var finishedQuestsIdsBefore = writer.Position;
		var finishedQuestsIdsCount = 0;
		writer.WriteShort(0);
		foreach (var item in FinishedQuestsIds)
		{
			writer.WriteShort(item);
			finishedQuestsIdsCount++;
		}
		var finishedQuestsIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, finishedQuestsIdsBefore);
		writer.WriteShort((short)finishedQuestsIdsCount);
		writer.Seek(SeekOrigin.Begin, finishedQuestsIdsAfter);
		var finishedQuestsCountsBefore = writer.Position;
		var finishedQuestsCountsCount = 0;
		writer.WriteShort(0);
		foreach (var item in FinishedQuestsCounts)
		{
			writer.WriteShort(item);
			finishedQuestsCountsCount++;
		}
		var finishedQuestsCountsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, finishedQuestsCountsBefore);
		writer.WriteShort((short)finishedQuestsCountsCount);
		writer.Seek(SeekOrigin.Begin, finishedQuestsCountsAfter);
		var activeQuestsBefore = writer.Position;
		var activeQuestsCount = 0;
		writer.WriteShort(0);
		foreach (var item in ActiveQuests)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			activeQuestsCount++;
		}
		var activeQuestsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, activeQuestsBefore);
		writer.WriteShort((short)activeQuestsCount);
		writer.Seek(SeekOrigin.Begin, activeQuestsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var finishedQuestsIdsCount = reader.ReadShort();
		var finishedQuestsIds = new short[finishedQuestsIdsCount];
		for (var i = 0; i < finishedQuestsIdsCount; i++)
		{
			finishedQuestsIds[i] = reader.ReadShort();
		}
		FinishedQuestsIds = finishedQuestsIds;
		var finishedQuestsCountsCount = reader.ReadShort();
		var finishedQuestsCounts = new short[finishedQuestsCountsCount];
		for (var i = 0; i < finishedQuestsCountsCount; i++)
		{
			finishedQuestsCounts[i] = reader.ReadShort();
		}
		FinishedQuestsCounts = finishedQuestsCounts;
		var activeQuestsCount = reader.ReadShort();
		var activeQuests = new QuestActiveInformations[activeQuestsCount];
		for (var i = 0; i < activeQuestsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<QuestActiveInformations>(reader.ReadUShort());
			entry.Deserialize(reader);
			activeQuests[i] = entry;
		}
		ActiveQuests = activeQuests;
	}
}
