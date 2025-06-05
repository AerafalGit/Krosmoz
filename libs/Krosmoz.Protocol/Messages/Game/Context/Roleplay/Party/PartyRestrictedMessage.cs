// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyRestrictedMessage : AbstractPartyMessage
{
	public new const uint StaticProtocolId = 6175;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyRestrictedMessage Empty =>
		new() { PartyId = 0, Restricted = false };

	public required bool Restricted { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(Restricted);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Restricted = reader.ReadBoolean();
	}
}
