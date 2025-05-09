// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Effects;

namespace Krosmoz.Protocol.Datacenter.Items;

public sealed class ItemSet : IDatacenterObject<ItemSet>
{
	public static string ModuleName =>
		"ItemSets";

	public required int Id { get; set; }

	public required List<uint> Items { get; set; }

	public required int NameId { get; set; }

	public required bool BonusIsSecret { get; set; }

	public required List<List<EffectInstance>> Effects { get; set; }

	public static ItemSet Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new ItemSet
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			Items = d2OClass.Fields[1].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			NameId = d2OClass.Fields[2].AsI18N(reader),
			BonusIsSecret = d2OClass.Fields[3].AsBoolean(reader),
			Effects = d2OClass.Fields[4].AsListOfList<EffectInstance>(reader, static (field, r) => field.AsObject<EffectInstance>(r)),
		};
	}
}
