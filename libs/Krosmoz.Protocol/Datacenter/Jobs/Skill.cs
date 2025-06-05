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

	public required string Name { get; set; }

	public required int ParentJobId { get; set; }

	public required bool IsForgemagus { get; set; }

	public required List<int> ModifiableItemTypeIds { get; set; }

	public required int GatheredRessourceItem { get; set; }

	public required List<int> CraftableItemIds { get; set; }

	public required int InteractiveId { get; set; }

	public required string UseAnimation { get; set; }

	public required bool IsRepair { get; set; }

	public required int Cursor { get; set; }

	public required int ElementActionId { get; set; }

	public required bool AvailableInHouse { get; set; }

	public required bool ClientDisplay { get; set; }

	public required int LevelMin { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		ParentJobId = d2OClass.ReadFieldAsInt(reader);
		IsForgemagus = d2OClass.ReadFieldAsBoolean(reader);
		ModifiableItemTypeIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsInt(r));
		GatheredRessourceItem = d2OClass.ReadFieldAsInt(reader);
		CraftableItemIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsInt(r));
		InteractiveId = d2OClass.ReadFieldAsInt(reader);
		UseAnimation = d2OClass.ReadFieldAsString(reader);
		IsRepair = d2OClass.ReadFieldAsBoolean(reader);
		Cursor = d2OClass.ReadFieldAsInt(reader);
		ElementActionId = d2OClass.ReadFieldAsInt(reader);
		AvailableInHouse = d2OClass.ReadFieldAsBoolean(reader);
		ClientDisplay = d2OClass.ReadFieldAsBoolean(reader);
		LevelMin = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsInt(writer, ParentJobId);
		d2OClass.WriteFieldAsBoolean(writer, IsForgemagus);
		d2OClass.WriteFieldAsList(writer, ModifiableItemTypeIds, static (c, r, v) => c.WriteFieldAsInt(r, v));
		d2OClass.WriteFieldAsInt(writer, GatheredRessourceItem);
		d2OClass.WriteFieldAsList(writer, CraftableItemIds, static (c, r, v) => c.WriteFieldAsInt(r, v));
		d2OClass.WriteFieldAsInt(writer, InteractiveId);
		d2OClass.WriteFieldAsString(writer, UseAnimation);
		d2OClass.WriteFieldAsBoolean(writer, IsRepair);
		d2OClass.WriteFieldAsInt(writer, Cursor);
		d2OClass.WriteFieldAsInt(writer, ElementActionId);
		d2OClass.WriteFieldAsBoolean(writer, AvailableInHouse);
		d2OClass.WriteFieldAsBoolean(writer, ClientDisplay);
		d2OClass.WriteFieldAsInt(writer, LevelMin);
	}
}
