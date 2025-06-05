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
		SideId = d2OClass.ReadFieldAsInt(reader);
		NamesId = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsI18N(r));
		ShortsId = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsI18N(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, SideId);
		d2OClass.WriteFieldAsList(writer, NamesId, static (c, r, v) => c.WriteFieldAsI18N(r, v));
		d2OClass.WriteFieldAsList(writer, ShortsId, static (c, r, v) => c.WriteFieldAsI18N(r, v));
	}
}
