// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Spells;

public sealed class Spell : IDatacenterObject
{
	public static string ModuleName =>
		"Spells";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required int DescriptionId { get; set; }

	public required string Description { get; set; }

	public required int TypeId { get; set; }

	public required string ScriptParams { get; set; }

	public required string ScriptParamsCritical { get; set; }

	public required int ScriptId { get; set; }

	public required int ScriptIdCritical { get; set; }

	public required int IconId { get; set; }

	public required List<uint> SpellLevels { get; set; }

	public required bool VerboseCast { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		DescriptionId = d2OClass.ReadFieldAsI18N(reader);
		Description = d2OClass.ReadFieldAsI18NString(DescriptionId);
		TypeId = d2OClass.ReadFieldAsInt(reader);
		ScriptParams = d2OClass.ReadFieldAsString(reader);
		ScriptParamsCritical = d2OClass.ReadFieldAsString(reader);
		ScriptId = d2OClass.ReadFieldAsInt(reader);
		ScriptIdCritical = d2OClass.ReadFieldAsInt(reader);
		IconId = d2OClass.ReadFieldAsInt(reader);
		SpellLevels = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		VerboseCast = d2OClass.ReadFieldAsBoolean(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsI18N(writer, DescriptionId);
		d2OClass.WriteFieldAsInt(writer, TypeId);
		d2OClass.WriteFieldAsString(writer, ScriptParams);
		d2OClass.WriteFieldAsString(writer, ScriptParamsCritical);
		d2OClass.WriteFieldAsInt(writer, ScriptId);
		d2OClass.WriteFieldAsInt(writer, ScriptIdCritical);
		d2OClass.WriteFieldAsInt(writer, IconId);
		d2OClass.WriteFieldAsList(writer, SpellLevels, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsBoolean(writer, VerboseCast);
	}
}
