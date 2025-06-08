// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Constants;
using Krosmoz.Protocol.Datacenter.Breeds;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Ipc.Types.Accounts;
using Krosmoz.Servers.GameServer.Database.Models.Characteristics;
using Krosmoz.Servers.GameServer.Database.Models.Characters;
using Krosmoz.Servers.GameServer.Factories.World;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;
using Krosmoz.Servers.GameServer.Models.Appearances;
using Krosmoz.Servers.GameServer.Models.Characteristics;
using Krosmoz.Servers.GameServer.Models.Options.Characters;
using Krosmoz.Servers.GameServer.Models.Shortcuts;
using Krosmoz.Servers.GameServer.Models.Spells;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Experiences;
using Krosmoz.Servers.GameServer.Services.Spells;
using Microsoft.Extensions.Options;

namespace Krosmoz.Servers.GameServer.Factories.Characters;

/// <summary>
/// Factory class responsible for creating and initializing character-related objects for the game server.
/// </summary>
public sealed class CharacterFactory : ICharacterFactory
{
    private static readonly CharacteristicFormulas s_formulasChanceDependant =
        owner => (int)(owner.Characteristics[CharacteristicIds.Chance] / 10d);

    private static readonly CharacteristicFormulas s_formulasWisdomDependant =
        owner => (int)(owner.Characteristics[CharacteristicIds.Sagesse] / 10d);

    private static readonly CharacteristicFormulas s_formulasAgilityDependant =
        owner => (int)(owner.Characteristics[CharacteristicIds.Agilite] / 10d);

    private readonly ISpellService _spellService;
    private readonly IExperienceService _experienceService;
    private readonly IWorldPositionFactory _worldPositionFactory;
    private readonly IOptionsMonitor<CharacterCreationOptions> _characterCreationOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterFactory"/> class.
    /// </summary>
    /// <param name="spellService">Service for managing spells.</param>
    /// <param name="experienceService">Service for managing experience levels.</param>
    /// <param name="worldPositionFactory">Factory for creating world positions.</param>
    /// <param name="characterCreationOptions">Options for character creation.</param>
    public CharacterFactory(
        ISpellService spellService,
        IExperienceService experienceService,
        IWorldPositionFactory worldPositionFactory,
        IOptionsMonitor<CharacterCreationOptions> characterCreationOptions)
    {
        _spellService = spellService;
        _experienceService = experienceService;
        _worldPositionFactory = worldPositionFactory;
        _characterCreationOptions = characterCreationOptions;
    }

    /// <summary>
    /// Creates a new character record with the specified parameters.
    /// </summary>
    /// <param name="account">The account associated with the character.</param>
    /// <param name="name">The name of the character.</param>
    /// <param name="breed">The breed of the character.</param>
    /// <param name="head">The head appearance of the character.</param>
    /// <param name="sex">The gender of the character.</param>
    /// <param name="look">The visual appearance of the character.</param>
    /// <returns>A new <see cref="CharacterRecord"/> instance.</returns>
    public CharacterRecord CreateCharacterRecord(IpcAccount account, string name, Breed breed, Head head, bool sex, ActorLook look)
    {
        var startSpells = breed.BreedSpellsId
            .Where(spellId => _spellService.GetSpellLevelByCharacterLevel((int)spellId, _characterCreationOptions.CurrentValue.Level) is not null)
            .Select(static spellId => (SpellIds)spellId)
            .ToList();

        var spellShortcutBar = startSpells
            .Select(static (spellId, slot) => new ShortcutBarObjectSpell((sbyte)slot, spellId))
            .ToList();

        return new CharacterRecord
        {
            Name = name,
            AccountId = account.Id,
            Experience = _experienceService.GetCharacterExperienceByLevel(_characterCreationOptions.CurrentValue.Level),
            Breed = (BreedIds)breed.Id,
            Sex = sex,
            Status = PlayerStatuses.PlayerStatusOffline,
            Look = look,
            Head = (ushort)head.Id,
            Kamas = 0,
            Position = new CharacterPosition(84674563, 315, (sbyte)Directions.SouthWest),
            Emotes = [EmoticonIds.Sasseoir],
            Spells = [SpellIds.CoupDePoing, ..startSpells],
            Characteristics = CreateDefaultCharacteristics(_characterCreationOptions.CurrentValue),
            DeathCount = 0,
            DeathMaxLevel = 0,
            DeathState = HardcoreOrEpicDeathStates.DeathStateAlive,
            PossibleChanges = CharacterRemodelings.CharacterRemodelingNotApplicable,
            MandatoryChanges = CharacterRemodelings.CharacterRemodelingNotApplicable,
            Restrictions = Restrictions.None,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            AlignmentHonor = 0,
            AlignmentSide = AlignmentSides.AlignmentNeutral,
            AlignmentStatus = AggressableStatuses.NonAggressable,
            AlignmentValue = 0,
            GeneralShortcutBar = [],
            SpellShortcutBar = spellShortcutBar
        };
    }

