// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyNewMemberMessage : PartyUpdateMessage
{
	public new const uint StaticProtocolId = 6306;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyNewMemberMessage Empty =>
		new() { PartyId = 0, MemberInformations = PartyMemberInformations.Empty };
}
