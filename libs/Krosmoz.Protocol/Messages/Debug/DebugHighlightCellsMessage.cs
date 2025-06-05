// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Debug;

public sealed class DebugHighlightCellsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 2001;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DebugHighlightCellsMessage Empty =>
		new() { Color = 0, Cells = [] };

	public required int Color { get; set; }

	public required IEnumerable<ushort> Cells { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(Color);
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
		Color = reader.ReadInt32();
		var cellsCount = reader.ReadInt16();
		var cells = new ushort[cellsCount];
		for (var i = 0; i < cellsCount; i++)
		{
			cells[i] = reader.ReadVarUInt16();
		}
		Cells = cells;
	}
}
