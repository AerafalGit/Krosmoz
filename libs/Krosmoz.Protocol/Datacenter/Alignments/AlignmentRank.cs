// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Alignments;

public sealed class AlignmentRank : IDatacenterObject
{
	public static string ModuleName =>
		"AlignmentRank";

	public required int Id { get; set; }

	public required int OrderId { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required int DescriptionId { get; set; }

	public required string Description { get; set; }

	public required int MinimumAlignment { get; set; }

	public required int ObjectsStolen { get; set; }

	public required List<int> Gifts { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		OrderId = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		DescriptionId = d2OClass.ReadFieldAsI18N(reader);
		Description = d2OClass.ReadFieldAsI18NString(DescriptionId);
		MinimumAlignment = d2OClass.ReadFieldAsInt(reader);
		ObjectsStolen = d2OClass.ReadFieldAsInt(reader);
		Gifts = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsInt(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, OrderId);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsI18N(writer, DescriptionId);
		d2OClass.WriteFieldAsInt(writer, MinimumAlignment);
		d2OClass.WriteFieldAsInt(writer, ObjectsStolen);
		d2OClass.WriteFieldAsList(writer, Gifts, static (c, r, v) => c.WriteFieldAsInt(r, v));
	}
}
