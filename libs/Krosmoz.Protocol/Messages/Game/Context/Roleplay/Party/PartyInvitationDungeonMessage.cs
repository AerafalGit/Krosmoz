// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyInvitationDungeonMessage : PartyInvitationMessage
{
	public new const uint StaticProtocolId = 6244;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyInvitationDungeonMessage Empty =>
		new() { PartyId = 0, ToId = 0, FromName = string.Empty, FromId = 0, MaxParticipants = 0, PartyName = string.Empty, PartyType = 0, DungeonId = 0 };

	public required ushort DungeonId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(DungeonId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		DungeonId = reader.ReadVarUInt16();
	}
}
