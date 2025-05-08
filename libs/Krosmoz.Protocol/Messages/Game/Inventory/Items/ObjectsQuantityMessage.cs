// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ObjectsQuantityMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6206;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectsQuantityMessage Empty =>
		new() { ObjectsUIDAndQty = [] };

	public required IEnumerable<ObjectItemQuantity> ObjectsUIDAndQty { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var objectsUIDAndQtyBefore = writer.Position;
		var objectsUIDAndQtyCount = 0;
		writer.WriteShort(0);
		foreach (var item in ObjectsUIDAndQty)
		{
			item.Serialize(writer);
			objectsUIDAndQtyCount++;
		}
		var objectsUIDAndQtyAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectsUIDAndQtyBefore);
		writer.WriteShort((short)objectsUIDAndQtyCount);
		writer.Seek(SeekOrigin.Begin, objectsUIDAndQtyAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var objectsUIDAndQtyCount = reader.ReadShort();
		var objectsUIDAndQty = new ObjectItemQuantity[objectsUIDAndQtyCount];
		for (var i = 0; i < objectsUIDAndQtyCount; i++)
		{
			var entry = ObjectItemQuantity.Empty;
			entry.Deserialize(reader);
			objectsUIDAndQty[i] = entry;
		}
		ObjectsUIDAndQty = objectsUIDAndQty;
	}
}
