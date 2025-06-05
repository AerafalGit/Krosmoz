// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class MapScrollAction : IDatacenterObject
{
	public static string ModuleName =>
		"MapScrollActions";

	public required int Id { get; set; }

	public required bool RightExists { get; set; }

	public required bool BottomExists { get; set; }

	public required bool LeftExists { get; set; }

	public required bool TopExists { get; set; }

	public required int RightMapId { get; set; }

	public required int BottomMapId { get; set; }

	public required int LeftMapId { get; set; }

	public required int TopMapId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		RightExists = d2OClass.ReadFieldAsBoolean(reader);
		BottomExists = d2OClass.ReadFieldAsBoolean(reader);
		LeftExists = d2OClass.ReadFieldAsBoolean(reader);
		TopExists = d2OClass.ReadFieldAsBoolean(reader);
		RightMapId = d2OClass.ReadFieldAsInt(reader);
		BottomMapId = d2OClass.ReadFieldAsInt(reader);
		LeftMapId = d2OClass.ReadFieldAsInt(reader);
		TopMapId = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsBoolean(writer, RightExists);
		d2OClass.WriteFieldAsBoolean(writer, BottomExists);
		d2OClass.WriteFieldAsBoolean(writer, LeftExists);
		d2OClass.WriteFieldAsBoolean(writer, TopExists);
		d2OClass.WriteFieldAsInt(writer, RightMapId);
		d2OClass.WriteFieldAsInt(writer, BottomMapId);
		d2OClass.WriteFieldAsInt(writer, LeftMapId);
		d2OClass.WriteFieldAsInt(writer, TopMapId);
	}
}
