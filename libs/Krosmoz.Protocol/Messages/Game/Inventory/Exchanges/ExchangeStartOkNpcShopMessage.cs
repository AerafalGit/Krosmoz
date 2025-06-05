// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeStartOkNpcShopMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5761;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeStartOkNpcShopMessage Empty =>
		new() { NpcSellerId = 0, TokenId = 0, ObjectsInfos = [] };

	public required int NpcSellerId { get; set; }

	public required ushort TokenId { get; set; }

	public required IEnumerable<ObjectItemToSellInNpcShop> ObjectsInfos { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(NpcSellerId);
		writer.WriteVarUInt16(TokenId);
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
		NpcSellerId = reader.ReadInt32();
		TokenId = reader.ReadVarUInt16();
		var objectsInfosCount = reader.ReadInt16();
		var objectsInfos = new ObjectItemToSellInNpcShop[objectsInfosCount];
		for (var i = 0; i < objectsInfosCount; i++)
		{
			var entry = ObjectItemToSellInNpcShop.Empty;
			entry.Deserialize(reader);
			objectsInfos[i] = entry;
		}
		ObjectsInfos = objectsInfos;
	}
}
