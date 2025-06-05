// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeStartedBidBuyerMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5904;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeStartedBidBuyerMessage Empty =>
		new() { BuyerDescriptor = SellerBuyerDescriptor.Empty };

	public required SellerBuyerDescriptor BuyerDescriptor { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		BuyerDescriptor.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		BuyerDescriptor = SellerBuyerDescriptor.Empty;
		BuyerDescriptor.Deserialize(reader);
	}
}
