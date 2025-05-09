// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Alignments;

public sealed class AlignmentRankJntGift : IDatacenterObject
{
	public static string ModuleName =>
		"AlignmentRankJntGift";

	public required int Id { get; set; }

	public required List<int> Gifts { get; set; }

	public required List<int> Parameters { get; set; }

	public required List<int> Levels { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		Gifts = d2OClass.Fields[1].AsList(reader, static (field, r) => field.AsInt(r));
		Parameters = d2OClass.Fields[2].AsList(reader, static (field, r) => field.AsInt(r));
		Levels = d2OClass.Fields[3].AsList(reader, static (field, r) => field.AsInt(r));
	}
}
