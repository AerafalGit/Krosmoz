// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Monsters;

public sealed class MonsterMiniBoss : IDatacenterObject<MonsterMiniBoss>
{
	public static string ModuleName =>
		"MonsterMiniBoss";

	public required int Id { get; set; }

	public required int MonsterReplacingId { get; set; }

	public static MonsterMiniBoss Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new MonsterMiniBoss
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			MonsterReplacingId = d2OClass.Fields[1].AsInt(reader),
		};
	}
}
