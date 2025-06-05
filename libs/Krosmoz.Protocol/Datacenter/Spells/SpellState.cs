// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Spells;

public sealed class SpellState : IDatacenterObject
{
	public static string ModuleName =>
		"SpellStates";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required bool PreventsSpellCast { get; set; }

	public required bool PreventsFight { get; set; }

	public required bool IsSilent { get; set; }

	public required List<int> EffectsIds { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		PreventsSpellCast = d2OClass.ReadFieldAsBoolean(reader);
		PreventsFight = d2OClass.ReadFieldAsBoolean(reader);
		IsSilent = d2OClass.ReadFieldAsBoolean(reader);
		EffectsIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsInt(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsBoolean(writer, PreventsSpellCast);
		d2OClass.WriteFieldAsBoolean(writer, PreventsFight);
		d2OClass.WriteFieldAsBoolean(writer, IsSilent);
		d2OClass.WriteFieldAsList(writer, EffectsIds, static (c, r, v) => c.WriteFieldAsInt(r, v));
	}
}
