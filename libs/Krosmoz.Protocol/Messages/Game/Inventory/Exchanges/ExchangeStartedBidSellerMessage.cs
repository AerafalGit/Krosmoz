// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeStartedBidSellerMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5905;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeStartedBidSellerMessage Empty =>
		new() { SellerDescriptor = SellerBuyerDescriptor.Empty, ObjectsInfos = [] };

	public required SellerBuyerDescriptor SellerDescriptor { get; set; }

	public required IEnumerable<ObjectItemToSellInBid> ObjectsInfos { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		SellerDescriptor.Serialize(writer);
		var objectsInfosBefore = writer.Position;
		var objectsInfosCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ObjectsInfos)
		{
			item.Serialize(writer);
			objectsInfosCount++;
		}
		var objectsInfosAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectsInfosBefore);
		writer.WriteInt16((short)objectsInfosCount);
		writer.Seek(SeekOrigin.Begin, objectsInfosAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SellerDescriptor = SellerBuyerDescriptor.Empty;
		SellerDescriptor.Deserialize(reader);
		var objectsInfosCount = reader.ReadInt16();
		var objectsInfos = new ObjectItemToSellInBid[objectsInfosCount];
		for (var i = 0; i < objectsInfosCount; i++)
		{
			var entry = ObjectItemToSellInBid.Empty;
			entry.Deserialize(reader);
			objectsInfos[i] = entry;
		}
		ObjectsInfos = objectsInfos;
	}
}
