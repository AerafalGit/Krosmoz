// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Monsters;

public sealed class MonsterDrop : IDatacenterObject
{
	public static string ModuleName =>
		"Monsters";

	public required int DropId { get; set; }

	public required int MonsterId { get; set; }

	public required int ObjectId { get; set; }

	public required double PercentDropForGrade1 { get; set; }

	public required double PercentDropForGrade2 { get; set; }

	public required double PercentDropForGrade3 { get; set; }

	public required double PercentDropForGrade4 { get; set; }

	public required double PercentDropForGrade5 { get; set; }

	public required int Count { get; set; }

	public required int FindCeil { get; set; }

	public required bool HasCriteria { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		DropId = d2OClass.Fields[0].AsInt(reader);
		MonsterId = d2OClass.Fields[1].AsInt(reader);
		ObjectId = d2OClass.Fields[2].AsInt(reader);
		PercentDropForGrade1 = d2OClass.Fields[3].AsDouble(reader);
		PercentDropForGrade2 = d2OClass.Fields[4].AsDouble(reader);
		PercentDropForGrade3 = d2OClass.Fields[5].AsDouble(reader);
		PercentDropForGrade4 = d2OClass.Fields[6].AsDouble(reader);
		PercentDropForGrade5 = d2OClass.Fields[7].AsDouble(reader);
		Count = d2OClass.Fields[8].AsInt(reader);
		FindCeil = d2OClass.Fields[9].AsInt(reader);
		HasCriteria = d2OClass.Fields[10].AsBoolean(reader);
	}
}
