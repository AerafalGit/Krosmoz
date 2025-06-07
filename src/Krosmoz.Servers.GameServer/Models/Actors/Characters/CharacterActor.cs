// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Ipc.Types.Accounts;
using Krosmoz.Protocol.Types.Game.Character.Alignment;
using Krosmoz.Protocol.Types.Game.Character.Characteristic;
using Krosmoz.Protocol.Types.Game.Character.Status;
using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Servers.GameServer.Database.Models.Characters;
using Krosmoz.Servers.GameServer.Models.Appearances;
using Krosmoz.Servers.GameServer.Models.Characteristics;
using Krosmoz.Servers.GameServer.Models.Shortcuts;
using Krosmoz.Servers.GameServer.Models.Social;
using Krosmoz.Servers.GameServer.Models.Spells;
using Krosmoz.Servers.GameServer.Models.World;
using Krosmoz.Servers.GameServer.Network.Transport;

namespace Krosmoz.Servers.GameServer.Models.Actors.Characters;

/// <summary>
/// Represents a character actor in the game, inheriting from <see cref="HumanoidActor"/>.
/// </summary>
public sealed class CharacterActor : HumanoidActor
{
    private WorldPosition _position;

    /// <summary>
    /// Gets or sets the character record associated with this actor.
    /// </summary>
    public CharacterRecord Record { get; set; }

    /// <summary>
    /// Gets or sets the connection associated with this character.
    /// </summary>
    public DofusConnection Connection { get; set; }

    /// <summary>
    /// Gets or sets the hero information associated with this character.
    /// </summary>
    public Heroes.Heroes Heroes { get; set; }

    /// <summary>
    /// Gets the IPC account associated with the character.
    /// </summary>
    public IpcAccount Account =>
        Heroes.Account;

    /// <summary>
    /// Determines whether the character is a hero.
    /// </summary>
    public bool IsHero =>
        Heroes.Master.Id == Id;

    /// <summary>
    /// Determines whether the character is a slave of a hero.
    /// </summary>
    public bool IsSlaveOfHero =>
        Heroes.Slaves.Any(x => x.Id == Id);

    /// <summary>
    /// Gets the unique identifier of the character.
    /// </summary>
    public override int Id =>
        Record.Id;

    /// <summary>
    /// Gets the name prefix for the character based on their hierarchy.
    /// </summary>
    public override string NamePrefix =>
        Heroes.Account.Hierarchy >= GameHierarchies.Moderator ? "★" : string.Empty;

    /// <summary>
    /// Gets or sets the name of the character.
    /// </summary>
    public override string Name
    {
        get => string.Concat(NamePrefix, ' ', Record.Name);
        set => Record.Name = value;
    }

    /// <summary>
    /// Gets the account ID associated with the character.
    /// </summary>
    public override int AccountId =>
        Account.Id;

    /// <summary>
    /// Gets or sets the world position of the character.
    /// </summary>
    public override WorldPosition Position
    {
        get => _position;
        set
        {
            _position = value;
            Record.Position = value;
        }
    }

    /// <summary>
    /// Gets or sets the visual appearance (look) of the character.
    /// </summary>
    public override ActorLook Look
    {
        get => Record.Look;
        set => Record.Look = value;
    }

    /// <summary>
    /// Gets or sets the sex of the character.
    /// </summary>
    public override bool Sex
    {
        get => Record.Sex;
        set => Record.Sex = value;
    }

    /// <summary>
    /// Gets or sets the restrictions applied to the character.
    /// </summary>
    public override Restrictions Restrictions
    {
        get => Record.Restrictions;
        set => Record.Restrictions = value;
    }

    /// <summary>
    /// Gets or sets the breed of the character.
    /// </summary>
    public BreedIds Breed
    {
        get => Record.Breed;
        set => Record.Breed = value;
    }

    /// <summary>
    /// Gets or sets the head appearance of the character.
    /// </summary>
    public ushort Head
    {
        get => Record.Head;
        set => Record.Head = value;
    }

