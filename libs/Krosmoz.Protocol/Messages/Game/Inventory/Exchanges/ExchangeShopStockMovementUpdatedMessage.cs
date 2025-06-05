// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeShopStockMovementUpdatedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5909;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeShopStockMovementUpdatedMessage Empty =>
		new() { ObjectInfo = ObjectItemToSell.Empty };

	public required ObjectItemToSell ObjectInfo { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		ObjectInfo.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectInfo = ObjectItemToSell.Empty;
		ObjectInfo.Deserialize(reader);
	}
}
