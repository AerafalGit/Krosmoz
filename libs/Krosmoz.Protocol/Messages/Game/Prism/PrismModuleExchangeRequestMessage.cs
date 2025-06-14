// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Prism;

public sealed class PrismModuleExchangeRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6531;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PrismModuleExchangeRequestMessage Empty =>
		new();
}
