// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Approach;

public sealed class AuthenticationTicketMessage : DofusMessage
{
	public new const uint StaticProtocolId = 110;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AuthenticationTicketMessage Empty =>
		new() { Lang = string.Empty, Ticket = string.Empty };

	public required string Lang { get; set; }

	public required string Ticket { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Lang);
		writer.WriteUtfPrefixedLength16(Ticket);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Lang = reader.ReadUtfPrefixedLength16();
		Ticket = reader.ReadUtfPrefixedLength16();
	}
}
