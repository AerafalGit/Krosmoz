// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Messages.Game.Guild;

public class GuildJoinedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5564;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildJoinedMessage Empty =>
		new() { GuildInfo = GuildInformations.Empty, MemberRights = 0, Enabled = false };

	public required GuildInformations GuildInfo { get; set; }

	public required uint MemberRights { get; set; }

	public required bool Enabled { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		GuildInfo.Serialize(writer);
		writer.WriteVarUInt32(MemberRights);
		writer.WriteBoolean(Enabled);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GuildInfo = GuildInformations.Empty;
		GuildInfo.Deserialize(reader);
		MemberRights = reader.ReadVarUInt32();
		Enabled = reader.ReadBoolean();
	}
}
