// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public class PartyMemberRemoveMessage : AbstractPartyEventMessage
{
	public new const uint StaticProtocolId = 5579;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyMemberRemoveMessage Empty =>
		new() { PartyId = 0, LeavingPlayerId = 0 };

	public required uint LeavingPlayerId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(LeavingPlayerId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		LeavingPlayerId = reader.ReadVarUInt32();
	}
}
