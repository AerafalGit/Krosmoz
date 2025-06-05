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
		new() { FinishedQuestsIds = [], FinishedQuestsCounts = [], ActiveQuests = [], ReinitDoneQuestsIds = [] };

	public required IEnumerable<ushort> FinishedQuestsIds { get; set; }

	public required IEnumerable<ushort> FinishedQuestsCounts { get; set; }

	public required IEnumerable<QuestActiveInformations> ActiveQuests { get; set; }

	public required IEnumerable<ushort> ReinitDoneQuestsIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var finishedQuestsIdsBefore = writer.Position;
		var finishedQuestsIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in FinishedQuestsIds)
		{
			writer.WriteVarUInt16(item);
			finishedQuestsIdsCount++;
		}
		var finishedQuestsIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, finishedQuestsIdsBefore);
		writer.WriteInt16((short)finishedQuestsIdsCount);
		writer.Seek(SeekOrigin.Begin, finishedQuestsIdsAfter);
		var finishedQuestsCountsBefore = writer.Position;
		var finishedQuestsCountsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in FinishedQuestsCounts)
		{
			writer.WriteVarUInt16(item);
			finishedQuestsCountsCount++;
		}
		var finishedQuestsCountsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, finishedQuestsCountsBefore);
		writer.WriteInt16((short)finishedQuestsCountsCount);
		writer.Seek(SeekOrigin.Begin, finishedQuestsCountsAfter);
		var activeQuestsBefore = writer.Position;
		var activeQuestsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ActiveQuests)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			activeQuestsCount++;
		}
		var activeQuestsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, activeQuestsBefore);
		writer.WriteInt16((short)activeQuestsCount);
		writer.Seek(SeekOrigin.Begin, activeQuestsAfter);
		var reinitDoneQuestsIdsBefore = writer.Position;
		var reinitDoneQuestsIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ReinitDoneQuestsIds)
		{
			writer.WriteVarUInt16(item);
			reinitDoneQuestsIdsCount++;
		}
		var reinitDoneQuestsIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, reinitDoneQuestsIdsBefore);
		writer.WriteInt16((short)reinitDoneQuestsIdsCount);
		writer.Seek(SeekOrigin.Begin, reinitDoneQuestsIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var finishedQuestsIdsCount = reader.ReadInt16();
		var finishedQuestsIds = new ushort[finishedQuestsIdsCount];
		for (var i = 0; i < finishedQuestsIdsCount; i++)
		{
			finishedQuestsIds[i] = reader.ReadVarUInt16();
		}
		FinishedQuestsIds = finishedQuestsIds;
		var finishedQuestsCountsCount = reader.ReadInt16();
		var finishedQuestsCounts = new ushort[finishedQuestsCountsCount];
		for (var i = 0; i < finishedQuestsCountsCount; i++)
		{
			finishedQuestsCounts[i] = reader.ReadVarUInt16();
		}
		FinishedQuestsCounts = finishedQuestsCounts;
		var activeQuestsCount = reader.ReadInt16();
		var activeQuests = new QuestActiveInformations[activeQuestsCount];
		for (var i = 0; i < activeQuestsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<QuestActiveInformations>(reader.ReadUInt16());
			entry.Deserialize(reader);
			activeQuests[i] = entry;
		}
		ActiveQuests = activeQuests;
		var reinitDoneQuestsIdsCount = reader.ReadInt16();
		var reinitDoneQuestsIds = new ushort[reinitDoneQuestsIdsCount];
		for (var i = 0; i < reinitDoneQuestsIdsCount; i++)
		{
			reinitDoneQuestsIds[i] = reader.ReadVarUInt16();
		}
		ReinitDoneQuestsIds = reinitDoneQuestsIds;
	}
}
