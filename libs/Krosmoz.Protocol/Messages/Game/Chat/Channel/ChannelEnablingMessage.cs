// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Chat.Channel;

public sealed class ChannelEnablingMessage : DofusMessage
{
	public new const uint StaticProtocolId = 890;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ChannelEnablingMessage Empty =>
		new() { Channel = 0, Enable = false };

	public required sbyte Channel { get; set; }

	public required bool Enable { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Channel);
		writer.WriteBoolean(Enable);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Channel = reader.ReadInt8();
		Enable = reader.ReadBoolean();
	}
}
