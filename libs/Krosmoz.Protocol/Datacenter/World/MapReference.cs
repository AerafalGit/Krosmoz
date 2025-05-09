// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class MapReference : IDatacenterObject
{
	public static string ModuleName =>
		"MapReferences";

	public required int Id { get; set; }

	public required int MapId { get; set; }

	public required int CellId { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		MapId = d2OClass.Fields[1].AsInt(reader);
		CellId = d2OClass.Fields[2].AsInt(reader);
	}
}
