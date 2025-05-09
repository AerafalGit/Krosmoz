// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Quest;

public sealed class QuestStepRewards : IDatacenterObject<QuestStepRewards>
{
	public static string ModuleName =>
		"QuestStepRewards";

	public required int Id { get; set; }

	public required int StepId { get; set; }

	public required int LevelMin { get; set; }

	public required int LevelMax { get; set; }

	public required List<List<uint>> ItemsReward { get; set; }

	public required List<uint> EmotesReward { get; set; }

	public required List<uint> JobsReward { get; set; }

	public required List<uint> SpellsReward { get; set; }

	public static QuestStepRewards Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new QuestStepRewards
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			StepId = d2OClass.Fields[1].AsInt(reader),
			LevelMin = d2OClass.Fields[2].AsInt(reader),
			LevelMax = d2OClass.Fields[3].AsInt(reader),
			ItemsReward = d2OClass.Fields[4].AsListOfList<uint>(reader, static (field, r) => field.AsUInt(r)),
			EmotesReward = d2OClass.Fields[5].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			JobsReward = d2OClass.Fields[6].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			SpellsReward = d2OClass.Fields[7].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
		};
	}
}
