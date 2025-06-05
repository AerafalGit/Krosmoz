// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class WorldMap : IDatacenterObject
{
	public static string ModuleName =>
		"WorldMaps";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required int OrigineX { get; set; }

	public required int OrigineY { get; set; }

	public required double MapWidth { get; set; }

	public required double MapHeight { get; set; }

	public required int HorizontalChunck { get; set; }

	public required int VerticalChunck { get; set; }

	public required bool ViewableEverywhere { get; set; }

	public required double MinScale { get; set; }

	public required double MaxScale { get; set; }

	public required double StartScale { get; set; }

	public required int CenterX { get; set; }

	public required int CenterY { get; set; }

	public required int TotalWidth { get; set; }

	public required int TotalHeight { get; set; }

	public required List<string> Zoom { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		OrigineX = d2OClass.ReadFieldAsInt(reader);
		OrigineY = d2OClass.ReadFieldAsInt(reader);
		MapWidth = d2OClass.ReadFieldAsNumber(reader);
		MapHeight = d2OClass.ReadFieldAsNumber(reader);
		HorizontalChunck = d2OClass.ReadFieldAsInt(reader);
		VerticalChunck = d2OClass.ReadFieldAsInt(reader);
		ViewableEverywhere = d2OClass.ReadFieldAsBoolean(reader);
		MinScale = d2OClass.ReadFieldAsNumber(reader);
		MaxScale = d2OClass.ReadFieldAsNumber(reader);
		StartScale = d2OClass.ReadFieldAsNumber(reader);
		CenterX = d2OClass.ReadFieldAsInt(reader);
		CenterY = d2OClass.ReadFieldAsInt(reader);
		TotalWidth = d2OClass.ReadFieldAsInt(reader);
		TotalHeight = d2OClass.ReadFieldAsInt(reader);
		Zoom = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsString(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsInt(writer, OrigineX);
		d2OClass.WriteFieldAsInt(writer, OrigineY);
		d2OClass.WriteFieldAsNumber(writer, MapWidth);
		d2OClass.WriteFieldAsNumber(writer, MapHeight);
		d2OClass.WriteFieldAsInt(writer, HorizontalChunck);
		d2OClass.WriteFieldAsInt(writer, VerticalChunck);
		d2OClass.WriteFieldAsBoolean(writer, ViewableEverywhere);
		d2OClass.WriteFieldAsNumber(writer, MinScale);
		d2OClass.WriteFieldAsNumber(writer, MaxScale);
		d2OClass.WriteFieldAsNumber(writer, StartScale);
		d2OClass.WriteFieldAsInt(writer, CenterX);
		d2OClass.WriteFieldAsInt(writer, CenterY);
		d2OClass.WriteFieldAsInt(writer, TotalWidth);
		d2OClass.WriteFieldAsInt(writer, TotalHeight);
		d2OClass.WriteFieldAsList(writer, Zoom, static (c, r, v) => c.WriteFieldAsString(r, v));
	}
}
