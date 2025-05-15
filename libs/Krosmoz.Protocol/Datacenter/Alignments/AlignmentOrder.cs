// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Alignments;

public sealed class AlignmentOrder : IDatacenterObject
{
	public static string ModuleName =>
		"AlignmentOrder";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required int SideId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		SideId = d2OClass.ReadFieldAsInt(reader);
	}
}
