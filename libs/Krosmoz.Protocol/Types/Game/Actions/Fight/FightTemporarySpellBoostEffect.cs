// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Actions.Fight;

public sealed class FightTemporarySpellBoostEffect : FightTemporaryBoostEffect
{
	public new const ushort StaticProtocolId = 207;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightTemporarySpellBoostEffect Empty =>
		new() { ParentBoostUid = 0, EffectId = 0, SpellId = 0, Dispelable = 0, TurnDuration = 0, TargetId = 0, Uid = 0, Delta = 0, BoostedSpellId = 0 };

	public required ushort BoostedSpellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(BoostedSpellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		BoostedSpellId = reader.ReadVarUInt16();
	}
}
