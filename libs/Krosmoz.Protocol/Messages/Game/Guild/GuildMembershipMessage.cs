// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildMembershipMessage : GuildJoinedMessage
{
	public new const uint StaticProtocolId = 5835;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GuildMembershipMessage Empty =>
		new() { Enabled = false, MemberRights = 0, GuildInfo = GuildInformations.Empty };
}
