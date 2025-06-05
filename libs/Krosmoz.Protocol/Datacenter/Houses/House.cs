// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Houses;

public sealed class House : IDatacenterObject
{
	public static string ModuleName =>
		"Houses";

	public required int TypeId { get; set; }

	public required int DefaultPrice { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required int DescriptionId { get; set; }

	public required string Description { get; set; }

	public required int GfxId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		TypeId = d2OClass.ReadFieldAsInt(reader);
		DefaultPrice = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		DescriptionId = d2OClass.ReadFieldAsI18N(reader);
		Description = d2OClass.ReadFieldAsI18NString(DescriptionId);
		GfxId = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, TypeId);
		d2OClass.WriteFieldAsInt(writer, DefaultPrice);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsI18N(writer, DescriptionId);
		d2OClass.WriteFieldAsInt(writer, GfxId);
	}
}
