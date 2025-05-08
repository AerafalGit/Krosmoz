// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ObjectsDeletedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6034;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectsDeletedMessage Empty =>
		new() { ObjectUID = [] };

	public required IEnumerable<int> ObjectUID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var objectUIDBefore = writer.Position;
		var objectUIDCount = 0;
		writer.WriteShort(0);
		foreach (var item in ObjectUID)
		{
			writer.WriteInt(item);
			objectUIDCount++;
		}
		var objectUIDAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectUIDBefore);
		writer.WriteShort((short)objectUIDCount);
		writer.Seek(SeekOrigin.Begin, objectUIDAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var objectUIDCount = reader.ReadShort();
		var objectUID = new int[objectUIDCount];
		for (var i = 0; i < objectUIDCount; i++)
		{
			objectUID[i] = reader.ReadInt();
		}
		ObjectUID = objectUID;
	}
}
