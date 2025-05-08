// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeItemObjectAddAsPaymentMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5766;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeItemObjectAddAsPaymentMessage Empty =>
		new() { PaymentType = 0, BAdd = false, ObjectToMoveId = 0, Quantity = 0 };

	public required sbyte PaymentType { get; set; }

	public required bool BAdd { get; set; }

	public required int ObjectToMoveId { get; set; }

	public required int Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(PaymentType);
		writer.WriteBoolean(BAdd);
		writer.WriteInt(ObjectToMoveId);
		writer.WriteInt(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PaymentType = reader.ReadSByte();
		BAdd = reader.ReadBoolean();
		ObjectToMoveId = reader.ReadInt();
		Quantity = reader.ReadInt();
	}
}
