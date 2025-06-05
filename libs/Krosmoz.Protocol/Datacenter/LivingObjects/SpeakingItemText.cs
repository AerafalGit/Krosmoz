// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.LivingObjects;

public sealed class SpeakingItemText : IDatacenterObject
{
	public static string ModuleName =>
		"SpeakingItemsText";

	public required int TextId { get; set; }

	public required double TextProba { get; set; }

	public required int TextStringId { get; set; }

	public required string TextString { get; set; }

	public required int TextLevel { get; set; }

	public required int TextSound { get; set; }

	public required string TextRestriction { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		TextId = d2OClass.ReadFieldAsInt(reader);
		TextProba = d2OClass.ReadFieldAsNumber(reader);
		TextStringId = d2OClass.ReadFieldAsI18N(reader);
		TextString = d2OClass.ReadFieldAsI18NString(TextStringId);
		TextLevel = d2OClass.ReadFieldAsInt(reader);
		TextSound = d2OClass.ReadFieldAsInt(reader);
		TextRestriction = d2OClass.ReadFieldAsString(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, TextId);
		d2OClass.WriteFieldAsNumber(writer, TextProba);
		d2OClass.WriteFieldAsI18N(writer, TextStringId);
		d2OClass.WriteFieldAsInt(writer, TextLevel);
		d2OClass.WriteFieldAsInt(writer, TextSound);
		d2OClass.WriteFieldAsString(writer, TextRestriction);
	}
}
