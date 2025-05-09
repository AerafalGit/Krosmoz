// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Mounts;

public sealed class Mount : IDatacenterObject<Mount>
{
	public static string ModuleName =>
		"Mounts";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required string Look { get; set; }

	public static Mount Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Mount
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			Look = d2OClass.Fields[2].AsString(reader),
		};
	}
}
