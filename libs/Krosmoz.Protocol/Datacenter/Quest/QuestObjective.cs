// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Flash.Geom;

namespace Krosmoz.Protocol.Datacenter.Quest;

public class QuestObjective : IDatacenterObject<QuestObjective>
{
	public static string ModuleName =>
		"QuestObjectives";

	public required int Id { get; set; }

	public required int StepId { get; set; }

	public required int TypeId { get; set; }

	public required int DialogId { get; set; }

	public required List<uint> Parameters { get; set; }

	public required Point Coords { get; set; }

	public required int MapId { get; set; }

	public static QuestObjective Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new QuestObjective
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			StepId = d2OClass.Fields[1].AsInt(reader),
			TypeId = d2OClass.Fields[2].AsInt(reader),
			DialogId = d2OClass.Fields[3].AsInt(reader),
			Parameters = d2OClass.Fields[4].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			Coords = d2OClass.Fields[5].AsObject<Point>(reader),
			MapId = d2OClass.Fields[6].AsInt(reader),
		};
	}
}
