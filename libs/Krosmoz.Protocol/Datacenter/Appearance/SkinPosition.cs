// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Tiphon;

namespace Krosmoz.Protocol.Datacenter.Appearance;

public sealed class SkinPosition : IDatacenterObject
{
	public static string ModuleName =>
		"SkinPositions";

	public required int Id { get; set; }

	public required List<TransformData> Transformation { get; set; }

	public required List<string> Clip { get; set; }

	public required List<uint> Skin { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		Transformation = d2OClass.ReadFieldAsList<TransformData>(reader, static (c, r) => c.ReadFieldAsObject<TransformData>(r));
		Clip = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsString(r));
		Skin = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsList(writer, Transformation, static (c, r, v) => c.WriteFieldAsObject(r, v));
		d2OClass.WriteFieldAsList(writer, Clip, static (c, r, v) => c.WriteFieldAsString(r, v));
		d2OClass.WriteFieldAsList(writer, Skin, static (c, r, v) => c.WriteFieldAsUInt(r, v));
	}
}
