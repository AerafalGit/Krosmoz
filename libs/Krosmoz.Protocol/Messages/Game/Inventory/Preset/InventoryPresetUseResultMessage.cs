// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Preset;

public sealed class InventoryPresetUseResultMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6163;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InventoryPresetUseResultMessage Empty =>
		new() { PresetId = 0, Code = 0, UnlinkedPosition = [] };

	public required sbyte PresetId { get; set; }

	public required sbyte Code { get; set; }

	public required IEnumerable<byte> UnlinkedPosition { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(PresetId);
		writer.WriteInt8(Code);
		var unlinkedPositionBefore = writer.Position;
		var unlinkedPositionCount = 0;
		writer.WriteInt16(0);
		foreach (var item in UnlinkedPosition)
		{
			writer.WriteUInt8(item);
			unlinkedPositionCount++;
		}
		var unlinkedPositionAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, unlinkedPositionBefore);
		writer.WriteInt16((short)unlinkedPositionCount);
		writer.Seek(SeekOrigin.Begin, unlinkedPositionAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PresetId = reader.ReadInt8();
		Code = reader.ReadInt8();
		var unlinkedPositionCount = reader.ReadInt16();
		var unlinkedPosition = new byte[unlinkedPositionCount];
		for (var i = 0; i < unlinkedPositionCount; i++)
		{
			unlinkedPosition[i] = reader.ReadUInt8();
		}
		UnlinkedPosition = unlinkedPosition;
	}
}
