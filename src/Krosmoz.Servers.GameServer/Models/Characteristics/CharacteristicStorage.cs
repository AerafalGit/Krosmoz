// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;

namespace Krosmoz.Servers.GameServer.Models.Characteristics;

/// <summary>
/// Represents the characteristics of a game entity, such as a character.
/// Provides access to various attributes and statistics.
/// </summary>
public sealed class CharacteristicStorage
{
    /// <summary>
    /// A dictionary containing all characteristics, indexed by their IDs.
    /// </summary>
    public Dictionary<CharacteristicIds, Characteristic> Characteristics { get; }

    /// <summary>
    /// Gets the characteristic associated with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the characteristic to retrieve.</param>
    /// <returns>The characteristic associated with the given ID.</returns>
    public Characteristic this[CharacteristicIds id] =>
        Characteristics[id];

    /// <summary>
    /// Gets the characteristic points.
    /// </summary>
    public Characteristic StatsPoints =>
        (CharacteristicUsable)Characteristics[CharacteristicIds.PointsDeCaracteristiques];

    /// <summary>
    /// Gets the spell points characteristic.
    /// </summary>
    public Characteristic SpellsPoints =>
        (CharacteristicUsable)Characteristics[CharacteristicIds.PointsDeSorts];

    /// <summary>
    /// Gets the energy points characteristic.
    /// </summary>
    public CharacteristicUsable EnergyPoints =>
        (CharacteristicUsable)Characteristics[CharacteristicIds.PointsDenergie];

    /// <summary>
    /// Gets the weight characteristic.
    /// </summary>
    public CharacteristicUsable Weight =>
        (CharacteristicUsable)Characteristics[CharacteristicIds.Poids];

    /// <summary>
    /// Gets the health characteristic.
    /// </summary>
    public CharacteristicHealth Health =>
        (CharacteristicHealth)Characteristics[CharacteristicIds.PointsDeVie];

    /// <summary>
    /// Gets the initiative characteristic.
    /// </summary>
    public CharacteristicInitiative Initiative =>
        (CharacteristicInitiative)Characteristics[CharacteristicIds.Initiative];

    /// <summary>
    /// Gets the prospecting characteristic.
    /// </summary>
    public Characteristic Prospecting =>
        Characteristics[CharacteristicIds.Prospection];

    /// <summary>
    /// Gets the action points characteristic.
    /// </summary>
    public CharacteristicUsable ActionPoints =>
        (CharacteristicUsable)Characteristics[CharacteristicIds.PointsDactionPA];

    /// <summary>
    /// Gets the movement points characteristic.
    /// </summary>
    public CharacteristicUsable MovementPoints =>
        (CharacteristicUsable)Characteristics[CharacteristicIds.PointsDeMouvementPM];

    /// <summary>
    /// Gets the strength characteristic.
    /// </summary>
    public Characteristic Strength =>
        Characteristics[CharacteristicIds.Force];

    /// <summary>
    /// Gets the vitality characteristic.
    /// </summary>
    public Characteristic Vitality =>
        Characteristics[CharacteristicIds.Vitalite];

    /// <summary>
    /// Gets the wisdom characteristic.
    /// </summary>
    public Characteristic Wisdom =>
        Characteristics[CharacteristicIds.Sagesse];

    /// <summary>
    /// Gets the chance characteristic.
    /// </summary>
    public Characteristic Chance =>
        Characteristics[CharacteristicIds.Chance];

    /// <summary>
    /// Gets the agility characteristic.
    /// </summary>
    public Characteristic Agility =>
        Characteristics[CharacteristicIds.Agilite];

    /// <summary>
    /// Gets the intelligence characteristic.
    /// </summary>
    public Characteristic Intelligence =>
        Characteristics[CharacteristicIds.Intelligence];

    /// <summary>
    /// Gets the range characteristic.
    /// </summary>
    public Characteristic Range =>
        Characteristics[CharacteristicIds.Portee];

    /// <summary>
    /// Gets the summonable creatures boost characteristic.
    /// </summary>
    public Characteristic SummonableCreaturesBoost =>
        Characteristics[CharacteristicIds.Invocation];

    /// <summary>
    /// Gets the reflect characteristic.
    /// </summary>
    public Characteristic Reflect =>
        Characteristics[CharacteristicIds.Renvoi];

    /// <summary>
    /// Gets the critical hit characteristic.
    /// </summary>
    public Characteristic CriticalHit =>
        Characteristics[CharacteristicIds.CoupsCritiques];

    /// <summary>
    /// Gets the critical miss characteristic.
    /// </summary>
    public Characteristic CriticalMiss =>
        Characteristics[CharacteristicIds.EchecCritique];

