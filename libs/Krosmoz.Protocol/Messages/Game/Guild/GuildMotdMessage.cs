// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildMotdMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6590;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildMotdMessage Empty =>
		new() { Content = string.Empty, Timestamp = 0 };

	public required string Content { get; set; }

	public required int Timestamp { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Content);
		writer.WriteInt32(Timestamp);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Content = reader.ReadUtfPrefixedLength16();
		Timestamp = reader.ReadInt32();
	}
}
