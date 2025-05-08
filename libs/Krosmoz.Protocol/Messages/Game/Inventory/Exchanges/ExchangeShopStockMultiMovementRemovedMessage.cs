// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeShopStockMultiMovementRemovedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6037;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeShopStockMultiMovementRemovedMessage Empty =>
		new() { ObjectIdList = [] };

	public required IEnumerable<int> ObjectIdList { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var objectIdListBefore = writer.Position;
		var objectIdListCount = 0;
		writer.WriteShort(0);
		foreach (var item in ObjectIdList)
		{
			writer.WriteInt(item);
			objectIdListCount++;
		}
		var objectIdListAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectIdListBefore);
		writer.WriteShort((short)objectIdListCount);
		writer.Seek(SeekOrigin.Begin, objectIdListAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var objectIdListCount = reader.ReadShort();
		var objectIdList = new int[objectIdListCount];
		for (var i = 0; i < objectIdListCount; i++)
		{
			objectIdList[i] = reader.ReadInt();
		}
		ObjectIdList = objectIdList;
	}
}
