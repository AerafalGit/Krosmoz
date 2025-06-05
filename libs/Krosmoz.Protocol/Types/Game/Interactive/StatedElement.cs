// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Interactive;

public sealed class StatedElement : DofusType
{
	public new const ushort StaticProtocolId = 108;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static StatedElement Empty =>
		new() { ElementId = 0, ElementCellId = 0, ElementState = 0 };

	public required int ElementId { get; set; }

	public required ushort ElementCellId { get; set; }

	public required uint ElementState { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(ElementId);
		writer.WriteVarUInt16(ElementCellId);
		writer.WriteVarUInt32(ElementState);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ElementId = reader.ReadInt32();
		ElementCellId = reader.ReadVarUInt16();
		ElementState = reader.ReadVarUInt32();
	}
}
