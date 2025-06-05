// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context;

public class MapCoordinates : DofusType
{
	public new const ushort StaticProtocolId = 174;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static MapCoordinates Empty =>
		new() { WorldX = 0, WorldY = 0 };

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
	}
}
