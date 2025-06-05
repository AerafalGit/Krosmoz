// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Effects.Instances;

namespace Krosmoz.Protocol.Datacenter.Spells;

public sealed class SpellLevel : IDatacenterObject
{
	public static string ModuleName =>
		"SpellLevels";

	public required int Id { get; set; }

	public required int SpellId { get; set; }

	public required int Grade { get; set; }

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

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		SpellId = d2OClass.ReadFieldAsInt(reader);
		Grade = d2OClass.ReadFieldAsInt(reader);
		SpellBreed = d2OClass.ReadFieldAsInt(reader);
		ApCost = d2OClass.ReadFieldAsInt(reader);
		MinRange = d2OClass.ReadFieldAsInt(reader);
		Range = d2OClass.ReadFieldAsInt(reader);
		CastInLine = d2OClass.ReadFieldAsBoolean(reader);
		CastInDiagonal = d2OClass.ReadFieldAsBoolean(reader);
		CastTestLos = d2OClass.ReadFieldAsBoolean(reader);
		CriticalHitProbability = d2OClass.ReadFieldAsInt(reader);
		CriticalFailureProbability = d2OClass.ReadFieldAsInt(reader);
		NeedFreeCell = d2OClass.ReadFieldAsBoolean(reader);
		NeedTakenCell = d2OClass.ReadFieldAsBoolean(reader);
		NeedFreeTrapCell = d2OClass.ReadFieldAsBoolean(reader);
		RangeCanBeBoosted = d2OClass.ReadFieldAsBoolean(reader);
		MaxStack = d2OClass.ReadFieldAsInt(reader);
		MaxCastPerTurn = d2OClass.ReadFieldAsInt(reader);
		MaxCastPerTarget = d2OClass.ReadFieldAsInt(reader);
		MinCastInterval = d2OClass.ReadFieldAsInt(reader);
		InitialCooldown = d2OClass.ReadFieldAsInt(reader);
		GlobalCooldown = d2OClass.ReadFieldAsInt(reader);
		MinPlayerLevel = d2OClass.ReadFieldAsInt(reader);
		CriticalFailureEndsTurn = d2OClass.ReadFieldAsBoolean(reader);
		HideEffects = d2OClass.ReadFieldAsBoolean(reader);
		Hidden = d2OClass.ReadFieldAsBoolean(reader);
		StatesRequired = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsInt(r));
		StatesForbidden = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsInt(r));
		Effects = d2OClass.ReadFieldAsList<EffectInstanceDice>(reader, static (c, r) => c.ReadFieldAsObject<EffectInstanceDice>(r));
		CriticalEffect = d2OClass.ReadFieldAsList<EffectInstanceDice>(reader, static (c, r) => c.ReadFieldAsObject<EffectInstanceDice>(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, SpellId);
		d2OClass.WriteFieldAsInt(writer, Grade);
		d2OClass.WriteFieldAsInt(writer, SpellBreed);
		d2OClass.WriteFieldAsInt(writer, ApCost);
		d2OClass.WriteFieldAsInt(writer, MinRange);
		d2OClass.WriteFieldAsInt(writer, Range);
		d2OClass.WriteFieldAsBoolean(writer, CastInLine);
		d2OClass.WriteFieldAsBoolean(writer, CastInDiagonal);
		d2OClass.WriteFieldAsBoolean(writer, CastTestLos);
		d2OClass.WriteFieldAsInt(writer, CriticalHitProbability);
		d2OClass.WriteFieldAsInt(writer, CriticalFailureProbability);
		d2OClass.WriteFieldAsBoolean(writer, NeedFreeCell);
		d2OClass.WriteFieldAsBoolean(writer, NeedTakenCell);
		d2OClass.WriteFieldAsBoolean(writer, NeedFreeTrapCell);
		d2OClass.WriteFieldAsBoolean(writer, RangeCanBeBoosted);
		d2OClass.WriteFieldAsInt(writer, MaxStack);
		d2OClass.WriteFieldAsInt(writer, MaxCastPerTurn);
		d2OClass.WriteFieldAsInt(writer, MaxCastPerTarget);
		d2OClass.WriteFieldAsInt(writer, MinCastInterval);
		d2OClass.WriteFieldAsInt(writer, InitialCooldown);
		d2OClass.WriteFieldAsInt(writer, GlobalCooldown);
		d2OClass.WriteFieldAsInt(writer, MinPlayerLevel);
		d2OClass.WriteFieldAsBoolean(writer, CriticalFailureEndsTurn);
		d2OClass.WriteFieldAsBoolean(writer, HideEffects);
		d2OClass.WriteFieldAsBoolean(writer, Hidden);
		d2OClass.WriteFieldAsList(writer, StatesRequired, static (c, r, v) => c.WriteFieldAsInt(r, v));
		d2OClass.WriteFieldAsList(writer, StatesForbidden, static (c, r, v) => c.WriteFieldAsInt(r, v));
		d2OClass.WriteFieldAsList(writer, Effects, static (c, r, v) => c.WriteFieldAsObject(r, v));
		d2OClass.WriteFieldAsList(writer, CriticalEffect, static (c, r, v) => c.WriteFieldAsObject(r, v));
	}
}
