// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class InventoryContentAndPresetMessage : InventoryContentMessage
{
	public new const uint StaticProtocolId = 6162;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new InventoryContentAndPresetMessage Empty =>
		new() { Kamas = 0, Objects = [], Presets = [] };

	public required IEnumerable<Types.Game.Inventory.Preset.Preset> Presets { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var presetsBefore = writer.Position;
		var presetsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Presets)
		{
			item.Serialize(writer);
			presetsCount++;
		}
		var presetsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, presetsBefore);
		writer.WriteInt16((short)presetsCount);
		writer.Seek(SeekOrigin.Begin, presetsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var presetsCount = reader.ReadInt16();
		var presets = new Types.Game.Inventory.Preset.Preset[presetsCount];
		for (var i = 0; i < presetsCount; i++)
		{
			var entry = Types.Game.Inventory.Preset.Preset.Empty;
			entry.Deserialize(reader);
			presets[i] = entry;
		}
		Presets = presets;
	}
}
