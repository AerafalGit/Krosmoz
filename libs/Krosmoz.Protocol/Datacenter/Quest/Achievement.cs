// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Quest;

public sealed class Achievement : IDatacenterObject
{
	public static string ModuleName =>
		"Achievements";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int CategoryId { get; set; }

	public required int DescriptionId { get; set; }

	public required int IconId { get; set; }

	public required int Points { get; set; }

	public required int Level { get; set; }

	public required int Order { get; set; }

	public required double KamasRatio { get; set; }

	public required double ExperienceRatio { get; set; }

	public required bool KamasScaleWithPlayerLevel { get; set; }

	public required List<int> ObjectiveIds { get; set; }

	public required List<int> RewardIds { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		NameId = d2OClass.Fields[1].AsI18N(reader);
		CategoryId = d2OClass.Fields[2].AsInt(reader);
		DescriptionId = d2OClass.Fields[3].AsI18N(reader);
		IconId = d2OClass.Fields[4].AsInt(reader);
		Points = d2OClass.Fields[5].AsInt(reader);
		Level = d2OClass.Fields[6].AsInt(reader);
		Order = d2OClass.Fields[7].AsInt(reader);
		KamasRatio = d2OClass.Fields[8].AsDouble(reader);
		ExperienceRatio = d2OClass.Fields[9].AsDouble(reader);
		KamasScaleWithPlayerLevel = d2OClass.Fields[10].AsBoolean(reader);
		ObjectiveIds = d2OClass.Fields[11].AsList(reader, static (field, r) => field.AsInt(r));
		RewardIds = d2OClass.Fields[12].AsList(reader, static (field, r) => field.AsInt(r));
	}
}
