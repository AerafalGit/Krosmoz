// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyInvitationDetailsRequestMessage : AbstractPartyMessage
{
	public new const uint StaticProtocolId = 6264;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyInvitationDetailsRequestMessage Empty =>
		new() { PartyId = 0 };
}
