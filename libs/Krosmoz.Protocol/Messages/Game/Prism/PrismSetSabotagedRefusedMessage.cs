// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Prism;

public sealed class PrismSetSabotagedRefusedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6466;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PrismSetSabotagedRefusedMessage Empty =>
		new() { SubAreaId = 0, Reason = 0 };

	public required ushort SubAreaId { get; set; }

	public required sbyte Reason { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(SubAreaId);
		writer.WriteInt8(Reason);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SubAreaId = reader.ReadVarUInt16();
		Reason = reader.ReadInt8();
	}
}
