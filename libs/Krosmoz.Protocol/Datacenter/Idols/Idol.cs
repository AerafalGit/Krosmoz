// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Idols;

public sealed class Idol : IDatacenterObject
{
	public static string ModuleName =>
		"Idols";

	public required int Id { get; set; }

	public required string Description { get; set; }

	public required int CategoryId { get; set; }

	public required int ItemId { get; set; }

	public required bool GroupOnly { get; set; }

	public required int Score { get; set; }

	public required int ExperienceBonus { get; set; }

	public required int DropBonus { get; set; }

	public required int SpellPairId { get; set; }

	public required List<int> SynergyIdolsIds { get; set; }

	public required List<double> SynergyIdolsCoeff { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		Description = d2OClass.ReadFieldAsString(reader);
		CategoryId = d2OClass.ReadFieldAsInt(reader);
		ItemId = d2OClass.ReadFieldAsInt(reader);
		GroupOnly = d2OClass.ReadFieldAsBoolean(reader);
		Score = d2OClass.ReadFieldAsInt(reader);
		ExperienceBonus = d2OClass.ReadFieldAsInt(reader);
		DropBonus = d2OClass.ReadFieldAsInt(reader);
		SpellPairId = d2OClass.ReadFieldAsInt(reader);
		SynergyIdolsIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsInt(r));
		SynergyIdolsCoeff = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsNumber(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsString(writer, Description);
		d2OClass.WriteFieldAsInt(writer, CategoryId);
		d2OClass.WriteFieldAsInt(writer, ItemId);
		d2OClass.WriteFieldAsBoolean(writer, GroupOnly);
		d2OClass.WriteFieldAsInt(writer, Score);
		d2OClass.WriteFieldAsInt(writer, ExperienceBonus);
		d2OClass.WriteFieldAsInt(writer, DropBonus);
		d2OClass.WriteFieldAsInt(writer, SpellPairId);
		d2OClass.WriteFieldAsList(writer, SynergyIdolsIds, static (c, r, v) => c.WriteFieldAsInt(r, v));
		d2OClass.WriteFieldAsList(writer, SynergyIdolsCoeff, static (c, r, v) => c.WriteFieldAsNumber(r, v));
	}
}
