// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Quest;

public sealed class Quest : IDatacenterObject<Quest>
{
	public static string ModuleName =>
		"Quests";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int CategoryId { get; set; }

	public required bool IsRepeatable { get; set; }

	public required int RepeatType { get; set; }

	public required int RepeatLimit { get; set; }

	public required bool IsDungeonQuest { get; set; }

	public required int LevelMin { get; set; }

	public required int LevelMax { get; set; }

	public required List<uint> StepIds { get; set; }

	public static Quest Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Quest
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			CategoryId = d2OClass.Fields[2].AsInt(reader),
			IsRepeatable = d2OClass.Fields[3].AsBoolean(reader),
			RepeatType = d2OClass.Fields[4].AsInt(reader),
			RepeatLimit = d2OClass.Fields[5].AsInt(reader),
			IsDungeonQuest = d2OClass.Fields[6].AsBoolean(reader),
			LevelMin = d2OClass.Fields[7].AsInt(reader),
			LevelMax = d2OClass.Fields[8].AsInt(reader),
			StepIds = d2OClass.Fields[9].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
		};
	}
}
