// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context;

public sealed class MapCoordinatesExtended : MapCoordinatesAndId
{
	public new const ushort StaticProtocolId = 176;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new MapCoordinatesExtended Empty =>
		new() { WorldY = 0, WorldX = 0, MapId = 0, SubAreaId = 0 };

	public required ushort SubAreaId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(SubAreaId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		SubAreaId = reader.ReadVarUInt16();
	}
}
