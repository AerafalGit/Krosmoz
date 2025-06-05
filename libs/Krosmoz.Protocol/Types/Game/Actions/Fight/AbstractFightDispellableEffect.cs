// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Actions.Fight;

public class AbstractFightDispellableEffect : DofusType
{
	public new const ushort StaticProtocolId = 206;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static AbstractFightDispellableEffect Empty =>
		new() { Uid = 0, TargetId = 0, TurnDuration = 0, Dispelable = 0, SpellId = 0, EffectId = 0, ParentBoostUid = 0 };

	public required uint Uid { get; set; }

	public required int TargetId { get; set; }

	public required short TurnDuration { get; set; }

	public required sbyte Dispelable { get; set; }

	public required ushort SpellId { get; set; }

	public required uint EffectId { get; set; }

	public required uint ParentBoostUid { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(Uid);
		writer.WriteInt32(TargetId);
		writer.WriteInt16(TurnDuration);
		writer.WriteInt8(Dispelable);
		writer.WriteVarUInt16(SpellId);
		writer.WriteVarUInt32(EffectId);
		writer.WriteVarUInt32(ParentBoostUid);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Uid = reader.ReadVarUInt32();
		TargetId = reader.ReadInt32();
		TurnDuration = reader.ReadInt16();
		Dispelable = reader.ReadInt8();
		SpellId = reader.ReadVarUInt16();
		EffectId = reader.ReadVarUInt32();
		ParentBoostUid = reader.ReadVarUInt32();
	}
}
