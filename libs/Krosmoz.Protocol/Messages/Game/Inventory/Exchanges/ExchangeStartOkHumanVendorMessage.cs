// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeStartOkHumanVendorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5767;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeStartOkHumanVendorMessage Empty =>
		new() { SellerId = 0, ObjectsInfos = [] };

	public required uint SellerId { get; set; }

	public required IEnumerable<ObjectItemToSellInHumanVendorShop> ObjectsInfos { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(SellerId);
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
		SellerId = reader.ReadVarUInt32();
		var objectsInfosCount = reader.ReadInt16();
		var objectsInfos = new ObjectItemToSellInHumanVendorShop[objectsInfosCount];
		for (var i = 0; i < objectsInfosCount; i++)
		{
			var entry = ObjectItemToSellInHumanVendorShop.Empty;
			entry.Deserialize(reader);
			objectsInfos[i] = entry;
		}
		ObjectsInfos = objectsInfos;
	}
}
