// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Actions.Fight;

public sealed class GameActionMarkedCell : DofusType
{
	public new const ushort StaticProtocolId = 85;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static GameActionMarkedCell Empty =>
		new() { CellId = 0, ZoneSize = 0, CellColor = 0, CellsType = 0 };

	public required ushort CellId { get; set; }

	public required sbyte ZoneSize { get; set; }

	public required int CellColor { get; set; }

	public required sbyte CellsType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(CellId);
		writer.WriteInt8(ZoneSize);
		writer.WriteInt32(CellColor);
		writer.WriteInt8(CellsType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CellId = reader.ReadVarUInt16();
		ZoneSize = reader.ReadInt8();
		CellColor = reader.ReadInt32();
		CellsType = reader.ReadInt8();
	}
}
