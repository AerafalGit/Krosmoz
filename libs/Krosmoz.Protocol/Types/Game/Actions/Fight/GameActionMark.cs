// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Actions.Fight;

public sealed class GameActionMark : DofusType
{
	public new const ushort StaticProtocolId = 351;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static GameActionMark Empty =>
		new() { MarkAuthorId = 0, MarkTeamId = 0, MarkSpellId = 0, MarkSpellLevel = 0, MarkId = 0, MarkType = 0, MarkimpactCell = 0, Cells = [], Active = false };

	public required int MarkAuthorId { get; set; }

	public required sbyte MarkTeamId { get; set; }

	public required int MarkSpellId { get; set; }

	public required sbyte MarkSpellLevel { get; set; }

	public required short MarkId { get; set; }

	public required sbyte MarkType { get; set; }

	public required short MarkimpactCell { get; set; }

	public required IEnumerable<GameActionMarkedCell> Cells { get; set; }

	public required bool Active { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(MarkAuthorId);
		writer.WriteInt8(MarkTeamId);
		writer.WriteInt32(MarkSpellId);
		writer.WriteInt8(MarkSpellLevel);
		writer.WriteInt16(MarkId);
		writer.WriteInt8(MarkType);
		writer.WriteInt16(MarkimpactCell);
		var cellsBefore = writer.Position;
		var cellsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Cells)
		{
			item.Serialize(writer);
			cellsCount++;
		}
		var cellsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, cellsBefore);
		writer.WriteInt16((short)cellsCount);
		writer.Seek(SeekOrigin.Begin, cellsAfter);
		writer.WriteBoolean(Active);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MarkAuthorId = reader.ReadInt32();
		MarkTeamId = reader.ReadInt8();
		MarkSpellId = reader.ReadInt32();
		MarkSpellLevel = reader.ReadInt8();
		MarkId = reader.ReadInt16();
		MarkType = reader.ReadInt8();
		MarkimpactCell = reader.ReadInt16();
		var cellsCount = reader.ReadInt16();
		var cells = new GameActionMarkedCell[cellsCount];
		for (var i = 0; i < cellsCount; i++)
		{
			var entry = GameActionMarkedCell.Empty;
			entry.Deserialize(reader);
			cells[i] = entry;
		}
		Cells = cells;
		Active = reader.ReadBoolean();
	}
}
