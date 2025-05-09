// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Flash.Geom;

namespace Krosmoz.Protocol.Datacenter.Quest.Objectives;

public sealed class QuestObjectiveDiscoverSubArea : QuestObjective
{
	public override void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		MapId = d2OClass.Fields[1].AsInt(reader);
		Coords = d2OClass.Fields[2].AsObject<Point>(reader);
		Parameters = d2OClass.Fields[3].AsList(reader, static (field, r) => field.AsUInt(r));
		StepId = d2OClass.Fields[4].AsInt(reader);
		DialogId = d2OClass.Fields[5].AsInt(reader);
		TypeId = d2OClass.Fields[6].AsInt(reader);
	}
}
