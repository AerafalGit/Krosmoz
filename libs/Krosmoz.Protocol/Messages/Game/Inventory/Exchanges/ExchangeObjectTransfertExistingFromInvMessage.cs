// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeObjectTransfertExistingFromInvMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6325;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeObjectTransfertExistingFromInvMessage Empty =>
		new();
}
