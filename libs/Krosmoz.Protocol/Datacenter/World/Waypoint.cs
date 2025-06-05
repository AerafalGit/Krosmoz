// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class Waypoint : IDatacenterObject
{
	public static string ModuleName =>
		"Waypoints";

	public required int Id { get; set; }

	public required int MapId { get; set; }

	public required int SubAreaId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		MapId = d2OClass.ReadFieldAsInt(reader);
		SubAreaId = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, MapId);
		d2OClass.WriteFieldAsInt(writer, SubAreaId);
	}
}
