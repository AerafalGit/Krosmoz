// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Monsters;

public sealed class MonsterGrade : IDatacenterObject<MonsterGrade>
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

	public static MonsterGrade Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		return new MonsterGrade
		{
			Grade = d2OClass.Fields[0].AsInt(reader),
			MonsterId = d2OClass.Fields[1].AsInt(reader),
			Level = d2OClass.Fields[2].AsInt(reader),
			LifePoints = d2OClass.Fields[3].AsInt(reader),
			ActionPoints = d2OClass.Fields[4].AsInt(reader),
			MovementPoints = d2OClass.Fields[5].AsInt(reader),
			PaDodge = d2OClass.Fields[6].AsInt(reader),
			PmDodge = d2OClass.Fields[7].AsInt(reader),
			Wisdom = d2OClass.Fields[8].AsInt(reader),
			EarthResistance = d2OClass.Fields[9].AsInt(reader),
			AirResistance = d2OClass.Fields[10].AsInt(reader),
			FireResistance = d2OClass.Fields[11].AsInt(reader),
			WaterResistance = d2OClass.Fields[12].AsInt(reader),
			NeutralResistance = d2OClass.Fields[13].AsInt(reader),
			GradeXp = d2OClass.Fields[14].AsInt(reader),
		};
	}
}
