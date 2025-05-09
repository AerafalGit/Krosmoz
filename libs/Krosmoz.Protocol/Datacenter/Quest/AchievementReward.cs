// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Quest;

public sealed class AchievementReward : IDatacenterObject
{
	public static string ModuleName =>
		"AchievementRewards";

	public required int Id { get; set; }

	public required int AchievementId { get; set; }

	public required int LevelMin { get; set; }

	public required int LevelMax { get; set; }

	public required List<uint> ItemsReward { get; set; }

	public required List<uint> ItemsQuantityReward { get; set; }

	public required List<uint> EmotesReward { get; set; }

	public required List<uint> SpellsReward { get; set; }

	public required List<uint> TitlesReward { get; set; }

	public required List<uint> OrnamentsReward { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		AchievementId = d2OClass.Fields[1].AsInt(reader);
		LevelMin = d2OClass.Fields[2].AsInt(reader);
		LevelMax = d2OClass.Fields[3].AsInt(reader);
		ItemsReward = d2OClass.Fields[4].AsList(reader, static (field, r) => field.AsUInt(r));
		ItemsQuantityReward = d2OClass.Fields[5].AsList(reader, static (field, r) => field.AsUInt(r));
		EmotesReward = d2OClass.Fields[6].AsList(reader, static (field, r) => field.AsUInt(r));
		SpellsReward = d2OClass.Fields[7].AsList(reader, static (field, r) => field.AsUInt(r));
		TitlesReward = d2OClass.Fields[8].AsList(reader, static (field, r) => field.AsUInt(r));
		OrnamentsReward = d2OClass.Fields[9].AsList(reader, static (field, r) => field.AsUInt(r));
	}
}
