// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Effects.Instances;

namespace Krosmoz.Protocol.Datacenter.Spells;

public sealed class SpellLevel : IDatacenterObject<SpellLevel>
{
	public static string ModuleName =>
		"SpellLevels";

	public required int Id { get; set; }

	public required int SpellId { get; set; }

	public required int SpellBreed { get; set; }

	public required int ApCost { get; set; }

	public required int MinRange { get; set; }

	public required int Range { get; set; }

	public required bool CastInLine { get; set; }

	public required bool CastInDiagonal { get; set; }

	public required bool CastTestLos { get; set; }

	public required int CriticalHitProbability { get; set; }

	public required int CriticalFailureProbability { get; set; }

	public required bool NeedFreeCell { get; set; }

	public required bool NeedTakenCell { get; set; }

	public required bool NeedFreeTrapCell { get; set; }

	public required bool RangeCanBeBoosted { get; set; }

	public required int MaxStack { get; set; }

	public required int MaxCastPerTurn { get; set; }

	public required int MaxCastPerTarget { get; set; }

	public required int MinCastInterval { get; set; }

	public required int InitialCooldown { get; set; }

	public required int GlobalCooldown { get; set; }

	public required int MinPlayerLevel { get; set; }

	public required bool CriticalFailureEndsTurn { get; set; }

	public required bool HideEffects { get; set; }

	public required bool Hidden { get; set; }

	public required List<int> StatesRequired { get; set; }

	public required List<int> StatesForbidden { get; set; }

	public required List<EffectInstanceDice> Effects { get; set; }

	public required List<EffectInstanceDice> CriticalEffect { get; set; }

	public static SpellLevel Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new SpellLevel
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			SpellId = d2OClass.Fields[1].AsInt(reader),
			SpellBreed = d2OClass.Fields[2].AsInt(reader),
			ApCost = d2OClass.Fields[3].AsInt(reader),
			MinRange = d2OClass.Fields[4].AsInt(reader),
			Range = d2OClass.Fields[5].AsInt(reader),
			CastInLine = d2OClass.Fields[6].AsBoolean(reader),
			CastInDiagonal = d2OClass.Fields[7].AsBoolean(reader),
			CastTestLos = d2OClass.Fields[8].AsBoolean(reader),
			CriticalHitProbability = d2OClass.Fields[9].AsInt(reader),
			CriticalFailureProbability = d2OClass.Fields[10].AsInt(reader),
			NeedFreeCell = d2OClass.Fields[11].AsBoolean(reader),
			NeedTakenCell = d2OClass.Fields[12].AsBoolean(reader),
			NeedFreeTrapCell = d2OClass.Fields[13].AsBoolean(reader),
			RangeCanBeBoosted = d2OClass.Fields[14].AsBoolean(reader),
			MaxStack = d2OClass.Fields[15].AsInt(reader),
			MaxCastPerTurn = d2OClass.Fields[16].AsInt(reader),
			MaxCastPerTarget = d2OClass.Fields[17].AsInt(reader),
			MinCastInterval = d2OClass.Fields[18].AsInt(reader),
			InitialCooldown = d2OClass.Fields[19].AsInt(reader),
			GlobalCooldown = d2OClass.Fields[20].AsInt(reader),
			MinPlayerLevel = d2OClass.Fields[21].AsInt(reader),
			CriticalFailureEndsTurn = d2OClass.Fields[22].AsBoolean(reader),
			HideEffects = d2OClass.Fields[23].AsBoolean(reader),
			Hidden = d2OClass.Fields[24].AsBoolean(reader),
			StatesRequired = d2OClass.Fields[25].AsList<int>(reader, static (field, r) => field.AsInt(r)),
			StatesForbidden = d2OClass.Fields[26].AsList<int>(reader, static (field, r) => field.AsInt(r)),
			Effects = d2OClass.Fields[27].AsList<EffectInstanceDice>(reader, static (field, r) => field.AsObject<EffectInstanceDice>(r)),
			CriticalEffect = d2OClass.Fields[28].AsList<EffectInstanceDice>(reader, static (field, r) => field.AsObject<EffectInstanceDice>(r)),
		};
	}
}
