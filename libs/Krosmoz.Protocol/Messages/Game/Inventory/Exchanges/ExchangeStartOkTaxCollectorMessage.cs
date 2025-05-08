// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeStartOkTaxCollectorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5780;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeStartOkTaxCollectorMessage Empty =>
		new() { CollectorId = 0, ObjectsInfos = [], GoldInfo = 0 };

	public required int CollectorId { get; set; }

	public required IEnumerable<ObjectItem> ObjectsInfos { get; set; }

	public required int GoldInfo { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(CollectorId);
		var objectsInfosBefore = writer.Position;
		var objectsInfosCount = 0;
		writer.WriteShort(0);
		foreach (var item in ObjectsInfos)
		{
			item.Serialize(writer);
			objectsInfosCount++;
		}
		var objectsInfosAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectsInfosBefore);
		writer.WriteShort((short)objectsInfosCount);
		writer.Seek(SeekOrigin.Begin, objectsInfosAfter);
		writer.WriteInt(GoldInfo);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CollectorId = reader.ReadInt();
		var objectsInfosCount = reader.ReadShort();
		var objectsInfos = new ObjectItem[objectsInfosCount];
		for (var i = 0; i < objectsInfosCount; i++)
		{
			var entry = ObjectItem.Empty;
			entry.Deserialize(reader);
			objectsInfos[i] = entry;
		}
		ObjectsInfos = objectsInfos;
		GoldInfo = reader.ReadInt();
	}
}
