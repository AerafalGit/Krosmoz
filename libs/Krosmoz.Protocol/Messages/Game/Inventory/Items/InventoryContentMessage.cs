// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public class InventoryContentMessage : DofusMessage
{
	public new const uint StaticProtocolId = 3016;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InventoryContentMessage Empty =>
		new() { Objects = [], Kamas = 0 };

	public required IEnumerable<ObjectItem> Objects { get; set; }

	public required int Kamas { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var objectsBefore = writer.Position;
		var objectsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Objects)
		{
			item.Serialize(writer);
			objectsCount++;
		}
		var objectsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectsBefore);
		writer.WriteShort((short)objectsCount);
		writer.Seek(SeekOrigin.Begin, objectsAfter);
		writer.WriteInt(Kamas);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var objectsCount = reader.ReadShort();
		var objects = new ObjectItem[objectsCount];
		for (var i = 0; i < objectsCount; i++)
		{
			var entry = ObjectItem.Empty;
			entry.Deserialize(reader);
			objects[i] = entry;
		}
		Objects = objects;
		Kamas = reader.ReadInt();
	}
}
