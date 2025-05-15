// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Effects;

namespace Krosmoz.Protocol.Datacenter.Items;

public class Item : IDatacenterObject
{
	public static string ModuleName =>
		"Items";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required int TypeId { get; set; }

	public required int DescriptionId { get; set; }

	public required string Description { get; set; }

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

	public virtual void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		TypeId = d2OClass.ReadFieldAsInt(reader);
		DescriptionId = d2OClass.ReadFieldAsI18N(reader);
		Description = d2OClass.ReadFieldAsI18NString(DescriptionId);
		IconId = d2OClass.ReadFieldAsInt(reader);
		Level = d2OClass.ReadFieldAsInt(reader);
		RealWeight = d2OClass.ReadFieldAsInt(reader);
		Cursed = d2OClass.ReadFieldAsBoolean(reader);
		UseAnimationId = d2OClass.ReadFieldAsInt(reader);
		Usable = d2OClass.ReadFieldAsBoolean(reader);
		Targetable = d2OClass.ReadFieldAsBoolean(reader);
		Exchangeable = d2OClass.ReadFieldAsBoolean(reader);
		Price = d2OClass.ReadFieldAsNumber(reader);
		TwoHanded = d2OClass.ReadFieldAsBoolean(reader);
		Etheral = d2OClass.ReadFieldAsBoolean(reader);
		ItemSetId = d2OClass.ReadFieldAsInt(reader);
		Criteria = d2OClass.ReadFieldAsString(reader);
		CriteriaTarget = d2OClass.ReadFieldAsString(reader);
		HideEffects = d2OClass.ReadFieldAsBoolean(reader);
		Enhanceable = d2OClass.ReadFieldAsBoolean(reader);
		NonUsableOnAnother = d2OClass.ReadFieldAsBoolean(reader);
		AppearanceId = d2OClass.ReadFieldAsInt(reader);
		SecretRecipe = d2OClass.ReadFieldAsBoolean(reader);
		RecipeSlots = d2OClass.ReadFieldAsInt(reader);
		RecipeIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		DropMonsterIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		BonusIsSecret = d2OClass.ReadFieldAsBoolean(reader);
		PossibleEffects = d2OClass.ReadFieldAsList<EffectInstance>(reader, static (c, r) => c.ReadFieldAsObject<EffectInstance>(r));
		FavoriteSubAreas = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		FavoriteSubAreasBonus = d2OClass.ReadFieldAsInt(reader);
	}
}
