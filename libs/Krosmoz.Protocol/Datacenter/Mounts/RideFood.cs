// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Mounts;

public sealed class RideFood : IDatacenterObject<RideFood>
{
	public static string ModuleName =>
		"RideFood";

	public required int Gid { get; set; }

	public required int TypeId { get; set; }

	public static RideFood Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new RideFood
		{
			Gid = d2OClass.Fields[0].AsInt(reader),
			TypeId = d2OClass.Fields[1].AsInt(reader),
		};
	}
}
