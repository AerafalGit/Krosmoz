// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Quest;

public sealed class QuestCategory : IDatacenterObject<QuestCategory>
{
	public static string ModuleName =>
		"QuestCategory";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int Order { get; set; }

	public required List<uint> QuestIds { get; set; }

	public static QuestCategory Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new QuestCategory
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			Order = d2OClass.Fields[2].AsInt(reader),
			QuestIds = d2OClass.Fields[3].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
		};
	}
}
