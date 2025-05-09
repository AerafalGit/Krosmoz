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

	public required int OptimalPlayerLevel { get; set; }

	public required List<int> MapIds { get; set; }

	public required int EntranceMapId { get; set; }

	public required int ExitMapId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		NameId = d2OClass.Fields[1].AsI18N(reader);
		OptimalPlayerLevel = d2OClass.Fields[2].AsInt(reader);
		MapIds = d2OClass.Fields[3].AsList(reader, static (field, r) => field.AsInt(r));
		EntranceMapId = d2OClass.Fields[4].AsInt(reader);
		ExitMapId = d2OClass.Fields[5].AsInt(reader);
	}
}
