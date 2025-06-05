// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Guild;

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildModificationValidMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6323;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildModificationValidMessage Empty =>
		new() { GuildName = string.Empty, GuildEmblem = GuildEmblem.Empty };

	public required string GuildName { get; set; }

	public required GuildEmblem GuildEmblem { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(GuildName);
		GuildEmblem.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GuildName = reader.ReadUtfPrefixedLength16();
		GuildEmblem = GuildEmblem.Empty;
		GuildEmblem.Deserialize(reader);
	}
}
