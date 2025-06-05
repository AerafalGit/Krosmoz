// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeShopStockMovementRemovedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5907;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeShopStockMovementRemovedMessage Empty =>
		new() { ObjectId = 0 };

	public required uint ObjectId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(ObjectId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectId = reader.ReadVarUInt32();
	}
}
