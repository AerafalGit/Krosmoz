// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Preset;

public sealed class InventoryPresetSaveCustomMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6329;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InventoryPresetSaveCustomMessage Empty =>
		new() { PresetId = 0, SymbolId = 0, ItemsPositions = [], ItemsUids = [] };

	public required sbyte PresetId { get; set; }

	public required sbyte SymbolId { get; set; }

	public required IEnumerable<byte> ItemsPositions { get; set; }

	public required IEnumerable<int> ItemsUids { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(PresetId);
		writer.WriteSByte(SymbolId);
		var itemsPositionsBefore = writer.Position;
		var itemsPositionsCount = 0;
		writer.WriteShort(0);
		foreach (var item in ItemsPositions)
		{
			writer.WriteByte(item);
			itemsPositionsCount++;
		}
		var itemsPositionsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, itemsPositionsBefore);
		writer.WriteShort((short)itemsPositionsCount);
		writer.Seek(SeekOrigin.Begin, itemsPositionsAfter);
		var itemsUidsBefore = writer.Position;
		var itemsUidsCount = 0;
		writer.WriteShort(0);
		foreach (var item in ItemsUids)
		{
			writer.WriteInt(item);
			itemsUidsCount++;
		}
		var itemsUidsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, itemsUidsBefore);
		writer.WriteShort((short)itemsUidsCount);
		writer.Seek(SeekOrigin.Begin, itemsUidsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PresetId = reader.ReadSByte();
		SymbolId = reader.ReadSByte();
		var itemsPositionsCount = reader.ReadShort();
		var itemsPositions = new byte[itemsPositionsCount];
		for (var i = 0; i < itemsPositionsCount; i++)
		{
			itemsPositions[i] = reader.ReadByte();
		}
		ItemsPositions = itemsPositions;
		var itemsUidsCount = reader.ReadShort();
		var itemsUids = new int[itemsUidsCount];
		for (var i = 0; i < itemsUidsCount; i++)
		{
			itemsUids[i] = reader.ReadInt();
		}
		ItemsUids = itemsUids;
	}
}
