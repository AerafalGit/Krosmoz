// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Creation;

public sealed class CharacterCreationRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 160;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CharacterCreationRequestMessage Empty =>
		new() { Name = string.Empty, Breed = 0, Sex = false, Colors = [], CosmeticId = 0 };

	public required string Name { get; set; }

	public required sbyte Breed { get; set; }

	public required bool Sex { get; set; }

	public required IEnumerable<int> Colors { get; set; }

	public required int CosmeticId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfLengthPrefixed16(Name);
		writer.WriteSByte(Breed);
		writer.WriteBoolean(Sex);
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
		writer.WriteInt(CosmeticId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Name = reader.ReadUtfLengthPrefixed16();
		Breed = reader.ReadSByte();
		Sex = reader.ReadBoolean();
		var colorsCount = reader.ReadShort();
		var colors = new int[colorsCount];
		for (var i = 0; i < colorsCount; i++)
		{
			colors[i] = reader.ReadInt();
		}
		Colors = colors;
		CosmeticId = reader.ReadInt();
	}
}
