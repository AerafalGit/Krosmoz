// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Alignments;

public sealed class AlignmentTitle : IDatacenterObject
{
	public static string ModuleName =>
		"AlignmentTitles";

	public required int SideId { get; set; }

	public required List<int> NamesId { get; set; }

	public required List<int> ShortsId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		SideId = d2OClass.Fields[0].AsInt(reader);
		NamesId = d2OClass.Fields[1].AsList(reader, static (field, r) => field.AsI18N(r));
		ShortsId = d2OClass.Fields[2].AsList(reader, static (field, r) => field.AsI18N(r));
	}
}
