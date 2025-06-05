// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Mounts;

public sealed class RideFood : IDatacenterObject
{
	public static string ModuleName =>
		"RideFood";

	public required int Gid { get; set; }

	public required int TypeId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Gid = d2OClass.ReadFieldAsInt(reader);
		TypeId = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Gid);
		d2OClass.WriteFieldAsInt(writer, TypeId);
	}
}
