// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeMountsStableBornAddMessage : ExchangeMountsStableAddMessage
{
	public new const uint StaticProtocolId = 6557;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeMountsStableBornAddMessage Empty =>
		new() { MountDescription = [] };
}
