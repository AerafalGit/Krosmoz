// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyInvitationArenaRequestMessage : PartyInvitationRequestMessage
{
	public new const uint StaticProtocolId = 6283;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyInvitationArenaRequestMessage Empty =>
		new() { Name = string.Empty };
}
