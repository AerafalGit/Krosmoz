// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items.Effects;

public sealed class ObjectEffectDice : ObjectEffect
{
	public new const ushort StaticProtocolId = 73;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectEffectDice Empty =>
		new() { ActionId = 0, DiceNum = 0, DiceSide = 0, DiceConst = 0 };

	public required ushort DiceNum { get; set; }

	public required ushort DiceSide { get; set; }

	public required ushort DiceConst { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(DiceNum);
		writer.WriteVarUInt16(DiceSide);
		writer.WriteVarUInt16(DiceConst);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		DiceNum = reader.ReadVarUInt16();
		DiceSide = reader.ReadVarUInt16();
		DiceConst = reader.ReadVarUInt16();
	}
}
