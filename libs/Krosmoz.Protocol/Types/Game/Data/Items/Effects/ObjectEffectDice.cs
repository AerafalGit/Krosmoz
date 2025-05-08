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

	public required short DiceNum { get; set; }

	public required short DiceSide { get; set; }

	public required short DiceConst { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(DiceNum);
		writer.WriteShort(DiceSide);
		writer.WriteShort(DiceConst);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		DiceNum = reader.ReadShort();
		DiceSide = reader.ReadShort();
		DiceConst = reader.ReadShort();
	}
}
