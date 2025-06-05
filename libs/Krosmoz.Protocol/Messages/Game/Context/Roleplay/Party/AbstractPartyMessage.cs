// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public class AbstractPartyMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6274;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AbstractPartyMessage Empty =>
		new() { PartyId = 0 };

	public required uint PartyId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(PartyId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PartyId = reader.ReadVarUInt32();
	}
}
