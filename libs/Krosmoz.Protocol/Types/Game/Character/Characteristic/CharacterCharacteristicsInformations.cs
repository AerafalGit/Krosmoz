// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Alignment;

namespace Krosmoz.Protocol.Types.Game.Character.Characteristic;

public sealed class CharacterCharacteristicsInformations : DofusType
{
	public new const ushort StaticProtocolId = 8;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static CharacterCharacteristicsInformations Empty =>
		new() { Experience = 0, ExperienceLevelFloor = 0, ExperienceNextLevelFloor = 0, Kamas = 0, StatsPoints = 0, AdditionnalPoints = 0, SpellsPoints = 0, AlignmentInfos = ActorExtendedAlignmentInformations.Empty, LifePoints = 0, MaxLifePoints = 0, EnergyPoints = 0, MaxEnergyPoints = 0, ActionPointsCurrent = 0, MovementPointsCurrent = 0, Initiative = CharacterBaseCharacteristic.Empty, Prospecting = CharacterBaseCharacteristic.Empty, ActionPoints = CharacterBaseCharacteristic.Empty, MovementPoints = CharacterBaseCharacteristic.Empty, Strength = CharacterBaseCharacteristic.Empty, Vitality = CharacterBaseCharacteristic.Empty, Wisdom = CharacterBaseCharacteristic.Empty, Chance = CharacterBaseCharacteristic.Empty, Agility = CharacterBaseCharacteristic.Empty, Intelligence = CharacterBaseCharacteristic.Empty, Range = CharacterBaseCharacteristic.Empty, SummonableCreaturesBoost = CharacterBaseCharacteristic.Empty, Reflect = CharacterBaseCharacteristic.Empty, CriticalHit = CharacterBaseCharacteristic.Empty, CriticalHitWeapon = 0, CriticalMiss = CharacterBaseCharacteristic.Empty, HealBonus = CharacterBaseCharacteristic.Empty, AllDamagesBonus = CharacterBaseCharacteristic.Empty, WeaponDamagesBonusPercent = CharacterBaseCharacteristic.Empty, DamagesBonusPercent = CharacterBaseCharacteristic.Empty, TrapBonus = CharacterBaseCharacteristic.Empty, TrapBonusPercent = CharacterBaseCharacteristic.Empty, GlyphBonusPercent = CharacterBaseCharacteristic.Empty, PermanentDamagePercent = CharacterBaseCharacteristic.Empty, TackleBlock = CharacterBaseCharacteristic.Empty, TackleEvade = CharacterBaseCharacteristic.Empty, PAAttack = CharacterBaseCharacteristic.Empty, PMAttack = CharacterBaseCharacteristic.Empty, PushDamageBonus = CharacterBaseCharacteristic.Empty, CriticalDamageBonus = CharacterBaseCharacteristic.Empty, NeutralDamageBonus = CharacterBaseCharacteristic.Empty, EarthDamageBonus = CharacterBaseCharacteristic.Empty, WaterDamageBonus = CharacterBaseCharacteristic.Empty, AirDamageBonus = CharacterBaseCharacteristic.Empty, FireDamageBonus = CharacterBaseCharacteristic.Empty, DodgePALostProbability = CharacterBaseCharacteristic.Empty, DodgePMLostProbability = CharacterBaseCharacteristic.Empty, NeutralElementResistPercent = CharacterBaseCharacteristic.Empty, EarthElementResistPercent = CharacterBaseCharacteristic.Empty, WaterElementResistPercent = CharacterBaseCharacteristic.Empty, AirElementResistPercent = CharacterBaseCharacteristic.Empty, FireElementResistPercent = CharacterBaseCharacteristic.Empty, NeutralElementReduction = CharacterBaseCharacteristic.Empty, EarthElementReduction = CharacterBaseCharacteristic.Empty, WaterElementReduction = CharacterBaseCharacteristic.Empty, AirElementReduction = CharacterBaseCharacteristic.Empty, FireElementReduction = CharacterBaseCharacteristic.Empty, PushDamageReduction = CharacterBaseCharacteristic.Empty, CriticalDamageReduction = CharacterBaseCharacteristic.Empty, PvpNeutralElementResistPercent = CharacterBaseCharacteristic.Empty, PvpEarthElementResistPercent = CharacterBaseCharacteristic.Empty, PvpWaterElementResistPercent = CharacterBaseCharacteristic.Empty, PvpAirElementResistPercent = CharacterBaseCharacteristic.Empty, PvpFireElementResistPercent = CharacterBaseCharacteristic.Empty, PvpNeutralElementReduction = CharacterBaseCharacteristic.Empty, PvpEarthElementReduction = CharacterBaseCharacteristic.Empty, PvpWaterElementReduction = CharacterBaseCharacteristic.Empty, PvpAirElementReduction = CharacterBaseCharacteristic.Empty, PvpFireElementReduction = CharacterBaseCharacteristic.Empty, SpellModifications = [], ProbationTime = 0 };

