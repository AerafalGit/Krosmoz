// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Inventory.Preset;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Preset;

public sealed class InventoryPresetItemUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6168;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InventoryPresetItemUpdateMessage Empty =>
		new() { PresetId = 0, PresetItem = PresetItem.Empty };

	public required sbyte PresetId { get; set; }

	public required PresetItem PresetItem { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(PresetId);
		PresetItem.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PresetId = reader.ReadInt8();
		PresetItem = PresetItem.Empty;
		PresetItem.Deserialize(reader);
	}
}
