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

	public required string ShortName { get; set; }

	public required int LongNameId { get; set; }

	public required string LongName { get; set; }

	public required int DescriptionId { get; set; }

	public required string Description { get; set; }

	public required int GameplayDescriptionId { get; set; }

	public required string GameplayDescription { get; set; }

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

	public required List<BreedRoleByBreed> BreedRoles { get; set; }

	public required List<uint> MaleColors { get; set; }

	public required List<uint> FemaleColors { get; set; }

	public required int SpawnMap { get; set; }

	public void Deserialize(D2OClass d2OClass, BigEndianReader reader)
	{
		Id = d2OClass.ReadFieldAsInt(reader);
		ShortNameId = d2OClass.ReadFieldAsI18N(reader);
		ShortName = d2OClass.ReadFieldAsI18NString(ShortNameId);
		LongNameId = d2OClass.ReadFieldAsI18N(reader);
		LongName = d2OClass.ReadFieldAsI18NString(LongNameId);
		DescriptionId = d2OClass.ReadFieldAsI18N(reader);
		Description = d2OClass.ReadFieldAsI18NString(DescriptionId);
		GameplayDescriptionId = d2OClass.ReadFieldAsI18N(reader);
		GameplayDescription = d2OClass.ReadFieldAsI18NString(GameplayDescriptionId);
		MaleLook = d2OClass.ReadFieldAsString(reader);
		FemaleLook = d2OClass.ReadFieldAsString(reader);
		CreatureBonesId = d2OClass.ReadFieldAsInt(reader);
		MaleArtwork = d2OClass.ReadFieldAsInt(reader);
		FemaleArtwork = d2OClass.ReadFieldAsInt(reader);
		StatsPointsForStrength = d2OClass.ReadFieldAsListOfList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		StatsPointsForIntelligence = d2OClass.ReadFieldAsListOfList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		StatsPointsForChance = d2OClass.ReadFieldAsListOfList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		StatsPointsForAgility = d2OClass.ReadFieldAsListOfList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		StatsPointsForVitality = d2OClass.ReadFieldAsListOfList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		StatsPointsForWisdom = d2OClass.ReadFieldAsListOfList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		BreedSpellsId = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		BreedRoles = d2OClass.ReadFieldAsList<BreedRoleByBreed>(reader, static (c, r) => c.ReadFieldAsObject<BreedRoleByBreed>(r));
		MaleColors = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		FemaleColors = d2OClass.ReadFieldAsList(reader, static (c, r) => c.ReadFieldAsUInt(r));
		SpawnMap = d2OClass.ReadFieldAsInt(reader);
	}

	public void Serialize(D2OClass d2OClass, BigEndianWriter writer)
	{
		d2OClass.WriteFieldAsInt(writer, Id);
		d2OClass.WriteFieldAsI18N(writer, ShortNameId);
		d2OClass.WriteFieldAsI18N(writer, LongNameId);
		d2OClass.WriteFieldAsI18N(writer, DescriptionId);
		d2OClass.WriteFieldAsI18N(writer, GameplayDescriptionId);
		d2OClass.WriteFieldAsString(writer, MaleLook);
		d2OClass.WriteFieldAsString(writer, FemaleLook);
		d2OClass.WriteFieldAsInt(writer, CreatureBonesId);
		d2OClass.WriteFieldAsInt(writer, MaleArtwork);
		d2OClass.WriteFieldAsInt(writer, FemaleArtwork);
		d2OClass.WriteFieldAsListOfList(writer, StatsPointsForStrength, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsListOfList(writer, StatsPointsForIntelligence, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsListOfList(writer, StatsPointsForChance, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsListOfList(writer, StatsPointsForAgility, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsListOfList(writer, StatsPointsForVitality, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsListOfList(writer, StatsPointsForWisdom, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsList(writer, BreedSpellsId, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsList<BreedRoleByBreed>(writer, BreedRoles, static (c, r, v) => c.WriteFieldAsObject<BreedRoleByBreed>(r, v));
		d2OClass.WriteFieldAsList(writer, MaleColors, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsList(writer, FemaleColors, static (c, r, v) => c.WriteFieldAsUInt(r, v));
		d2OClass.WriteFieldAsInt(writer, SpawnMap);
	}
}
