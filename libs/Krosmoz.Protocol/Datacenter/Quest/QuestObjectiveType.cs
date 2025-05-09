// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Quest;

public sealed class QuestObjectiveType : IDatacenterObject<QuestObjectiveType>
{
	public static string ModuleName =>
		"QuestObjectiveTypes";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public static QuestObjectiveType Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new QuestObjectiveType
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
		};
	}
}