    /// <summary>
    /// Creates a new character actor from the specified character record and connection.
    /// </summary>
    /// <param name="connection">The connection associated with the character.</param>
    /// <param name="characterRecord">The character record to initialize the actor from.</param>
    /// <returns>A new <see cref="CharacterActor"/> instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the character's position is invalid.</exception>
    public CharacterActor CreateCharacter(DofusConnection connection, CharacterRecord characterRecord)
    {
        if (!_worldPositionFactory.TryCreateWorldPosition(characterRecord.Position.MapId, characterRecord.Position.CellId, (Directions)characterRecord.Position.Orientation, out var position))
            throw new InvalidOperationException($"Invalid position for character {characterRecord.Name} on map {characterRecord.Position.MapId} and cell {characterRecord.Position.CellId}.");

        var level = _experienceService.GetCharacterLevelByExperience(characterRecord.Experience);

        var experienceLevelFloor = _experienceService.GetCharacterExperienceByLevel(level);
        var experienceNextLevelFloor = _experienceService.GetCharacterNextExperienceByLevel(level);

        var alignmentGrade = _experienceService.GetAlignmentGradeByExperience(characterRecord.AlignmentHonor);

        var alignmentHonorGradeFloor = _experienceService.GetAlignmentExperienceByGrade(alignmentGrade);
        var alignmentNextHonorGradeFloor = _experienceService.GetAlignmentNextExperienceByGrade(alignmentGrade);

        var character = new CharacterActor(characterRecord, connection, position)
        {
            Level = level,
            ExperienceLevelFloor = experienceLevelFloor,
            ExperienceNextLevelFloor = experienceNextLevelFloor,
            AlignmentGrade = (sbyte)alignmentGrade,
            AlignmentHonorGradeFloor = (ushort)alignmentHonorGradeFloor,
            AlignmentHonorNextGradeFloor = (ushort)alignmentNextHonorGradeFloor
        };

        PopulateCharacteristics(character);
        PopulateSpells(character);

        return character;
    }

