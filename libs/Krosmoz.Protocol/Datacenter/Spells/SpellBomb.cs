// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Spells;

public sealed class SpellBomb : IDatacenterObject
{
	public static string ModuleName =>
		"SpellBombs";

	public required int Id { get; set; }

	public required int ChainReactionSpellId { get; set; }

	public required int ExplodSpellId { get; set; }

	public required int WallId { get; set; }

	public required int InstantSpellId { get; set; }

	public required int ComboCoeff { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		ChainReactionSpellId = d2OClass.ReadFieldAsInt(reader);
		ExplodSpellId = d2OClass.ReadFieldAsInt(reader);
		WallId = d2OClass.ReadFieldAsInt(reader);
		InstantSpellId = d2OClass.ReadFieldAsInt(reader);
		ComboCoeff = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, ChainReactionSpellId);
		d2OClass.WriteFieldAsInt(writer, ExplodSpellId);
		d2OClass.WriteFieldAsInt(writer, WallId);
		d2OClass.WriteFieldAsInt(writer, InstantSpellId);
		d2OClass.WriteFieldAsInt(writer, ComboCoeff);
	}
}
