// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Spells;

public sealed class SpellBomb : IDatacenterObject<SpellBomb>
{
	public static string ModuleName =>
		"SpellBombs";

	public required int Id { get; set; }

	public required int ChainReactionSpellId { get; set; }

	public required int ExplodSpellId { get; set; }

	public required int WallId { get; set; }

	public required int InstantSpellId { get; set; }

	public required int ComboCoeff { get; set; }

	public static SpellBomb Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new SpellBomb
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			ChainReactionSpellId = d2OClass.Fields[1].AsInt(reader),
			ExplodSpellId = d2OClass.Fields[2].AsInt(reader),
			WallId = d2OClass.Fields[3].AsInt(reader),
			InstantSpellId = d2OClass.Fields[4].AsInt(reader),
			ComboCoeff = d2OClass.Fields[5].AsInt(reader),
		};
	}
}
