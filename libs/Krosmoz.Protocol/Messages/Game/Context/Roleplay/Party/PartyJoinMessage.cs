// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyJoinMessage : AbstractPartyMessage
{
	public new const uint StaticProtocolId = 5576;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyJoinMessage Empty =>
		new() { PartyId = 0, PartyType = 0, PartyLeaderId = 0, MaxParticipants = 0, Members = [], Guests = [], Restricted = false, PartyName = string.Empty };

	public required sbyte PartyType { get; set; }

	public required uint PartyLeaderId { get; set; }

	public required sbyte MaxParticipants { get; set; }

	public required IEnumerable<PartyMemberInformations> Members { get; set; }

	public required IEnumerable<PartyGuestInformations> Guests { get; set; }

	public required bool Restricted { get; set; }

	public required string PartyName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(PartyType);
		writer.WriteVarUInt32(PartyLeaderId);
		writer.WriteInt8(MaxParticipants);
		var membersBefore = writer.Position;
		var membersCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Members)
		{
			writer.WriteUInt16(item.ProtocolId);
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
		writer.WriteBoolean(Restricted);
		writer.WriteUtfPrefixedLength16(PartyName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PartyType = reader.ReadInt8();
		PartyLeaderId = reader.ReadVarUInt32();
		MaxParticipants = reader.ReadInt8();
		var membersCount = reader.ReadInt16();
		var members = new PartyMemberInformations[membersCount];
		for (var i = 0; i < membersCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<PartyMemberInformations>(reader.ReadUInt16());
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
		Restricted = reader.ReadBoolean();
		PartyName = reader.ReadUtfPrefixedLength16();
	}
}
