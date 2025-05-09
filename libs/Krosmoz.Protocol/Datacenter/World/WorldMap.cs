// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class WorldMap : IDatacenterObject<WorldMap>
{
	public static string ModuleName =>
		"WorldMaps";

	public required int Id { get; set; }

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

	public static WorldMap Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new WorldMap
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			OrigineX = d2OClass.Fields[1].AsInt(reader),
			OrigineY = d2OClass.Fields[2].AsInt(reader),
			MapWidth = d2OClass.Fields[3].AsDouble(reader),
			MapHeight = d2OClass.Fields[4].AsDouble(reader),
			HorizontalChunck = d2OClass.Fields[5].AsInt(reader),
			VerticalChunck = d2OClass.Fields[6].AsInt(reader),
			ViewableEverywhere = d2OClass.Fields[7].AsBoolean(reader),
			MinScale = d2OClass.Fields[8].AsDouble(reader),
			MaxScale = d2OClass.Fields[9].AsDouble(reader),
			StartScale = d2OClass.Fields[10].AsDouble(reader),
			CenterX = d2OClass.Fields[11].AsInt(reader),
			CenterY = d2OClass.Fields[12].AsInt(reader),
			TotalWidth = d2OClass.Fields[13].AsInt(reader),
			TotalHeight = d2OClass.Fields[14].AsInt(reader),
			Zoom = d2OClass.Fields[15].AsList<string>(reader, static (field, r) => field.AsString(r)),
		};
	}
}
