// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeTypesItemsExchangerDescriptionForUserMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5752;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeTypesItemsExchangerDescriptionForUserMessage Empty =>
		new() { ItemTypeDescriptions = [] };

	public required IEnumerable<BidExchangerObjectInfo> ItemTypeDescriptions { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var itemTypeDescriptionsBefore = writer.Position;
		var itemTypeDescriptionsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ItemTypeDescriptions)
		{
			item.Serialize(writer);
			itemTypeDescriptionsCount++;
		}
		var itemTypeDescriptionsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, itemTypeDescriptionsBefore);
		writer.WriteInt16((short)itemTypeDescriptionsCount);
		writer.Seek(SeekOrigin.Begin, itemTypeDescriptionsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var itemTypeDescriptionsCount = reader.ReadInt16();
		var itemTypeDescriptions = new BidExchangerObjectInfo[itemTypeDescriptionsCount];
		for (var i = 0; i < itemTypeDescriptionsCount; i++)
		{
			var entry = BidExchangerObjectInfo.Empty;
			entry.Deserialize(reader);
			itemTypeDescriptions[i] = entry;
		}
		ItemTypeDescriptions = itemTypeDescriptions;
	}
}
