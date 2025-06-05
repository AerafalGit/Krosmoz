// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Quest;

public sealed class QuestActiveDetailedInformations : QuestActiveInformations
{
	public new const ushort StaticProtocolId = 382;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new QuestActiveDetailedInformations Empty =>
		new() { QuestId = 0, StepId = 0, Objectives = [] };

	public required ushort StepId { get; set; }

	public required IEnumerable<QuestObjectiveInformations> Objectives { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(StepId);
		var objectivesBefore = writer.Position;
		var objectivesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Objectives)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			objectivesCount++;
		}
		var objectivesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectivesBefore);
		writer.WriteInt16((short)objectivesCount);
		writer.Seek(SeekOrigin.Begin, objectivesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		StepId = reader.ReadVarUInt16();
		var objectivesCount = reader.ReadInt16();
		var objectives = new QuestObjectiveInformations[objectivesCount];
		for (var i = 0; i < objectivesCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<QuestObjectiveInformations>(reader.ReadUInt16());
			entry.Deserialize(reader);
			objectives[i] = entry;
		}
		Objectives = objectives;
	}
}
