// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.AmbientSounds;

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class MapPosition : IDatacenterObject
{
	public static string ModuleName =>
		"MapPositions";

	public required int Id { get; set; }

	public required int PosX { get; set; }

	public required int PosY { get; set; }

	public required bool Outdoor { get; set; }

	public required int Capabilities { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required bool ShowNameOnFingerpost { get; set; }

	public required List<MapAmbientSound> Sounds { get; set; }

	public required List<List<int>> Playlists { get; set; }

	public required int SubAreaId { get; set; }

	public required int WorldMap { get; set; }

	public required bool HasPriorityOnWorldmap { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		PosX = d2OClass.ReadFieldAsInt(reader);
		PosY = d2OClass.ReadFieldAsInt(reader);
		Outdoor = d2OClass.ReadFieldAsBoolean(reader);
		Capabilities = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		ShowNameOnFingerpost = d2OClass.ReadFieldAsBoolean(reader);
		Sounds = d2OClass.ReadFieldAsList<MapAmbientSound>(reader, static (c, r) => c.ReadFieldAsObject<MapAmbientSound>(r));
		Playlists = d2OClass.ReadFieldAsListOfList(reader, static (c, r) => c.ReadFieldAsInt(r));
		SubAreaId = d2OClass.ReadFieldAsInt(reader);
		WorldMap = d2OClass.ReadFieldAsInt(reader);
		HasPriorityOnWorldmap = d2OClass.ReadFieldAsBoolean(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, PosX);
		d2OClass.WriteFieldAsInt(writer, PosY);
		d2OClass.WriteFieldAsBoolean(writer, Outdoor);
		d2OClass.WriteFieldAsInt(writer, Capabilities);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsBoolean(writer, ShowNameOnFingerpost);
		d2OClass.WriteFieldAsList(writer, Sounds, static (c, r, v) => c.WriteFieldAsObject(r, v));
		d2OClass.WriteFieldAsListOfList(writer, Playlists, static (c, r, v) => c.WriteFieldAsInt(r, v));
		d2OClass.WriteFieldAsInt(writer, SubAreaId);
		d2OClass.WriteFieldAsInt(writer, WorldMap);
		d2OClass.WriteFieldAsBoolean(writer, HasPriorityOnWorldmap);
	}
}
