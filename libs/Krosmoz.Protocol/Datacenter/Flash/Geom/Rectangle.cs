// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Flash.Geom;

public sealed class Rectangle : IDatacenterObject<Rectangle>
{
	public static string ModuleName =>
		"SubAreas";

	public required int X { get; set; }

	public required int Y { get; set; }

	public required int Width { get; set; }

	public required int Height { get; set; }

	public static Rectangle Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Rectangle
		{
			X = d2OClass.Fields[0].AsInt(reader),
			Y = d2OClass.Fields[1].AsInt(reader),
			Width = d2OClass.Fields[2].AsInt(reader),
			Height = d2OClass.Fields[3].AsInt(reader),
		};
	}
}
