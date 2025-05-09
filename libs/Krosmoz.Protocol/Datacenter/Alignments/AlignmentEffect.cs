// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Alignments;

public sealed class AlignmentEffect : IDatacenterObject<AlignmentEffect>
{
	public static string ModuleName =>
		"AlignmentEffect";

	public required int Id { get; set; }

	public required int CharacteristicId { get; set; }

	public required int DescriptionId { get; set; }

	public static AlignmentEffect Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new AlignmentEffect
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			CharacteristicId = d2OClass.Fields[1].AsInt(reader),
			DescriptionId = d2OClass.Fields[2].AsI18N(reader),
		};
	}
}
