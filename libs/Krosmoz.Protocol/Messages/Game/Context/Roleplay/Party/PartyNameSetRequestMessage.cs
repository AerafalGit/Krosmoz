// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyNameSetRequestMessage : AbstractPartyMessage
{
	public new const uint StaticProtocolId = 6503;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyNameSetRequestMessage Empty =>
		new() { PartyId = 0, PartyName = string.Empty };

	public required string PartyName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfPrefixedLength16(PartyName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PartyName = reader.ReadUtfPrefixedLength16();
	}
}
