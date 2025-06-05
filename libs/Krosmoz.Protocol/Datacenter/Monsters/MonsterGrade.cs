// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Monsters;

public sealed class MonsterGrade : IDatacenterObject
{
	public static string ModuleName =>
		"Monsters";

	public required int Grade { get; set; }

	public required int MonsterId { get; set; }

	public required int Level { get; set; }

	public required int LifePoints { get; set; }

	public required int ActionPoints { get; set; }

	public required int MovementPoints { get; set; }

	public required int PaDodge { get; set; }

	public required int PmDodge { get; set; }

	public required int Wisdom { get; set; }

	public required int EarthResistance { get; set; }

	public required int AirResistance { get; set; }

	public required int FireResistance { get; set; }

	public required int WaterResistance { get; set; }

	public required int NeutralResistance { get; set; }

	public required int GradeXp { get; set; }

	public required int DamageReflect { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Grade = d2OClass.ReadFieldAsInt(reader);
		MonsterId = d2OClass.ReadFieldAsInt(reader);
		Level = d2OClass.ReadFieldAsInt(reader);
		LifePoints = d2OClass.ReadFieldAsInt(reader);
		ActionPoints = d2OClass.ReadFieldAsInt(reader);
		MovementPoints = d2OClass.ReadFieldAsInt(reader);
		PaDodge = d2OClass.ReadFieldAsInt(reader);
		PmDodge = d2OClass.ReadFieldAsInt(reader);
		Wisdom = d2OClass.ReadFieldAsInt(reader);
		EarthResistance = d2OClass.ReadFieldAsInt(reader);
		AirResistance = d2OClass.ReadFieldAsInt(reader);
		FireResistance = d2OClass.ReadFieldAsInt(reader);
		WaterResistance = d2OClass.ReadFieldAsInt(reader);
		NeutralResistance = d2OClass.ReadFieldAsInt(reader);
		GradeXp = d2OClass.ReadFieldAsInt(reader);
		DamageReflect = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Grade);
		d2OClass.WriteFieldAsInt(writer, MonsterId);
		d2OClass.WriteFieldAsInt(writer, Level);
		d2OClass.WriteFieldAsInt(writer, LifePoints);
		d2OClass.WriteFieldAsInt(writer, ActionPoints);
		d2OClass.WriteFieldAsInt(writer, MovementPoints);
		d2OClass.WriteFieldAsInt(writer, PaDodge);
		d2OClass.WriteFieldAsInt(writer, PmDodge);
		d2OClass.WriteFieldAsInt(writer, Wisdom);
		d2OClass.WriteFieldAsInt(writer, EarthResistance);
		d2OClass.WriteFieldAsInt(writer, AirResistance);
		d2OClass.WriteFieldAsInt(writer, FireResistance);
		d2OClass.WriteFieldAsInt(writer, WaterResistance);
		d2OClass.WriteFieldAsInt(writer, NeutralResistance);
		d2OClass.WriteFieldAsInt(writer, GradeXp);
		d2OClass.WriteFieldAsInt(writer, DamageReflect);
	}
}
