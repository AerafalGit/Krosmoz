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
		new() { PartyId = 0, PartyType = 0, PartyLeaderId = 0, MaxParticipants = 0, Members = [], Guests = [], Restricted = false };

	public required sbyte PartyType { get; set; }

	public required int PartyLeaderId { get; set; }

	public required sbyte MaxParticipants { get; set; }

	public required IEnumerable<PartyMemberInformations> Members { get; set; }

	public required IEnumerable<PartyGuestInformations> Guests { get; set; }

	public required bool Restricted { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteSByte(PartyType);
		writer.WriteInt(PartyLeaderId);
		writer.WriteSByte(MaxParticipants);
		var membersBefore = writer.Position;
		var membersCount = 0;
		writer.WriteShort(0);
		foreach (var item in Members)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			membersCount++;
		}
		var membersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, membersBefore);
		writer.WriteShort((short)membersCount);
		writer.Seek(SeekOrigin.Begin, membersAfter);
		var guestsBefore = writer.Position;
		var guestsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Guests)
		{
			item.Serialize(writer);
			guestsCount++;
		}
		var guestsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, guestsBefore);
		writer.WriteShort((short)guestsCount);
		writer.Seek(SeekOrigin.Begin, guestsAfter);
		writer.WriteBoolean(Restricted);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PartyType = reader.ReadSByte();
		PartyLeaderId = reader.ReadInt();
		MaxParticipants = reader.ReadSByte();
		var membersCount = reader.ReadShort();
		var members = new PartyMemberInformations[membersCount];
		for (var i = 0; i < membersCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<PartyMemberInformations>(reader.ReadUShort());
			entry.Deserialize(reader);
			members[i] = entry;
		}
		Members = members;
		var guestsCount = reader.ReadShort();
		var guests = new PartyGuestInformations[guestsCount];
		for (var i = 0; i < guestsCount; i++)
		{
			var entry = PartyGuestInformations.Empty;
			entry.Deserialize(reader);
			guests[i] = entry;
		}
		Guests = guests;
		Restricted = reader.ReadBoolean();
	}
}
