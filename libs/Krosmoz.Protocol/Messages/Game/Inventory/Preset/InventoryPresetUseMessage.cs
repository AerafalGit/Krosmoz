// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Preset;

public sealed class InventoryPresetUseMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6167;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InventoryPresetUseMessage Empty =>
		new() { PresetId = 0 };

	public required sbyte PresetId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(PresetId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PresetId = reader.ReadInt8();
	}
}
