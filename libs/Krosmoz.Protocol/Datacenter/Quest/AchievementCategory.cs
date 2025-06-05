// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Quest;

public sealed class AchievementCategory : IDatacenterObject
{
	public static string ModuleName =>
		"AchievementCategories";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required int ParentId { get; set; }

	public required string Icon { get; set; }

	public required int Order { get; set; }

	public required string Color { get; set; }

	public required List<uint> AchievementIds { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		ParentId = d2OClass.ReadFieldAsInt(reader);
		Icon = d2OClass.ReadFieldAsString(reader);
		Order = d2OClass.ReadFieldAsInt(reader);
		Color = d2OClass.ReadFieldAsString(reader);
		AchievementIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsInt(writer, ParentId);
		d2OClass.WriteFieldAsString(writer, Icon);
		d2OClass.WriteFieldAsInt(writer, Order);
		d2OClass.WriteFieldAsString(writer, Color);
		d2OClass.WriteFieldAsList(writer, AchievementIds, static (c, r, v) => c.WriteFieldAsUInt(r, v));
	}
}
