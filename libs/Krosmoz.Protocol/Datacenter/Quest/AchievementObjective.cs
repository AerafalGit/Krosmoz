// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Quest;

public sealed class AchievementObjective : IDatacenterObject
{
	public static string ModuleName =>
		"AchievementObjectives";

	public required int Id { get; set; }

	public required int AchievementId { get; set; }

	public required int Order { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required string Criterion { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		AchievementId = d2OClass.ReadFieldAsInt(reader);
		Order = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		Criterion = d2OClass.ReadFieldAsString(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, AchievementId);
		d2OClass.WriteFieldAsInt(writer, Order);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsString(writer, Criterion);
	}
}
