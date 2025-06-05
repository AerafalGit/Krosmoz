// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Character.Choice;

public class AbstractCharacterToRefurbishInformation : AbstractCharacterInformation
{
	public new const ushort StaticProtocolId = 475;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new AbstractCharacterToRefurbishInformation Empty =>
		new() { Id = 0, Colors = [], CosmeticId = 0 };

	public required IEnumerable<int> Colors { get; set; }

	public required uint CosmeticId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
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
		writer.WriteVarUInt32(CosmeticId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var colorsCount = reader.ReadInt16();
		var colors = new int[colorsCount];
		for (var i = 0; i < colorsCount; i++)
		{
			colors[i] = reader.ReadInt32();
		}
		Colors = colors;
		CosmeticId = reader.ReadVarUInt32();
	}
}
