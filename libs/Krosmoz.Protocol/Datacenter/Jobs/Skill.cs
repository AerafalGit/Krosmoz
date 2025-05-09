// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Jobs;

public sealed class Skill : IDatacenterObject
{
	public static string ModuleName =>
		"Skills";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int ParentJobId { get; set; }

	public required bool IsForgemagus { get; set; }

	public required int ModifiableItemType { get; set; }

	public required int GatheredRessourceItem { get; set; }

	public required List<int> CraftableItemIds { get; set; }

	public required int InteractiveId { get; set; }

	public required string UseAnimation { get; set; }

	public required bool IsRepair { get; set; }

	public required int Cursor { get; set; }

	public required bool AvailableInHouse { get; set; }

	public required int LevelMin { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		NameId = d2OClass.Fields[1].AsI18N(reader);
		ParentJobId = d2OClass.Fields[2].AsInt(reader);
		IsForgemagus = d2OClass.Fields[3].AsBoolean(reader);
		ModifiableItemType = d2OClass.Fields[4].AsInt(reader);
		GatheredRessourceItem = d2OClass.Fields[5].AsInt(reader);
		CraftableItemIds = d2OClass.Fields[6].AsList(reader, static (field, r) => field.AsInt(r));
		InteractiveId = d2OClass.Fields[7].AsInt(reader);
		UseAnimation = d2OClass.Fields[8].AsString(reader);
		IsRepair = d2OClass.Fields[9].AsBoolean(reader);
		Cursor = d2OClass.Fields[10].AsInt(reader);
		AvailableInHouse = d2OClass.Fields[11].AsBoolean(reader);
		LevelMin = d2OClass.Fields[12].AsInt(reader);
	}
}
