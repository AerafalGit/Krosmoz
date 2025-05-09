// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Mounts;

public sealed class MountBehavior : IDatacenterObject<MountBehavior>
{
	public static string ModuleName =>
		"MountBehaviors";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int DescriptionId { get; set; }

	public static MountBehavior Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new MountBehavior
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			DescriptionId = d2OClass.Fields[2].AsI18N(reader),
		};
	}
}
