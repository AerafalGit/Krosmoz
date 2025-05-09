// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Jobs;

public sealed class Recipe : IDatacenterObject
{
	public static string ModuleName =>
		"Recipes";

	public required int ResultId { get; set; }

	public required int ResultLevel { get; set; }

	public required List<int> IngredientIds { get; set; }

	public required List<uint> Quantities { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		ResultId = d2OClass.Fields[0].AsInt(reader);
		ResultLevel = d2OClass.Fields[1].AsInt(reader);
		IngredientIds = d2OClass.Fields[2].AsList(reader, static (field, r) => field.AsInt(r));
		Quantities = d2OClass.Fields[3].AsList(reader, static (field, r) => field.AsUInt(r));
	}
}
