// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Objects;

public sealed class ObjectGroundListAddedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5925;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectGroundListAddedMessage Empty =>
		new() { Cells = [], ReferenceIds = [] };

	public required IEnumerable<short> Cells { get; set; }

	public required IEnumerable<int> ReferenceIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var cellsBefore = writer.Position;
		var cellsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Cells)
		{
			writer.WriteShort(item);
			cellsCount++;
		}
		var cellsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, cellsBefore);
		writer.WriteShort((short)cellsCount);
		writer.Seek(SeekOrigin.Begin, cellsAfter);
		var referenceIdsBefore = writer.Position;
		var referenceIdsCount = 0;
		writer.WriteShort(0);
		foreach (var item in ReferenceIds)
		{
			writer.WriteInt(item);
			referenceIdsCount++;
		}
		var referenceIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, referenceIdsBefore);
		writer.WriteShort((short)referenceIdsCount);
		writer.Seek(SeekOrigin.Begin, referenceIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var cellsCount = reader.ReadShort();
		var cells = new short[cellsCount];
		for (var i = 0; i < cellsCount; i++)
		{
			cells[i] = reader.ReadShort();
		}
		Cells = cells;
		var referenceIdsCount = reader.ReadShort();
		var referenceIds = new int[referenceIdsCount];
		for (var i = 0; i < referenceIdsCount; i++)
		{
			referenceIds[i] = reader.ReadInt();
		}
		ReferenceIds = referenceIds;
	}
}
