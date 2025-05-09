// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Effects.Instances;

public sealed class EffectInstanceInteger : IDatacenterObject<EffectInstanceInteger>
{
	public static string ModuleName =>
		"SpellLevels";

	public required int EffectId { get; set; }

	public required int Duration { get; set; }

	public required bool Hidden { get; set; }

	public required int Random { get; set; }

	public required int Value { get; set; }

	public required string RawZone { get; set; }

	public required int TargetId { get; set; }

	public required int Group { get; set; }

	public required string TargetMask { get; set; }

	public static EffectInstanceInteger Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new EffectInstanceInteger
		{
			EffectId = d2OClass.Fields[0].AsInt(reader),
			Duration = d2OClass.Fields[1].AsInt(reader),
			Hidden = d2OClass.Fields[2].AsBoolean(reader),
			Random = d2OClass.Fields[3].AsInt(reader),
			Value = d2OClass.Fields[4].AsInt(reader),
			RawZone = d2OClass.Fields[5].AsString(reader),
			TargetId = d2OClass.Fields[6].AsInt(reader),
			Group = d2OClass.Fields[7].AsInt(reader),
			TargetMask = d2OClass.Fields[8].AsString(reader),
		};
	}
}
