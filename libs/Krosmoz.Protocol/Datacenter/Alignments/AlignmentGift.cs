// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Alignments;

public sealed class AlignmentGift : IDatacenterObject<AlignmentGift>
{
	public static string ModuleName =>
		"AlignmentGift";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int EffectId { get; set; }

	public required int GfxId { get; set; }

	public static AlignmentGift Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new AlignmentGift
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			EffectId = d2OClass.Fields[2].AsInt(reader),
			GfxId = d2OClass.Fields[3].AsInt(reader),
		};
	}
}
