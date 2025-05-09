// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Items;

public sealed class IncarnationLevel : IDatacenterObject<IncarnationLevel>
{
	public static string ModuleName =>
		"IncarnationLevels";

	public required int Id { get; set; }

	public required int IncarnationId { get; set; }

	public required int Level { get; set; }

	public required int RequiredXp { get; set; }

	public static IncarnationLevel Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new IncarnationLevel
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			IncarnationId = d2OClass.Fields[1].AsInt(reader),
			Level = d2OClass.Fields[2].AsInt(reader),
			RequiredXp = d2OClass.Fields[3].AsInt(reader),
		};
	}
}
