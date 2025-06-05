// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Effects;

namespace Krosmoz.Protocol.Datacenter.Items;

public sealed class Weapon : Item
{
	public required int Range { get; set; }

	public required int CriticalHitBonus { get; set; }

	public required int MinRange { get; set; }

	public required int MaxCastPerTurn { get; set; }

	public required bool CastTestLos { get; set; }

	public required int CriticalFailureProbability { get; set; }

	public required int CriticalHitProbability { get; set; }

	public required bool CastInDiagonal { get; set; }

    public required int ApCost { get; set; }

	public required bool CastInLine { get; set; }

	public override void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		FavoriteSubAreasBonus = d2OClass.ReadFieldAsInt(reader);
		CraftXpRatio = d2OClass.ReadFieldAsInt(reader);
		Range = d2OClass.ReadFieldAsInt(reader);
		BonusIsSecret = d2OClass.ReadFieldAsBoolean(reader);
		CriticalHitBonus = d2OClass.ReadFieldAsInt(reader);
		CriteriaTarget = d2OClass.ReadFieldAsString(reader);
		MinRange = d2OClass.ReadFieldAsInt(reader);
		MaxCastPerTurn = d2OClass.ReadFieldAsInt(reader);
		DescriptionId = d2OClass.ReadFieldAsI18N(reader);
		Description = d2OClass.ReadFieldAsI18NString(DescriptionId);
		RecipeIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		SecretRecipe = d2OClass.ReadFieldAsBoolean(reader);
		Etheral = d2OClass.ReadFieldAsBoolean(reader);
		AppearanceId = d2OClass.ReadFieldAsInt(reader);
		Id = d2OClass.ReadFieldAsInt(reader);
		DropMonsterIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		Cursed = d2OClass.ReadFieldAsBoolean(reader);
		Exchangeable = d2OClass.ReadFieldAsBoolean(reader);
		Level = d2OClass.ReadFieldAsInt(reader);
		RealWeight = d2OClass.ReadFieldAsInt(reader);
		CastTestLos = d2OClass.ReadFieldAsBoolean(reader);
		FavoriteSubAreas = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		CriticalFailureProbability = d2OClass.ReadFieldAsInt(reader);
		HideEffects = d2OClass.ReadFieldAsBoolean(reader);
		Criteria = d2OClass.ReadFieldAsString(reader);
		Targetable = d2OClass.ReadFieldAsBoolean(reader);
		CriticalHitProbability = d2OClass.ReadFieldAsInt(reader);
		TwoHanded = d2OClass.ReadFieldAsBoolean(reader);
		NonUsableOnAnother = d2OClass.ReadFieldAsBoolean(reader);
		ItemSetId = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		CastInDiagonal = d2OClass.ReadFieldAsBoolean(reader);
		Price = d2OClass.ReadFieldAsNumber(reader);
		Enhanceable = d2OClass.ReadFieldAsBoolean(reader);
		NeedUseConfirm = d2OClass.ReadFieldAsBoolean(reader);
		ApCost = d2OClass.ReadFieldAsInt(reader);
		Usable = d2OClass.ReadFieldAsBoolean(reader);
		CastInLine = d2OClass.ReadFieldAsBoolean(reader);
		PossibleEffects = d2OClass.ReadFieldAsList<EffectInstance>(reader, static (c, r) => c.ReadFieldAsObject<EffectInstance>(r));
		UseAnimationId = d2OClass.ReadFieldAsInt(reader);
		IconId = d2OClass.ReadFieldAsInt(reader);
		TypeId = d2OClass.ReadFieldAsInt(reader);
		RecipeSlots = d2OClass.ReadFieldAsInt(reader);
	}

	public override void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, FavoriteSubAreasBonus);
		d2OClass.WriteFieldAsInt(writer, CraftXpRatio);
		d2OClass.WriteFieldAsInt(writer, Range);
		d2OClass.WriteFieldAsBoolean(writer, BonusIsSecret);
		d2OClass.WriteFieldAsInt(writer, CriticalHitBonus);
		d2OClass.WriteFieldAsString(writer, CriteriaTarget);
		d2OClass.WriteFieldAsInt(writer, MinRange);
		d2OClass.WriteFieldAsInt(writer, MaxCastPerTurn);
		d2OClass.WriteFieldAsI18N(writer, DescriptionId);
		d2OClass.WriteFieldAsList(writer, RecipeIds, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsBoolean(writer, SecretRecipe);
		d2OClass.WriteFieldAsBoolean(writer, Etheral);
		d2OClass.WriteFieldAsInt(writer, AppearanceId);
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsList(writer, DropMonsterIds, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsBoolean(writer, Cursed);
		d2OClass.WriteFieldAsBoolean(writer, Exchangeable);
		d2OClass.WriteFieldAsInt(writer, Level);
		d2OClass.WriteFieldAsInt(writer, RealWeight);
		d2OClass.WriteFieldAsBoolean(writer, CastTestLos);
		d2OClass.WriteFieldAsList(writer, FavoriteSubAreas, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsInt(writer, CriticalFailureProbability);
		d2OClass.WriteFieldAsBoolean(writer, HideEffects);
		d2OClass.WriteFieldAsString(writer, Criteria);
		d2OClass.WriteFieldAsBoolean(writer, Targetable);
		d2OClass.WriteFieldAsInt(writer, CriticalHitProbability);
		d2OClass.WriteFieldAsBoolean(writer, TwoHanded);
		d2OClass.WriteFieldAsBoolean(writer, NonUsableOnAnother);
		d2OClass.WriteFieldAsInt(writer, ItemSetId);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsBoolean(writer, CastInDiagonal);
		d2OClass.WriteFieldAsNumber(writer, Price);
		d2OClass.WriteFieldAsBoolean(writer, Enhanceable);
		d2OClass.WriteFieldAsBoolean(writer, NeedUseConfirm);
		d2OClass.WriteFieldAsInt(writer, ApCost);
		d2OClass.WriteFieldAsBoolean(writer, Usable);
		d2OClass.WriteFieldAsBoolean(writer, CastInLine);
		d2OClass.WriteFieldAsList(writer, PossibleEffects, static (c, r, v) => c.WriteFieldAsObject(r, v));
		d2OClass.WriteFieldAsInt(writer, UseAnimationId);
		d2OClass.WriteFieldAsInt(writer, IconId);
		d2OClass.WriteFieldAsInt(writer, TypeId);
		d2OClass.WriteFieldAsInt(writer, RecipeSlots);
	}
}
