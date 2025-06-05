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

	public required bool HasCriteria { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		DropId = d2OClass.ReadFieldAsInt(reader);
		MonsterId = d2OClass.ReadFieldAsInt(reader);
		ObjectId = d2OClass.ReadFieldAsInt(reader);
		PercentDropForGrade1 = d2OClass.ReadFieldAsNumber(reader);
		PercentDropForGrade2 = d2OClass.ReadFieldAsNumber(reader);
		PercentDropForGrade3 = d2OClass.ReadFieldAsNumber(reader);
		PercentDropForGrade4 = d2OClass.ReadFieldAsNumber(reader);
		PercentDropForGrade5 = d2OClass.ReadFieldAsNumber(reader);
		Count = d2OClass.ReadFieldAsInt(reader);
		HasCriteria = d2OClass.ReadFieldAsBoolean(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, DropId);
		d2OClass.WriteFieldAsInt(writer, MonsterId);
		d2OClass.WriteFieldAsInt(writer, ObjectId);
		d2OClass.WriteFieldAsNumber(writer, PercentDropForGrade1);
		d2OClass.WriteFieldAsNumber(writer, PercentDropForGrade2);
		d2OClass.WriteFieldAsNumber(writer, PercentDropForGrade3);
		d2OClass.WriteFieldAsNumber(writer, PercentDropForGrade4);
		d2OClass.WriteFieldAsNumber(writer, PercentDropForGrade5);
		d2OClass.WriteFieldAsInt(writer, Count);
		d2OClass.WriteFieldAsBoolean(writer, HasCriteria);
	}
}
