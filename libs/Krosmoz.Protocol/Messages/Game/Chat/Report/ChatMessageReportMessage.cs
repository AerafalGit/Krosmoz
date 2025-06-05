// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Chat.Report;

public sealed class ChatMessageReportMessage : DofusMessage
{
	public new const uint StaticProtocolId = 821;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ChatMessageReportMessage Empty =>
		new() { SenderName = string.Empty, Content = string.Empty, Timestamp = 0, Channel = 0, Fingerprint = string.Empty, Reason = 0 };

	public required string SenderName { get; set; }

	public required string Content { get; set; }

	public required int Timestamp { get; set; }

	public required sbyte Channel { get; set; }

	public required string Fingerprint { get; set; }

	public required sbyte Reason { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(SenderName);
		writer.WriteUtfPrefixedLength16(Content);
		writer.WriteInt32(Timestamp);
		writer.WriteInt8(Channel);
		writer.WriteUtfPrefixedLength16(Fingerprint);
		writer.WriteInt8(Reason);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SenderName = reader.ReadUtfPrefixedLength16();
		Content = reader.ReadUtfPrefixedLength16();
		Timestamp = reader.ReadInt32();
		Channel = reader.ReadInt8();
		Fingerprint = reader.ReadUtfPrefixedLength16();
		Reason = reader.ReadInt8();
	}
}
