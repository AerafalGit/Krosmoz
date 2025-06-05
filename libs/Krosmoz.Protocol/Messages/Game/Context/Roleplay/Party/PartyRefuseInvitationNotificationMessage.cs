// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyRefuseInvitationNotificationMessage : AbstractPartyEventMessage
{
	public new const uint StaticProtocolId = 5596;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyRefuseInvitationNotificationMessage Empty =>
		new() { PartyId = 0, GuestId = 0 };

	public required uint GuestId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(GuestId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		GuestId = reader.ReadVarUInt32();
	}
}
