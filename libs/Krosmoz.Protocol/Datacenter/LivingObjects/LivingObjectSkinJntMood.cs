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
		SkinId = d2OClass.Fields[0].AsInt(reader);
		Moods = d2OClass.Fields[1].AsListOfList(reader, static (field, r) => field.AsInt(r));
	}
}
