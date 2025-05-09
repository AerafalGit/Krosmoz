// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Spells;

public sealed class SpellState : IDatacenterObject<SpellState>
{
	public static string ModuleName =>
		"SpellStates";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required bool PreventsSpellCast { get; set; }

	public required bool PreventsFight { get; set; }

	public required bool Critical { get; set; }

	public static SpellState Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new SpellState
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			PreventsSpellCast = d2OClass.Fields[2].AsBoolean(reader),
			PreventsFight = d2OClass.Fields[3].AsBoolean(reader),
			Critical = d2OClass.Fields[4].AsBoolean(reader),
		};
	}
}
