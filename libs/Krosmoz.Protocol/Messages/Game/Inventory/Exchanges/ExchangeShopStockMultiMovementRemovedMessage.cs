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

	public required IEnumerable<uint> ObjectIdList { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var objectIdListBefore = writer.Position;
		var objectIdListCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ObjectIdList)
		{
			writer.WriteVarUInt32(item);
			objectIdListCount++;
		}
		var objectIdListAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectIdListBefore);
		writer.WriteInt16((short)objectIdListCount);
		writer.Seek(SeekOrigin.Begin, objectIdListAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var objectIdListCount = reader.ReadInt16();
		var objectIdList = new uint[objectIdListCount];
		for (var i = 0; i < objectIdListCount; i++)
		{
			objectIdList[i] = reader.ReadVarUInt32();
		}
		ObjectIdList = objectIdList;
	}
}
