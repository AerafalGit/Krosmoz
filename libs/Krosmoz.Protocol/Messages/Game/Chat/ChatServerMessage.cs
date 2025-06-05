// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Chat;

public class ChatServerMessage : ChatAbstractServerMessage
{
	public new const uint StaticProtocolId = 881;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ChatServerMessage Empty =>
		new() { Fingerprint = string.Empty, Timestamp = 0, Content = string.Empty, Channel = 0, SenderId = 0, SenderName = string.Empty, SenderAccountId = 0 };

	public required int SenderId { get; set; }

	public required string SenderName { get; set; }

	public required int SenderAccountId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(SenderId);
		writer.WriteUtfPrefixedLength16(SenderName);
		writer.WriteInt32(SenderAccountId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		SenderId = reader.ReadInt32();
		SenderName = reader.ReadUtfPrefixedLength16();
		SenderAccountId = reader.ReadInt32();
	}
}
