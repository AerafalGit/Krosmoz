// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeBidHouseInListUpdatedMessage : ExchangeBidHouseInListAddedMessage
{
	public new const uint StaticProtocolId = 6337;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeBidHouseInListUpdatedMessage Empty =>
		new() { Prices = [], Effects = [], ObjGenericId = 0, ItemUID = 0 };
}
