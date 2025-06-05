// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Effects;

public class EffectInstance : IDatacenterObject
{
	public static string ModuleName =>
		"SpellLevels";

	public required int EffectUid { get; set; }

	public required int EffectId { get; set; }

	public required int TargetId { get; set; }

	public required string TargetMask { get; set; }

	public required int Duration { get; set; }

	public required int Random { get; set; }

	public required int Group { get; set; }

	public required bool VisibleInTooltip { get; set; }

	public required bool VisibleInBuffUi { get; set; }

	public required bool VisibleInFightLog { get; set; }

	public required string RawZone { get; set; }

	public required int Delay { get; set; }

	public required string Triggers { get; set; }

	public virtual void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		EffectUid = d2OClass.ReadFieldAsInt(reader);
		EffectId = d2OClass.ReadFieldAsInt(reader);
		TargetId = d2OClass.ReadFieldAsInt(reader);
		TargetMask = d2OClass.ReadFieldAsString(reader);
		Duration = d2OClass.ReadFieldAsInt(reader);
		Random = d2OClass.ReadFieldAsInt(reader);
		Group = d2OClass.ReadFieldAsInt(reader);
		VisibleInTooltip = d2OClass.ReadFieldAsBoolean(reader);
		VisibleInBuffUi = d2OClass.ReadFieldAsBoolean(reader);
		VisibleInFightLog = d2OClass.ReadFieldAsBoolean(reader);
		RawZone = d2OClass.ReadFieldAsString(reader);
		Delay = d2OClass.ReadFieldAsInt(reader);
		Triggers = d2OClass.ReadFieldAsString(reader);
	}

	public virtual void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, EffectUid);
		d2OClass.WriteFieldAsInt(writer, EffectId);
		d2OClass.WriteFieldAsInt(writer, TargetId);
		d2OClass.WriteFieldAsString(writer, TargetMask);
		d2OClass.WriteFieldAsInt(writer, Duration);
		d2OClass.WriteFieldAsInt(writer, Random);
		d2OClass.WriteFieldAsInt(writer, Group);
		d2OClass.WriteFieldAsBoolean(writer, VisibleInTooltip);
		d2OClass.WriteFieldAsBoolean(writer, VisibleInBuffUi);
		d2OClass.WriteFieldAsBoolean(writer, VisibleInFightLog);
		d2OClass.WriteFieldAsString(writer, RawZone);
		d2OClass.WriteFieldAsInt(writer, Delay);
		d2OClass.WriteFieldAsString(writer, Triggers);
	}
}
