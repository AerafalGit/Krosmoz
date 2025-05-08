// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyInvitationCancelledForGuestMessage : AbstractPartyMessage
{
	public new const uint StaticProtocolId = 6256;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyInvitationCancelledForGuestMessage Empty =>
		new() { PartyId = 0, CancelerId = 0 };

	public required int CancelerId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(CancelerId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CancelerId = reader.ReadInt();
	}
}
