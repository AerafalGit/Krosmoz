// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeCraftPaymentModificationRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6579;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeCraftPaymentModificationRequestMessage Empty =>
		new() { Quantity = 0 };

	public required uint Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Quantity = reader.ReadVarUInt32();
	}
}
