// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameContextRemoveMultipleElementsWithEventsMessage : GameContextRemoveMultipleElementsMessage
{
	public new const uint StaticProtocolId = 6416;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameContextRemoveMultipleElementsWithEventsMessage Empty =>
		new() { Id = [], ElementEventIds = [] };

	public required IEnumerable<sbyte> ElementEventIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var elementEventIdsBefore = writer.Position;
		var elementEventIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ElementEventIds)
		{
			writer.WriteInt8(item);
			elementEventIdsCount++;
		}
		var elementEventIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, elementEventIdsBefore);
		writer.WriteInt16((short)elementEventIdsCount);
		writer.Seek(SeekOrigin.Begin, elementEventIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var elementEventIdsCount = reader.ReadInt16();
		var elementEventIds = new sbyte[elementEventIdsCount];
		for (var i = 0; i < elementEventIdsCount; i++)
		{
			elementEventIds[i] = reader.ReadInt8();
		}
		ElementEventIds = elementEventIds;
	}
}
