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

	public required short StepId { get; set; }

	public required IEnumerable<QuestObjectiveInformations> Objectives { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(StepId);
		var objectivesBefore = writer.Position;
		var objectivesCount = 0;
		writer.WriteShort(0);
		foreach (var item in Objectives)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			objectivesCount++;
		}
		var objectivesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectivesBefore);
		writer.WriteShort((short)objectivesCount);
		writer.Seek(SeekOrigin.Begin, objectivesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		StepId = reader.ReadShort();
		var objectivesCount = reader.ReadShort();
		var objectives = new QuestObjectiveInformations[objectivesCount];
		for (var i = 0; i < objectivesCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<QuestObjectiveInformations>(reader.ReadUShort());
			entry.Deserialize(reader);
			objectives[i] = entry;
		}
		Objectives = objectives;
	}
}
