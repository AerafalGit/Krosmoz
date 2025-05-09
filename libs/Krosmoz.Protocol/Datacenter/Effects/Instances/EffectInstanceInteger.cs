// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Effects.Instances;

public class EffectInstanceInteger : EffectInstance
{
	public required int Value { get; set; }

	public override void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		EffectId = d2OClass.Fields[0].AsInt(reader);
		Duration = d2OClass.Fields[1].AsInt(reader);
		Hidden = d2OClass.Fields[2].AsBoolean(reader);
		Random = d2OClass.Fields[3].AsInt(reader);
		Value = d2OClass.Fields[4].AsInt(reader);
		RawZone = d2OClass.Fields[5].AsString(reader);
		TargetId = d2OClass.Fields[6].AsInt(reader);
		Group = d2OClass.Fields[7].AsInt(reader);
		TargetMask = d2OClass.Fields[8].AsString(reader);
	}
}