	public required ulong Experience { get; set; }

	public required ulong ExperienceLevelFloor { get; set; }

	public required ulong ExperienceNextLevelFloor { get; set; }

	public required int Kamas { get; set; }

	public required ushort StatsPoints { get; set; }

	public required ushort AdditionnalPoints { get; set; }

	public required ushort SpellsPoints { get; set; }

	public required ActorExtendedAlignmentInformations AlignmentInfos { get; set; }

	public required uint LifePoints { get; set; }

	public required uint MaxLifePoints { get; set; }

	public required ushort EnergyPoints { get; set; }

	public required ushort MaxEnergyPoints { get; set; }

	public required short ActionPointsCurrent { get; set; }

	public required short MovementPointsCurrent { get; set; }

	public required CharacterBaseCharacteristic Initiative { get; set; }

	public required CharacterBaseCharacteristic Prospecting { get; set; }

	public required CharacterBaseCharacteristic ActionPoints { get; set; }

	public required CharacterBaseCharacteristic MovementPoints { get; set; }

	public required CharacterBaseCharacteristic Strength { get; set; }

	public required CharacterBaseCharacteristic Vitality { get; set; }

	public required CharacterBaseCharacteristic Wisdom { get; set; }

	public required CharacterBaseCharacteristic Chance { get; set; }

	public required CharacterBaseCharacteristic Agility { get; set; }

	public required CharacterBaseCharacteristic Intelligence { get; set; }

	public required CharacterBaseCharacteristic Range { get; set; }

	public required CharacterBaseCharacteristic SummonableCreaturesBoost { get; set; }

	public required CharacterBaseCharacteristic Reflect { get; set; }

	public required CharacterBaseCharacteristic CriticalHit { get; set; }

	public required ushort CriticalHitWeapon { get; set; }

	public required CharacterBaseCharacteristic CriticalMiss { get; set; }

	public required CharacterBaseCharacteristic HealBonus { get; set; }

	public required CharacterBaseCharacteristic AllDamagesBonus { get; set; }

	public required CharacterBaseCharacteristic WeaponDamagesBonusPercent { get; set; }

	public required CharacterBaseCharacteristic DamagesBonusPercent { get; set; }

	public required CharacterBaseCharacteristic TrapBonus { get; set; }

	public required CharacterBaseCharacteristic TrapBonusPercent { get; set; }

	public required CharacterBaseCharacteristic GlyphBonusPercent { get; set; }

	public required CharacterBaseCharacteristic PermanentDamagePercent { get; set; }

	public required CharacterBaseCharacteristic TackleBlock { get; set; }

	public required CharacterBaseCharacteristic TackleEvade { get; set; }

	public required CharacterBaseCharacteristic PAAttack { get; set; }

	public required CharacterBaseCharacteristic PMAttack { get; set; }

	public required CharacterBaseCharacteristic PushDamageBonus { get; set; }

	public required CharacterBaseCharacteristic CriticalDamageBonus { get; set; }

	public required CharacterBaseCharacteristic NeutralDamageBonus { get; set; }

	public required CharacterBaseCharacteristic EarthDamageBonus { get; set; }

	public required CharacterBaseCharacteristic WaterDamageBonus { get; set; }

	public required CharacterBaseCharacteristic AirDamageBonus { get; set; }

	public required CharacterBaseCharacteristic FireDamageBonus { get; set; }

	public required CharacterBaseCharacteristic DodgePALostProbability { get; set; }

	public required CharacterBaseCharacteristic DodgePMLostProbability { get; set; }

	public required CharacterBaseCharacteristic NeutralElementResistPercent { get; set; }

	public required CharacterBaseCharacteristic EarthElementResistPercent { get; set; }

	public required CharacterBaseCharacteristic WaterElementResistPercent { get; set; }

	public required CharacterBaseCharacteristic AirElementResistPercent { get; set; }

	public required CharacterBaseCharacteristic FireElementResistPercent { get; set; }

	public required CharacterBaseCharacteristic NeutralElementReduction { get; set; }

	public required CharacterBaseCharacteristic EarthElementReduction { get; set; }

	public required CharacterBaseCharacteristic WaterElementReduction { get; set; }

	public required CharacterBaseCharacteristic AirElementReduction { get; set; }

