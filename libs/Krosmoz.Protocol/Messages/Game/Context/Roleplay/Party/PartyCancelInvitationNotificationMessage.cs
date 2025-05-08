// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyCancelInvitationNotificationMessage : AbstractPartyEventMessage
{
	public new const uint StaticProtocolId = 6251;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyCancelInvitationNotificationMessage Empty =>
		new() { PartyId = 0, CancelerId = 0, GuestId = 0 };

	public required int CancelerId { get; set; }

	public required int GuestId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(CancelerId);
		writer.WriteInt(GuestId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CancelerId = reader.ReadInt();
		GuestId = reader.ReadInt();
	}
}
