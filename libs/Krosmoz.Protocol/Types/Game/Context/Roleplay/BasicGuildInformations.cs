// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Social;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public class BasicGuildInformations : AbstractSocialGroupInfos
{
	public new const ushort StaticProtocolId = 365;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new BasicGuildInformations Empty =>
		new() { GuildId = 0, GuildName = string.Empty };

	public required int GuildId { get; set; }

	public required string GuildName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(GuildId);
		writer.WriteUtfLengthPrefixed16(GuildName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		GuildId = reader.ReadInt();
		GuildName = reader.ReadUtfLengthPrefixed16();
	}
}