    /// <summary>
    /// Gets the heal bonus characteristic.
    /// </summary>
    public Characteristic HealBonus =>
        Characteristics[CharacteristicIds.Soins];

    /// <summary>
    /// Gets the all damages bonus characteristic.
    /// </summary>
    public Characteristic AllDamagesBonus =>
        Characteristics[CharacteristicIds.Dommages];

    /// <summary>
    /// Gets the weapon damages bonus percentage characteristic.
    /// </summary>
    public Characteristic WeaponDamagesBonusPercent =>
        Characteristics[CharacteristicIds.MaitriseDarme];

    /// <summary>
    /// Gets the damages bonus percentage characteristic.
    /// </summary>
    public Characteristic DamagesBonusPercent =>
        Characteristics[CharacteristicIds.Puissance];

    /// <summary>
    /// Gets the trap bonus characteristic.
    /// </summary>
    public Characteristic TrapBonus =>
        Characteristics[CharacteristicIds.Piegesfixe];

    /// <summary>
    /// Gets the trap bonus percentage characteristic.
    /// </summary>
    public Characteristic TrapBonusPercent =>
        Characteristics[CharacteristicIds.PiegesPuissance];

    /// <summary>
    /// Gets the glyph bonus percentage characteristic.
    /// </summary>
    public Characteristic GlyphBonusPercent =>
        Characteristics[CharacteristicIds.BonusDePuissancePourLesGlyphes];

    /// <summary>
    /// Gets the permanent damage percentage characteristic.
    /// </summary>
    public Characteristic PermanentDamagePercent =>
        Characteristics[CharacteristicIds.MultiplicateurDeDommages];

    /// <summary>
    /// Gets the tackle block characteristic.
    /// </summary>
    public Characteristic TackleBlock =>
        Characteristics[CharacteristicIds.Tacle];

    /// <summary>
    /// Gets the tackle evade characteristic.
    /// </summary>
    public Characteristic TackleEvade =>
        Characteristics[CharacteristicIds.Fuite];

    /// <summary>
    /// Gets the PA attack characteristic.
    /// </summary>
    public Characteristic PaAttack =>
        Characteristics[CharacteristicIds.RetraitPA];

    /// <summary>
    /// Gets the PM attack characteristic.
    /// </summary>
    public Characteristic PmAttack =>
        Characteristics[CharacteristicIds.RetraitPM];

    /// <summary>
    /// Gets the push damage bonus characteristic.
    /// </summary>
    public Characteristic PushDamageBonus =>
        Characteristics[CharacteristicIds.Poussee];

    /// <summary>
    /// Gets the critical damage bonus characteristic.
    /// </summary>
    public Characteristic CriticalDamageBonus =>
        Characteristics[CharacteristicIds.DommagesCritiques];

    /// <summary>
    /// Gets the neutral damage bonus characteristic.
    /// </summary>
    public Characteristic NeutralDamageBonus =>
        Characteristics[CharacteristicIds.DommagesNeutrefixe];

    /// <summary>
    /// Gets the earth damage bonus characteristic.
    /// </summary>
    public Characteristic EarthDamageBonus =>
        Characteristics[CharacteristicIds.DommagesTerrefixe];

    /// <summary>
    /// Gets the water damage bonus characteristic.
    /// </summary>
    public Characteristic WaterDamageBonus =>
        Characteristics[CharacteristicIds.DommagesEaufixe];

    /// <summary>
    /// Gets the air damage bonus characteristic.
    /// </summary>
    public Characteristic AirDamageBonus =>
        Characteristics[CharacteristicIds.DommagesAirfixe];

    /// <summary>
    /// Gets the fire damage bonus characteristic.
    /// </summary>
    public Characteristic FireDamageBonus =>
        Characteristics[CharacteristicIds.DommagesFeufixe];

    /// <summary>
    /// Gets the dodge PA lost probability characteristic.
    /// </summary>
    public Characteristic DodgePaLostProbability =>
        Characteristics[CharacteristicIds.EsquivePA];

    /// <summary>
    /// Gets the dodge PM lost probability characteristic.
    /// </summary>
    public Characteristic DodgePmLostProbability =>
        Characteristics[CharacteristicIds.EsquivePM];

    /// <summary>
    /// Gets the neutral element resist percentage characteristic.
    /// </summary>
    public Characteristic NeutralElementResistPercent =>
        Characteristics[CharacteristicIds.ResistancesNeutre];

