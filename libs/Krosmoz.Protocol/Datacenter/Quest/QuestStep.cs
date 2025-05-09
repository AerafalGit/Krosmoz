// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Quest;

public sealed class QuestStep : IDatacenterObject<QuestStep>
{
	public static string ModuleName =>
		"QuestSteps";

	public required int Id { get; set; }

	public required int QuestId { get; set; }

	public required int NameId { get; set; }

	public required int DescriptionId { get; set; }

	public required int DialogId { get; set; }

	public required int OptimalLevel { get; set; }

	public required double Duration { get; set; }

	public required bool KamasScaleWithPlayerLevel { get; set; }

	public required double KamasRatio { get; set; }

	public required double XpRatio { get; set; }

	public required List<uint> ObjectiveIds { get; set; }

	public required List<uint> RewardsIds { get; set; }

	public static QuestStep Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new QuestStep
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			QuestId = d2OClass.Fields[1].AsInt(reader),
			NameId = d2OClass.Fields[2].AsI18N(reader),
			DescriptionId = d2OClass.Fields[3].AsI18N(reader),
			DialogId = d2OClass.Fields[4].AsInt(reader),
			OptimalLevel = d2OClass.Fields[5].AsInt(reader),
			Duration = d2OClass.Fields[6].AsDouble(reader),
			KamasScaleWithPlayerLevel = d2OClass.Fields[7].AsBoolean(reader),
			KamasRatio = d2OClass.Fields[8].AsDouble(reader),
			XpRatio = d2OClass.Fields[9].AsDouble(reader),
			ObjectiveIds = d2OClass.Fields[10].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			RewardsIds = d2OClass.Fields[11].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
		};
	}
}
