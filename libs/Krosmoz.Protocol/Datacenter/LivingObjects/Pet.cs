// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.LivingObjects;

public sealed class Pet : IDatacenterObject
{
	public static string ModuleName =>
		"Pets";

	public required int Id { get; set; }

	public required List<int> FoodItems { get; set; }

	public required List<int> FoodTypes { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		FoodItems = d2OClass.Fields[1].AsList(reader, static (field, r) => field.AsInt(r));
		FoodTypes = d2OClass.Fields[2].AsList(reader, static (field, r) => field.AsInt(r));
	}
}
