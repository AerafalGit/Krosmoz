// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeBidHouseItemAddOkMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5945;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeBidHouseItemAddOkMessage Empty =>
		new() { ItemInfo = ObjectItemToSellInBid.Empty };

	public required ObjectItemToSellInBid ItemInfo { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		ItemInfo.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ItemInfo = ObjectItemToSellInBid.Empty;
		ItemInfo.Deserialize(reader);
	}
}