    /// <summary>
    /// Populates the characteristics of the specified character actor.
    /// </summary>
    /// <param name="character">The character actor to populate characteristics for.</param>
    private static void PopulateCharacteristics(CharacterActor character)
    {
        foreach (var (id, characteristicData) in character.Record.Characteristics)
        {
            switch (id)
            {
                case CharacteristicIds.PointsDeVie:
                    character.Characteristics.Characteristics.Add(id, new CharacteristicHealth(character, id, characteristicData.Base) { Additional = characteristicData.Bonus });
                    break;

                case CharacteristicIds.PointsDeCaracteristiques:
                case CharacteristicIds.PointsDeSorts:
                    character.Characteristics.Characteristics.Add(id, new CharacteristicUsable(character, id, characteristicData.Base) { Additional = characteristicData.Bonus });
                    break;

                case CharacteristicIds.PointsDenergie:
                    character.Characteristics.Characteristics.Add(id, new CharacteristicUsable(character, id, characteristicData.Base, 10_000) { Additional = characteristicData.Bonus });
                    break;

                case CharacteristicIds.Poids:
                    character.Characteristics.Characteristics.Add(id, new CharacteristicUsable(character, id, characteristicData.Base) { Additional = characteristicData.Bonus });
                    break;

                case CharacteristicIds.Initiative:
                    character.Characteristics.Characteristics.Add(id, new CharacteristicInitiative(character, id, characteristicData.Base) { Additional = characteristicData.Bonus });
                    break;

                case CharacteristicIds.PointsDactionPA:
                    character.Characteristics.Characteristics.Add(id, new CharacteristicUsable(character, id, characteristicData.Base, ProtocolConstants.MaxAp) { Additional = characteristicData.Bonus });
                    break;

                case CharacteristicIds.PointsDeMouvementPM:
                    character.Characteristics.Characteristics.Add(id, new CharacteristicUsable(character, id, characteristicData.Base, ProtocolConstants.MaxMp) { Additional = characteristicData.Bonus });
                    break;

                case CharacteristicIds.Prospection:
                    character.Characteristics.Characteristics.Add(id, new Characteristic(character, id, characteristicData.Base, s_formulasChanceDependant) { Additional = characteristicData.Bonus });
                    break;

                case CharacteristicIds.EsquivePA:
                case CharacteristicIds.EsquivePM:
                case CharacteristicIds.RetraitPA:
                case CharacteristicIds.RetraitPM:
                    character.Characteristics.Characteristics.Add(id, new Characteristic(character, id, characteristicData.Base, s_formulasWisdomDependant) { Additional = characteristicData.Bonus });
                    break;

                case CharacteristicIds.Tacle:
                case CharacteristicIds.Fuite:
                    character.Characteristics.Characteristics.Add(id, new Characteristic(character, id, characteristicData.Base, s_formulasAgilityDependant) { Additional = characteristicData.Bonus });
                    break;

                // TODO: ElementResistPercentages
                // TODO: AllDamagePercentages

                default:
                    character.Characteristics.Characteristics.Add(id, new Characteristic(character, id, characteristicData.Base) { Additional = characteristicData.Bonus });
                    break;
            }
        }
    }

    /// <summary>
    /// Populates the spells of the specified character actor.
    /// </summary>
    /// <param name="character">The character actor to populate spells for.</param>
    /// <exception cref="InvalidOperationException">Thrown if a spell or its level is not found.</exception>
    private void PopulateSpells(CharacterActor character)
    {
        byte spellPositon = 64;

        foreach (var spellId in character.Record.Spells)
        {
            if (!_spellService.TryGetSpell((int)spellId, out var spell))
                throw new InvalidOperationException($"Spell with ID {spellId} not found.");

            // TODO: Search real spell level
            if (!_spellService.TryGetSpellLevel((int)spell.SpellLevels[0], out var spellLevel))
                throw new InvalidOperationException($"Spell level for spell ID {spellId} not found.");

            character.Spells.Add(new SpellWrapper(spell, spellLevel, spellPositon));

            spellPositon++;
        }
    }

