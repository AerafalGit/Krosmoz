// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Quest;

public sealed class AchievementCategory : IDatacenterObject<AchievementCategory>
{
	public static string ModuleName =>
		"AchievementCategories";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int ParentId { get; set; }

	public required string Icon { get; set; }

	public required int Order { get; set; }

	public required string Color { get; set; }

	public required List<uint> AchievementIds { get; set; }

	public static AchievementCategory Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new AchievementCategory
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			ParentId = d2OClass.Fields[2].AsInt(reader),
			Icon = d2OClass.Fields[3].AsString(reader),
			Order = d2OClass.Fields[4].AsInt(reader),
			Color = d2OClass.Fields[5].AsString(reader),
			AchievementIds = d2OClass.Fields[6].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
		};
	}
}
