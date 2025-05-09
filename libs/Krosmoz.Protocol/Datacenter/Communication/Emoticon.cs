// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Communication;

public sealed class Emoticon : IDatacenterObject<Emoticon>
{
	public static string ModuleName =>
		"Emoticons";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int ShortcutId { get; set; }

	public required uint Order { get; set; }

	public required string DefaultAnim { get; set; }

	public required bool Persistancy { get; set; }

	public required bool Eight_directions { get; set; }

	public required bool Aura { get; set; }

	public required List<string> Anims { get; set; }

	public required uint Cooldown { get; set; }

	public required uint Duration { get; set; }

	public required uint Weight { get; set; }

	public static Emoticon Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Emoticon
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			ShortcutId = d2OClass.Fields[2].AsI18N(reader),
			Order = d2OClass.Fields[3].AsUInt(reader),
			DefaultAnim = d2OClass.Fields[4].AsString(reader),
			Persistancy = d2OClass.Fields[5].AsBoolean(reader),
			Eight_directions = d2OClass.Fields[6].AsBoolean(reader),
			Aura = d2OClass.Fields[7].AsBoolean(reader),
			Anims = d2OClass.Fields[8].AsList<string>(reader, static (field, r) => field.AsString(r)),
			Cooldown = d2OClass.Fields[9].AsUInt(reader),
			Duration = d2OClass.Fields[10].AsUInt(reader),
			Weight = d2OClass.Fields[11].AsUInt(reader),
		};
	}
}
