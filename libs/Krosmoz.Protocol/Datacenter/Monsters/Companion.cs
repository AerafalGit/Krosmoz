// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Monsters;

public sealed class Companion : IDatacenterObject
{
	public static string ModuleName =>
		"Companions";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required string Look { get; set; }

	public required bool WebDisplay { get; set; }

	public required int DescriptionId { get; set; }

	public required string Description { get; set; }

	public required int StartingSpellLevelId { get; set; }

	public required int AssetId { get; set; }

	public required List<uint> Characteristics { get; set; }

	public required List<uint> Spells { get; set; }

	public required int CreatureBoneId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		Look = d2OClass.ReadFieldAsString(reader);
		WebDisplay = d2OClass.ReadFieldAsBoolean(reader);
		DescriptionId = d2OClass.ReadFieldAsI18N(reader);
		Description = d2OClass.ReadFieldAsI18NString(DescriptionId);
		StartingSpellLevelId = d2OClass.ReadFieldAsInt(reader);
		AssetId = d2OClass.ReadFieldAsInt(reader);
		Characteristics = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		Spells = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		CreatureBoneId = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsString(writer, Look);
		d2OClass.WriteFieldAsBoolean(writer, WebDisplay);
		d2OClass.WriteFieldAsI18N(writer, DescriptionId);
		d2OClass.WriteFieldAsInt(writer, StartingSpellLevelId);
		d2OClass.WriteFieldAsInt(writer, AssetId);
		d2OClass.WriteFieldAsList(writer, Characteristics, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsList(writer, Spells, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsInt(writer, CreatureBoneId);
	}
}
