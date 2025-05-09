// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Houses;

public sealed class House : IDatacenterObject<House>
{
	public static string ModuleName =>
		"Houses";

	public required int TypeId { get; set; }

	public required int DefaultPrice { get; set; }

	public required int NameId { get; set; }

	public required int DescriptionId { get; set; }

	public required int GfxId { get; set; }

	public static House Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new House
		{
			TypeId = d2OClass.Fields[0].AsInt(reader),
			DefaultPrice = d2OClass.Fields[1].AsInt(reader),
			NameId = d2OClass.Fields[2].AsI18N(reader),
			DescriptionId = d2OClass.Fields[3].AsI18N(reader),
			GfxId = d2OClass.Fields[4].AsInt(reader),
		};
	}
}
