// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Appearance;

public sealed class Title : IDatacenterObject
{
	public static string ModuleName =>
		"Titles";

	public required int Id { get; set; }

	public required int NameMaleId { get; set; }

	public required string NameMale { get; set; }

	public required int NameFemaleId { get; set; }

	public required string NameFemale { get; set; }

	public required bool Visible { get; set; }

	public required int CategoryId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameMaleId = d2OClass.ReadFieldAsI18N(reader);
		NameMale = d2OClass.ReadFieldAsI18NString(NameMaleId);
		NameFemaleId = d2OClass.ReadFieldAsI18N(reader);
		NameFemale = d2OClass.ReadFieldAsI18NString(NameFemaleId);
		Visible = d2OClass.ReadFieldAsBoolean(reader);
		CategoryId = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameMaleId);
		d2OClass.WriteFieldAsI18N(writer, NameFemaleId);
		d2OClass.WriteFieldAsBoolean(writer, Visible);
		d2OClass.WriteFieldAsInt(writer, CategoryId);
	}
}
