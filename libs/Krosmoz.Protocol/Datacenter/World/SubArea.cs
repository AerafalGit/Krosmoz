// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.AmbientSounds;
using Krosmoz.Protocol.Datacenter.Flash.Geom;

namespace Krosmoz.Protocol.Datacenter.World;

public sealed class SubArea : IDatacenterObject<SubArea>
{
	public static string ModuleName =>
		"SubAreas";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int AreaId { get; set; }

	public required List<AmbientSound> AmbientSounds { get; set; }

	public required List<uint> MapIds { get; set; }

	public required Rectangle Bounds { get; set; }

	public required List<int> Shape { get; set; }

	public required List<uint> CustomWorldMap { get; set; }

	public required uint PackId { get; set; }

	public required uint Level { get; set; }

	public required bool IsConquestVillage { get; set; }

	public required bool DisplayOnWorldMap { get; set; }

	public required List<uint> Monsters { get; set; }

	public required List<uint> EntranceMapIds { get; set; }

	public required List<uint> ExitMapIds { get; set; }

	public required bool Capturable { get; set; }

	public static SubArea Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new SubArea
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			AreaId = d2OClass.Fields[2].AsInt(reader),
			AmbientSounds = d2OClass.Fields[3].AsList<AmbientSound>(reader, static (field, r) => field.AsObject<AmbientSound>(r)),
			MapIds = d2OClass.Fields[4].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			Bounds = d2OClass.Fields[5].AsObject<Rectangle>(reader),
			Shape = d2OClass.Fields[6].AsList<int>(reader, static (field, r) => field.AsInt(r)),
			CustomWorldMap = d2OClass.Fields[7].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			PackId = d2OClass.Fields[8].AsUInt(reader),
			Level = d2OClass.Fields[9].AsUInt(reader),
			IsConquestVillage = d2OClass.Fields[10].AsBoolean(reader),
			DisplayOnWorldMap = d2OClass.Fields[11].AsBoolean(reader),
			Monsters = d2OClass.Fields[12].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			EntranceMapIds = d2OClass.Fields[13].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			ExitMapIds = d2OClass.Fields[14].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			Capturable = d2OClass.Fields[15].AsBoolean(reader),
		};
	}
}
