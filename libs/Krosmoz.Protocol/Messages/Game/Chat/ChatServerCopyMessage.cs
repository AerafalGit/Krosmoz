// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Chat;

public class ChatServerCopyMessage : ChatAbstractServerMessage
{
	public new const uint StaticProtocolId = 882;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ChatServerCopyMessage Empty =>
		new() { Fingerprint = string.Empty, Timestamp = 0, Content = string.Empty, Channel = 0, ReceiverId = 0, ReceiverName = string.Empty };

	public required uint ReceiverId { get; set; }

	public required string ReceiverName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(ReceiverId);
		writer.WriteUtfPrefixedLength16(ReceiverName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ReceiverId = reader.ReadVarUInt32();
		ReceiverName = reader.ReadUtfPrefixedLength16();
	}
}
