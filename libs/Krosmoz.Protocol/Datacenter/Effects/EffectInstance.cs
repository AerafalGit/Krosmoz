// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Effects;

public class EffectInstance : IDatacenterObject
{
	public static string ModuleName =>
		"SpellLevels";

	public required int EffectId { get; set; }

	public required int TargetId { get; set; }

	public required string TargetMask { get; set; }

	public required int Duration { get; set; }

	public required int Random { get; set; }

	public required int Group { get; set; }

	public required bool Hidden { get; set; }

	public required string RawZone { get; set; }

	public virtual void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		EffectId = d2OClass.Fields[0].AsInt(reader);
		TargetId = d2OClass.Fields[1].AsInt(reader);
		TargetMask = d2OClass.Fields[2].AsString(reader);
		Duration = d2OClass.Fields[3].AsInt(reader);
		Random = d2OClass.Fields[4].AsInt(reader);
		Group = d2OClass.Fields[5].AsInt(reader);
		Hidden = d2OClass.Fields[6].AsBoolean(reader);
		RawZone = d2OClass.Fields[7].AsString(reader);
	}
}