    /// <summary>
    /// Creates the default characteristics for a character based on the specified options.
    /// </summary>
    /// <param name="options">The character creation options.</param>
    /// <returns>A dictionary of default characteristics.</returns>
    private static Dictionary<CharacteristicIds, CharacteristicData> CreateDefaultCharacteristics(CharacterCreationOptions options)
    {
        return new Dictionary<CharacteristicIds, CharacteristicData>
        {
            [CharacteristicIds.PointsDeCaracteristiques] = new() { Base = (options.Level - 1) * 5 },
            [CharacteristicIds.PointsDeSorts] = new() { Base = options.Level - 1 },
            [CharacteristicIds.PointsDenergie] = new() { Base = 10_000 },
            [CharacteristicIds.Poids] = new(),
            [CharacteristicIds.PointsDeVie] = new(),
            [CharacteristicIds.Initiative] = new(),
            [CharacteristicIds.Prospection] = new() { Base = 100 },
            [CharacteristicIds.PointsDactionPA] = new() { Base = options.Level >= 100 ? 7 : 6 },
            [CharacteristicIds.PointsDeMouvementPM] = new() { Base = 3 },
            [CharacteristicIds.Force] = new(),
            [CharacteristicIds.Vitalite] = new() { Base = 50 + (options.Level - 1) * 5 },
            [CharacteristicIds.Sagesse] = new(),
            [CharacteristicIds.Chance] = new(),
            [CharacteristicIds.Agilite] = new(),
            [CharacteristicIds.Intelligence] = new(),
            [CharacteristicIds.Portee] = new() { Base = 1 },
            [CharacteristicIds.Invocation] = new() { Base = 1 },
            [CharacteristicIds.Renvoi] = new(),
            [CharacteristicIds.CoupsCritiques] = new(),
            [CharacteristicIds.EchecCritique] = new(),
            [CharacteristicIds.Soins] = new(),
            [CharacteristicIds.Dommages] = new(),
            [CharacteristicIds.MaitriseDarme] = new(),
            [CharacteristicIds.Puissance] = new(),
            [CharacteristicIds.Piegesfixe] = new(),
            [CharacteristicIds.PiegesPuissance] = new(),
            [CharacteristicIds.BonusDePuissancePourLesGlyphes] = new(),
            [CharacteristicIds.MultiplicateurDeDommages] = new(),
            [CharacteristicIds.Tacle] = new(),
            [CharacteristicIds.Fuite] = new(),
            [CharacteristicIds.RetraitPA] = new(),
            [CharacteristicIds.RetraitPM] = new(),
            [CharacteristicIds.Poussee] = new(),
            [CharacteristicIds.DommagesCritiques] = new(),
            [CharacteristicIds.DommagesNeutrefixe] = new(),
            [CharacteristicIds.DommagesTerrefixe] = new(),
            [CharacteristicIds.DommagesEaufixe] = new(),
            [CharacteristicIds.DommagesAirfixe] = new(),
            [CharacteristicIds.DommagesFeufixe] = new(),
            [CharacteristicIds.EsquivePA] = new(),
            [CharacteristicIds.EsquivePM] = new(),
            [CharacteristicIds.ResistancesNeutre] = new(),
            [CharacteristicIds.ResistancesTerre] = new(),
            [CharacteristicIds.ResistancesEau] = new(),
            [CharacteristicIds.ResistancesAir] = new(),
            [CharacteristicIds.ResistancesFeu] = new(),
            [CharacteristicIds.ResistancesNeutrefixe] = new(),
            [CharacteristicIds.ResistancesTerrefixe] = new(),
            [CharacteristicIds.ResistancesEaufixe] = new(),
            [CharacteristicIds.ResistancesAirfixe] = new(),
            [CharacteristicIds.ResistancesFeufixe] = new(),
            [CharacteristicIds.ResistancesPousseefixe] = new(),
            [CharacteristicIds.ResistancesCoupsCritiquesfixe] = new(),
            [CharacteristicIds.ResistancesJCJNeutre] = new(),
            [CharacteristicIds.ResistancesJCJTerre] = new(),
            [CharacteristicIds.ResistancesJCJEau] = new(),
            [CharacteristicIds.ResistancesJCJAir] = new(),
            [CharacteristicIds.ResistancesJCJFeu] = new(),
            [CharacteristicIds.ResistancesJCJNeutrefixe] = new(),
            [CharacteristicIds.ResistancesJCJTerrefixe] = new(),
            [CharacteristicIds.ResistancesJCJEaufixe] = new(),
            [CharacteristicIds.ResistancesJCJAirfixe] = new(),
            [CharacteristicIds.ResistancesJCJFeufixe] = new()
        };
    }
}
