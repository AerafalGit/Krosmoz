// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Chat;

public sealed class ChatServerCopyWithObjectMessage : ChatServerCopyMessage
{
	public new const uint StaticProtocolId = 884;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ChatServerCopyWithObjectMessage Empty =>
		new() { Fingerprint = string.Empty, Timestamp = 0, Content = string.Empty, Channel = 0, ReceiverName = string.Empty, ReceiverId = 0, Objects = [] };

	public required IEnumerable<ObjectItem> Objects { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var objectsBefore = writer.Position;
		var objectsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Objects)
		{
			item.Serialize(writer);
			objectsCount++;
		}
		var objectsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectsBefore);
		writer.WriteInt16((short)objectsCount);
		writer.Seek(SeekOrigin.Begin, objectsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var objectsCount = reader.ReadInt16();
		var objects = new ObjectItem[objectsCount];
		for (var i = 0; i < objectsCount; i++)
		{
			var entry = ObjectItem.Empty;
			entry.Deserialize(reader);
			objects[i] = entry;
		}
		Objects = objects;
	}
}
