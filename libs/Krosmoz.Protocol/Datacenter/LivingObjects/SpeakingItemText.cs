// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.LivingObjects;

public sealed class SpeakingItemText : IDatacenterObject<SpeakingItemText>
{
	public static string ModuleName =>
		"SpeakingItemsText";

	public required int TextId { get; set; }

	public required double TextProba { get; set; }

	public required int TextStringId { get; set; }

	public required int TextLevel { get; set; }

	public required int TextSound { get; set; }

	public required string TextRestriction { get; set; }

	public static SpeakingItemText Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new SpeakingItemText
		{
			TextId = d2OClass.Fields[0].AsInt(reader),
			TextProba = d2OClass.Fields[1].AsDouble(reader),
			TextStringId = d2OClass.Fields[2].AsI18N(reader),
			TextLevel = d2OClass.Fields[3].AsInt(reader),
			TextSound = d2OClass.Fields[4].AsInt(reader),
			TextRestriction = d2OClass.Fields[5].AsString(reader),
		};
	}
}
