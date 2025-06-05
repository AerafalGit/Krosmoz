// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Preset;

public sealed class InventoryPresetDeleteResultMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6173;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InventoryPresetDeleteResultMessage Empty =>
		new() { PresetId = 0, Code = 0 };

	public required sbyte PresetId { get; set; }

	public required sbyte Code { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(PresetId);
		writer.WriteInt8(Code);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PresetId = reader.ReadInt8();
		Code = reader.ReadInt8();
	}
}
