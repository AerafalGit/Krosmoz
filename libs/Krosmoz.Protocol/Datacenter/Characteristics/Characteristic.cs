// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Characteristics;

public sealed class Characteristic : IDatacenterObject
{
	public static string ModuleName =>
		"Characteristics";

	public required int Id { get; set; }

	public required string Keyword { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required string Asset { get; set; }

	public required int CategoryId { get; set; }

	public required bool Visible { get; set; }

	public required int Order { get; set; }

	public required bool Upgradable { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		Keyword = d2OClass.ReadFieldAsString(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		Asset = d2OClass.ReadFieldAsString(reader);
		CategoryId = d2OClass.ReadFieldAsInt(reader);
		Visible = d2OClass.ReadFieldAsBoolean(reader);
		Order = d2OClass.ReadFieldAsInt(reader);
		Upgradable = d2OClass.ReadFieldAsBoolean(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsString(writer, Keyword);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsString(writer, Asset);
		d2OClass.WriteFieldAsInt(writer, CategoryId);
		d2OClass.WriteFieldAsBoolean(writer, Visible);
		d2OClass.WriteFieldAsInt(writer, Order);
		d2OClass.WriteFieldAsBoolean(writer, Upgradable);
	}
}
