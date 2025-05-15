// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Geom;

public sealed class Rectangle : IDatacenterObject
{
	public static string ModuleName =>
		"SubAreas";

	public required int X { get; set; }

	public required int Y { get; set; }

	public required int Width { get; set; }

	public required int Height { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		X = d2OClass.ReadFieldAsInt(reader);
		Y = d2OClass.ReadFieldAsInt(reader);
		Width = d2OClass.ReadFieldAsInt(reader);
		Height = d2OClass.ReadFieldAsInt(reader);
	}
}
