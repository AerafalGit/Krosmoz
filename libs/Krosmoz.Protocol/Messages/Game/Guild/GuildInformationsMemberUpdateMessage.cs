// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Guild;

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildInformationsMemberUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5597;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildInformationsMemberUpdateMessage Empty =>
		new() { Member = GuildMember.Empty };

	public required GuildMember Member { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Member.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Member = GuildMember.Empty;
		Member.Deserialize(reader);
	}
}
