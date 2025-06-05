// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class Dungeon : IDatacenterObject
{
	public static string ModuleName =>
		"Dungeons";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required int OptimalPlayerLevel { get; set; }

	public required List<int> MapIds { get; set; }

	public required int EntranceMapId { get; set; }

	public required int ExitMapId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		OptimalPlayerLevel = d2OClass.ReadFieldAsInt(reader);
		MapIds = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsInt(r));
		EntranceMapId = d2OClass.ReadFieldAsInt(reader);
		ExitMapId = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsInt(writer, OptimalPlayerLevel);
		d2OClass.WriteFieldAsList(writer, MapIds, static (c, r, v) => c.WriteFieldAsInt(r, v));
		d2OClass.WriteFieldAsInt(writer, EntranceMapId);
		d2OClass.WriteFieldAsInt(writer, ExitMapId);
	}
}
