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

	public required int Seq { get; set; }

	public required short LastPacketId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(Seq);
		writer.WriteShort(LastPacketId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Seq = reader.ReadInt();
		LastPacketId = reader.ReadShort();
	}
}
