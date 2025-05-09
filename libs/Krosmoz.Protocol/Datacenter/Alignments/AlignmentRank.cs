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

	public required int DescriptionId { get; set; }

	public required int MinimumAlignment { get; set; }

	public required int ObjectsStolen { get; set; }

	public required List<int> Gifts { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		OrderId = d2OClass.Fields[1].AsInt(reader);
		NameId = d2OClass.Fields[2].AsI18N(reader);
		DescriptionId = d2OClass.Fields[3].AsI18N(reader);
		MinimumAlignment = d2OClass.Fields[4].AsInt(reader);
		ObjectsStolen = d2OClass.Fields[5].AsInt(reader);
		Gifts = d2OClass.Fields[6].AsList(reader, static (field, r) => field.AsInt(r));
	}
}
