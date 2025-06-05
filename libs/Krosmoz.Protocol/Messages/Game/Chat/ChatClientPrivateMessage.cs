// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Chat;

public class ChatClientPrivateMessage : ChatAbstractClientMessage
{
	public new const uint StaticProtocolId = 851;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ChatClientPrivateMessage Empty =>
		new() { Content = string.Empty, Receiver = string.Empty };

	public required string Receiver { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfPrefixedLength16(Receiver);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Receiver = reader.ReadUtfPrefixedLength16();
	}
}
