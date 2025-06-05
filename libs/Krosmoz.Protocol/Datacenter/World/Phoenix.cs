// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class Phoenix : IDatacenterObject
{
	public static string ModuleName =>
		"Phoenixes";

	public required int MapId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		MapId = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, MapId);
	}
}
