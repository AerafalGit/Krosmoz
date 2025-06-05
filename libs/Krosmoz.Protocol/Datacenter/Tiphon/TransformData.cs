// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Tiphon;

public sealed class TransformData : IDatacenterObject
{
	public static string ModuleName =>
		"SkinPositions";

	public required double X { get; set; }

	public required double Y { get; set; }

	public required double ScaleX { get; set; }

	public required double ScaleY { get; set; }

	public required int Rotation { get; set; }

	public required string OriginalClip { get; set; }

	public required string OverrideClip { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		X = d2OClass.ReadFieldAsNumber(reader);
		Y = d2OClass.ReadFieldAsNumber(reader);
		ScaleX = d2OClass.ReadFieldAsNumber(reader);
		ScaleY = d2OClass.ReadFieldAsNumber(reader);
		Rotation = d2OClass.ReadFieldAsInt(reader);
		OriginalClip = d2OClass.ReadFieldAsString(reader);
		OverrideClip = d2OClass.ReadFieldAsString(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsNumber(writer, X);
		d2OClass.WriteFieldAsNumber(writer, Y);
		d2OClass.WriteFieldAsNumber(writer, ScaleX);
		d2OClass.WriteFieldAsNumber(writer, ScaleY);
		d2OClass.WriteFieldAsInt(writer, Rotation);
		d2OClass.WriteFieldAsString(writer, OriginalClip);
		d2OClass.WriteFieldAsString(writer, OverrideClip);
	}
}
