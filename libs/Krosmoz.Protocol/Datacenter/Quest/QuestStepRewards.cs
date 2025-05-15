// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Quest;

public sealed class QuestStepRewards : IDatacenterObject
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

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		StepId = d2OClass.ReadFieldAsInt(reader);
		LevelMin = d2OClass.ReadFieldAsInt(reader);
		LevelMax = d2OClass.ReadFieldAsInt(reader);
		ItemsReward = d2OClass.ReadFieldAsListOfList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		EmotesReward = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		JobsReward = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		SpellsReward = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
	}
}
