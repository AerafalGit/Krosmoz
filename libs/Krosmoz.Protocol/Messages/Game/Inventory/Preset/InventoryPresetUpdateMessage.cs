// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Preset;

public sealed class InventoryPresetUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6171;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InventoryPresetUpdateMessage Empty =>
		new() { Preset = Types.Game.Inventory.Preset.Preset.Empty };

	public required Types.Game.Inventory.Preset.Preset Preset { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Preset.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Preset = Types.Game.Inventory.Preset.Preset.Empty;
		Preset.Deserialize(reader);
	}
}
