// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyLeaderUpdateMessage : AbstractPartyEventMessage
{
	public new const uint StaticProtocolId = 5578;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyLeaderUpdateMessage Empty =>
		new() { PartyId = 0, PartyLeaderId = 0 };

	public required uint PartyLeaderId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(PartyLeaderId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PartyLeaderId = reader.ReadVarUInt32();
	}
}
