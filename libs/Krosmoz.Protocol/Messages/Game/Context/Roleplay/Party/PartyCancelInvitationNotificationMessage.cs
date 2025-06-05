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

	public required uint CancelerId { get; set; }

	public required uint GuestId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(CancelerId);
		writer.WriteVarUInt32(GuestId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CancelerId = reader.ReadVarUInt32();
		GuestId = reader.ReadVarUInt32();
	}
}
