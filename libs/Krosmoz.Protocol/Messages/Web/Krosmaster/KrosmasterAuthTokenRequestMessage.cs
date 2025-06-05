// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Web.Krosmaster;

public sealed class KrosmasterAuthTokenRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6346;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static KrosmasterAuthTokenRequestMessage Empty =>
		new();
}
