// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.AmbientSounds;

public sealed class PlaylistSound : IDatacenterObject
{
	public static string ModuleName =>
		"Playlists";

	public required string Id { get; set; }

	public required int Volume { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsString(reader);
		Volume = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsString(writer, Id);
		d2OClass.WriteFieldAsInt(writer, Volume);
	}
}
