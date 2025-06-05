// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public class ExchangeBidPriceMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5755;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeBidPriceMessage Empty =>
		new() { GenericId = 0, AveragePrice = 0 };

	public required ushort GenericId { get; set; }

	public required int AveragePrice { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(GenericId);
		writer.WriteVarInt32(AveragePrice);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GenericId = reader.ReadVarUInt16();
		AveragePrice = reader.ReadVarInt32();
	}
}
