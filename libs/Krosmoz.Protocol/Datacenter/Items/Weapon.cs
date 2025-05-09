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
		FavoriteSubAreasBonus = d2OClass.Fields[0].AsInt(reader);
		Range = d2OClass.Fields[1].AsInt(reader);
		BonusIsSecret = d2OClass.Fields[2].AsBoolean(reader);
		CriticalHitBonus = d2OClass.Fields[3].AsInt(reader);
		CriteriaTarget = d2OClass.Fields[4].AsString(reader);
		MinRange = d2OClass.Fields[5].AsInt(reader);
		MaxCastPerTurn = d2OClass.Fields[6].AsInt(reader);
		DescriptionId = d2OClass.Fields[7].AsI18N(reader);
		RecipeIds = d2OClass.Fields[8].AsList(reader, static (field, r) => field.AsUInt(r));
		SecretRecipe = d2OClass.Fields[9].AsBoolean(reader);
		Etheral = d2OClass.Fields[10].AsBoolean(reader);
		AppearanceId = d2OClass.Fields[11].AsInt(reader);
		Id = d2OClass.Fields[12].AsInt(reader);
		DropMonsterIds = d2OClass.Fields[13].AsList(reader, static (field, r) => field.AsUInt(r));
		Cursed = d2OClass.Fields[14].AsBoolean(reader);
		Exchangeable = d2OClass.Fields[15].AsBoolean(reader);
		Level = d2OClass.Fields[16].AsInt(reader);
		RealWeight = d2OClass.Fields[17].AsInt(reader);
		CastTestLos = d2OClass.Fields[18].AsBoolean(reader);
		FavoriteSubAreas = d2OClass.Fields[19].AsList(reader, static (field, r) => field.AsUInt(r));
		CriticalFailureProbability = d2OClass.Fields[20].AsInt(reader);
		HideEffects = d2OClass.Fields[21].AsBoolean(reader);
		Criteria = d2OClass.Fields[22].AsString(reader);
		Targetable = d2OClass.Fields[23].AsBoolean(reader);
		CriticalHitProbability = d2OClass.Fields[24].AsInt(reader);
		TwoHanded = d2OClass.Fields[25].AsBoolean(reader);
		NonUsableOnAnother = d2OClass.Fields[26].AsBoolean(reader);
		ItemSetId = d2OClass.Fields[27].AsInt(reader);
		NameId = d2OClass.Fields[28].AsI18N(reader);
		CastInDiagonal = d2OClass.Fields[29].AsBoolean(reader);
		Price = d2OClass.Fields[30].AsDouble(reader);
		Enhanceable = d2OClass.Fields[31].AsBoolean(reader);
		ApCost = d2OClass.Fields[32].AsInt(reader);
		Usable = d2OClass.Fields[33].AsBoolean(reader);
		CastInLine = d2OClass.Fields[34].AsBoolean(reader);
		PossibleEffects = d2OClass.Fields[35].AsList<EffectInstance>(reader, static (field, r) => field.AsObject<EffectInstance>(r));
		UseAnimationId = d2OClass.Fields[36].AsInt(reader);
		IconId = d2OClass.Fields[37].AsInt(reader);
		TypeId = d2OClass.Fields[38].AsInt(reader);
		RecipeSlots = d2OClass.Fields[39].AsInt(reader);
	}
}
