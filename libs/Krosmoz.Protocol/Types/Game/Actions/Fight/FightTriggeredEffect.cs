// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Actions.Fight;

public sealed class FightTriggeredEffect : AbstractFightDispellableEffect
{
	public new const ushort StaticProtocolId = 210;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightTriggeredEffect Empty =>
		new() { ParentBoostUid = 0, EffectId = 0, SpellId = 0, Dispelable = 0, TurnDuration = 0, TargetId = 0, Uid = 0, Param1 = 0, Param2 = 0, Param3 = 0, Delay = 0 };

	public required int Param1 { get; set; }

	public required int Param2 { get; set; }

	public required int Param3 { get; set; }

	public required short Delay { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(Param1);
		writer.WriteInt32(Param2);
		writer.WriteInt32(Param3);
		writer.WriteInt16(Delay);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Param1 = reader.ReadInt32();
		Param2 = reader.ReadInt32();
		Param3 = reader.ReadInt32();
		Delay = reader.ReadInt16();
	}
}