    /// <summary>
    /// Gets or sets the status of the character.
    /// </summary>
    public PlayerStatuses Status
    {
        get => Record.Status;
        set => Record.Status = value;
    }

    /// <summary>
    /// Gets or sets the status message of the character.
    /// </summary>
    public string? StatusMessage
    {
        get => Record.StatusMessage;
        set => Record.StatusMessage = value;
    }

    /// <summary>
    /// Gets or sets the amount of Kamas (in-game currency) the character has.
    /// </summary>
    public int Kamas
    {
        get => Record.Kamas;
        set => Record.Kamas = value;
    }

    /// <summary>
    /// Gets or sets the number of times the character has died.
    /// </summary>
    public ushort DeathCount
    {
        get => Record.DeathCount;
        set => Record.DeathCount = value;
    }

    /// <summary>
    /// Gets or sets the maximum level reached by the character before death.
    /// </summary>
    public byte DeathMaxLevel
    {
        get => Record.DeathMaxLevel;
        set => Record.DeathMaxLevel = value;
    }

    /// <summary>
    /// Gets or sets the death state of the character in hardcore or epic mode.
    /// </summary>
    public HardcoreOrEpicDeathStates DeathState
    {
        get => Record.DeathState;
        set => Record.DeathState = value;
    }

    /// <summary>
    /// Gets or sets the experience points of the character.
    /// </summary>
    public ulong Experience
    {
        get => Record.Experience;
        set => Record.Experience = value;
    }

    /// <summary>
    /// Gets or sets the experience required for the current level.
    /// </summary>
    public ulong ExperienceLevelFloor { get; set; }

    /// <summary>
    /// Gets or sets the experience required for the next level.
    /// </summary>
    public ulong ExperienceNextLevelFloor { get; set; }

    /// <summary>
    /// Gets or sets the percentage of experience progress within the current level.
    /// </summary>
    public ushort ExperiencePercent { get; set; }

    /// <summary>
    /// Gets or sets the alignment side of the character.
    /// </summary>
    public AlignmentSides AlignmentSide
    {
        get => Record.AlignmentSide;
        set => Record.AlignmentSide = value;
    }

    /// <summary>
    /// Gets or sets the alignment value of the character.
    /// </summary>
    public sbyte AlignmentValue
    {
        get => Record.AlignmentValue;
        set => Record.AlignmentValue = value;
    }

    /// <summary>
    /// Gets or sets the alignment honor points of the character.
    /// </summary>
    public ushort AlignmentHonor
    {
        get => Record.AlignmentHonor;
        set => Record.AlignmentHonor = value;
    }

    /// <summary>
    /// Gets or sets the alignment status of the character.
    /// </summary>
    public AggressableStatuses AlignmentStatus
    {
        get => Record.AlignmentStatus;
        set => Record.AlignmentStatus = value;
    }

    /// <summary>
    /// Gets the character's power, calculated as the sum of their ID and level.
    /// </summary>
    public uint CharacterPower =>
        (uint)Id + Level;

    /// <summary>
    /// Gets or sets the alignment grade of the character.
    /// </summary>
    public sbyte AlignmentGrade { get; set; }

    /// <summary>
    /// Gets or sets the honor grade floor for the character's alignment.
    /// </summary>
    public ushort AlignmentHonorGradeFloor { get; set; }

    /// <summary>
    /// Gets or sets the honor grade floor for the next alignment grade.
    /// </summary>
    public ushort AlignmentHonorNextGradeFloor { get; set; }

    /// <summary>
    /// Gets or sets the level of the character.
    /// </summary>
    public byte Level { get; set; }

    /// <summary>
    /// Gets or sets the current state of the character.
    /// </summary>
    public PlayerStates State { get; set; }

    /// <summary>
    /// Gets the list of friends associated with the character.
    /// </summary>
    public List<SocialWrapper> Friends { get; }

