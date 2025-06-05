// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Basic;

public sealed class BasicAckMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6362;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static BasicAckMessage Empty =>
		new() { Seq = 0, LastPacketId = 0 };

	public required uint Seq { get; set; }

	public required ushort LastPacketId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(Seq);
		writer.WriteVarUInt16(LastPacketId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Seq = reader.ReadVarUInt32();
		LastPacketId = reader.ReadVarUInt16();
	}
}
