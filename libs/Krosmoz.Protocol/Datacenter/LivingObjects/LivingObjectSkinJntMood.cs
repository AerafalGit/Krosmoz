// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.LivingObjects;

public sealed class LivingObjectSkinJntMood : IDatacenterObject
{
	public static string ModuleName =>
		"LivingObjectSkinJntMood";

	public required int SkinId { get; set; }

	public required List<List<int>> Moods { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		SkinId = d2OClass.ReadFieldAsInt(reader);
		Moods = d2OClass.ReadFieldAsListOfList(reader, static (c, r) => c.ReadFieldAsInt(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, SkinId);
		d2OClass.WriteFieldAsListOfList(writer, Moods, static (c, r, v) => c.WriteFieldAsInt(r, v));
	}
}
