// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Achievement;

public sealed class Achievement : DofusType
{
	public new const ushort StaticProtocolId = 363;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static Achievement Empty =>
		new() { Id = 0, FinishedObjective = [], StartedObjectives = [] };

	public required ushort Id { get; set; }

	public required IEnumerable<AchievementObjective> FinishedObjective { get; set; }

	public required IEnumerable<AchievementStartedObjective> StartedObjectives { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(Id);
		var finishedObjectiveBefore = writer.Position;
		var finishedObjectiveCount = 0;
		writer.WriteInt16(0);
		foreach (var item in FinishedObjective)
		{
			item.Serialize(writer);
			finishedObjectiveCount++;
		}
		var finishedObjectiveAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, finishedObjectiveBefore);
		writer.WriteInt16((short)finishedObjectiveCount);
		writer.Seek(SeekOrigin.Begin, finishedObjectiveAfter);
		var startedObjectivesBefore = writer.Position;
		var startedObjectivesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in StartedObjectives)
		{
			item.Serialize(writer);
			startedObjectivesCount++;
		}
		var startedObjectivesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, startedObjectivesBefore);
		writer.WriteInt16((short)startedObjectivesCount);
		writer.Seek(SeekOrigin.Begin, startedObjectivesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadVarUInt16();
		var finishedObjectiveCount = reader.ReadInt16();
		var finishedObjective = new AchievementObjective[finishedObjectiveCount];
		for (var i = 0; i < finishedObjectiveCount; i++)
		{
			var entry = AchievementObjective.Empty;
			entry.Deserialize(reader);
			finishedObjective[i] = entry;
		}
		FinishedObjective = finishedObjective;
		var startedObjectivesCount = reader.ReadInt16();
		var startedObjectives = new AchievementStartedObjective[startedObjectivesCount];
		for (var i = 0; i < startedObjectivesCount; i++)
		{
			var entry = AchievementStartedObjective.Empty;
			entry.Deserialize(reader);
			startedObjectives[i] = entry;
		}
		StartedObjectives = startedObjectives;
	}
}
