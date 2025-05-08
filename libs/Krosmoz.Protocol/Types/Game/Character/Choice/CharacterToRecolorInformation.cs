// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Character.Choice;

public sealed class CharacterToRecolorInformation : AbstractCharacterInformation
{
	public new const ushort StaticProtocolId = 212;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new CharacterToRecolorInformation Empty =>
		new() { Id = 0, Colors = [] };

	public required IEnumerable<int> Colors { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var colorsBefore = writer.Position;
		var colorsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Colors)
		{
			writer.WriteInt(item);
			colorsCount++;
		}
		var colorsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, colorsBefore);
		writer.WriteShort((short)colorsCount);
		writer.Seek(SeekOrigin.Begin, colorsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var colorsCount = reader.ReadShort();
		var colors = new int[colorsCount];
		for (var i = 0; i < colorsCount; i++)
		{
			colors[i] = reader.ReadInt();
		}
		Colors = colors;
	}
}
