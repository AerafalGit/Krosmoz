// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class MapCoordinates : IDatacenterObject
{
	public static string ModuleName =>
		"MapCoordinates";

	public required int CompressedCoords { get; set; }

	public required List<int> MapIds { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		CompressedCoords = d2OClass.ReadFieldAsInt(reader);
		MapIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsInt(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, CompressedCoords);
		d2OClass.WriteFieldAsList(writer, MapIds, static (c, r, v) => c.WriteFieldAsInt(r, v));
	}
}