    /// <summary>
    /// Gets the earth element resist percentage characteristic.
    /// </summary>
    public Characteristic EarthElementResistPercent =>
        Characteristics[CharacteristicIds.ResistancesTerre];

    /// <summary>
    /// Gets the water element resist percentage characteristic.
    /// </summary>
    public Characteristic WaterElementResistPercent =>
        Characteristics[CharacteristicIds.ResistancesEau];

    /// <summary>
    /// Gets the air element resist percentage characteristic.
    /// </summary>
    public Characteristic AirElementResistPercent =>
        Characteristics[CharacteristicIds.ResistancesAir];

    /// <summary>
    /// Gets the fire element resist percentage characteristic.
    /// </summary>
    public Characteristic FireElementResistPercent =>
        Characteristics[CharacteristicIds.ResistancesFeu];

    /// <summary>
    /// Gets the neutral element reduction characteristic.
    /// </summary>
    public Characteristic NeutralElementReduction =>
        Characteristics[CharacteristicIds.ResistancesNeutrefixe];

    /// <summary>
    /// Gets the earth element reduction characteristic.
    /// </summary>
    public Characteristic EarthElementReduction =>
        Characteristics[CharacteristicIds.ResistancesTerrefixe];

    /// <summary>
    /// Gets the water element reduction characteristic.
    /// </summary>
    public Characteristic WaterElementReduction =>
        Characteristics[CharacteristicIds.ResistancesEaufixe];

    /// <summary>
    /// Gets the air element reduction characteristic.
    /// </summary>
    public Characteristic AirElementReduction =>
        Characteristics[CharacteristicIds.ResistancesAirfixe];

    /// <summary>
    /// Gets the fire element reduction characteristic.
    /// </summary>
    public Characteristic FireElementReduction =>
        Characteristics[CharacteristicIds.ResistancesFeufixe];

    /// <summary>
    /// Gets the push damage reduction characteristic.
    /// </summary>
    public Characteristic PushDamageReduction =>
        Characteristics[CharacteristicIds.ResistancesPousseefixe];

    /// <summary>
    /// Gets the critical damage reduction characteristic.
    /// </summary>
    public Characteristic CriticalDamageReduction =>
        Characteristics[CharacteristicIds.ResistancesCoupsCritiquesfixe];

    /// <summary>
    /// Gets the PvP neutral element resist percentage characteristic.
    /// </summary>
    public Characteristic PvpNeutralElementResistPercent =>
        Characteristics[CharacteristicIds.ResistancesJCJNeutre];

    /// <summary>
    /// Gets the PvP earth element resist percentage characteristic.
    /// </summary>
    public Characteristic PvpEarthElementResistPercent =>
        Characteristics[CharacteristicIds.ResistancesJCJTerre];

    /// <summary>
    /// Gets the PvP water element resist percentage characteristic.
    /// </summary>
    public Characteristic PvpWaterElementResistPercent =>
        Characteristics[CharacteristicIds.ResistancesJCJEau];

    /// <summary>
    /// Gets the PvP air element resist percentage characteristic.
    /// </summary>
    public Characteristic PvpAirElementResistPercent =>
        Characteristics[CharacteristicIds.ResistancesJCJAir];

    /// <summary>
    /// Gets the PvP fire element resist percentage characteristic.
    /// </summary>
    public Characteristic PvpFireElementResistPercent =>
        Characteristics[CharacteristicIds.ResistancesJCJFeu];

    /// <summary>
    /// Gets the PvP neutral element reduction characteristic.
    /// </summary>
    public Characteristic PvpNeutralElementReduction =>
        Characteristics[CharacteristicIds.ResistancesJCJNeutrefixe];

    /// <summary>
    /// Gets the PvP earth element reduction characteristic.
    /// </summary>
    public Characteristic PvpEarthElementReduction =>
        Characteristics[CharacteristicIds.ResistancesJCJTerrefixe];

    /// <summary>
    /// Gets the PvP water element reduction characteristic.
    /// </summary>
    public Characteristic PvpWaterElementReduction =>
        Characteristics[CharacteristicIds.ResistancesJCJEaufixe];

    /// <summary>
    /// Gets the PvP air element reduction characteristic.
    /// </summary>
    public Characteristic PvpAirElementReduction =>
        Characteristics[CharacteristicIds.ResistancesJCJAirfixe];

    /// <summary>
    /// Gets the PvP fire element reduction characteristic.
    /// </summary>
    public Characteristic PvpFireElementReduction =>
        Characteristics[CharacteristicIds.ResistancesJCJFeufixe];

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacteristicStorage"/> class.
    /// </summary>
    public CharacteristicStorage()
    {
        Characteristics = [];
    }
}
