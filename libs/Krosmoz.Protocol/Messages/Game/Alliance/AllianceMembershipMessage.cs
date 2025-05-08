// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceMembershipMessage : AllianceJoinedMessage
{
	public new const uint StaticProtocolId = 6390;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new AllianceMembershipMessage Empty =>
		new() { Enabled = false, AllianceInfo = AllianceInformations.Empty };
}
