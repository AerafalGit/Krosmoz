// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Actions.Fight;

public class FightTemporaryBoostEffect : AbstractFightDispellableEffect
{
	public new const ushort StaticProtocolId = 209;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightTemporaryBoostEffect Empty =>
		new() { ParentBoostUid = 0, EffectId = 0, SpellId = 0, Dispelable = 0, TurnDuration = 0, TargetId = 0, Uid = 0, Delta = 0 };

	public required short Delta { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt16(Delta);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Delta = reader.ReadInt16();
	}
}
