// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Character.Choice;

public class CharacterRemodelingInformation : AbstractCharacterInformation
{
	public new const ushort StaticProtocolId = 479;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new CharacterRemodelingInformation Empty =>
		new() { Id = 0, Name = string.Empty, Breed = 0, Sex = false, CosmeticId = 0, Colors = [] };

	public required string Name { get; set; }

	public required sbyte Breed { get; set; }

	public required bool Sex { get; set; }

	public required ushort CosmeticId { get; set; }

	public required IEnumerable<int> Colors { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfPrefixedLength16(Name);
		writer.WriteInt8(Breed);
		writer.WriteBoolean(Sex);
		writer.WriteVarUInt16(CosmeticId);
		var colorsBefore = writer.Position;
		var colorsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Colors)
		{
			writer.WriteInt32(item);
			colorsCount++;
		}
		var colorsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, colorsBefore);
		writer.WriteInt16((short)colorsCount);
		writer.Seek(SeekOrigin.Begin, colorsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Name = reader.ReadUtfPrefixedLength16();
		Breed = reader.ReadInt8();
		Sex = reader.ReadBoolean();
		CosmeticId = reader.ReadVarUInt16();
		var colorsCount = reader.ReadInt16();
		var colors = new int[colorsCount];
		for (var i = 0; i < colorsCount; i++)
		{
			colors[i] = reader.ReadInt32();
		}
		Colors = colors;
	}
}
