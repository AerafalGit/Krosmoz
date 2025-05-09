// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Appearance;

public sealed class Title : IDatacenterObject<Title>
{
	public static string ModuleName =>
		"Titles";

	public required int Id { get; set; }

	public required int NameMaleId { get; set; }

	public required int NameFemaleId { get; set; }

	public required bool Visible { get; set; }

	public required int CategoryId { get; set; }

	public static Title Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Title
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameMaleId = d2OClass.Fields[1].AsI18N(reader),
			NameFemaleId = d2OClass.Fields[2].AsI18N(reader),
			Visible = d2OClass.Fields[3].AsBoolean(reader),
			CategoryId = d2OClass.Fields[4].AsInt(reader),
		};
	}
}