    /// <summary>
    /// Gets the list of ignored players associated with the character.
    /// </summary>
    public List<SocialWrapper> Ignored { get; }

    /// <summary>
    /// Gets the list of spells associated with the character.
    /// </summary>
    public List<SpellWrapper> Spells { get; }

    /// <summary>
    /// Gets the characteristic storage for the character.
    /// </summary>
    public CharacteristicStorage Characteristics { get; }

    /// <summary>
    /// Gets the list of general shortcuts for the character.
    /// </summary>
    public List<ShortcutBarObject> GeneralShortcutBar =>
        Record.GeneralShortcutBar;

    /// <summary>
    /// Gets the list of spell shortcuts for the character.
    /// </summary>
    public List<ShortcutBarObjectSpell> SpellShortcutBar =>
        Record.SpellShortcutBar;

    /// <summary>
    /// Gets or sets a value indicating whether the character is fully loaded.
    /// </summary>
    public bool IsLoaded { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the character connected.
    /// </summary>
    public DateTime ConnectedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterActor"/> class.
    /// </summary>
    /// <param name="record">The character record.</param>
    /// <param name="heroes">The hero information.</param>
    /// <param name="connection">The connection associated with the character.</param>
    /// <param name="position">The world position of the character.</param>
    /// <param name="characteristics">The characteristic storage for the character.</param>
    public CharacterActor(CharacterRecord record, Heroes.Heroes heroes, DofusConnection connection, WorldPosition position, CharacteristicStorage characteristics)
    {
        Record = record;
        Heroes = heroes;
        Connection = connection;
        Characteristics = characteristics;
        Friends = [];
        Ignored = [];
        Spells = [];
        _position = position;
    }

    /// <summary>
    /// Retrieves the game roleplay actor information for the character.
    /// </summary>
    /// <returns>An instance of <see cref="GameRolePlayCharacterInformations"/> containing the character's roleplay information.</returns>
    public override GameRolePlayActorInformations GetGameRolePlayActorInformations()
    {
        return new GameRolePlayCharacterInformations
        {
            AccountId = Account.Id,
            ContextualId = Id,
            Disposition = GetEntityDispositionInformations(),
            Look = Look.GetEntityLook(),
            Name = Name,
            HumanoidInfo = GetHumanInformations(),
            AlignmentInfos = GetActorAlignmentInformations()
        };
    }

    /// <summary>
    /// Retrieves the player's status.
    /// </summary>
    /// <returns>An instance of <see cref="PlayerStatus"/> representing the player's status.</returns>
    public PlayerStatus GetPlayerStatus()
    {
        return string.IsNullOrEmpty(StatusMessage)
            ? new PlayerStatus { StatusId = (sbyte)Status }
            : new PlayerStatusExtended { StatusId = (sbyte)Status, Message = StatusMessage };
    }

    /// <summary>
    /// Retrieves the alignment information for the character.
    /// </summary>
    /// <returns>An instance of <see cref="ActorAlignmentInformations"/> containing the character's alignment information.</returns>
    public ActorAlignmentInformations GetActorAlignmentInformations()
    {
        return new ActorAlignmentInformations
        {
            AlignmentSide = (sbyte)AlignmentSide,
            AlignmentValue = AlignmentValue,
            AlignmentGrade = AlignmentGrade,
            CharacterPower = CharacterPower
        };
    }

    /// <summary>
    /// Retrieves the extended alignment information for the character.
    /// </summary>
    /// <returns>An instance of <see cref="ActorExtendedAlignmentInformations"/> containing the character's extended alignment information.</returns>
    public ActorExtendedAlignmentInformations GetActorExtendedAlignmentInformations()
    {
        return new ActorExtendedAlignmentInformations
        {
            Aggressable = (sbyte)AlignmentStatus,
            AlignmentGrade = AlignmentGrade,
            AlignmentSide = (sbyte)AlignmentSide,
            AlignmentValue = AlignmentValue,
            CharacterPower = CharacterPower,
            Honor = AlignmentHonor,
            HonorGradeFloor = AlignmentHonorGradeFloor,
            HonorNextGradeFloor = AlignmentHonorNextGradeFloor,
        };
    }

    /// <summary>
    /// Retrieves the character's characteristics information.
    /// </summary>
    /// <returns>An instance of <see cref="CharacterCharacteristicsInformations"/> containing the character's characteristics.</returns>
    public CharacterCharacteristicsInformations GetCharacterCharacteristicsInformations()
    {
        return new CharacterCharacteristicsInformations
        {
            ActionPoints = Characteristics.ActionPoints.GetCharacterBaseCharacteristic(),
            MovementPoints = Characteristics.MovementPoints.GetCharacterBaseCharacteristic(),
            Vitality = Characteristics.Vitality.GetCharacterBaseCharacteristic(),
            Wisdom = Characteristics.Wisdom.GetCharacterBaseCharacteristic(),
            Strength = Characteristics.Strength.GetCharacterBaseCharacteristic(),
            Chance = Characteristics.Chance.GetCharacterBaseCharacteristic(),
            Intelligence = Characteristics.Intelligence.GetCharacterBaseCharacteristic(),
            Agility = Characteristics.Agility.GetCharacterBaseCharacteristic(),
            HealBonus = Characteristics.HealBonus.GetCharacterBaseCharacteristic(),
            AirDamageBonus = Characteristics.AirDamageBonus.GetCharacterBaseCharacteristic(),
            EarthDamageBonus = Characteristics.EarthDamageBonus.GetCharacterBaseCharacteristic(),
            FireDamageBonus = Characteristics.FireDamageBonus.GetCharacterBaseCharacteristic(),
            WaterDamageBonus = Characteristics.WaterDamageBonus.GetCharacterBaseCharacteristic(),
            NeutralDamageBonus = Characteristics.NeutralDamageBonus.GetCharacterBaseCharacteristic(),
            AirElementReduction = Characteristics.AirElementReduction.GetCharacterBaseCharacteristic(),
            EarthElementReduction = Characteristics.EarthElementReduction.GetCharacterBaseCharacteristic(),
            FireElementReduction = Characteristics.FireElementReduction.GetCharacterBaseCharacteristic(),
            WaterElementReduction = Characteristics.WaterElementReduction.GetCharacterBaseCharacteristic(),
            NeutralElementReduction = Characteristics.NeutralElementReduction.GetCharacterBaseCharacteristic(),
            AirElementResistPercent = Characteristics.AirElementResistPercent.GetCharacterBaseCharacteristic(),
            EarthElementResistPercent = Characteristics.EarthElementResistPercent.GetCharacterBaseCharacteristic(),
            FireElementResistPercent = Characteristics.FireElementResistPercent.GetCharacterBaseCharacteristic(),
            WaterElementResistPercent = Characteristics.WaterElementResistPercent.GetCharacterBaseCharacteristic(),
            NeutralElementResistPercent = Characteristics.NeutralElementResistPercent.GetCharacterBaseCharacteristic(),
            PvpAirElementReduction = Characteristics.PvpAirElementReduction.GetCharacterBaseCharacteristic(),
            PvpEarthElementReduction = Characteristics.PvpEarthElementReduction.GetCharacterBaseCharacteristic(),
            PvpFireElementReduction = Characteristics.PvpFireElementReduction.GetCharacterBaseCharacteristic(),
            PvpWaterElementReduction = Characteristics.PvpWaterElementReduction.GetCharacterBaseCharacteristic(),
            PvpNeutralElementReduction = Characteristics.PvpNeutralElementReduction.GetCharacterBaseCharacteristic(),
            PvpAirElementResistPercent = Characteristics.PvpAirElementResistPercent.GetCharacterBaseCharacteristic(),
            PvpEarthElementResistPercent = Characteristics.PvpEarthElementResistPercent.GetCharacterBaseCharacteristic(),
            PvpFireElementResistPercent = Characteristics.PvpFireElementResistPercent.GetCharacterBaseCharacteristic(),
            PvpWaterElementResistPercent = Characteristics.PvpWaterElementResistPercent.GetCharacterBaseCharacteristic(),
            PvpNeutralElementResistPercent = Characteristics.PvpNeutralElementResistPercent.GetCharacterBaseCharacteristic(),
            AllDamagesBonus = Characteristics.AllDamagesBonus.GetCharacterBaseCharacteristic(),
            CriticalDamageBonus = Characteristics.CriticalDamageBonus.GetCharacterBaseCharacteristic(),
            CriticalDamageReduction = Characteristics.CriticalDamageReduction.GetCharacterBaseCharacteristic(),
            CriticalHit = Characteristics.CriticalHit.GetCharacterBaseCharacteristic(),
            CriticalMiss = Characteristics.CriticalMiss.GetCharacterBaseCharacteristic(),
            DamagesBonusPercent = Characteristics.DamagesBonusPercent.GetCharacterBaseCharacteristic(),
            DodgePALostProbability = Characteristics.DodgePaLostProbability.GetCharacterBaseCharacteristic(),
            DodgePMLostProbability = Characteristics.DodgePmLostProbability.GetCharacterBaseCharacteristic(),
            GlyphBonusPercent = Characteristics.GlyphBonusPercent.GetCharacterBaseCharacteristic(),
            Initiative = Characteristics.Initiative.GetCharacterBaseCharacteristic(),
            PAAttack = Characteristics.PaAttack.GetCharacterBaseCharacteristic(),
            PMAttack = Characteristics.PmAttack.GetCharacterBaseCharacteristic(),
            Range = Characteristics.Range.GetCharacterBaseCharacteristic(),
            SummonableCreaturesBoost = Characteristics.SummonableCreaturesBoost.GetCharacterBaseCharacteristic(),
            PermanentDamagePercent = Characteristics.PermanentDamagePercent.GetCharacterBaseCharacteristic(),
            Prospecting = Characteristics.Prospecting.GetCharacterBaseCharacteristic(),
            PushDamageBonus = Characteristics.PushDamageBonus.GetCharacterBaseCharacteristic(),
            PushDamageReduction = Characteristics.PushDamageReduction.GetCharacterBaseCharacteristic(),
            Reflect = Characteristics.Reflect.GetCharacterBaseCharacteristic(),
            TackleBlock = Characteristics.TackleBlock.GetCharacterBaseCharacteristic(),
            TackleEvade = Characteristics.TackleEvade.GetCharacterBaseCharacteristic(),
            TrapBonus = Characteristics.TrapBonus.GetCharacterBaseCharacteristic(),
            TrapBonusPercent = Characteristics.TrapBonusPercent.GetCharacterBaseCharacteristic(),
            WeaponDamagesBonusPercent = Characteristics.WeaponDamagesBonusPercent.GetCharacterBaseCharacteristic(),
            ActionPointsCurrent = (short)Characteristics.ActionPoints.Total,
            EnergyPoints = (ushort)Characteristics.EnergyPoints.Total,
            MaxEnergyPoints = (ushort)Characteristics.EnergyPoints.TotalMax,
            LifePoints = (ushort)Characteristics.Health.Total,
            MaxLifePoints = (ushort)Characteristics.Health.TotalMax,
            MovementPointsCurrent = (short)Characteristics.MovementPoints.Total,
            StatsPoints = (ushort)Characteristics.StatsPoints.Total,
            SpellsPoints = (ushort)Characteristics.SpellsPoints.Total,
            Experience = Experience,
            ExperienceLevelFloor = ExperienceLevelFloor,
            ExperienceNextLevelFloor = ExperienceNextLevelFloor,
            Kamas = Kamas,
            AdditionnalPoints = 0,
            CriticalHitWeapon = 0,
            ProbationTime = 0,
            AlignmentInfos = GetActorExtendedAlignmentInformations(),
            SpellModifications = [] // TODO: fights
        };
    }
}
