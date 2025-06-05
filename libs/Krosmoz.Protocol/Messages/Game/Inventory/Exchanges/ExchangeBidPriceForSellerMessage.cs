// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeBidPriceForSellerMessage : ExchangeBidPriceMessage
{
	public new const uint StaticProtocolId = 6464;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeBidPriceForSellerMessage Empty =>
		new() { AveragePrice = 0, GenericId = 0, AllIdentical = false, MinimalPrices = [] };

	public required bool AllIdentical { get; set; }

	public required IEnumerable<uint> MinimalPrices { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(AllIdentical);
		var minimalPricesBefore = writer.Position;
		var minimalPricesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in MinimalPrices)
		{
			writer.WriteVarUInt32(item);
			minimalPricesCount++;
		}
		var minimalPricesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, minimalPricesBefore);
		writer.WriteInt16((short)minimalPricesCount);
		writer.Seek(SeekOrigin.Begin, minimalPricesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		AllIdentical = reader.ReadBoolean();
		var minimalPricesCount = reader.ReadInt16();
		var minimalPrices = new uint[minimalPricesCount];
		for (var i = 0; i < minimalPricesCount; i++)
		{
			minimalPrices[i] = reader.ReadVarUInt32();
		}
		MinimalPrices = minimalPrices;
	}
}
