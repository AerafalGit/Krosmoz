// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Monsters;

public sealed class Monster : IDatacenterObject<Monster>
{
	public static string ModuleName =>
		"Monsters";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required int GfxId { get; set; }

	public required int Race { get; set; }

	public required List<MonsterGrade> Grades { get; set; }

	public required string Look { get; set; }

	public required bool UseSummonSlot { get; set; }

	public required bool UseBombSlot { get; set; }

	public required bool CanPlay { get; set; }

	public required List<AnimFunMonsterData> AnimFunList { get; set; }

	public required bool CanTackle { get; set; }

	public required bool IsBoss { get; set; }

	public required List<MonsterDrop> Drops { get; set; }

	public required List<uint> Subareas { get; set; }

	public required List<uint> Spells { get; set; }

	public required int FavoriteSubareaId { get; set; }

	public required bool IsMiniBoss { get; set; }

	public required bool IsQuestMonster { get; set; }

	public required int CorrespondingMiniBossId { get; set; }

	public static Monster Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new Monster
		{
			Id = d2OClass.Fields[0].AsInt(reader),
			NameId = d2OClass.Fields[1].AsI18N(reader),
			GfxId = d2OClass.Fields[2].AsInt(reader),
			Race = d2OClass.Fields[3].AsInt(reader),
			Grades = d2OClass.Fields[4].AsList<MonsterGrade>(reader, static (field, r) => field.AsObject<MonsterGrade>(r)),
			Look = d2OClass.Fields[5].AsString(reader),
			UseSummonSlot = d2OClass.Fields[6].AsBoolean(reader),
			UseBombSlot = d2OClass.Fields[7].AsBoolean(reader),
			CanPlay = d2OClass.Fields[8].AsBoolean(reader),
			AnimFunList = d2OClass.Fields[9].AsList<AnimFunMonsterData>(reader, static (field, r) => field.AsObject<AnimFunMonsterData>(r)),
			CanTackle = d2OClass.Fields[10].AsBoolean(reader),
			IsBoss = d2OClass.Fields[11].AsBoolean(reader),
			Drops = d2OClass.Fields[12].AsList<MonsterDrop>(reader, static (field, r) => field.AsObject<MonsterDrop>(r)),
			Subareas = d2OClass.Fields[13].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			Spells = d2OClass.Fields[14].AsList<uint>(reader, static (field, r) => field.AsUInt(r)),
			FavoriteSubareaId = d2OClass.Fields[15].AsInt(reader),
			IsMiniBoss = d2OClass.Fields[16].AsBoolean(reader),
			IsQuestMonster = d2OClass.Fields[17].AsBoolean(reader),
			CorrespondingMiniBossId = d2OClass.Fields[18].AsInt(reader),
		};
	}
}
