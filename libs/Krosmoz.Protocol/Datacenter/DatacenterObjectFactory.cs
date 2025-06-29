// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Datacenter;

/// <summary>
/// A factory class responsible for creating datacenter objects based on their names.
/// </summary>
public sealed class DatacenterObjectFactory : IDatacenterObjectFactory
{
	/// <summary>
	/// Creates a datacenter object based on the specified name.
	/// </summary>
	/// <param name="name">The name of the datacenter object to create.</param>
	/// <returns>
	/// An instance of <see cref="IDatacenterObject"/> representing the created object.
	/// Throws an exception if the specified name does not match any known datacenter object type.
	/// </returns>
	public IDatacenterObject CreateInstance(string name)
	{
		return name switch
		{
			nameof(Abuse.AbuseReasons) => new Abuse.AbuseReasons { AbuseReasonId = 0, Mask = 0, ReasonTextId = 0, ReasonText = string.Empty, },
			nameof(Quest.AchievementCategory) => new Quest.AchievementCategory { Id = 0, NameId = 0, Name = string.Empty, ParentId = 0, Icon = string.Empty, Order = 0, Color = string.Empty, AchievementIds = [], },
			nameof(Quest.AchievementObjective) => new Quest.AchievementObjective { Id = 0, AchievementId = 0, Order = 0, NameId = 0, Name = string.Empty, Criterion = string.Empty, },
			nameof(Quest.AchievementReward) => new Quest.AchievementReward { Id = 0, AchievementId = 0, LevelMin = 0, LevelMax = 0, ItemsReward = [], ItemsQuantityReward = [], EmotesReward = [], SpellsReward = [], TitlesReward = [], OrnamentsReward = [], },
			nameof(Quest.Achievement) => new Quest.Achievement { Id = 0, NameId = 0, Name = string.Empty, CategoryId = 0, DescriptionId = 0, Description = string.Empty, IconId = 0, Points = 0, Level = 0, Order = 0, KamasRatio = 0, ExperienceRatio = 0, KamasScaleWithPlayerLevel = false, ObjectiveIds = [], RewardIds = [], },
			nameof(Misc.ActionDescription) => new Misc.ActionDescription { Id = 0, TypeId = 0, Name = string.Empty, DescriptionId = 0, Description = string.Empty, Trusted = false, NeedInteraction = false, MaxUsePerFrame = 0, MinimalUseInterval = 0, NeedConfirmation = false, },
			nameof(Alignments.AlignmentBalance) => new Alignments.AlignmentBalance { Id = 0, StartValue = 0, EndValue = 0, NameId = 0, Name = string.Empty, DescriptionId = 0, Description = string.Empty, },
			nameof(Alignments.AlignmentEffect) => new Alignments.AlignmentEffect { Id = 0, CharacteristicId = 0, DescriptionId = 0, Description = string.Empty, },
			nameof(Alignments.AlignmentGift) => new Alignments.AlignmentGift { Id = 0, NameId = 0, Name = string.Empty, EffectId = 0, GfxId = 0, },
			nameof(Alignments.AlignmentOrder) => new Alignments.AlignmentOrder { Id = 0, NameId = 0, Name = string.Empty, SideId = 0, },
			nameof(Alignments.AlignmentRank) => new Alignments.AlignmentRank { Id = 0, OrderId = 0, NameId = 0, Name = string.Empty, DescriptionId = 0, Description = string.Empty, MinimumAlignment = 0, ObjectsStolen = 0, Gifts = [], },
			nameof(Alignments.AlignmentRankJntGift) => new Alignments.AlignmentRankJntGift { Id = 0, Gifts = [], Parameters = [], Levels = [], },
			nameof(Alignments.AlignmentSide) => new Alignments.AlignmentSide { Id = 0, NameId = 0, Name = string.Empty, CanConquest = false, },
			nameof(Alignments.AlignmentTitle) => new Alignments.AlignmentTitle { SideId = 0, NamesId = [], ShortsId = [], },
			nameof(Almanax.AlmanaxCalendar) => new Almanax.AlmanaxCalendar { Id = 0, NameId = 0, Name = string.Empty, DescId = 0, Desc = string.Empty, NpcId = 0, },
			nameof(Appearance.Appearance) => new Krosmoz.Protocol.Datacenter.Appearance.Appearance { Id = 0, Type = 0, Data = string.Empty, },
			nameof(World.Area) => new World.Area { Id = 0, NameId = 0, Name = string.Empty, SuperAreaId = 0, ContainHouses = false, ContainPaddocks = false, Bounds = (Geom.Rectangle)CreateInstance("Rectangle"), WorldmapId = 0, HasWorldMap = false, },
			nameof(Breeds.BreedRole) => new Breeds.BreedRole { Id = 0, NameId = 0, Name = string.Empty, DescriptionId = 0, Description = string.Empty, AssetId = 0, Color = 0, },
			nameof(Breeds.BreedRoleByBreed) => new Breeds.BreedRoleByBreed { BreedId = 0, RoleId = 0, DescriptionId = 0, Description = string.Empty, Value = 0, Order = 0, },
			nameof(Breeds.Breed) => new Breeds.Breed { Id = 0, ShortNameId = 0, ShortName = string.Empty, LongNameId = 0, LongName = string.Empty, DescriptionId = 0, Description = string.Empty, GameplayDescriptionId = 0, GameplayDescription = string.Empty, MaleLook = string.Empty, FemaleLook = string.Empty, CreatureBonesId = 0, MaleArtwork = 0, FemaleArtwork = 0, StatsPointsForStrength = [], StatsPointsForIntelligence = [], StatsPointsForChance = [], StatsPointsForAgility = [], StatsPointsForVitality = [], StatsPointsForWisdom = [], BreedSpellsId = [], BreedRoles = [], MaleColors = [], FemaleColors = [], SpawnMap = 0, },
			nameof(Misc.CensoredContent) => new Misc.CensoredContent { Lang = string.Empty, Type = 0, OldValue = 0, NewValue = 0, },
			nameof(Communication.CensoredWord) => new Communication.CensoredWord { Id = 0, ListId = 0, Language = string.Empty, Word = string.Empty, DeepLooking = false, },
			nameof(Challenges.Challenge) => new Challenges.Challenge { Id = 0, NameId = 0, Name = string.Empty, DescriptionId = 0, Description = string.Empty, },
			nameof(Characteristics.CharacteristicCategory) => new Characteristics.CharacteristicCategory { Id = 0, NameId = 0, Name = string.Empty, Order = 0, CharacteristicIds = [], },
			nameof(Characteristics.Characteristic) => new Characteristics.Characteristic { Id = 0, Keyword = string.Empty, NameId = 0, Name = string.Empty, Asset = string.Empty, CategoryId = 0, Visible = false, Order = 0, Upgradable = false, },
			nameof(Communication.ChatChannel) => new Communication.ChatChannel { Id = 0, NameId = 0, Name = string.Empty, DescriptionId = 0, Description = string.Empty, Shortcut = string.Empty, ShortcutKey = string.Empty, IsPrivate = false, AllowObjects = false, },
			nameof(Documents.Comic) => new Documents.Comic { Id = 0, RemoteId = string.Empty, },
			nameof(Monsters.CompanionCharacteristic) => new Monsters.CompanionCharacteristic { Id = 0, CaracId = 0, CompanionId = 0, Order = 0, InitialValue = 0, LevelPerValue = 0, ValuePerLevel = 0, },
			nameof(Monsters.Companion) => new Monsters.Companion { Id = 0, NameId = 0, Name = string.Empty, Look = string.Empty, WebDisplay = false, DescriptionId = 0, Description = string.Empty, StartingSpellLevelId = 0, AssetId = 0, Characteristics = [], Spells = [], CreatureBoneId = 0, },
			nameof(Monsters.CompanionSpell) => new Monsters.CompanionSpell { Id = 0, SpellId = 0, CompanionId = 0, GradeByLevel = string.Empty, },
			nameof(Appearance.CreatureBoneOverride) => new Appearance.CreatureBoneOverride { BoneId = 0, CreatureBoneId = 0, },
			nameof(Appearance.CreatureBoneType) => new Appearance.CreatureBoneType { Id = 0, CreatureBoneId = 0, },
			nameof(Documents.Document) => new Documents.Document { Id = 0, TypeId = 0, ShowTitle = false, ShowBackgroundImage = false, TitleId = 0, Title = string.Empty, AuthorId = 0, Author = string.Empty, SubTitleId = 0, SubTitle = string.Empty, ContentId = 0, Content = string.Empty, ContentCSS = string.Empty, ClientProperties = string.Empty, },
			nameof(World.Dungeon) => new World.Dungeon { Id = 0, NameId = 0, Name = string.Empty, OptimalPlayerLevel = 0, MapIds = [], EntranceMapId = 0, ExitMapId = 0, },
			nameof(Effects.Effect) => new Effects.Effect { Id = 0, DescriptionId = 0, Description = string.Empty, IconId = 0, Characteristic = 0, Category = 0, Operator = string.Empty, ShowInTooltip = false, UseDice = false, ForceMinMax = false, Boost = false, Active = false, OppositeId = 0, TheoreticalDescriptionId = 0, TheoreticalDescription = string.Empty, TheoreticalPattern = 0, ShowInSet = false, BonusType = 0, UseInFight = false, EffectPriority = 0, ElementId = 0, },
			nameof(Guild.EmblemBackground) => new Guild.EmblemBackground { Id = 0, Order = 0, },
			nameof(Guild.EmblemSymbolCategory) => new Guild.EmblemSymbolCategory { Id = 0, NameId = 0, Name = string.Empty, },
			nameof(Guild.EmblemSymbol) => new Guild.EmblemSymbol { Id = 0, SkinId = 0, IconId = 0, Order = 0, CategoryId = 0, Colorizable = false, },
			nameof(Communication.Emoticon) => new Communication.Emoticon { Id = 0, NameId = 0, Name = string.Empty, ShortcutId = 0, Shortcut = string.Empty, Order = 0, DefaultAnim = string.Empty, Persistancy = false, EightDirections = false, Aura = false, Anims = [], Cooldown = 0, Duration = 0, Weight = 0, },
			nameof(ExternalNotifications.ExternalNotification) => new ExternalNotifications.ExternalNotification { Id = 0, CategoryId = 0, IconId = 0, ColorId = 0, DescriptionId = 0, Description = string.Empty, DefaultEnable = false, DefaultSound = false, DefaultMultiAccount = false, DefaultNotify = false, Name = string.Empty, MessageId = 0, Message = string.Empty, },
			nameof(Breeds.Head) => new Breeds.Head { Id = 0, Skins = string.Empty, AssetId = string.Empty, Breed = 0, Gender = 0, Label = string.Empty, Order = 0, },
			nameof(World.HintCategory) => new World.HintCategory { Id = 0, NameId = 0, Name = string.Empty, },
			nameof(World.Hint) => new World.Hint { Id = 0, CategoryId = 0, Gfx = 0, NameId = 0, Name = string.Empty, MapId = 0, RealMapId = 0, X = 0, Y = 0, Outdoor = false, SubareaId = 0, WorldMapId = 0, },
			nameof(Houses.House) => new Houses.House { TypeId = 0, DefaultPrice = 0, NameId = 0, Name = string.Empty, DescriptionId = 0, Description = string.Empty, GfxId = 0, },
			nameof(Idols.Idol) => new Idols.Idol { Id = 0, Description = string.Empty, CategoryId = 0, ItemId = 0, GroupOnly = false, Score = 0, ExperienceBonus = 0, DropBonus = 0, SpellPairId = 0, SynergyIdolsIds = [], SynergyIdolsCoeff = [], },
			nameof(Items.Incarnation) => new Items.Incarnation { Id = 0, LookMale = string.Empty, LookFemale = string.Empty, },
			nameof(Items.IncarnationLevel) => new Items.IncarnationLevel { Id = 0, IncarnationId = 0, Level = 0, RequiredXp = 0, },
			nameof(Communication.InfoMessage) => new Communication.InfoMessage { TypeId = 0, MessageId = 0, TextId = 0, Text = string.Empty, },
			nameof(Interactives.Interactive) => new Interactives.Interactive { Id = 0, NameId = 0, Name = string.Empty, ActionId = 0, DisplayTooltip = false, },
			nameof(Items.Item) => new Items.Item { Id = 0, NameId = 0, Name = string.Empty, TypeId = 0, DescriptionId = 0, Description = string.Empty, IconId = 0, Level = 0, RealWeight = 0, Cursed = false, UseAnimationId = 0, Usable = false, Targetable = false, Exchangeable = false, Price = 0, TwoHanded = false, Etheral = false, ItemSetId = 0, Criteria = string.Empty, CriteriaTarget = string.Empty, HideEffects = false, Enhanceable = false, NonUsableOnAnother = false, AppearanceId = 0, SecretRecipe = false, RecipeSlots = 0, RecipeIds = [], DropMonsterIds = [], BonusIsSecret = false, PossibleEffects = [], FavoriteSubAreas = [], FavoriteSubAreasBonus = 0, CraftXpRatio = 0, NeedUseConfirm = false, },
			nameof(Items.Weapon) => new Items.Weapon { FavoriteSubAreasBonus = 0, CraftXpRatio = 0, Range = 0, BonusIsSecret = false, CriticalHitBonus = 0, CriteriaTarget = string.Empty, MinRange = 0, MaxCastPerTurn = 0, DescriptionId = 0, Description = string.Empty, RecipeIds = [], SecretRecipe = false, Etheral = false, AppearanceId = 0, Id = 0, DropMonsterIds = [], Cursed = false, Exchangeable = false, Level = 0, RealWeight = 0, CastTestLos = false, FavoriteSubAreas = [], CriticalFailureProbability = 0, HideEffects = false, Criteria = string.Empty, Targetable = false, CriticalHitProbability = 0, TwoHanded = false, NonUsableOnAnother = false, ItemSetId = 0, NameId = 0, Name = string.Empty, CastInDiagonal = false, Price = 0, Enhanceable = false, NeedUseConfirm = false, ApCost = 0, Usable = false, CastInLine = false, PossibleEffects = [], UseAnimationId = 0, IconId = 0, TypeId = 0, RecipeSlots = 0, },
			nameof(Items.ItemSet) => new Items.ItemSet { Id = 0, Items = [], NameId = 0, Name = string.Empty, BonusIsSecret = false, Effects = [], },
			nameof(Items.ItemType) => new Items.ItemType { Id = 0, NameId = 0, Name = string.Empty, SuperTypeId = 0, Plural = false, Gender = 0, RawZone = string.Empty, Mimickable = false, CraftXpRatio = 0, },
			nameof(Jobs.Job) => new Jobs.Job { Id = 0, NameId = 0, Name = string.Empty, IconId = 0, },
			nameof(Quest.TreasureHunt.LegendaryTreasureHunt) => new Quest.TreasureHunt.LegendaryTreasureHunt { Id = 0, NameId = 0, Name = string.Empty, Level = 0, ChestId = 0, MonsterId = 0, MapItemId = 0, XpRatio = 0, },
			nameof(LivingObjects.LivingObjectSkinJntMood) => new LivingObjects.LivingObjectSkinJntMood { SkinId = 0, Moods = [], },
			nameof(World.MapCoordinates) => new World.MapCoordinates { CompressedCoords = 0, MapIds = [], },
			nameof(World.MapPosition) => new World.MapPosition { Id = 0, PosX = 0, PosY = 0, Outdoor = false, Capabilities = 0, NameId = 0, Name = string.Empty, ShowNameOnFingerpost = false, Sounds = [], Playlists = [], SubAreaId = 0, WorldMap = 0, HasPriorityOnWorldmap = false, },
			nameof(AmbientSounds.MapAmbientSound) => new AmbientSounds.MapAmbientSound { Id = 0, Volume = 0, CriterionId = 0, SilenceMin = 0, SilenceMax = 0, Channel = 0, TypeId = 0, },
			nameof(World.MapReference) => new World.MapReference { Id = 0, MapId = 0, CellId = 0, },
			nameof(World.MapScrollAction) => new World.MapScrollAction { Id = 0, RightExists = false, BottomExists = false, LeftExists = false, TopExists = false, RightMapId = 0, BottomMapId = 0, LeftMapId = 0, TopMapId = 0, },
			nameof(Monsters.MonsterMiniBoss) => new Monsters.MonsterMiniBoss { Id = 0, MonsterReplacingId = 0, },
			nameof(Monsters.MonsterRace) => new Monsters.MonsterRace { Id = 0, SuperRaceId = 0, NameId = 0, Name = string.Empty, Monsters = [], },
			nameof(Monsters.AnimFunMonsterData) => new Monsters.AnimFunMonsterData { AnimName = string.Empty, AnimWeight = 0, },
			nameof(Monsters.MonsterGrade) => new Monsters.MonsterGrade { Grade = 0, MonsterId = 0, Level = 0, LifePoints = 0, ActionPoints = 0, MovementPoints = 0, PaDodge = 0, PmDodge = 0, Wisdom = 0, EarthResistance = 0, AirResistance = 0, FireResistance = 0, WaterResistance = 0, NeutralResistance = 0, GradeXp = 0, DamageReflect = 0, },
			nameof(Monsters.Monster) => new Monsters.Monster { Id = 0, NameId = 0, Name = string.Empty, GfxId = 0, Race = 0, Grades = [], Look = string.Empty, UseSummonSlot = false, UseBombSlot = false, CanPlay = false, AnimFunList = [], CanTackle = false, IsBoss = false, Drops = [], Subareas = [], Spells = [], FavoriteSubareaId = 0, IsMiniBoss = false, IsQuestMonster = false, CorrespondingMiniBossId = 0, SpeedAdjust = 0, CreatureBoneId = 0, CanBePushed = false, FastAnimsFun = false, CanSwitchPos = false, IncompatibleIdols = [], },
			nameof(Monsters.MonsterDrop) => new Monsters.MonsterDrop { DropId = 0, MonsterId = 0, ObjectId = 0, PercentDropForGrade1 = 0, PercentDropForGrade2 = 0, PercentDropForGrade3 = 0, PercentDropForGrade4 = 0, PercentDropForGrade5 = 0, Count = 0, HasCriteria = false, },
			nameof(Monsters.MonsterSuperRace) => new Monsters.MonsterSuperRace { Id = 0, NameId = 0, Name = string.Empty, },
			nameof(Misc.Month) => new Misc.Month { Id = 0, NameId = 0, Name = string.Empty, },
			nameof(Mounts.MountBehavior) => new Mounts.MountBehavior { Id = 0, NameId = 0, Name = string.Empty, DescriptionId = 0, Description = string.Empty, },
			nameof(Mounts.MountBone) => new Mounts.MountBone { Id = 0, },
			nameof(Mounts.Mount) => new Mounts.Mount { Id = 0, NameId = 0, Name = string.Empty, Look = string.Empty, },
			nameof(Notifications.Notification) => new Notifications.Notification { Id = 0, TitleId = 0, Title = string.Empty, MessageId = 0, Message = string.Empty, IconId = 0, TypeId = 0, Trigger = string.Empty, },
			nameof(Npcs.NpcAction) => new Npcs.NpcAction { Id = 0, RealId = 0, NameId = 0, Name = string.Empty, },
			nameof(Npcs.NpcMessage) => new Npcs.NpcMessage { Id = 0, MessageId = 0, Message = string.Empty, },
			nameof(Npcs.Npc) => new Npcs.Npc { Id = 0, NameId = 0, Name = string.Empty, DialogMessages = [], DialogReplies = [], Actions = [], Gender = 0, Look = string.Empty, AnimFunList = [], FastAnimsFun = false, },
			nameof(Npcs.AnimFunNpcData) => new Npcs.AnimFunNpcData { AnimName = string.Empty, AnimWeight = 0, },
			nameof(Misc.OptionalFeature) => new Misc.OptionalFeature { Id = 0, Keyword = string.Empty, },
			nameof(Appearance.Ornament) => new Appearance.Ornament { Id = 0, NameId = 0, Name = string.Empty, Visible = false, AssetId = 0, IconId = 0, Rarity = 0, Order = 0, },
			nameof(Misc.Pack) => new Misc.Pack { Id = 0, Name = string.Empty, HasSubAreas = false, },
			nameof(LivingObjects.Pet) => new LivingObjects.Pet { Id = 0, FoodItems = [], FoodTypes = [], MinDurationBeforeMeal = 0, MaxDurationBeforeMeal = 0, PossibleEffects = [], },
			nameof(World.Phoenix) => new World.Phoenix { MapId = 0, },
			nameof(Playlists.Playlist) => new Playlists.Playlist { Id = 0, SilenceDuration = 0, Iteration = 0, Type = 0, Sounds = [], },
			nameof(AmbientSounds.PlaylistSound) => new AmbientSounds.PlaylistSound { Id = string.Empty, Volume = 0, },
			nameof(Quest.TreasureHunt.PointOfInterest) => new Quest.TreasureHunt.PointOfInterest { Id = 0, NameId = 0, Name = string.Empty, CategoryId = 0, },
			nameof(Quest.TreasureHunt.PointOfInterestCategory) => new Quest.TreasureHunt.PointOfInterestCategory { Id = 0, ActionLabelId = 0, ActionLabel = string.Empty, },
			nameof(Items.PresetIcon) => new Items.PresetIcon { Id = 0, Order = 0, },
			nameof(Quest.QuestCategory) => new Quest.QuestCategory { Id = 0, NameId = 0, Name = string.Empty, Order = 0, QuestIds = [], },
			nameof(Quest.Objectives.QuestObjectiveCraftItem) => new Quest.Objectives.QuestObjectiveCraftItem { Id = 0, MapId = 0, Coords = (Geom.Point)CreateInstance("Point"), Parameters = [], StepId = 0, DialogId = 0, TypeId = 0, },
			nameof(Quest.QuestObjective) => new Quest.QuestObjective { Id = 0, StepId = 0, TypeId = 0, DialogId = 0, Parameters = [], Coords = (Geom.Point)CreateInstance("Point"), MapId = 0, },
			nameof(Quest.Objectives.QuestObjectiveDiscoverSubArea) => new Quest.Objectives.QuestObjectiveDiscoverSubArea { Id = 0, MapId = 0, Coords = (Geom.Point)CreateInstance("Point"), Parameters = [], StepId = 0, DialogId = 0, TypeId = 0, },
			nameof(Quest.Objectives.QuestObjectiveFightMonstersOnMap) => new Quest.Objectives.QuestObjectiveFightMonstersOnMap { Id = 0, MapId = 0, Coords = (Geom.Point)CreateInstance("Point"), Parameters = [], StepId = 0, DialogId = 0, TypeId = 0, },
			nameof(Quest.Objectives.QuestObjectiveGoToNpc) => new Quest.Objectives.QuestObjectiveGoToNpc { Id = 0, MapId = 0, Coords = (Geom.Point)CreateInstance("Point"), Parameters = [], StepId = 0, DialogId = 0, TypeId = 0, },
			nameof(Quest.Objectives.QuestObjectiveFightMonster) => new Quest.Objectives.QuestObjectiveFightMonster { Id = 0, MapId = 0, Coords = (Geom.Point)CreateInstance("Point"), Parameters = [], StepId = 0, DialogId = 0, TypeId = 0, },
			nameof(Quest.Objectives.QuestObjectiveBringSoulToNpc) => new Quest.Objectives.QuestObjectiveBringSoulToNpc { Id = 0, MapId = 0, Coords = (Geom.Point)CreateInstance("Point"), Parameters = [], StepId = 0, DialogId = 0, TypeId = 0, },
			nameof(Quest.Objectives.QuestObjectiveDiscoverMap) => new Quest.Objectives.QuestObjectiveDiscoverMap { Id = 0, MapId = 0, Coords = (Geom.Point)CreateInstance("Point"), Parameters = [], StepId = 0, DialogId = 0, TypeId = 0, },
			nameof(Quest.Objectives.QuestObjectiveBringItemToNpc) => new Quest.Objectives.QuestObjectiveBringItemToNpc { Id = 0, MapId = 0, Coords = (Geom.Point)CreateInstance("Point"), Parameters = [], StepId = 0, DialogId = 0, TypeId = 0, },
			nameof(Quest.Objectives.QuestObjectiveFreeForm) => new Quest.Objectives.QuestObjectiveFreeForm { Id = 0, MapId = 0, Coords = (Geom.Point)CreateInstance("Point"), Parameters = [], StepId = 0, DialogId = 0, TypeId = 0, },
			nameof(Quest.Objectives.QuestObjectiveMultiFightMonster) => new Quest.Objectives.QuestObjectiveMultiFightMonster { Id = 0, MapId = 0, Coords = (Geom.Point)CreateInstance("Point"), Parameters = [], StepId = 0, DialogId = 0, TypeId = 0, },
			nameof(Quest.Objectives.QuestObjectiveDuelSpecificPlayer) => new Quest.Objectives.QuestObjectiveDuelSpecificPlayer { Id = 0, MapId = 0, Coords = (Geom.Point)CreateInstance("Point"), Parameters = [], StepId = 0, DialogId = 0, TypeId = 0, },
			nameof(Geom.Point) => new Geom.Point { X = 0, Y = 0, },
			nameof(Quest.QuestObjectiveType) => new Quest.QuestObjectiveType { Id = 0, NameId = 0, Name = string.Empty, },
			nameof(Quest.Quest) => new Krosmoz.Protocol.Datacenter.Quest.Quest { Id = 0, NameId = 0, Name = string.Empty, CategoryId = 0, IsRepeatable = false, RepeatType = 0, RepeatLimit = 0, IsDungeonQuest = false, LevelMin = 0, LevelMax = 0, StepIds = [], IsPartyQuest = false, },
			nameof(Quest.QuestStepRewards) => new Quest.QuestStepRewards { Id = 0, StepId = 0, LevelMin = 0, LevelMax = 0, ItemsReward = [], EmotesReward = [], JobsReward = [], SpellsReward = [], },
			nameof(Quest.QuestStep) => new Quest.QuestStep { Id = 0, QuestId = 0, NameId = 0, Name = string.Empty, DescriptionId = 0, Description = string.Empty, DialogId = 0, OptimalLevel = 0, Duration = 0, KamasScaleWithPlayerLevel = false, KamasRatio = 0, XpRatio = 0, ObjectiveIds = [], RewardsIds = [], },
			nameof(Guild.RankName) => new Guild.RankName { Id = 0, NameId = 0, Name = string.Empty, Order = 0, },
			nameof(Jobs.Recipe) => new Jobs.Recipe { ResultId = 0, ResultNameId = 0, ResultName = string.Empty, ResultTypeId = 0, ResultLevel = 0, IngredientIds = [], Quantities = [], JobId = 0, SkillId = 0, },
			nameof(Mounts.RideFood) => new Mounts.RideFood { Gid = 0, TypeId = 0, },
			nameof(Servers.ServerCommunity) => new Servers.ServerCommunity { Id = 0, NameId = 0, Name = string.Empty, DefaultCountries = [], ShortId = string.Empty, },
			nameof(Servers.ServerGameType) => new Servers.ServerGameType { Id = 0, NameId = 0, Name = string.Empty, },
			nameof(Servers.ServerPopulation) => new Servers.ServerPopulation { Id = 0, NameId = 0, Name = string.Empty, Weight = 0, },
			nameof(Servers.Server) => new Servers.Server { Id = 0, NameId = 0, Name = string.Empty, CommentId = 0, Comment = string.Empty, OpeningDate = 0, Language = string.Empty, PopulationId = 0, GameTypeId = 0, CommunityId = 0, RestrictedToLanguages = [], },
			nameof(Interactives.SkillName) => new Interactives.SkillName { Id = 0, NameId = 0, Name = string.Empty, },
			nameof(Jobs.Skill) => new Jobs.Skill { Id = 0, NameId = 0, Name = string.Empty, ParentJobId = 0, IsForgemagus = false, ModifiableItemTypeIds = [], GatheredRessourceItem = 0, CraftableItemIds = [], InteractiveId = 0, UseAnimation = string.Empty, IsRepair = false, Cursor = 0, ElementActionId = 0, AvailableInHouse = false, ClientDisplay = false, LevelMin = 0, },
			nameof(Appearance.SkinMapping) => new Appearance.SkinMapping { Id = 0, LowDefId = 0, },
			nameof(Appearance.SkinPosition) => new Appearance.SkinPosition { Id = 0, Transformation = [], Clip = [], Skin = [], },
			nameof(Tiphon.TransformData) => new Tiphon.TransformData { X = 0, Y = 0, ScaleX = 0, ScaleY = 0, Rotation = 0, OriginalClip = string.Empty, OverrideClip = string.Empty, },
			nameof(Communication.Smiley) => new Communication.Smiley { Id = 0, Order = 0, GfxId = string.Empty, ForPlayers = false, Triggers = [], },
			nameof(Sounds.SoundBones) => new Sounds.SoundBones { Id = 0, Keys = [], Values = [], },
			nameof(Sounds.SoundAnimation) => new Sounds.SoundAnimation { Id = 0, Label = string.Empty, Name = string.Empty, Filename = string.Empty, Volume = 0, Rolloff = 0, AutomationDuration = 0, AutomationVolume = 0, AutomationFadeIn = 0, AutomationFadeOut = 0, NoCutSilence = false, StartFrame = 0, },
			nameof(Sounds.SoundUi) => new Sounds.SoundUi { Id = 0, UiName = string.Empty, OpenFile = string.Empty, CloseFile = string.Empty, SubElements = [], },
			nameof(Sounds.SoundUiElement) => new Sounds.SoundUiElement { Id = 0, Name = string.Empty, HookId = 0, File = string.Empty, },
			nameof(Sounds.SoundUiHook) => new Sounds.SoundUiHook { Id = 0, Name = string.Empty, },
			nameof(LivingObjects.SpeakingItemText) => new LivingObjects.SpeakingItemText { TextId = 0, TextProba = 0, TextStringId = 0, TextString = string.Empty, TextLevel = 0, TextSound = 0, TextRestriction = string.Empty, },
			nameof(LivingObjects.SpeakingItemsTrigger) => new LivingObjects.SpeakingItemsTrigger { TriggersId = 0, TextIds = [], States = [], },
			nameof(Spells.SpellBomb) => new Spells.SpellBomb { Id = 0, ChainReactionSpellId = 0, ExplodSpellId = 0, WallId = 0, InstantSpellId = 0, ComboCoeff = 0, },
			nameof(Spells.SpellLevel) => new Spells.SpellLevel { Id = 0, SpellId = 0, Grade = 0, SpellBreed = 0, ApCost = 0, MinRange = 0, Range = 0, CastInLine = false, CastInDiagonal = false, CastTestLos = false, CriticalHitProbability = 0, CriticalFailureProbability = 0, NeedFreeCell = false, NeedTakenCell = false, NeedFreeTrapCell = false, RangeCanBeBoosted = false, MaxStack = 0, MaxCastPerTurn = 0, MaxCastPerTarget = 0, MinCastInterval = 0, InitialCooldown = 0, GlobalCooldown = 0, MinPlayerLevel = 0, CriticalFailureEndsTurn = false, HideEffects = false, Hidden = false, StatesRequired = [], StatesForbidden = [], Effects = [], CriticalEffect = [], },
			nameof(Effects.EffectInstance) => new Effects.EffectInstance { EffectUid = 0, EffectId = 0, TargetId = 0, TargetMask = string.Empty, Duration = 0, Random = 0, Group = 0, VisibleInTooltip = false, VisibleInBuffUi = false, VisibleInFightLog = false, RawZone = string.Empty, Delay = 0, Triggers = string.Empty, },
			nameof(Effects.Instances.EffectInstanceDice) => new Effects.Instances.EffectInstanceDice { VisibleInTooltip = false, Random = 0, RawZone = string.Empty, TargetId = 0, TargetMask = string.Empty, EffectId = 0, DiceNum = 0, Duration = 0, VisibleInFightLog = false, EffectUid = 0, DiceSide = 0, Value = 0, VisibleInBuffUi = false, Delay = 0, Triggers = string.Empty, Group = 0, },
			nameof(Effects.Instances.EffectInstanceInteger) => new Effects.Instances.EffectInstanceInteger { VisibleInTooltip = false, Random = 0, RawZone = string.Empty, TargetId = 0, TargetMask = string.Empty, EffectId = 0, Duration = 0, VisibleInFightLog = false, EffectUid = 0, Value = 0, VisibleInBuffUi = false, Triggers = string.Empty, Delay = 0, Group = 0, },
			nameof(Spells.SpellPair) => new Spells.SpellPair { Id = 0, NameId = 0, Name = string.Empty, DescriptionId = 0, Description = string.Empty, IconId = 0, },
			nameof(Spells.Spell) => new Spells.Spell { Id = 0, NameId = 0, Name = string.Empty, DescriptionId = 0, Description = string.Empty, TypeId = 0, ScriptParams = string.Empty, ScriptParamsCritical = string.Empty, ScriptId = 0, ScriptIdCritical = 0, IconId = 0, SpellLevels = [], VerboseCast = false, },
			nameof(Spells.SpellState) => new Spells.SpellState { Id = 0, NameId = 0, Name = string.Empty, PreventsSpellCast = false, PreventsFight = false, IsSilent = false, EffectsIds = [], },
			nameof(Spells.SpellType) => new Spells.SpellType { Id = 0, LongNameId = 0, LongName = string.Empty, ShortNameId = 0, ShortName = string.Empty, },
			nameof(Interactives.StealthBones) => new Interactives.StealthBones { Id = 0, },
			nameof(World.SubArea) => new World.SubArea { Id = 0, NameId = 0, Name = string.Empty, AreaId = 0, AmbientSounds = [], Playlists = [], MapIds = [], Bounds = (Geom.Rectangle)CreateInstance("Rectangle"), Shape = [], CustomWorldMap = [], PackId = 0, Level = 0, IsConquestVillage = false, BasicAccountAllowed = false, DisplayOnWorldMap = false, Monsters = [], EntranceMapIds = [], ExitMapIds = [], Capturable = false, },
			nameof(AmbientSounds.AmbientSound) => new AmbientSounds.AmbientSound { Id = string.Empty, Volume = 0, CriterionId = 0, SilenceMin = 0, SilenceMax = 0, Channel = 0, TypeId = 0, },
			nameof(Geom.Rectangle) => new Geom.Rectangle { X = 0, Y = 0, Width = 0, Height = 0, },
			nameof(World.SuperArea) => new World.SuperArea { Id = 0, NameId = 0, Name = string.Empty, WorldmapId = 0, HasWorldMap = false, },
			nameof(Npcs.TaxCollectorFirstname) => new Npcs.TaxCollectorFirstname { Id = 0, FirstnameId = 0, Firstname = string.Empty, },
			nameof(Npcs.TaxCollectorName) => new Npcs.TaxCollectorName { Id = 0, NameId = 0, Name = string.Empty, },
			nameof(Misc.Tips) => new Misc.Tips { Id = 0, DescId = 0, Desc = string.Empty, },
			nameof(Appearance.TitleCategory) => new Appearance.TitleCategory { Id = 0, NameId = 0, Name = string.Empty, },
			nameof(Appearance.Title) => new Appearance.Title { Id = 0, NameMaleId = 0, NameMale = string.Empty, NameFemaleId = 0, NameFemale = string.Empty, Visible = false, CategoryId = 0, },
			nameof(Misc.TypeAction) => new Misc.TypeAction { Id = 0, ElementName = string.Empty, ElementId = 0, },
			nameof(Misc.Url) => new Misc.Url { Id = 0, BrowserId = 0, Uri = string.Empty, Param = string.Empty, Method = string.Empty, },
			nameof(Items.VeteranReward) => new Items.VeteranReward { Id = 0, RequiredSubDays = 0, ItemGID = 0, ItemQuantity = 0, },
			nameof(World.Waypoint) => new World.Waypoint { Id = 0, MapId = 0, SubAreaId = 0, },
			nameof(World.WorldMap) => new World.WorldMap { Id = 0, NameId = 0, Name = string.Empty, OrigineX = 0, OrigineY = 0, MapWidth = 0, MapHeight = 0, HorizontalChunck = 0, VerticalChunck = 0, ViewableEverywhere = false, MinScale = 0, MaxScale = 0, StartScale = 0, CenterX = 0, CenterY = 0, TotalWidth = 0, TotalHeight = 0, Zoom = [], },
			_ => throw new Exception($"Unknown datacenter object name: {name}")
		};
	}
}
