// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.AmbientSounds;
using Krosmoz.Protocol.Datacenter.Geom;

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class SubArea : IDatacenterObject
{
	public static string ModuleName =>
		"SubAreas";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required int AreaId { get; set; }

	public required List<AmbientSound> AmbientSounds { get; set; }

	public required List<List<int>> Playlists { get; set; }

	public required List<uint> MapIds { get; set; }

	public required Rectangle Bounds { get; set; }

	public required List<int> Shape { get; set; }

	public required List<uint> CustomWorldMap { get; set; }

	public required uint PackId { get; set; }

	public required uint Level { get; set; }

	public required bool IsConquestVillage { get; set; }

	public required bool BasicAccountAllowed { get; set; }

	public required bool DisplayOnWorldMap { get; set; }

	public required List<uint> Monsters { get; set; }

	public required List<uint> EntranceMapIds { get; set; }

	public required List<uint> ExitMapIds { get; set; }

	public required bool Capturable { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		AreaId = d2OClass.ReadFieldAsInt(reader);
		AmbientSounds = d2OClass.ReadFieldAsList<AmbientSound>(reader, static (c, r) => c.ReadFieldAsObject<AmbientSound>(r));
		Playlists = d2OClass.ReadFieldAsListOfList(reader, static (c, r) => c.ReadFieldAsInt(r));
		MapIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		Bounds = d2OClass.ReadFieldAsObject<Rectangle>(reader);
		Shape = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsInt(r));
		CustomWorldMap = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		PackId = d2OClass.ReadFieldAsUInt(reader);
		Level = d2OClass.ReadFieldAsUInt(reader);
		IsConquestVillage = d2OClass.ReadFieldAsBoolean(reader);
		BasicAccountAllowed = d2OClass.ReadFieldAsBoolean(reader);
		DisplayOnWorldMap = d2OClass.ReadFieldAsBoolean(reader);
		Monsters = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		EntranceMapIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		ExitMapIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		Capturable = d2OClass.ReadFieldAsBoolean(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsInt(writer, AreaId);
		d2OClass.WriteFieldAsList(writer, AmbientSounds, static (c, r, v) => c.WriteFieldAsObject(r, v));
		d2OClass.WriteFieldAsListOfList(writer, Playlists, static (c, r, v) => c.WriteFieldAsInt(r, v));
		d2OClass.WriteFieldAsList(writer, MapIds, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsObject(writer, Bounds);
		d2OClass.WriteFieldAsList(writer, Shape, static (c, r, v) => c.WriteFieldAsInt(r, v));
		d2OClass.WriteFieldAsList(writer, CustomWorldMap, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsUInt(writer, PackId);
		d2OClass.WriteFieldAsUInt(writer, Level);
		d2OClass.WriteFieldAsBoolean(writer, IsConquestVillage);
		d2OClass.WriteFieldAsBoolean(writer, BasicAccountAllowed);
		d2OClass.WriteFieldAsBoolean(writer, DisplayOnWorldMap);
		d2OClass.WriteFieldAsList(writer, Monsters, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsList(writer, EntranceMapIds, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsList(writer, ExitMapIds, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsBoolean(writer, Capturable);
	}
}
