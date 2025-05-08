// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Chat;

public class ChatAbstractServerMessage : DofusMessage
{
	public new const uint StaticProtocolId = 880;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ChatAbstractServerMessage Empty =>
		new() { Channel = 0, Content = string.Empty, Timestamp = 0, Fingerprint = string.Empty };

	public required sbyte Channel { get; set; }

	public required string Content { get; set; }

	public required int Timestamp { get; set; }

	public required string Fingerprint { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(Channel);
		writer.WriteUtfLengthPrefixed16(Content);
		writer.WriteInt(Timestamp);
		writer.WriteUtfLengthPrefixed16(Fingerprint);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Channel = reader.ReadSByte();
		Content = reader.ReadUtfLengthPrefixed16();
		Timestamp = reader.ReadInt();
		Fingerprint = reader.ReadUtfLengthPrefixed16();
	}
}
