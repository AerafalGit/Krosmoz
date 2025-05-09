// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Spells;

public sealed class Spell : IDatacenterObject<Spell>
{
	public static string ModuleName =>
		"Spells";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int DescriptionId { get; set; }

	public required int TypeId { get; set; }

	public required string ScriptParams { get; set; }

	public required string ScriptParamsCritical { get; set; }

	public required int ScriptId { get; set; }

	public required int ScriptIdCritical { get; set; }

	public required int IconId { get; set; }

	public required List<uint> SpellLevels { get; set; }

	public static Spell Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Spell
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			DescriptionId = d2OClass.Fields[2].AsI18N(reader),
			TypeId = d2OClass.Fields[3].AsInt(reader),
			ScriptParams = d2OClass.Fields[4].AsString(reader),
			ScriptParamsCritical = d2OClass.Fields[5].AsString(reader),
			ScriptId = d2OClass.Fields[6].AsInt(reader),
			ScriptIdCritical = d2OClass.Fields[7].AsInt(reader),
			IconId = d2OClass.Fields[8].AsInt(reader),
			SpellLevels = d2OClass.Fields[9].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
		};
	}
}
