// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class MapCoordinates : IDatacenterObject<MapCoordinates>
{
	public static string ModuleName =>
		"MapCoordinates";

	public required int CompressedCoords { get; set; }

	public required List<int> MapIds { get; set; }

	public static MapCoordinates Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new MapCoordinates
		{
			CompressedCoords = d2OClass.Fields[0].AsInt(reader),
			MapIds = d2OClass.Fields[1].AsList<int>(reader, static (field, r) => field.AsInt(r)),
		};
	}
}
