// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory;

public sealed class ObjectAveragePricesMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6335;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectAveragePricesMessage Empty =>
		new() { Ids = [], AvgPrices = [] };

	public required IEnumerable<short> Ids { get; set; }

	public required IEnumerable<int> AvgPrices { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var idsBefore = writer.Position;
		var idsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Ids)
		{
			writer.WriteInt16(item);
			idsCount++;
		}
		var idsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, idsBefore);
		writer.WriteInt16((short)idsCount);
		writer.Seek(SeekOrigin.Begin, idsAfter);
		var avgPricesBefore = writer.Position;
		var avgPricesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in AvgPrices)
		{
			writer.WriteInt32(item);
			avgPricesCount++;
		}
		var avgPricesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, avgPricesBefore);
		writer.WriteInt16((short)avgPricesCount);
		writer.Seek(SeekOrigin.Begin, avgPricesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var idsCount = reader.ReadInt16();
		var ids = new short[idsCount];
		for (var i = 0; i < idsCount; i++)
		{
			ids[i] = reader.ReadInt16();
		}
		Ids = ids;
		var avgPricesCount = reader.ReadInt16();
		var avgPrices = new int[avgPricesCount];
		for (var i = 0; i < avgPricesCount; i++)
		{
			avgPrices[i] = reader.ReadInt32();
		}
		AvgPrices = avgPrices;
	}
}
