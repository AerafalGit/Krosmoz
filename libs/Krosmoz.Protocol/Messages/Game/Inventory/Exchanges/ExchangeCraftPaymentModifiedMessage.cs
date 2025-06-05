// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeCraftPaymentModifiedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6578;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeCraftPaymentModifiedMessage Empty =>
		new() { GoldSum = 0 };

	public required uint GoldSum { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(GoldSum);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GoldSum = reader.ReadVarUInt32();
	}
}
