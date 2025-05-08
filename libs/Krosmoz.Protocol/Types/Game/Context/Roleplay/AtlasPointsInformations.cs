// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class AtlasPointsInformations : DofusType
{
	public new const ushort StaticProtocolId = 175;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static AtlasPointsInformations Empty =>
		new() { Type = 0, Coords = [] };

	public required sbyte Type { get; set; }

	public required IEnumerable<MapCoordinatesExtended> Coords { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(Type);
		var coordsBefore = writer.Position;
		var coordsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Coords)
		{
			item.Serialize(writer);
			coordsCount++;
		}
		var coordsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, coordsBefore);
		writer.WriteShort((short)coordsCount);
		writer.Seek(SeekOrigin.Begin, coordsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Type = reader.ReadSByte();
		var coordsCount = reader.ReadShort();
		var coords = new MapCoordinatesExtended[coordsCount];
		for (var i = 0; i < coordsCount; i++)
		{
			var entry = MapCoordinatesExtended.Empty;
			entry.Deserialize(reader);
			coords[i] = entry;
		}
		Coords = coords;
	}
}
