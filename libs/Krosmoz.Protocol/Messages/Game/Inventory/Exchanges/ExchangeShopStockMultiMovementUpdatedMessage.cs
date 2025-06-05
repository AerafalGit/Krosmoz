// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeShopStockMultiMovementUpdatedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6038;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeShopStockMultiMovementUpdatedMessage Empty =>
		new() { ObjectInfoList = [] };

	public required IEnumerable<ObjectItemToSell> ObjectInfoList { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var objectInfoListBefore = writer.Position;
		var objectInfoListCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ObjectInfoList)
		{
			item.Serialize(writer);
			objectInfoListCount++;
		}
		var objectInfoListAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectInfoListBefore);
		writer.WriteInt16((short)objectInfoListCount);
		writer.Seek(SeekOrigin.Begin, objectInfoListAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var objectInfoListCount = reader.ReadInt16();
		var objectInfoList = new ObjectItemToSell[objectInfoListCount];
		for (var i = 0; i < objectInfoListCount; i++)
		{
			var entry = ObjectItemToSell.Empty;
			entry.Deserialize(reader);
			objectInfoList[i] = entry;
		}
		ObjectInfoList = objectInfoList;
	}
}
