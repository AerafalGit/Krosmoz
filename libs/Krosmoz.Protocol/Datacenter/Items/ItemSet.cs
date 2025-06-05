// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Effects;

namespace Krosmoz.Protocol.Datacenter.Items;

public sealed class ItemSet : IDatacenterObject
{
	public static string ModuleName =>
		"ItemSets";

	public required int Id { get; set; }

	public required List<uint> Items { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

	public required bool BonusIsSecret { get; set; }

	public required List<List<EffectInstance>> Effects { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		Items = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		BonusIsSecret = d2OClass.ReadFieldAsBoolean(reader);
		Effects = d2OClass.ReadFieldAsListOfList<EffectInstance>(reader, static (c, r) => c.ReadFieldAsObject<EffectInstance>(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsList(writer, Items, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsBoolean(writer, BonusIsSecret);
		d2OClass.WriteFieldAsListOfList(writer, Effects, static (c, r, v) => c.WriteFieldAsObject(r, v));
	}
}
