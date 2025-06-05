// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Tinsel;

public sealed class TitlesAndOrnamentsListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6367;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TitlesAndOrnamentsListMessage Empty =>
		new() { Titles = [], Ornaments = [], ActiveTitle = 0, ActiveOrnament = 0 };

	public required IEnumerable<ushort> Titles { get; set; }

	public required IEnumerable<ushort> Ornaments { get; set; }

	public required ushort ActiveTitle { get; set; }

	public required ushort ActiveOrnament { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var titlesBefore = writer.Position;
		var titlesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Titles)
		{
			writer.WriteVarUInt16(item);
			titlesCount++;
		}
		var titlesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, titlesBefore);
		writer.WriteInt16((short)titlesCount);
		writer.Seek(SeekOrigin.Begin, titlesAfter);
		var ornamentsBefore = writer.Position;
		var ornamentsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Ornaments)
		{
			writer.WriteVarUInt16(item);
			ornamentsCount++;
		}
		var ornamentsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, ornamentsBefore);
		writer.WriteInt16((short)ornamentsCount);
		writer.Seek(SeekOrigin.Begin, ornamentsAfter);
		writer.WriteVarUInt16(ActiveTitle);
		writer.WriteVarUInt16(ActiveOrnament);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var titlesCount = reader.ReadInt16();
		var titles = new ushort[titlesCount];
		for (var i = 0; i < titlesCount; i++)
		{
			titles[i] = reader.ReadVarUInt16();
		}
		Titles = titles;
		var ornamentsCount = reader.ReadInt16();
		var ornaments = new ushort[ornamentsCount];
		for (var i = 0; i < ornamentsCount; i++)
		{
			ornaments[i] = reader.ReadVarUInt16();
		}
		Ornaments = ornaments;
		ActiveTitle = reader.ReadVarUInt16();
		ActiveOrnament = reader.ReadVarUInt16();
	}
}
