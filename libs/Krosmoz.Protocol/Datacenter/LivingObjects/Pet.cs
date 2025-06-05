// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Effects;

namespace Krosmoz.Protocol.Datacenter.LivingObjects;

public sealed class Pet : IDatacenterObject
{
	public static string ModuleName =>
		"Pets";

	public required int Id { get; set; }

	public required List<int> FoodItems { get; set; }

	public required List<int> FoodTypes { get; set; }

	public required int MinDurationBeforeMeal { get; set; }

	public required int MaxDurationBeforeMeal { get; set; }

	public required List<EffectInstance> PossibleEffects { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		FoodItems = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsInt(r));
		FoodTypes = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsInt(r));
		MinDurationBeforeMeal = d2OClass.ReadFieldAsInt(reader);
		MaxDurationBeforeMeal = d2OClass.ReadFieldAsInt(reader);
		PossibleEffects = d2OClass.ReadFieldAsList<EffectInstance>(reader, static (c, r) => c.ReadFieldAsObject<EffectInstance>(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsList(writer, FoodItems, static (c, r, v) => c.WriteFieldAsInt(r, v));
		d2OClass.WriteFieldAsList(writer, FoodTypes, static (c, r, v) => c.WriteFieldAsInt(r, v));
		d2OClass.WriteFieldAsInt(writer, MinDurationBeforeMeal);
		d2OClass.WriteFieldAsInt(writer, MaxDurationBeforeMeal);
		d2OClass.WriteFieldAsList(writer, PossibleEffects, static (c, r, v) => c.WriteFieldAsObject(r, v));
	}
}
