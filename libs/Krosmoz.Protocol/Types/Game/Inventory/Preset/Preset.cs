// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Inventory.Preset;

public sealed class Preset : DofusType
{
	public new const ushort StaticProtocolId = 355;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static Preset Empty =>
		new() { PresetId = 0, SymbolId = 0, Mount = false, Objects = [] };

	public required sbyte PresetId { get; set; }

	public required sbyte SymbolId { get; set; }

	public required bool Mount { get; set; }

	public required IEnumerable<PresetItem> Objects { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(PresetId);
		writer.WriteSByte(SymbolId);
		writer.WriteBoolean(Mount);
		var objectsBefore = writer.Position;
		var objectsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Objects)
		{
			item.Serialize(writer);
			objectsCount++;
		}
		var objectsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectsBefore);
		writer.WriteShort((short)objectsCount);
		writer.Seek(SeekOrigin.Begin, objectsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PresetId = reader.ReadSByte();
		SymbolId = reader.ReadSByte();
		Mount = reader.ReadBoolean();
		var objectsCount = reader.ReadShort();
		var objects = new PresetItem[objectsCount];
		for (var i = 0; i < objectsCount; i++)
		{
			var entry = PresetItem.Empty;
			entry.Deserialize(reader);
			objects[i] = entry;
		}
		Objects = objects;
	}
}
