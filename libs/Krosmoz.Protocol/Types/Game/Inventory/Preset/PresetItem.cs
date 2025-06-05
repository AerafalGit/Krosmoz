// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Inventory.Preset;

public sealed class PresetItem : DofusType
{
	public new const ushort StaticProtocolId = 354;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static PresetItem Empty =>
		new() { Position = 0, ObjGid = 0, ObjUid = 0 };

	public required byte Position { get; set; }

	public required ushort ObjGid { get; set; }

	public required uint ObjUid { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt8(Position);
		writer.WriteVarUInt16(ObjGid);
		writer.WriteVarUInt32(ObjUid);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Position = reader.ReadUInt8();
		ObjGid = reader.ReadVarUInt16();
		ObjUid = reader.ReadVarUInt32();
	}
}
