// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Interactives;

public sealed class Interactive : IDatacenterObject
{
	public static string ModuleName =>
		"Interactives";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required int ActionId { get; set; }

	public required bool DisplayTooltip { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		ActionId = d2OClass.ReadFieldAsInt(reader);
		DisplayTooltip = d2OClass.ReadFieldAsBoolean(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsInt(writer, ActionId);
		d2OClass.WriteFieldAsBoolean(writer, DisplayTooltip);
	}
}
