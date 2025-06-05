// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Monsters;

public sealed class CompanionSpell : IDatacenterObject
{
	public static string ModuleName =>
		"CompanionSpells";

	public required int Id { get; set; }

	public required int SpellId { get; set; }

	public required int CompanionId { get; set; }

	public required string GradeByLevel { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		SpellId = d2OClass.ReadFieldAsInt(reader);
		CompanionId = d2OClass.ReadFieldAsInt(reader);
		GradeByLevel = d2OClass.ReadFieldAsString(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsInt(writer, SpellId);
		d2OClass.WriteFieldAsInt(writer, CompanionId);
		d2OClass.WriteFieldAsString(writer, GradeByLevel);
	}
}
