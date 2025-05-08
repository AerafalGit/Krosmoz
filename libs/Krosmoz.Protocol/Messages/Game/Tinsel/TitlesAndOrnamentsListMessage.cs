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

	public required IEnumerable<short> Titles { get; set; }

	public required IEnumerable<short> Ornaments { get; set; }

	public required short ActiveTitle { get; set; }

	public required short ActiveOrnament { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var titlesBefore = writer.Position;
		var titlesCount = 0;
		writer.WriteShort(0);
		foreach (var item in Titles)
		{
			writer.WriteShort(item);
			titlesCount++;
		}
		var titlesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, titlesBefore);
		writer.WriteShort((short)titlesCount);
		writer.Seek(SeekOrigin.Begin, titlesAfter);
		var ornamentsBefore = writer.Position;
		var ornamentsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Ornaments)
		{
			writer.WriteShort(item);
			ornamentsCount++;
		}
		var ornamentsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, ornamentsBefore);
		writer.WriteShort((short)ornamentsCount);
		writer.Seek(SeekOrigin.Begin, ornamentsAfter);
		writer.WriteShort(ActiveTitle);
		writer.WriteShort(ActiveOrnament);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var titlesCount = reader.ReadShort();
		var titles = new short[titlesCount];
		for (var i = 0; i < titlesCount; i++)
		{
			titles[i] = reader.ReadShort();
		}
		Titles = titles;
		var ornamentsCount = reader.ReadShort();
		var ornaments = new short[ornamentsCount];
		for (var i = 0; i < ornamentsCount; i++)
		{
			ornaments[i] = reader.ReadShort();
		}
		Ornaments = ornaments;
		ActiveTitle = reader.ReadShort();
		ActiveOrnament = reader.ReadShort();
	}
}
