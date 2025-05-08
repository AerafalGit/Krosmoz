// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Choice;

public sealed class CharacterSelectionWithRecolorMessage : CharacterSelectionMessage
{
	public new const uint StaticProtocolId = 6075;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new CharacterSelectionWithRecolorMessage Empty =>
		new() { Id = 0, IndexedColor = [] };

	public required IEnumerable<int> IndexedColor { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var indexedColorBefore = writer.Position;
		var indexedColorCount = 0;
		writer.WriteShort(0);
		foreach (var item in IndexedColor)
		{
			writer.WriteInt(item);
			indexedColorCount++;
		}
		var indexedColorAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, indexedColorBefore);
		writer.WriteShort((short)indexedColorCount);
		writer.Seek(SeekOrigin.Begin, indexedColorAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var indexedColorCount = reader.ReadShort();
		var indexedColor = new int[indexedColorCount];
		for (var i = 0; i < indexedColorCount; i++)
		{
			indexedColor[i] = reader.ReadInt();
		}
		IndexedColor = indexedColor;
	}
}
