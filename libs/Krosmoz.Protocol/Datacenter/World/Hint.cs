// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class Hint : IDatacenterObject<Hint>
{
	public static string ModuleName =>
		"Hints";

	public required int Id { get; set; }

	public required int CategoryId { get; set; }

	public required int Gfx { get; set; }

	public required int NameId { get; set; }

	public required int MapId { get; set; }

	public required int RealMapId { get; set; }

	public required int X { get; set; }

	public required int Y { get; set; }

	public required bool Outdoor { get; set; }

	public required int SubareaId { get; set; }

	public required int WorldMapId { get; set; }

	public static Hint Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Hint
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			CategoryId = d2OClass.Fields[1].AsInt(reader),
			Gfx = d2OClass.Fields[2].AsInt(reader),
			NameId = d2OClass.Fields[3].AsI18N(reader),
			MapId = d2OClass.Fields[4].AsInt(reader),
			RealMapId = d2OClass.Fields[5].AsInt(reader),
			X = d2OClass.Fields[6].AsInt(reader),
			Y = d2OClass.Fields[7].AsInt(reader),
			Outdoor = d2OClass.Fields[8].AsBoolean(reader),
			SubareaId = d2OClass.Fields[9].AsInt(reader),
			WorldMapId = d2OClass.Fields[10].AsInt(reader),
		};
	}
}
