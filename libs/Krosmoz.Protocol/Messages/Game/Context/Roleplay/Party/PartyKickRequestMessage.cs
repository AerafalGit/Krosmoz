// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyKickRequestMessage : AbstractPartyMessage
{
	public new const uint StaticProtocolId = 5592;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyKickRequestMessage Empty =>
		new() { PartyId = 0, PlayerId = 0 };

	public required uint PlayerId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(PlayerId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PlayerId = reader.ReadVarUInt32();
	}
}
