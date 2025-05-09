// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter.Breeds;

public sealed class Breed : IDatacenterObject
{
	public static string ModuleName =>
		"Breeds";

	public required int Id { get; set; }

	public required int ShortNameId { get; set; }

	public required int LongNameId { get; set; }

	public required int DescriptionId { get; set; }

	public required int GameplayDescriptionId { get; set; }

	public required string MaleLook { get; set; }

	public required string FemaleLook { get; set; }

	public required int CreatureBonesId { get; set; }

	public required int MaleArtwork { get; set; }

	public required int FemaleArtwork { get; set; }

	public required List<List<uint>> StatsPointsForStrength { get; set; }

	public required List<List<uint>> StatsPointsForIntelligence { get; set; }

	public required List<List<uint>> StatsPointsForChance { get; set; }

	public required List<List<uint>> StatsPointsForAgility { get; set; }

	public required List<List<uint>> StatsPointsForVitality { get; set; }

	public required List<List<uint>> StatsPointsForWisdom { get; set; }

	public required List<uint> BreedSpellsId { get; set; }

	public required List<uint> MaleColors { get; set; }

	public required List<uint> FemaleColors { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.Fields[0].AsInt(reader);
		ShortNameId = d2OClass.Fields[1].AsI18N(reader);
		LongNameId = d2OClass.Fields[2].AsI18N(reader);
		DescriptionId = d2OClass.Fields[3].AsI18N(reader);
		GameplayDescriptionId = d2OClass.Fields[4].AsI18N(reader);
		MaleLook = d2OClass.Fields[5].AsString(reader);
		FemaleLook = d2OClass.Fields[6].AsString(reader);
		CreatureBonesId = d2OClass.Fields[7].AsInt(reader);
		MaleArtwork = d2OClass.Fields[8].AsInt(reader);
		FemaleArtwork = d2OClass.Fields[9].AsInt(reader);
		StatsPointsForStrength = d2OClass.Fields[10].AsListOfList(reader, static (field, r) => field.AsUInt(r));
		StatsPointsForIntelligence = d2OClass.Fields[11].AsListOfList(reader, static (field, r) => field.AsUInt(r));
		StatsPointsForChance = d2OClass.Fields[12].AsListOfList(reader, static (field, r) => field.AsUInt(r));
		StatsPointsForAgility = d2OClass.Fields[13].AsListOfList(reader, static (field, r) => field.AsUInt(r));
		StatsPointsForVitality = d2OClass.Fields[14].AsListOfList(reader, static (field, r) => field.AsUInt(r));
		StatsPointsForWisdom = d2OClass.Fields[15].AsListOfList(reader, static (field, r) => field.AsUInt(r));
		BreedSpellsId = d2OClass.Fields[16].AsList(reader, static (field, r) => field.AsUInt(r));
		MaleColors = d2OClass.Fields[17].AsList(reader, static (field, r) => field.AsUInt(r));
		FemaleColors = d2OClass.Fields[18].AsList(reader, static (field, r) => field.AsUInt(r));
	}
}
