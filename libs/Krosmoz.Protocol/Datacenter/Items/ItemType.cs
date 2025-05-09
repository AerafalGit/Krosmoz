// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Items;

public sealed class ItemType : IDatacenterObject<ItemType>
{
	public static string ModuleName =>
		"ItemTypes";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int SuperTypeId { get; set; }

	public required bool Plural { get; set; }

	public required int Gender { get; set; }

	public required string RawZone { get; set; }

	public required bool NeedUseConfirm { get; set; }

	public static ItemType Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new ItemType
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			SuperTypeId = d2OClass.Fields[2].AsInt(reader),
			Plural = d2OClass.Fields[3].AsBoolean(reader),
			Gender = d2OClass.Fields[4].AsInt(reader),
			RawZone = d2OClass.Fields[5].AsString(reader),
			NeedUseConfirm = d2OClass.Fields[6].AsBoolean(reader),
		};
	}
}
