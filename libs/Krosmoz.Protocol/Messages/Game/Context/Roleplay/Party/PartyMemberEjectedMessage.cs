// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyMemberEjectedMessage : PartyMemberRemoveMessage
{
	public new const uint StaticProtocolId = 6252;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyMemberEjectedMessage Empty =>
		new() { PartyId = 0, LeavingPlayerId = 0, KickerId = 0 };

	public required uint KickerId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(KickerId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		KickerId = reader.ReadVarUInt32();
	}
}
