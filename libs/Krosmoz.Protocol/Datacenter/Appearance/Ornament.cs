// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Appearance;

public sealed class Ornament : IDatacenterObject<Ornament>
{
	public static string ModuleName =>
		"Ornaments";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required bool Visible { get; set; }

	public required int AssetId { get; set; }

	public required int IconId { get; set; }

	public required int Rarity { get; set; }

	public required int Order { get; set; }

	public static Ornament Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Ornament
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			Visible = d2OClass.Fields[2].AsBoolean(reader),
			AssetId = d2OClass.Fields[3].AsInt(reader),
			IconId = d2OClass.Fields[4].AsInt(reader),
			Rarity = d2OClass.Fields[5].AsInt(reader),
			Order = d2OClass.Fields[6].AsInt(reader),
		};
	}
}
