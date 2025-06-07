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
    private readonly Dictionary<CharacteristicIds, Characteristic> _characteristics;

    /// <summary>
    /// Gets the characteristic associated with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the characteristic to retrieve.</param>
    /// <returns>The characteristic associated with the given ID.</returns>
    public Characteristic this[CharacteristicIds id] =>
        _characteristics[id];

    /// <summary>
    /// Gets the characteristic points.
    /// </summary>
    public Characteristic StatsPoints =>
        (CharacteristicUsable)_characteristics[CharacteristicIds.PointsDeCaracteristiques];

    /// <summary>
    /// Gets the spell points characteristic.
    /// </summary>
    public Characteristic SpellsPoints =>
        (CharacteristicUsable)_characteristics[CharacteristicIds.PointsDeSorts];

    /// <summary>
    /// Gets the energy points characteristic.
    /// </summary>
    public CharacteristicUsable EnergyPoints =>
        (CharacteristicUsable)_characteristics[CharacteristicIds.PointsDenergie];

    /// <summary>
    /// Gets the health characteristic.
    /// </summary>
    public CharacteristicHealth Health =>
        (CharacteristicHealth)_characteristics[CharacteristicIds.PointsDeVie];

    /// <summary>
    /// Gets the initiative characteristic.
    /// </summary>
    public CharacteristicInitiative Initiative =>
        (CharacteristicInitiative)_characteristics[CharacteristicIds.Initiative];

    /// <summary>
    /// Gets the prospecting characteristic.
    /// </summary>
    public Characteristic Prospecting =>
        _characteristics[CharacteristicIds.Prospection];

    /// <summary>
    /// Gets the action points characteristic.
    /// </summary>
    public CharacteristicUsable ActionPoints =>
        (CharacteristicUsable)_characteristics[CharacteristicIds.PointsDactionPA];

    /// <summary>
    /// Gets the movement points characteristic.
    /// </summary>
    public CharacteristicUsable MovementPoints =>
        (CharacteristicUsable)_characteristics[CharacteristicIds.PointsDeMouvementPM];

    /// <summary>
    /// Gets the strength characteristic.
    /// </summary>
    public Characteristic Strength =>
        _characteristics[CharacteristicIds.Force];

    /// <summary>
    /// Gets the vitality characteristic.
    /// </summary>
    public Characteristic Vitality =>
        _characteristics[CharacteristicIds.Vitalite];

    /// <summary>
    /// Gets the wisdom characteristic.
    /// </summary>
    public Characteristic Wisdom =>
        _characteristics[CharacteristicIds.Sagesse];

    /// <summary>
    /// Gets the chance characteristic.
    /// </summary>
    public Characteristic Chance =>
        _characteristics[CharacteristicIds.Chance];

    /// <summary>
    /// Gets the agility characteristic.
    /// </summary>
    public Characteristic Agility =>
        _characteristics[CharacteristicIds.Agilite];

    /// <summary>
    /// Gets the intelligence characteristic.
    /// </summary>
    public Characteristic Intelligence =>
        _characteristics[CharacteristicIds.Intelligence];

    /// <summary>
    /// Gets the range characteristic.
    /// </summary>
    public Characteristic Range =>
        _characteristics[CharacteristicIds.Portee];

    /// <summary>
    /// Gets the summonable creatures boost characteristic.
    /// </summary>
    public Characteristic SummonableCreaturesBoost =>
        _characteristics[CharacteristicIds.Invocation];

    /// <summary>
    /// Gets the reflect characteristic.
    /// </summary>
    public Characteristic Reflect =>
        _characteristics[CharacteristicIds.Renvoi];

    /// <summary>
    /// Gets the critical hit characteristic.
    /// </summary>
    public Characteristic CriticalHit =>
        _characteristics[CharacteristicIds.CoupsCritiques];

    /// <summary>
    /// Gets the critical miss characteristic.
    /// </summary>
    public Characteristic CriticalMiss =>
        _characteristics[CharacteristicIds.EchecCritique];

    /// <summary>
    /// Gets the heal bonus characteristic.
    /// </summary>
    public Characteristic HealBonus =>
        _characteristics[CharacteristicIds.Soins];

    /// <summary>
    /// Gets the all damages bonus characteristic.
    /// </summary>
    public Characteristic AllDamagesBonus =>
        _characteristics[CharacteristicIds.Dommages];

    /// <summary>
    /// Gets the weapon damages bonus percentage characteristic.
    /// </summary>
    public Characteristic WeaponDamagesBonusPercent =>
        _characteristics[CharacteristicIds.MaitriseDarme];

    /// <summary>
    /// Gets the damages bonus percentage characteristic.
    /// </summary>
    public Characteristic DamagesBonusPercent =>
        _characteristics[CharacteristicIds.Puissance];

    /// <summary>
    /// Gets the trap bonus characteristic.
    /// </summary>
    public Characteristic TrapBonus =>
        _characteristics[CharacteristicIds.Piegesfixe];

    /// <summary>
    /// Gets the trap bonus percentage characteristic.
    /// </summary>
    public Characteristic TrapBonusPercent =>
        _characteristics[CharacteristicIds.PiegesPuissance];

    /// <summary>
    /// Gets the glyph bonus percentage characteristic.
    /// </summary>
    public Characteristic GlyphBonusPercent =>
        _characteristics[CharacteristicIds.BonusDePuissancePourLesGlyphes];

    /// <summary>
    /// Gets the permanent damage percentage characteristic.
    /// </summary>
    public Characteristic PermanentDamagePercent =>
        _characteristics[CharacteristicIds.MultiplicateurDeDommages];

    /// <summary>
    /// Gets the tackle block characteristic.
    /// </summary>
    public Characteristic TackleBlock =>
        _characteristics[CharacteristicIds.Tacle];

    /// <summary>
    /// Gets the tackle evade characteristic.
    /// </summary>
    public Characteristic TackleEvade =>
        _characteristics[CharacteristicIds.Fuite];

    /// <summary>
    /// Gets the PA attack characteristic.
    /// </summary>
    public Characteristic PaAttack =>
        _characteristics[CharacteristicIds.RetraitPA];

    /// <summary>
    /// Gets the PM attack characteristic.
    /// </summary>
    public Characteristic PmAttack =>
        _characteristics[CharacteristicIds.RetraitPM];

    /// <summary>
    /// Gets the push damage bonus characteristic.
    /// </summary>
    public Characteristic PushDamageBonus =>
        _characteristics[CharacteristicIds.Poussee];

    /// <summary>
    /// Gets the critical damage bonus characteristic.
    /// </summary>
    public Characteristic CriticalDamageBonus =>
        _characteristics[CharacteristicIds.DommagesCritiques];

    /// <summary>
    /// Gets the neutral damage bonus characteristic.
    /// </summary>
    public Characteristic NeutralDamageBonus =>
        _characteristics[CharacteristicIds.DommagesNeutrefixe];

    /// <summary>
    /// Gets the earth damage bonus characteristic.
    /// </summary>
    public Characteristic EarthDamageBonus =>
        _characteristics[CharacteristicIds.DommagesTerrefixe];

    /// <summary>
    /// Gets the water damage bonus characteristic.
    /// </summary>
    public Characteristic WaterDamageBonus =>
        _characteristics[CharacteristicIds.DommagesEaufixe];

    /// <summary>
    /// Gets the air damage bonus characteristic.
    /// </summary>
    public Characteristic AirDamageBonus =>
        _characteristics[CharacteristicIds.DommagesAirfixe];

    /// <summary>
    /// Gets the fire damage bonus characteristic.
    /// </summary>
    public Characteristic FireDamageBonus =>
        _characteristics[CharacteristicIds.DommagesFeufixe];

    /// <summary>
    /// Gets the dodge PA lost probability characteristic.
    /// </summary>
    public Characteristic DodgePaLostProbability =>
        _characteristics[CharacteristicIds.EsquivePA];

    /// <summary>
    /// Gets the dodge PM lost probability characteristic.
    /// </summary>
    public Characteristic DodgePmLostProbability =>
        _characteristics[CharacteristicIds.EsquivePM];

    /// <summary>
    /// Gets the neutral element resist percentage characteristic.
    /// </summary>
    public Characteristic NeutralElementResistPercent =>
        _characteristics[CharacteristicIds.ResistancesNeutre];

    /// <summary>
    /// Gets the earth element resist percentage characteristic.
    /// </summary>
    public Characteristic EarthElementResistPercent =>
        _characteristics[CharacteristicIds.ResistancesTerre];

    /// <summary>
    /// Gets the water element resist percentage characteristic.
    /// </summary>
    public Characteristic WaterElementResistPercent =>
        _characteristics[CharacteristicIds.ResistancesEau];

    /// <summary>
    /// Gets the air element resist percentage characteristic.
    /// </summary>
    public Characteristic AirElementResistPercent =>
        _characteristics[CharacteristicIds.ResistancesAir];

    /// <summary>
    /// Gets the fire element resist percentage characteristic.
    /// </summary>
    public Characteristic FireElementResistPercent =>
        _characteristics[CharacteristicIds.ResistancesFeu];

    /// <summary>
    /// Gets the neutral element reduction characteristic.
    /// </summary>
    public Characteristic NeutralElementReduction =>
        _characteristics[CharacteristicIds.ResistancesNeutrefixe];

    /// <summary>
    /// Gets the earth element reduction characteristic.
    /// </summary>
    public Characteristic EarthElementReduction =>
        _characteristics[CharacteristicIds.ResistancesTerrefixe];

    /// <summary>
    /// Gets the water element reduction characteristic.
    /// </summary>
    public Characteristic WaterElementReduction =>
        _characteristics[CharacteristicIds.ResistancesEaufixe];

    /// <summary>
    /// Gets the air element reduction characteristic.
    /// </summary>
    public Characteristic AirElementReduction =>
        _characteristics[CharacteristicIds.ResistancesAirfixe];

    /// <summary>
    /// Gets the fire element reduction characteristic.
    /// </summary>
    public Characteristic FireElementReduction =>
        _characteristics[CharacteristicIds.ResistancesFeufixe];

    /// <summary>
    /// Gets the push damage reduction characteristic.
    /// </summary>
    public Characteristic PushDamageReduction =>
        _characteristics[CharacteristicIds.ResistancesPousseefixe];

    /// <summary>
    /// Gets the critical damage reduction characteristic.
    /// </summary>
    public Characteristic CriticalDamageReduction =>
        _characteristics[CharacteristicIds.ResistancesCoupsCritiquesfixe];

    /// <summary>
    /// Gets the PvP neutral element resist percentage characteristic.
    /// </summary>
    public Characteristic PvpNeutralElementResistPercent =>
        _characteristics[CharacteristicIds.ResistancesJCJNeutre];

    /// <summary>
    /// Gets the PvP earth element resist percentage characteristic.
    /// </summary>
    public Characteristic PvpEarthElementResistPercent =>
        _characteristics[CharacteristicIds.ResistancesJCJTerre];

    /// <summary>
    /// Gets the PvP water element resist percentage characteristic.
    /// </summary>
    public Characteristic PvpWaterElementResistPercent =>
        _characteristics[CharacteristicIds.ResistancesJCJEau];

    /// <summary>
    /// Gets the PvP air element resist percentage characteristic.
    /// </summary>
    public Characteristic PvpAirElementResistPercent =>
        _characteristics[CharacteristicIds.ResistancesJCJAir];

    /// <summary>
    /// Gets the PvP fire element resist percentage characteristic.
    /// </summary>
    public Characteristic PvpFireElementResistPercent =>
        _characteristics[CharacteristicIds.ResistancesJCJFeu];

    /// <summary>
    /// Gets the PvP neutral element reduction characteristic.
    /// </summary>
    public Characteristic PvpNeutralElementReduction =>
        _characteristics[CharacteristicIds.ResistancesJCJNeutrefixe];

    /// <summary>
    /// Gets the PvP earth element reduction characteristic.
    /// </summary>
    public Characteristic PvpEarthElementReduction =>
        _characteristics[CharacteristicIds.ResistancesJCJTerrefixe];

    /// <summary>
    /// Gets the PvP water element reduction characteristic.
    /// </summary>
    public Characteristic PvpWaterElementReduction =>
        _characteristics[CharacteristicIds.ResistancesJCJEaufixe];

    /// <summary>
    /// Gets the PvP air element reduction characteristic.
    /// </summary>
    public Characteristic PvpAirElementReduction =>
        _characteristics[CharacteristicIds.ResistancesJCJAirfixe];

    /// <summary>
    /// Gets the PvP fire element reduction characteristic.
    /// </summary>
    public Characteristic PvpFireElementReduction =>
        _characteristics[CharacteristicIds.ResistancesJCJFeufixe];

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacteristicStorage"/> class.
    /// </summary>
    /// <param name="characteristics">A dictionary of characteristics indexed by their IDs.</param>
    public CharacteristicStorage(Dictionary<CharacteristicIds, Characteristic> characteristics)
    {
        _characteristics = characteristics;
    }
}
