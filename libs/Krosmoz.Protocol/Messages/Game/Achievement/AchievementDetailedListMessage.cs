// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Achievement;

public sealed class AchievementDetailedListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6358;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AchievementDetailedListMessage Empty =>
		new() { StartedAchievements = [], FinishedAchievements = [] };

	public required IEnumerable<Types.Game.Achievement.Achievement> StartedAchievements { get; set; }

	public required IEnumerable<Types.Game.Achievement.Achievement> FinishedAchievements { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var startedAchievementsBefore = writer.Position;
		var startedAchievementsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in StartedAchievements)
		{
			item.Serialize(writer);
			startedAchievementsCount++;
		}
		var startedAchievementsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, startedAchievementsBefore);
		writer.WriteInt16((short)startedAchievementsCount);
		writer.Seek(SeekOrigin.Begin, startedAchievementsAfter);
		var finishedAchievementsBefore = writer.Position;
		var finishedAchievementsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in FinishedAchievements)
		{
			item.Serialize(writer);
			finishedAchievementsCount++;
		}
		var finishedAchievementsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, finishedAchievementsBefore);
		writer.WriteInt16((short)finishedAchievementsCount);
		writer.Seek(SeekOrigin.Begin, finishedAchievementsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var startedAchievementsCount = reader.ReadInt16();
		var startedAchievements = new Types.Game.Achievement.Achievement[startedAchievementsCount];
		for (var i = 0; i < startedAchievementsCount; i++)
		{
			var entry = Types.Game.Achievement.Achievement.Empty;
			entry.Deserialize(reader);
			startedAchievements[i] = entry;
		}
		StartedAchievements = startedAchievements;
		var finishedAchievementsCount = reader.ReadInt16();
		var finishedAchievements = new Types.Game.Achievement.Achievement[finishedAchievementsCount];
		for (var i = 0; i < finishedAchievementsCount; i++)
		{
			var entry = Types.Game.Achievement.Achievement.Empty;
			entry.Deserialize(reader);
			finishedAchievements[i] = entry;
		}
		FinishedAchievements = finishedAchievements;
	}
}
