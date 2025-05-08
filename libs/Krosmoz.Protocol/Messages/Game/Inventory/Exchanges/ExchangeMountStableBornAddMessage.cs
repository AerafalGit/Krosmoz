// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Mount;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeMountStableBornAddMessage : ExchangeMountStableAddMessage
{
	public new const uint StaticProtocolId = 5966;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeMountStableBornAddMessage Empty =>
		new() { MountDescription = MountClientData.Empty };
}
