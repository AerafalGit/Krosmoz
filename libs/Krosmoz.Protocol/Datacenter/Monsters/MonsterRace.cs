// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Monsters;

public sealed class MonsterRace : IDatacenterObject
{
	public static string ModuleName =>
		"MonsterRaces";

	public required int Id { get; set; }

	public required int SuperRaceId { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required List<uint> Monsters { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		SuperRaceId = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		Monsters = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, SuperRaceId);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsList(writer, Monsters, static (c, r, v) => c.WriteFieldAsUInt(r, v));
	}
}
