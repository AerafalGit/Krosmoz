// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.AmbientSounds;

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class MapPosition : IDatacenterObject<MapPosition>
{
	public static string ModuleName =>
		"MapPositions";

	public required int Id { get; set; }

	public required int PosX { get; set; }

	public required int PosY { get; set; }

	public required bool Outdoor { get; set; }

	public required int Capabilities { get; set; }

	public required int NameId { get; set; }

	public required List<AmbientSound> Sounds { get; set; }

	public required int SubAreaId { get; set; }

	public required int WorldMap { get; set; }

	public required bool HasPriorityOnWorldmap { get; set; }

	public static MapPosition Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new MapPosition
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			PosX = d2OClass.Fields[1].AsInt(reader),
			PosY = d2OClass.Fields[2].AsInt(reader),
			Outdoor = d2OClass.Fields[3].AsBoolean(reader),
			Capabilities = d2OClass.Fields[4].AsInt(reader),
			NameId = d2OClass.Fields[5].AsI18N(reader),
			Sounds = d2OClass.Fields[6].AsList<AmbientSound>(reader, static (field, r) => field.AsObject<AmbientSound>(r)),
			SubAreaId = d2OClass.Fields[7].AsInt(reader),
			WorldMap = d2OClass.Fields[8].AsInt(reader),
			HasPriorityOnWorldmap = d2OClass.Fields[9].AsBoolean(reader),
		};
	}
}
