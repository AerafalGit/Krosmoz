// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyNewGuestMessage : AbstractPartyEventMessage
{
	public new const uint StaticProtocolId = 6260;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyNewGuestMessage Empty =>
		new() { PartyId = 0, Guest = PartyGuestInformations.Empty };

	public required PartyGuestInformations Guest { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		Guest.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Guest = PartyGuestInformations.Empty;
		Guest.Deserialize(reader);
	}
}
