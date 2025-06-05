// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;
using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ExchangeObjectsModifiedMessage : ExchangeObjectMessage
{
	public new const uint StaticProtocolId = 6533;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeObjectsModifiedMessage Empty =>
		new() { Remote = false, Object = [] };

	public required IEnumerable<ObjectItem> Object { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var objectBefore = writer.Position;
		var objectCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Object)
		{
			item.Serialize(writer);
			objectCount++;
		}
		var objectAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectBefore);
		writer.WriteInt16((short)objectCount);
		writer.Seek(SeekOrigin.Begin, objectAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var objectCount = reader.ReadInt16();
		var @object = new ObjectItem[objectCount];
		for (var i = 0; i < objectCount; i++)
		{
			var entry = ObjectItem.Empty;
			entry.Deserialize(reader);
			@object[i] = entry;
		}
		Object = @object;
	}
}
