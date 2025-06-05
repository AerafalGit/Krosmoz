// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Appearance;

public sealed class Ornament : IDatacenterObject
{
	public static string ModuleName =>
		"Ornaments";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required bool Visible { get; set; }

	public required int AssetId { get; set; }

	public required int IconId { get; set; }

	public required int Rarity { get; set; }

	public required int Order { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		Visible = d2OClass.ReadFieldAsBoolean(reader);
		AssetId = d2OClass.ReadFieldAsInt(reader);
		IconId = d2OClass.ReadFieldAsInt(reader);
		Rarity = d2OClass.ReadFieldAsInt(reader);
		Order = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsBoolean(writer, Visible);
		d2OClass.WriteFieldAsInt(writer, AssetId);
		d2OClass.WriteFieldAsInt(writer, IconId);
		d2OClass.WriteFieldAsInt(writer, Rarity);
		d2OClass.WriteFieldAsInt(writer, Order);
	}
}
