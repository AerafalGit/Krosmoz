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

	public required int ActionId { get; set; }

	public required bool DisplayTooltip { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		NameId = d2OClass.Fields[1].AsI18N(reader);
		ActionId = d2OClass.Fields[2].AsInt(reader);
		DisplayTooltip = d2OClass.Fields[3].AsBoolean(reader);
	}
}
