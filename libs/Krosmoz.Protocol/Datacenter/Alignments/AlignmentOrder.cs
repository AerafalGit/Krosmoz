// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Alignments;

public sealed class AlignmentOrder : IDatacenterObject<AlignmentOrder>
{
	public static string ModuleName =>
		"AlignmentOrder";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int SideId { get; set; }

	public static AlignmentOrder Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new AlignmentOrder
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			SideId = d2OClass.Fields[2].AsInt(reader),
		};
	}
}
