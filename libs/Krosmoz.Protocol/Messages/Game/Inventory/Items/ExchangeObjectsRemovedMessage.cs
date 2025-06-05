// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ExchangeObjectsRemovedMessage : ExchangeObjectMessage
{
	public new const uint StaticProtocolId = 6532;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeObjectsRemovedMessage Empty =>
		new() { Remote = false, ObjectUID = [] };

	public required IEnumerable<uint> ObjectUID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var objectUIDBefore = writer.Position;
		var objectUIDCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ObjectUID)
		{
			writer.WriteVarUInt32(item);
			objectUIDCount++;
		}
		var objectUIDAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectUIDBefore);
		writer.WriteInt16((short)objectUIDCount);
		writer.Seek(SeekOrigin.Begin, objectUIDAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var objectUIDCount = reader.ReadInt16();
		var objectUID = new uint[objectUIDCount];
		for (var i = 0; i < objectUIDCount; i++)
		{
			objectUID[i] = reader.ReadVarUInt32();
		}
		ObjectUID = objectUID;
	}
}
