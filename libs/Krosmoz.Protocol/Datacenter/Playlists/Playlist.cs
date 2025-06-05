// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.AmbientSounds;

namespace Krosmoz.Protocol.Datacenter.Playlists;

public sealed class Playlist : IDatacenterObject
{
	public static string ModuleName =>
		"Playlists";

	public required int Id { get; set; }

	public required uint SilenceDuration { get; set; }

	public required int Iteration { get; set; }

	public required int Type { get; set; }

	public required List<PlaylistSound> Sounds { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		SilenceDuration = d2OClass.ReadFieldAsUInt(reader);
		Iteration = d2OClass.ReadFieldAsInt(reader);
		Type = d2OClass.ReadFieldAsInt(reader);
		Sounds = d2OClass.ReadFieldAsList<PlaylistSound>(reader, static (c, r) => c.ReadFieldAsObject<PlaylistSound>(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsUInt(writer, SilenceDuration);
		d2OClass.WriteFieldAsInt(writer, Iteration);
		d2OClass.WriteFieldAsInt(writer, Type);
		d2OClass.WriteFieldAsList(writer, Sounds, static (c, r, v) => c.WriteFieldAsObject(r, v));
	}
}
