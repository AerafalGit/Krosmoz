// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Quest.TreasureHunt;

public sealed class LegendaryTreasureHunt : IDatacenterObject
{
	public static string ModuleName =>
		"LegendaryTreasureHunts";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required int Level { get; set; }

	public required uint ChestId { get; set; }

	public required uint MonsterId { get; set; }

	public required uint MapItemId { get; set; }

	public required double XpRatio { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		Level = d2OClass.ReadFieldAsInt(reader);
		ChestId = d2OClass.ReadFieldAsUInt(reader);
		MonsterId = d2OClass.ReadFieldAsUInt(reader);
		MapItemId = d2OClass.ReadFieldAsUInt(reader);
		XpRatio = d2OClass.ReadFieldAsNumber(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsInt(writer, Level);
		d2OClass.WriteFieldAsUInt(writer, ChestId);
		d2OClass.WriteFieldAsUInt(writer, MonsterId);
		d2OClass.WriteFieldAsUInt(writer, MapItemId);
		d2OClass.WriteFieldAsNumber(writer, XpRatio);
	}
}
