// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Preset;

public sealed class InventoryPresetSaveMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6165;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InventoryPresetSaveMessage Empty =>
		new() { PresetId = 0, SymbolId = 0, SaveEquipment = false };

	public required sbyte PresetId { get; set; }

	public required sbyte SymbolId { get; set; }

	public required bool SaveEquipment { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(PresetId);
		writer.WriteInt8(SymbolId);
		writer.WriteBoolean(SaveEquipment);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PresetId = reader.ReadInt8();
		SymbolId = reader.ReadInt8();
		SaveEquipment = reader.ReadBoolean();
	}
}
