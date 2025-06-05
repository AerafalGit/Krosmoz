// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Quest;

public sealed class QuestStep : IDatacenterObject
{
	public static string ModuleName =>
		"QuestSteps";

	public required int Id { get; set; }

	public required int QuestId { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required int DescriptionId { get; set; }

	public required string Description { get; set; }

	public required int DialogId { get; set; }

	public required int OptimalLevel { get; set; }

	public required double Duration { get; set; }

	public required bool KamasScaleWithPlayerLevel { get; set; }

	public required double KamasRatio { get; set; }

	public required double XpRatio { get; set; }

	public required List<uint> ObjectiveIds { get; set; }

	public required List<uint> RewardsIds { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		QuestId = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		DescriptionId = d2OClass.ReadFieldAsI18N(reader);
		Description = d2OClass.ReadFieldAsI18NString(DescriptionId);
		DialogId = d2OClass.ReadFieldAsInt(reader);
		OptimalLevel = d2OClass.ReadFieldAsInt(reader);
		Duration = d2OClass.ReadFieldAsNumber(reader);
		KamasScaleWithPlayerLevel = d2OClass.ReadFieldAsBoolean(reader);
		KamasRatio = d2OClass.ReadFieldAsNumber(reader);
		XpRatio = d2OClass.ReadFieldAsNumber(reader);
		ObjectiveIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		RewardsIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, QuestId);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsI18N(writer, DescriptionId);
		d2OClass.WriteFieldAsInt(writer, DialogId);
		d2OClass.WriteFieldAsInt(writer, OptimalLevel);
		d2OClass.WriteFieldAsNumber(writer, Duration);
		d2OClass.WriteFieldAsBoolean(writer, KamasScaleWithPlayerLevel);
		d2OClass.WriteFieldAsNumber(writer, KamasRatio);
		d2OClass.WriteFieldAsNumber(writer, XpRatio);
		d2OClass.WriteFieldAsList(writer, ObjectiveIds, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsList(writer, RewardsIds, static (c, r, v) => c.WriteFieldAsUInt(r, v));
	}
}
