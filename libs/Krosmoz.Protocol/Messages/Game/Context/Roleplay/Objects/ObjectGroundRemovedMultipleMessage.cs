// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Objects;

public sealed class ObjectGroundRemovedMultipleMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5944;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectGroundRemovedMultipleMessage Empty =>
		new() { Cells = [] };

	public required IEnumerable<ushort> Cells { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var cellsBefore = writer.Position;
		var cellsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Cells)
		{
			writer.WriteVarUInt16(item);
			cellsCount++;
		}
		var cellsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, cellsBefore);
		writer.WriteInt16((short)cellsCount);
		writer.Seek(SeekOrigin.Begin, cellsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var cellsCount = reader.ReadInt16();
		var cells = new ushort[cellsCount];
		for (var i = 0; i < cellsCount; i++)
		{
			cells[i] = reader.ReadVarUInt16();
		}
		Cells = cells;
	}
}
