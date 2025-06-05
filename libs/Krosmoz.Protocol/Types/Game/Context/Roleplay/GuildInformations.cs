// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Guild;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public class GuildInformations : BasicGuildInformations
{
	public new const ushort StaticProtocolId = 127;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GuildInformations Empty =>
		new() { GuildName = string.Empty, GuildId = 0, GuildEmblem = GuildEmblem.Empty };

	public required GuildEmblem GuildEmblem { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		GuildEmblem.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		GuildEmblem = GuildEmblem.Empty;
		GuildEmblem.Deserialize(reader);
	}
}
