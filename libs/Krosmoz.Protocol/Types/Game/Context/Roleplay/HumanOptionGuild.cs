// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class HumanOptionGuild : HumanOption
{
	public new const ushort StaticProtocolId = 409;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new HumanOptionGuild Empty =>
		new() { GuildInformations = GuildInformations.Empty };

	public required GuildInformations GuildInformations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		GuildInformations.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		GuildInformations = GuildInformations.Empty;
		GuildInformations.Deserialize(reader);
	}
}
