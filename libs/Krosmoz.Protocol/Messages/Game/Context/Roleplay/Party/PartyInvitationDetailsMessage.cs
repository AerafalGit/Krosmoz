// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public class PartyInvitationDetailsMessage : AbstractPartyMessage
{
	public new const uint StaticProtocolId = 6263;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyInvitationDetailsMessage Empty =>
		new() { PartyId = 0, PartyType = 0, PartyName = string.Empty, FromId = 0, FromName = string.Empty, LeaderId = 0, Members = [], Guests = [] };

	public required sbyte PartyType { get; set; }

	public required string PartyName { get; set; }

	public required uint FromId { get; set; }

	public required string FromName { get; set; }

	public required uint LeaderId { get; set; }

	public required IEnumerable<PartyInvitationMemberInformations> Members { get; set; }

	public required IEnumerable<PartyGuestInformations> Guests { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(PartyType);
		writer.WriteUtfPrefixedLength16(PartyName);
		writer.WriteVarUInt32(FromId);
		writer.WriteUtfPrefixedLength16(FromName);
		writer.WriteVarUInt32(LeaderId);
		var membersBefore = writer.Position;
		var membersCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Members)
		{
			item.Serialize(writer);
			membersCount++;
		}
		var membersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, membersBefore);
		writer.WriteInt16((short)membersCount);
		writer.Seek(SeekOrigin.Begin, membersAfter);
		var guestsBefore = writer.Position;
		var guestsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Guests)
		{
			item.Serialize(writer);
			guestsCount++;
		}
		var guestsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, guestsBefore);
		writer.WriteInt16((short)guestsCount);
		writer.Seek(SeekOrigin.Begin, guestsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PartyType = reader.ReadInt8();
		PartyName = reader.ReadUtfPrefixedLength16();
		FromId = reader.ReadVarUInt32();
		FromName = reader.ReadUtfPrefixedLength16();
		LeaderId = reader.ReadVarUInt32();
		var membersCount = reader.ReadInt16();
		var members = new PartyInvitationMemberInformations[membersCount];
		for (var i = 0; i < membersCount; i++)
		{
			var entry = PartyInvitationMemberInformations.Empty;
			entry.Deserialize(reader);
			members[i] = entry;
		}
		Members = members;
		var guestsCount = reader.ReadInt16();
		var guests = new PartyGuestInformations[guestsCount];
		for (var i = 0; i < guestsCount; i++)
		{
			var entry = PartyGuestInformations.Empty;
			entry.Deserialize(reader);
			guests[i] = entry;
		}
		Guests = guests;
	}
}
