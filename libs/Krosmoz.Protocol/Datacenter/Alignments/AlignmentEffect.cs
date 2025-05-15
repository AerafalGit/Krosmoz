// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Alignments;

public sealed class AlignmentEffect : IDatacenterObject
{
	public static string ModuleName =>
		"AlignmentEffect";

	public required int Id { get; set; }

	public required int CharacteristicId { get; set; }

	public required int DescriptionId { get; set; }

	public required string Description { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		CharacteristicId = d2OClass.ReadFieldAsInt(reader);
		DescriptionId = d2OClass.ReadFieldAsI18N(reader);
		Description = d2OClass.ReadFieldAsI18NString(DescriptionId);
	}
}
