// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Alignments;

public sealed class AlignmentBalance : IDatacenterObject
{
	public static string ModuleName =>
		"AlignmentBalance";

	public required int Id { get; set; }

	public required int StartValue { get; set; }

	public required int EndValue { get; set; }

	public required int NameId { get; set; }

	public required int DescriptionId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		StartValue = d2OClass.Fields[1].AsInt(reader);
		EndValue = d2OClass.Fields[2].AsInt(reader);
		NameId = d2OClass.Fields[3].AsI18N(reader);
		DescriptionId = d2OClass.Fields[4].AsI18N(reader);
	}
}
