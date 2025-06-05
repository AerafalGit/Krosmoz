// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeSellMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5778;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeSellMessage Empty =>
		new() { ObjectToSellId = 0, Quantity = 0 };

	public required uint ObjectToSellId { get; set; }

	public required uint Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(ObjectToSellId);
		writer.WriteVarUInt32(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectToSellId = reader.ReadVarUInt32();
		Quantity = reader.ReadVarUInt32();
	}
}
