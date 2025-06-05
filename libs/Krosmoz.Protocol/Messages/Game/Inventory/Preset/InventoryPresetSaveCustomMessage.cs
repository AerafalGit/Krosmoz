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

	public required IEnumerable<uint> ItemsUids { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(PresetId);
		writer.WriteInt8(SymbolId);
		var itemsPositionsBefore = writer.Position;
		var itemsPositionsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ItemsPositions)
		{
			writer.WriteUInt8(item);
			itemsPositionsCount++;
		}
		var itemsPositionsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, itemsPositionsBefore);
		writer.WriteInt16((short)itemsPositionsCount);
		writer.Seek(SeekOrigin.Begin, itemsPositionsAfter);
		var itemsUidsBefore = writer.Position;
		var itemsUidsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ItemsUids)
		{
			writer.WriteVarUInt32(item);
			itemsUidsCount++;
		}
		var itemsUidsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, itemsUidsBefore);
		writer.WriteInt16((short)itemsUidsCount);
		writer.Seek(SeekOrigin.Begin, itemsUidsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PresetId = reader.ReadInt8();
		SymbolId = reader.ReadInt8();
		var itemsPositionsCount = reader.ReadInt16();
		var itemsPositions = new byte[itemsPositionsCount];
		for (var i = 0; i < itemsPositionsCount; i++)
		{
			itemsPositions[i] = reader.ReadUInt8();
		}
		ItemsPositions = itemsPositions;
		var itemsUidsCount = reader.ReadInt16();
		var itemsUids = new uint[itemsUidsCount];
		for (var i = 0; i < itemsUidsCount; i++)
		{
			itemsUids[i] = reader.ReadVarUInt32();
		}
		ItemsUids = itemsUids;
	}
}
