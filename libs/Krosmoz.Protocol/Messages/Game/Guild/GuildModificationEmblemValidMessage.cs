// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Guild;

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildModificationEmblemValidMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6328;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildModificationEmblemValidMessage Empty =>
		new() { GuildEmblem = GuildEmblem.Empty };

	public required GuildEmblem GuildEmblem { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		GuildEmblem.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GuildEmblem = GuildEmblem.Empty;
		GuildEmblem.Deserialize(reader);
	}
}
