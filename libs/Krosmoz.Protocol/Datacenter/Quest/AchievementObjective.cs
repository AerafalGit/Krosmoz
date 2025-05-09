// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Quest;

public sealed class AchievementObjective : IDatacenterObject
{
	public static string ModuleName =>
		"AchievementObjectives";

	public required int Id { get; set; }

	public required int AchievementId { get; set; }

	public required int NameId { get; set; }

	public required string Criterion { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		AchievementId = d2OClass.Fields[1].AsInt(reader);
		NameId = d2OClass.Fields[2].AsI18N(reader);
		Criterion = d2OClass.Fields[3].AsString(reader);
	}
}
