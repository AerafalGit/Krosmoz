// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context;

public class MapCoordinatesAndId : MapCoordinates
{
	public new const ushort StaticProtocolId = 392;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new MapCoordinatesAndId Empty =>
		new() { WorldY = 0, WorldX = 0, MapId = 0 };

	public required int MapId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(MapId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MapId = reader.ReadInt32();
	}
}
