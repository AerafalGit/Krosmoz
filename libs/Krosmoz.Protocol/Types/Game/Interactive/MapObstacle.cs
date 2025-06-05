// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Interactive;

public sealed class MapObstacle : DofusType
{
	public new const ushort StaticProtocolId = 200;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static MapObstacle Empty =>
		new() { ObstacleCellId = 0, State = 0 };

	public required ushort ObstacleCellId { get; set; }

	public required sbyte State { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ObstacleCellId);
		writer.WriteInt8(State);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObstacleCellId = reader.ReadVarUInt16();
		State = reader.ReadInt8();
	}
}
