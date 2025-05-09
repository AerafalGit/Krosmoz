// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Effects;

namespace Krosmoz.Protocol.Datacenter.Items;

public class Item : IDatacenterObject<Item>
{
	public static string ModuleName =>
		"Items";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int TypeId { get; set; }

	public required int DescriptionId { get; set; }

	public required int IconId { get; set; }

	public required int Level { get; set; }

	public required int RealWeight { get; set; }

	public required bool Cursed { get; set; }

	public required int UseAnimationId { get; set; }

	public required bool Usable { get; set; }

	public required bool Targetable { get; set; }

	public required bool Exchangeable { get; set; }

	public required double Price { get; set; }

	public required bool TwoHanded { get; set; }

	public required bool Etheral { get; set; }

	public required int ItemSetId { get; set; }

	public required string Criteria { get; set; }

	public required string CriteriaTarget { get; set; }

	public required bool HideEffects { get; set; }

	public required bool Enhanceable { get; set; }

	public required bool NonUsableOnAnother { get; set; }

	public required int AppearanceId { get; set; }

	public required bool SecretRecipe { get; set; }

	public required int RecipeSlots { get; set; }

	public required List<uint> RecipeIds { get; set; }

	public required List<uint> DropMonsterIds { get; set; }

	public required bool BonusIsSecret { get; set; }

	public required List<EffectInstance> PossibleEffects { get; set; }

	public required List<uint> FavoriteSubAreas { get; set; }

	public required int FavoriteSubAreasBonus { get; set; }

	public static Item Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Item
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			TypeId = d2OClass.Fields[2].AsInt(reader),
			DescriptionId = d2OClass.Fields[3].AsI18N(reader),
			IconId = d2OClass.Fields[4].AsInt(reader),
			Level = d2OClass.Fields[5].AsInt(reader),
			RealWeight = d2OClass.Fields[6].AsInt(reader),
			Cursed = d2OClass.Fields[7].AsBoolean(reader),
			UseAnimationId = d2OClass.Fields[8].AsInt(reader),
			Usable = d2OClass.Fields[9].AsBoolean(reader),
			Targetable = d2OClass.Fields[10].AsBoolean(reader),
			Exchangeable = d2OClass.Fields[11].AsBoolean(reader),
			Price = d2OClass.Fields[12].AsDouble(reader),
			TwoHanded = d2OClass.Fields[13].AsBoolean(reader),
			Etheral = d2OClass.Fields[14].AsBoolean(reader),
			ItemSetId = d2OClass.Fields[15].AsInt(reader),
			Criteria = d2OClass.Fields[16].AsString(reader),
			CriteriaTarget = d2OClass.Fields[17].AsString(reader),
			HideEffects = d2OClass.Fields[18].AsBoolean(reader),
			Enhanceable = d2OClass.Fields[19].AsBoolean(reader),
			NonUsableOnAnother = d2OClass.Fields[20].AsBoolean(reader),
			AppearanceId = d2OClass.Fields[21].AsInt(reader),
			SecretRecipe = d2OClass.Fields[22].AsBoolean(reader),
			RecipeSlots = d2OClass.Fields[23].AsInt(reader),
			RecipeIds = d2OClass.Fields[24].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			DropMonsterIds = d2OClass.Fields[25].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			BonusIsSecret = d2OClass.Fields[26].AsBoolean(reader),
			PossibleEffects = d2OClass.Fields[27].AsList<EffectInstance>(reader, static (field, r) => field.AsObject<EffectInstance>(r)),
			FavoriteSubAreas = d2OClass.Fields[28].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			FavoriteSubAreasBonus = d2OClass.Fields[29].AsInt(reader),
		};
	}
}
