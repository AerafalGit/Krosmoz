// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Effects.Instances;

public class EffectInstanceInteger : EffectInstance
{
	public required int Value { get; set; }

	public override void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		EffectId = d2OClass.ReadFieldAsInt(reader);
		Duration = d2OClass.ReadFieldAsInt(reader);
		Hidden = d2OClass.ReadFieldAsBoolean(reader);
		Random = d2OClass.ReadFieldAsInt(reader);
		Value = d2OClass.ReadFieldAsInt(reader);
		RawZone = d2OClass.ReadFieldAsString(reader);
		TargetId = d2OClass.ReadFieldAsInt(reader);
		Group = d2OClass.ReadFieldAsInt(reader);
		TargetMask = d2OClass.ReadFieldAsString(reader);
	}
}
