// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Items;

public sealed class IncarnationLevel : IDatacenterObject
{
	public static string ModuleName =>
		"IncarnationLevels";

	public required int Id { get; set; }

	public required int IncarnationId { get; set; }

	public required int Level { get; set; }

	public required int RequiredXp { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		IncarnationId = d2OClass.ReadFieldAsInt(reader);
		Level = d2OClass.ReadFieldAsInt(reader);
		RequiredXp = d2OClass.ReadFieldAsInt(reader);
	}
}
