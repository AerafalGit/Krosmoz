// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeItemGoldAddAsPaymentMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5770;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeItemGoldAddAsPaymentMessage Empty =>
		new() { PaymentType = 0, Quantity = 0 };

	public required sbyte PaymentType { get; set; }

	public required int Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(PaymentType);
		writer.WriteInt(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PaymentType = reader.ReadSByte();
		Quantity = reader.ReadInt();
	}
}
