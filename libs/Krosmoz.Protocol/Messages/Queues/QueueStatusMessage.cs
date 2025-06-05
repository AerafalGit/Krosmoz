// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Queues;

public sealed class QueueStatusMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6100;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static QueueStatusMessage Empty =>
		new() { Position = 0, Total = 0 };

	public required ushort Position { get; set; }

	public required ushort Total { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt16(Position);
		writer.WriteUInt16(Total);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Position = reader.ReadUInt16();
		Total = reader.ReadUInt16();
	}
}
