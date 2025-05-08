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

	public required int ObjectToSellId { get; set; }

	public required int Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(ObjectToSellId);
		writer.WriteInt(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectToSellId = reader.ReadInt();
		Quantity = reader.ReadInt();
	}
}
