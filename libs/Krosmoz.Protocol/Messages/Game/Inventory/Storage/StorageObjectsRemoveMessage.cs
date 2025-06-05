// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Storage;

public sealed class StorageObjectsRemoveMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6035;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static StorageObjectsRemoveMessage Empty =>
		new() { ObjectUIDList = [] };

	public required IEnumerable<uint> ObjectUIDList { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var objectUIDListBefore = writer.Position;
		var objectUIDListCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ObjectUIDList)
		{
			writer.WriteVarUInt32(item);
			objectUIDListCount++;
		}
		var objectUIDListAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectUIDListBefore);
		writer.WriteInt16((short)objectUIDListCount);
		writer.Seek(SeekOrigin.Begin, objectUIDListAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var objectUIDListCount = reader.ReadInt16();
		var objectUIDList = new uint[objectUIDListCount];
		for (var i = 0; i < objectUIDListCount; i++)
		{
			objectUIDList[i] = reader.ReadVarUInt32();
		}
		ObjectUIDList = objectUIDList;
	}
}
