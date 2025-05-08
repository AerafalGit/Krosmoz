// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyModifiableStatusMessage : AbstractPartyMessage
{
	public new const uint StaticProtocolId = 6277;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyModifiableStatusMessage Empty =>
		new() { PartyId = 0, Enabled = false };

	public required bool Enabled { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(Enabled);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Enabled = reader.ReadBoolean();
	}
}
