// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Chat;

public class ChatClientMultiMessage : ChatAbstractClientMessage
{
	public new const uint StaticProtocolId = 861;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ChatClientMultiMessage Empty =>
		new() { Content = string.Empty, Channel = 0 };

	public required sbyte Channel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(Channel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Channel = reader.ReadInt8();
	}
}
