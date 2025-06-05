// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Effects.Instances;

public class EffectInstanceInteger : EffectInstance
{
	public required int Value { get; set; }

	public override void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		VisibleInTooltip = d2OClass.ReadFieldAsBoolean(reader);
		Random = d2OClass.ReadFieldAsInt(reader);
		RawZone = d2OClass.ReadFieldAsString(reader);
		TargetId = d2OClass.ReadFieldAsInt(reader);
		TargetMask = d2OClass.ReadFieldAsString(reader);
		EffectId = d2OClass.ReadFieldAsInt(reader);
		Duration = d2OClass.ReadFieldAsInt(reader);
		VisibleInFightLog = d2OClass.ReadFieldAsBoolean(reader);
		EffectUid = d2OClass.ReadFieldAsInt(reader);
		Value = d2OClass.ReadFieldAsInt(reader);
		VisibleInBuffUi = d2OClass.ReadFieldAsBoolean(reader);
		Triggers = d2OClass.ReadFieldAsString(reader);
		Delay = d2OClass.ReadFieldAsInt(reader);
		Group = d2OClass.ReadFieldAsInt(reader);
	}

	public override void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsBoolean(writer, VisibleInTooltip);
		d2OClass.WriteFieldAsInt(writer, Random);
		d2OClass.WriteFieldAsString(writer, RawZone);
		d2OClass.WriteFieldAsInt(writer, TargetId);
		d2OClass.WriteFieldAsString(writer, TargetMask);
		d2OClass.WriteFieldAsInt(writer, EffectId);
		d2OClass.WriteFieldAsInt(writer, Duration);
		d2OClass.WriteFieldAsBoolean(writer, VisibleInFightLog);
		d2OClass.WriteFieldAsInt(writer, EffectUid);
		d2OClass.WriteFieldAsInt(writer, Value);
		d2OClass.WriteFieldAsBoolean(writer, VisibleInBuffUi);
		d2OClass.WriteFieldAsString(writer, Triggers);
		d2OClass.WriteFieldAsInt(writer, Delay);
		d2OClass.WriteFieldAsInt(writer, Group);
	}
}