	public required CharacterBaseCharacteristic FireElementReduction { get; set; }

	public required CharacterBaseCharacteristic PushDamageReduction { get; set; }

	public required CharacterBaseCharacteristic CriticalDamageReduction { get; set; }

	public required CharacterBaseCharacteristic PvpNeutralElementResistPercent { get; set; }

	public required CharacterBaseCharacteristic PvpEarthElementResistPercent { get; set; }

	public required CharacterBaseCharacteristic PvpWaterElementResistPercent { get; set; }

	public required CharacterBaseCharacteristic PvpAirElementResistPercent { get; set; }

	public required CharacterBaseCharacteristic PvpFireElementResistPercent { get; set; }

	public required CharacterBaseCharacteristic PvpNeutralElementReduction { get; set; }

	public required CharacterBaseCharacteristic PvpEarthElementReduction { get; set; }

	public required CharacterBaseCharacteristic PvpWaterElementReduction { get; set; }

	public required CharacterBaseCharacteristic PvpAirElementReduction { get; set; }

	public required CharacterBaseCharacteristic PvpFireElementReduction { get; set; }

	public required IEnumerable<CharacterSpellModification> SpellModifications { get; set; }

	public required int ProbationTime { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt64(Experience);
		writer.WriteVarUInt64(ExperienceLevelFloor);
		writer.WriteVarUInt64(ExperienceNextLevelFloor);
		writer.WriteInt32(Kamas);
		writer.WriteVarUInt16(StatsPoints);
		writer.WriteVarUInt16(AdditionnalPoints);
		writer.WriteVarUInt16(SpellsPoints);
		AlignmentInfos.Serialize(writer);
		writer.WriteVarUInt32(LifePoints);
		writer.WriteVarUInt32(MaxLifePoints);
		writer.WriteVarUInt16(EnergyPoints);
		writer.WriteVarUInt16(MaxEnergyPoints);
		writer.WriteVarInt16(ActionPointsCurrent);
		writer.WriteVarInt16(MovementPointsCurrent);
		Initiative.Serialize(writer);
		Prospecting.Serialize(writer);
		ActionPoints.Serialize(writer);
		MovementPoints.Serialize(writer);
		Strength.Serialize(writer);
		Vitality.Serialize(writer);
		Wisdom.Serialize(writer);
		Chance.Serialize(writer);
		Agility.Serialize(writer);
		Intelligence.Serialize(writer);
		Range.Serialize(writer);
		SummonableCreaturesBoost.Serialize(writer);
		Reflect.Serialize(writer);
		CriticalHit.Serialize(writer);
		writer.WriteVarUInt16(CriticalHitWeapon);
		CriticalMiss.Serialize(writer);
		HealBonus.Serialize(writer);
		AllDamagesBonus.Serialize(writer);
		WeaponDamagesBonusPercent.Serialize(writer);
		DamagesBonusPercent.Serialize(writer);
		TrapBonus.Serialize(writer);
		TrapBonusPercent.Serialize(writer);
		GlyphBonusPercent.Serialize(writer);
		PermanentDamagePercent.Serialize(writer);
		TackleBlock.Serialize(writer);
		TackleEvade.Serialize(writer);
		PAAttack.Serialize(writer);
		PMAttack.Serialize(writer);
		PushDamageBonus.Serialize(writer);
		CriticalDamageBonus.Serialize(writer);
		NeutralDamageBonus.Serialize(writer);
		EarthDamageBonus.Serialize(writer);
		WaterDamageBonus.Serialize(writer);
		AirDamageBonus.Serialize(writer);
		FireDamageBonus.Serialize(writer);
		DodgePALostProbability.Serialize(writer);
		DodgePMLostProbability.Serialize(writer);
		NeutralElementResistPercent.Serialize(writer);
		EarthElementResistPercent.Serialize(writer);
		WaterElementResistPercent.Serialize(writer);
		AirElementResistPercent.Serialize(writer);
		FireElementResistPercent.Serialize(writer);
		NeutralElementReduction.Serialize(writer);
		EarthElementReduction.Serialize(writer);
		WaterElementReduction.Serialize(writer);
		AirElementReduction.Serialize(writer);
		FireElementReduction.Serialize(writer);
		PushDamageReduction.Serialize(writer);
		CriticalDamageReduction.Serialize(writer);
		PvpNeutralElementResistPercent.Serialize(writer);
		PvpEarthElementResistPercent.Serialize(writer);
		PvpWaterElementResistPercent.Serialize(writer);
		PvpAirElementResistPercent.Serialize(writer);
		PvpFireElementResistPercent.Serialize(writer);
		PvpNeutralElementReduction.Serialize(writer);
		PvpEarthElementReduction.Serialize(writer);
		PvpWaterElementReduction.Serialize(writer);
		PvpAirElementReduction.Serialize(writer);
		PvpFireElementReduction.Serialize(writer);
		var spellModificationsBefore = writer.Position;
		var spellModificationsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in SpellModifications)
		{
			item.Serialize(writer);
			spellModificationsCount++;
		}
		var spellModificationsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, spellModificationsBefore);
		writer.WriteInt16((short)spellModificationsCount);
		writer.Seek(SeekOrigin.Begin, spellModificationsAfter);
		writer.WriteInt32(ProbationTime);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Experience = reader.ReadVarUInt64();
		ExperienceLevelFloor = reader.ReadVarUInt64();
		ExperienceNextLevelFloor = reader.ReadVarUInt64();
		Kamas = reader.ReadInt32();
		StatsPoints = reader.ReadVarUInt16();
		AdditionnalPoints = reader.ReadVarUInt16();
		SpellsPoints = reader.ReadVarUInt16();
		AlignmentInfos = ActorExtendedAlignmentInformations.Empty;
		AlignmentInfos.Deserialize(reader);
		LifePoints = reader.ReadVarUInt32();
		MaxLifePoints = reader.ReadVarUInt32();
		EnergyPoints = reader.ReadVarUInt16();
		MaxEnergyPoints = reader.ReadVarUInt16();
		ActionPointsCurrent = reader.ReadVarInt16();
		MovementPointsCurrent = reader.ReadVarInt16();
		Initiative = CharacterBaseCharacteristic.Empty;
		Initiative.Deserialize(reader);
		Prospecting = CharacterBaseCharacteristic.Empty;
		Prospecting.Deserialize(reader);
		ActionPoints = CharacterBaseCharacteristic.Empty;
		ActionPoints.Deserialize(reader);
		MovementPoints = CharacterBaseCharacteristic.Empty;
		MovementPoints.Deserialize(reader);
		Strength = CharacterBaseCharacteristic.Empty;
		Strength.Deserialize(reader);
		Vitality = CharacterBaseCharacteristic.Empty;
		Vitality.Deserialize(reader);
		Wisdom = CharacterBaseCharacteristic.Empty;
		Wisdom.Deserialize(reader);
		Chance = CharacterBaseCharacteristic.Empty;
		Chance.Deserialize(reader);
		Agility = CharacterBaseCharacteristic.Empty;
		Agility.Deserialize(reader);
		Intelligence = CharacterBaseCharacteristic.Empty;
		Intelligence.Deserialize(reader);
		Range = CharacterBaseCharacteristic.Empty;
		Range.Deserialize(reader);
		SummonableCreaturesBoost = CharacterBaseCharacteristic.Empty;
		SummonableCreaturesBoost.Deserialize(reader);
		Reflect = CharacterBaseCharacteristic.Empty;
		Reflect.Deserialize(reader);
		CriticalHit = CharacterBaseCharacteristic.Empty;
		CriticalHit.Deserialize(reader);
		CriticalHitWeapon = reader.ReadVarUInt16();
		CriticalMiss = CharacterBaseCharacteristic.Empty;
		CriticalMiss.Deserialize(reader);
		HealBonus = CharacterBaseCharacteristic.Empty;
		HealBonus.Deserialize(reader);
		AllDamagesBonus = CharacterBaseCharacteristic.Empty;
		AllDamagesBonus.Deserialize(reader);
		WeaponDamagesBonusPercent = CharacterBaseCharacteristic.Empty;
		WeaponDamagesBonusPercent.Deserialize(reader);
		DamagesBonusPercent = CharacterBaseCharacteristic.Empty;
		DamagesBonusPercent.Deserialize(reader);
		TrapBonus = CharacterBaseCharacteristic.Empty;
		TrapBonus.Deserialize(reader);
		TrapBonusPercent = CharacterBaseCharacteristic.Empty;
		TrapBonusPercent.Deserialize(reader);
		GlyphBonusPercent = CharacterBaseCharacteristic.Empty;
		GlyphBonusPercent.Deserialize(reader);
		PermanentDamagePercent = CharacterBaseCharacteristic.Empty;
		PermanentDamagePercent.Deserialize(reader);
		TackleBlock = CharacterBaseCharacteristic.Empty;
		TackleBlock.Deserialize(reader);
		TackleEvade = CharacterBaseCharacteristic.Empty;
		TackleEvade.Deserialize(reader);
		PAAttack = CharacterBaseCharacteristic.Empty;
		PAAttack.Deserialize(reader);
		PMAttack = CharacterBaseCharacteristic.Empty;
		PMAttack.Deserialize(reader);
		PushDamageBonus = CharacterBaseCharacteristic.Empty;
		PushDamageBonus.Deserialize(reader);
		CriticalDamageBonus = CharacterBaseCharacteristic.Empty;
		CriticalDamageBonus.Deserialize(reader);
		NeutralDamageBonus = CharacterBaseCharacteristic.Empty;
		NeutralDamageBonus.Deserialize(reader);
		EarthDamageBonus = CharacterBaseCharacteristic.Empty;
		EarthDamageBonus.Deserialize(reader);
		WaterDamageBonus = CharacterBaseCharacteristic.Empty;
		WaterDamageBonus.Deserialize(reader);
		AirDamageBonus = CharacterBaseCharacteristic.Empty;
		AirDamageBonus.Deserialize(reader);
		FireDamageBonus = CharacterBaseCharacteristic.Empty;
		FireDamageBonus.Deserialize(reader);
		DodgePALostProbability = CharacterBaseCharacteristic.Empty;
		DodgePALostProbability.Deserialize(reader);
		DodgePMLostProbability = CharacterBaseCharacteristic.Empty;
		DodgePMLostProbability.Deserialize(reader);
		NeutralElementResistPercent = CharacterBaseCharacteristic.Empty;
		NeutralElementResistPercent.Deserialize(reader);
		EarthElementResistPercent = CharacterBaseCharacteristic.Empty;
		EarthElementResistPercent.Deserialize(reader);
		WaterElementResistPercent = CharacterBaseCharacteristic.Empty;
		WaterElementResistPercent.Deserialize(reader);
		AirElementResistPercent = CharacterBaseCharacteristic.Empty;
		AirElementResistPercent.Deserialize(reader);
		FireElementResistPercent = CharacterBaseCharacteristic.Empty;
		FireElementResistPercent.Deserialize(reader);
		NeutralElementReduction = CharacterBaseCharacteristic.Empty;
		NeutralElementReduction.Deserialize(reader);
		EarthElementReduction = CharacterBaseCharacteristic.Empty;
		EarthElementReduction.Deserialize(reader);
		WaterElementReduction = CharacterBaseCharacteristic.Empty;
		WaterElementReduction.Deserialize(reader);
		AirElementReduction = CharacterBaseCharacteristic.Empty;
		AirElementReduction.Deserialize(reader);
		FireElementReduction = CharacterBaseCharacteristic.Empty;
		FireElementReduction.Deserialize(reader);
		PushDamageReduction = CharacterBaseCharacteristic.Empty;
		PushDamageReduction.Deserialize(reader);
		CriticalDamageReduction = CharacterBaseCharacteristic.Empty;
		CriticalDamageReduction.Deserialize(reader);
		PvpNeutralElementResistPercent = CharacterBaseCharacteristic.Empty;
		PvpNeutralElementResistPercent.Deserialize(reader);
		PvpEarthElementResistPercent = CharacterBaseCharacteristic.Empty;
		PvpEarthElementResistPercent.Deserialize(reader);
		PvpWaterElementResistPercent = CharacterBaseCharacteristic.Empty;
		PvpWaterElementResistPercent.Deserialize(reader);
		PvpAirElementResistPercent = CharacterBaseCharacteristic.Empty;
		PvpAirElementResistPercent.Deserialize(reader);
		PvpFireElementResistPercent = CharacterBaseCharacteristic.Empty;
		PvpFireElementResistPercent.Deserialize(reader);
		PvpNeutralElementReduction = CharacterBaseCharacteristic.Empty;
		PvpNeutralElementReduction.Deserialize(reader);
		PvpEarthElementReduction = CharacterBaseCharacteristic.Empty;
		PvpEarthElementReduction.Deserialize(reader);
		PvpWaterElementReduction = CharacterBaseCharacteristic.Empty;
		PvpWaterElementReduction.Deserialize(reader);
		PvpAirElementReduction = CharacterBaseCharacteristic.Empty;
		PvpAirElementReduction.Deserialize(reader);
		PvpFireElementReduction = CharacterBaseCharacteristic.Empty;
		PvpFireElementReduction.Deserialize(reader);
		var spellModificationsCount = reader.ReadInt16();
		var spellModifications = new CharacterSpellModification[spellModificationsCount];
		for (var i = 0; i < spellModificationsCount; i++)
		{
			var entry = CharacterSpellModification.Empty;
			entry.Deserialize(reader);
			spellModifications[i] = entry;
		}
		SpellModifications = spellModifications;
		ProbationTime = reader.ReadInt32();
	}
}
