// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Flash.Geom;

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class Area : IDatacenterObject
{
	public static string ModuleName =>
		"Areas";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int SuperAreaId { get; set; }

	public required bool ContainHouses { get; set; }

	public required bool ContainPaddocks { get; set; }

	public required Rectangle Bounds { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		NameId = d2OClass.Fields[1].AsI18N(reader);
		SuperAreaId = d2OClass.Fields[2].AsInt(reader);
		ContainHouses = d2OClass.Fields[3].AsBoolean(reader);
		ContainPaddocks = d2OClass.Fields[4].AsBoolean(reader);
		Bounds = d2OClass.Fields[5].AsObject<Rectangle>(reader);
	}
}
