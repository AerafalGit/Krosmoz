// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Preset;

public sealed class InventoryPresetItemUpdateRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6210;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InventoryPresetItemUpdateRequestMessage Empty =>
		new() { PresetId = 0, Position = 0, ObjUid = 0 };

	public required sbyte PresetId { get; set; }

	public required byte Position { get; set; }

	public required uint ObjUid { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(PresetId);
		writer.WriteUInt8(Position);
		writer.WriteVarUInt32(ObjUid);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PresetId = reader.ReadInt8();
		Position = reader.ReadUInt8();
		ObjUid = reader.ReadVarUInt32();
	}
}
