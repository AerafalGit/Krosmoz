// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Monsters;

public sealed class MonsterRace : IDatacenterObject<MonsterRace>
{
	public static string ModuleName =>
		"MonsterRaces";

	public required int Id { get; set; }

	public required int SuperRaceId { get; set; }

	public required int NameId { get; set; }

	public required List<uint> Monsters { get; set; }

	public static MonsterRace Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new MonsterRace
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			SuperRaceId = d2OClass.Fields[1].AsInt(reader),
			NameId = d2OClass.Fields[2].AsI18N(reader),
			Monsters = d2OClass.Fields[3].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
		};
	}
}
