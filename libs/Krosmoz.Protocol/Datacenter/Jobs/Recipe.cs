// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Jobs;

public sealed class Recipe : IDatacenterObject
{
	public static string ModuleName =>
		"Recipes";

	public required int ResultId { get; set; }

	public required int ResultNameId { get; set; }

	public required string ResultName { get; set; }

	public required uint ResultTypeId { get; set; }

	public required int ResultLevel { get; set; }

	public required List<int> IngredientIds { get; set; }

	public required List<uint> Quantities { get; set; }

	public required int JobId { get; set; }

	public required int SkillId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		ResultId = d2OClass.ReadFieldAsInt(reader);
		ResultNameId = d2OClass.ReadFieldAsI18N(reader);
		ResultName = d2OClass.ReadFieldAsI18NString(ResultNameId);
		ResultTypeId = d2OClass.ReadFieldAsUInt(reader);
		ResultLevel = d2OClass.ReadFieldAsInt(reader);
		IngredientIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsInt(r));
		Quantities = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		JobId = d2OClass.ReadFieldAsInt(reader);
		SkillId = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, ResultId);
		d2OClass.WriteFieldAsI18N(writer, ResultNameId);
		d2OClass.WriteFieldAsUInt(writer, ResultTypeId);
		d2OClass.WriteFieldAsInt(writer, ResultLevel);
		d2OClass.WriteFieldAsList(writer, IngredientIds, static (c, r, v) => c.WriteFieldAsInt(r, v));
		d2OClass.WriteFieldAsList(writer, Quantities, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsInt(writer, JobId);
		d2OClass.WriteFieldAsInt(writer, SkillId);
	}
}
