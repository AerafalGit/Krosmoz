// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.AmbientSounds;

public sealed class AmbientSound : IDatacenterObject<AmbientSound>
{
	public static string ModuleName =>
		"SubAreas";

	public required int Id { get; set; }

	public required int Volume { get; set; }

	public required int CriterionId { get; set; }

	public required int SilenceMin { get; set; }

	public required int SilenceMax { get; set; }

	public required int Channel { get; set; }

	public required int Type_id { get; set; }

	public static AmbientSound Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new AmbientSound
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			Volume = d2OClass.Fields[1].AsInt(reader),
			CriterionId = d2OClass.Fields[2].AsInt(reader),
			SilenceMin = d2OClass.Fields[3].AsInt(reader),
			SilenceMax = d2OClass.Fields[4].AsInt(reader),
			Channel = d2OClass.Fields[5].AsInt(reader),
			Type_id = d2OClass.Fields[6].AsInt(reader),
		};
	}
}
