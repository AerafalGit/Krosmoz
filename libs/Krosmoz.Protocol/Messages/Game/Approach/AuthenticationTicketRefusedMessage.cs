// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Approach;

public sealed class AuthenticationTicketRefusedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 112;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AuthenticationTicketRefusedMessage Empty =>
		new();
}
