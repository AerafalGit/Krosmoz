// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeClearPaymentForCraftMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6145;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeClearPaymentForCraftMessage Empty =>
		new() { PaymentType = 0 };

	public required sbyte PaymentType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(PaymentType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PaymentType = reader.ReadSByte();
	}
}
