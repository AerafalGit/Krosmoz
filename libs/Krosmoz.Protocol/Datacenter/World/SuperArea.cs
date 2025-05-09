// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class SuperArea : IDatacenterObject<SuperArea>
{
	public static string ModuleName =>
		"SuperAreas";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int WorldmapId { get; set; }

	public static SuperArea Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new SuperArea
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			WorldmapId = d2OClass.Fields[2].AsInt(reader),
		};
	}
}
