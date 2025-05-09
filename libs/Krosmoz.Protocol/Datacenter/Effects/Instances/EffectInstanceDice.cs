// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Effects.Instances;

public sealed class EffectInstanceDice : EffectInstanceInteger
{
	public required int DiceNum { get; set; }

	public required int DiceSide { get; set; }

	public override void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		EffectId = d2OClass.Fields[0].AsInt(reader);
		DiceNum = d2OClass.Fields[1].AsInt(reader);
		Duration = d2OClass.Fields[2].AsInt(reader);
		Hidden = d2OClass.Fields[3].AsBoolean(reader);
		DiceSide = d2OClass.Fields[4].AsInt(reader);
		Value = d2OClass.Fields[5].AsInt(reader);
		Random = d2OClass.Fields[6].AsInt(reader);
		RawZone = d2OClass.Fields[7].AsString(reader);
		TargetId = d2OClass.Fields[8].AsInt(reader);
		Group = d2OClass.Fields[9].AsInt(reader);
		TargetMask = d2OClass.Fields[10].AsString(reader);
	}
}
