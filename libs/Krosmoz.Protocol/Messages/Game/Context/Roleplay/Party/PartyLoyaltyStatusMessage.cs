// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyLoyaltyStatusMessage : AbstractPartyMessage
{
	public new const uint StaticProtocolId = 6270;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyLoyaltyStatusMessage Empty =>
		new() { PartyId = 0, Loyal = false };

	public required bool Loyal { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(Loyal);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Loyal = reader.ReadBoolean();
	}
}
