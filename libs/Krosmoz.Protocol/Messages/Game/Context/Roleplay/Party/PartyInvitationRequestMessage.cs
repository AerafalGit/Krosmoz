// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public class PartyInvitationRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5585;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PartyInvitationRequestMessage Empty =>
		new() { Name = string.Empty };

	public required string Name { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Name);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Name = reader.ReadUtfPrefixedLength16();
	}
}
