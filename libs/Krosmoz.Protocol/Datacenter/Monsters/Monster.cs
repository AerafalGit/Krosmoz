// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Monsters;

public sealed class Monster : IDatacenterObject
{
	public static string ModuleName =>
		"Monsters";

	public required int Id { get; set; }

	public required int NameId { get; set; }

	public required string Name { get; set; }

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

	public required int SpeedAdjust { get; set; }

	public required int CreatureBoneId { get; set; }

	public required bool CanBePushed { get; set; }

	public required bool FastAnimsFun { get; set; }

	public required bool CanSwitchPos { get; set; }

	public required List<uint> IncompatibleIdols { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		NameId = d2OClass.ReadFieldAsI18N(reader);
		Name = d2OClass.ReadFieldAsI18NString(NameId);
		GfxId = d2OClass.ReadFieldAsInt(reader);
		Race = d2OClass.ReadFieldAsInt(reader);
		Grades = d2OClass.ReadFieldAsList<MonsterGrade>(reader, static (c, r) => c.ReadFieldAsObject<MonsterGrade>(r));
		Look = d2OClass.ReadFieldAsString(reader);
		UseSummonSlot = d2OClass.ReadFieldAsBoolean(reader);
		UseBombSlot = d2OClass.ReadFieldAsBoolean(reader);
		CanPlay = d2OClass.ReadFieldAsBoolean(reader);
		AnimFunList = d2OClass.ReadFieldAsList<AnimFunMonsterData>(reader, static (c, r) => c.ReadFieldAsObject<AnimFunMonsterData>(r));
		CanTackle = d2OClass.ReadFieldAsBoolean(reader);
		IsBoss = d2OClass.ReadFieldAsBoolean(reader);
		Drops = d2OClass.ReadFieldAsList<MonsterDrop>(reader, static (c, r) => c.ReadFieldAsObject<MonsterDrop>(r));
		Subareas = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		Spells = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		FavoriteSubareaId = d2OClass.ReadFieldAsInt(reader);
		IsMiniBoss = d2OClass.ReadFieldAsBoolean(reader);
		IsQuestMonster = d2OClass.ReadFieldAsBoolean(reader);
		CorrespondingMiniBossId = d2OClass.ReadFieldAsInt(reader);
		SpeedAdjust = d2OClass.ReadFieldAsInt(reader);
		CreatureBoneId = d2OClass.ReadFieldAsInt(reader);
		CanBePushed = d2OClass.ReadFieldAsBoolean(reader);
		FastAnimsFun = d2OClass.ReadFieldAsBoolean(reader);
		CanSwitchPos = d2OClass.ReadFieldAsBoolean(reader);
		IncompatibleIdols = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, NameId);
		d2OClass.WriteFieldAsInt(writer, GfxId);
		d2OClass.WriteFieldAsInt(writer, Race);
		d2OClass.WriteFieldAsList<MonsterGrade>(writer, Grades, static (c, r, v) => c.WriteFieldAsObject<MonsterGrade>(r, v));
		d2OClass.WriteFieldAsString(writer, Look);
		d2OClass.WriteFieldAsBoolean(writer, UseSummonSlot);
		d2OClass.WriteFieldAsBoolean(writer, UseBombSlot);
		d2OClass.WriteFieldAsBoolean(writer, CanPlay);
		d2OClass.WriteFieldAsList<AnimFunMonsterData>(writer, AnimFunList, static (c, r, v) => c.WriteFieldAsObject<AnimFunMonsterData>(r, v));
		d2OClass.WriteFieldAsBoolean(writer, CanTackle);
		d2OClass.WriteFieldAsBoolean(writer, IsBoss);
		d2OClass.WriteFieldAsList<MonsterDrop>(writer, Drops, static (c, r, v) => c.WriteFieldAsObject<MonsterDrop>(r, v));
		d2OClass.WriteFieldAsList(writer, Subareas, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsList(writer, Spells, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsInt(writer, FavoriteSubareaId);
		d2OClass.WriteFieldAsBoolean(writer, IsMiniBoss);
		d2OClass.WriteFieldAsBoolean(writer, IsQuestMonster);
		d2OClass.WriteFieldAsInt(writer, CorrespondingMiniBossId);
		d2OClass.WriteFieldAsInt(writer, SpeedAdjust);
		d2OClass.WriteFieldAsInt(writer, CreatureBoneId);
		d2OClass.WriteFieldAsBoolean(writer, CanBePushed);
		d2OClass.WriteFieldAsBoolean(writer, FastAnimsFun);
		d2OClass.WriteFieldAsBoolean(writer, CanSwitchPos);
		d2OClass.WriteFieldAsList(writer, IncompatibleIdols, static (c, r, v) => c.WriteFieldAsUInt(r, v));
	}
}
