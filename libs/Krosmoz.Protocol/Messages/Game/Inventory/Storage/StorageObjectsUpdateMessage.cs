// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Storage;

public sealed class StorageObjectsUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6036;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static StorageObjectsUpdateMessage Empty =>
		new() { ObjectList = [] };

	public required IEnumerable<ObjectItem> ObjectList { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var objectListBefore = writer.Position;
		var objectListCount = 0;
		writer.WriteShort(0);
		foreach (var item in ObjectList)
		{
			item.Serialize(writer);
			objectListCount++;
		}
		var objectListAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectListBefore);
		writer.WriteShort((short)objectListCount);
		writer.Seek(SeekOrigin.Begin, objectListAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var objectListCount = reader.ReadShort();
		var objectList = new ObjectItem[objectListCount];
		for (var i = 0; i < objectListCount; i++)
		{
			var entry = ObjectItem.Empty;
			entry.Deserialize(reader);
			objectList[i] = entry;
		}
		ObjectList = objectList;
	}
}
