// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Achievement;

namespace Krosmoz.Protocol.Messages.Game.Achievement;

public sealed class AchievementListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6205;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AchievementListMessage Empty =>
		new() { FinishedAchievementsIds = [], RewardableAchievements = [] };

	public required IEnumerable<ushort> FinishedAchievementsIds { get; set; }

	public required IEnumerable<AchievementRewardable> RewardableAchievements { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var finishedAchievementsIdsBefore = writer.Position;
		var finishedAchievementsIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in FinishedAchievementsIds)
		{
			writer.WriteVarUInt16(item);
			finishedAchievementsIdsCount++;
		}
		var finishedAchievementsIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, finishedAchievementsIdsBefore);
		writer.WriteInt16((short)finishedAchievementsIdsCount);
		writer.Seek(SeekOrigin.Begin, finishedAchievementsIdsAfter);
		var rewardableAchievementsBefore = writer.Position;
		var rewardableAchievementsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in RewardableAchievements)
		{
			item.Serialize(writer);
			rewardableAchievementsCount++;
		}
		var rewardableAchievementsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, rewardableAchievementsBefore);
		writer.WriteInt16((short)rewardableAchievementsCount);
		writer.Seek(SeekOrigin.Begin, rewardableAchievementsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var finishedAchievementsIdsCount = reader.ReadInt16();
		var finishedAchievementsIds = new ushort[finishedAchievementsIdsCount];
		for (var i = 0; i < finishedAchievementsIdsCount; i++)
		{
			finishedAchievementsIds[i] = reader.ReadVarUInt16();
		}
		FinishedAchievementsIds = finishedAchievementsIds;
		var rewardableAchievementsCount = reader.ReadInt16();
		var rewardableAchievements = new AchievementRewardable[rewardableAchievementsCount];
		for (var i = 0; i < rewardableAchievementsCount; i++)
		{
			var entry = AchievementRewardable.Empty;
			entry.Deserialize(reader);
			rewardableAchievements[i] = entry;
		}
		RewardableAchievements = rewardableAchievements;
	}
}
