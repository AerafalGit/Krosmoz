﻿// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Frozen;
using Krosmoz.Protocol.Datacenter.Jobs;
using Krosmoz.Protocol.Datacenter.World;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Serialization.D2P;
using Krosmoz.Serialization.DLM;
using Krosmoz.Serialization.DLM.Elements;
using Krosmoz.Serialization.ELE;
using Krosmoz.Serialization.ELE.Types;
using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.GameServer.Database;
using Krosmoz.Servers.GameServer.Database.Models.Interactives;
using Krosmoz.Servers.GameServer.Database.Models.Maps;
using Krosmoz.Servers.GameServer.Models.World.Cells;
using Krosmoz.Servers.GameServer.Services.Datacenter;
using Krosmoz.Tools.Seeds.Base;
using Microsoft.EntityFrameworkCore;

namespace Krosmoz.Tools.Seeds.Maps;

/// <summary>
/// Represents a seeder for populating map-related data in the database.
/// </summary>
public sealed class MapSeeder : BaseSeeder
{
    private static readonly FrozenDictionary<int, InteractiveIds> s_gfxIdToInteractiveId = new Dictionary<int, InteractiveIds>
    {
        // Common
        [224] = InteractiveIds.Puits,

        // Paysan
        [660] = InteractiveIds.Ble,
        [661] = InteractiveIds.Houblon,
        [662] = InteractiveIds.Lin,
        [663] = InteractiveIds.Chanvre,
        [664] = InteractiveIds.Orge,
        [665] = InteractiveIds.Seigle,
        [667] = InteractiveIds.Malt,
        [683] = InteractiveIds.Riz,
        [701] = InteractiveIds.Avoine,
        [1245] = InteractiveIds.Frostiz,

        // Alchimiste
        [3212] = InteractiveIds.Ortie,
        [3213] = InteractiveIds.Sauge,
        [677] = InteractiveIds.TrefleA5Feuilles,
        [678] = InteractiveIds.MentheSauvage,
        [679] = InteractiveIds.OrchideeFreyesque,
        [680] = InteractiveIds.Edelweiss,
        [684] = InteractiveIds.Pandouille,
        [1288] = InteractiveIds.PerceNeige,
        [3234] = InteractiveIds.Ginseng,
        [3223] = InteractiveIds.Belladone,
        [3226] = InteractiveIds.Mandragore,

        // Bucheron
        [650] = InteractiveIds.Frene,
        [653] = InteractiveIds.Chene,
        [655] = InteractiveIds.If,
        [657] = InteractiveIds.Ebene,
        [659] = InteractiveIds.Orme,
        [654] = InteractiveIds.Erable,
        [658] = InteractiveIds.Charme,
        [651] = InteractiveIds.Chataignier,
        [652] = InteractiveIds.Noyer,

        // Pecheur
        [1018] = InteractiveIds.PetitsPoissonsDeRiviere,
        [1019] = InteractiveIds.PoissonsDeRiviere,
        [1021] = InteractiveIds.PoissonsDeRiviereGeants,

        // Mineur
        [1081] = InteractiveIds.Fer,
        [2204] = InteractiveIds.Fer,
        [4918] = InteractiveIds.Fer,
        [1075] = InteractiveIds.PierreCuivree,
        [1080] = InteractiveIds.PierreCuivree,
        [2200] = InteractiveIds.PierreCuivree,
        [4920] = InteractiveIds.PierreCuivree,
        [1074] = InteractiveIds.Bronze,
        [2188] = InteractiveIds.Bronze,
        [4921] = InteractiveIds.Bronze,
        [1063] = InteractiveIds.PierreDeKobalte,
        [2205] = InteractiveIds.PierreDeKobalte,
        [4922] = InteractiveIds.PierreDeKobalte,
        [1072] = InteractiveIds.Argent,
        [2186] = InteractiveIds.Argent,
        [1079] = InteractiveIds.Or,
        [2208] = InteractiveIds.Or,
        [1073] = InteractiveIds.PierreDeBauxite,
        [2187] = InteractiveIds.PierreDeBauxite,
        [1077] = InteractiveIds.Etain,
        [2203] = InteractiveIds.Etain,
        [1078] = InteractiveIds.Manganese,
        [2209] = InteractiveIds.Manganese,
        [2206] = InteractiveIds.Manganese,
        [1076] = InteractiveIds.Silicate,
        [2202] = InteractiveIds.Silicate,
        [4919] = InteractiveIds.Silicate,
        [1290] = InteractiveIds.Obsidienne,
        [2207] = InteractiveIds.Obsidienne,

        // Zaaps & Zaapis
        [5247] = InteractiveIds.Zaap,
        [24193] = InteractiveIds.Zaap,
        [21830] = InteractiveIds.Zaap,
        [37410] = InteractiveIds.Zaap,
        [37411] = InteractiveIds.Zaap,
        [38001] = InteractiveIds.Zaap,
        [38002] = InteractiveIds.Zaap,
        [38003] = InteractiveIds.Zaap
    }.ToFrozenDictionary();

    private readonly Dictionary<int, IdentifiableElement[]> _elements;
    private readonly List<MapRecord> _maps;
    private readonly List<InteractiveRecord> _interactives;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapSeeder"/> class.
    /// </summary>
    /// <param name="datacenterService">The service for accessing datacenter data.</param>
    /// <param name="authDbContext">The authentication database context.</param>
    /// <param name="gameDbContext">The game database context.</param>
    public MapSeeder(IDatacenterService datacenterService, AuthDbContext authDbContext, GameDbContext gameDbContext)
        : base(datacenterService, authDbContext, gameDbContext)
    {
        _elements = [];
        _maps = [];
        _interactives = [];
    }

    /// <summary>
    /// Seeds the map and interactive data into the database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public override async Task SeedAsync(CancellationToken cancellationToken)
    {
        await GameDbContext.Maps.ExecuteDeleteAsync(cancellationToken);
        await GameDbContext.Interactives.ExecuteDeleteAsync(cancellationToken);

        var d2PFile = DatacenterService.GetMaps();
        var eleFile = DatacenterService.GetEle();

        var skills = DatacenterService.GetObjects<Skill>();

        foreach (var mapPosition in DatacenterService.GetObjects<MapPosition>())
            GenerateMap(d2PFile, mapPosition, eleFile);

        var elements = _elements.Values
            .SelectMany(static x => x)
            .GroupBy(static x => x.Element.Identifier)
            .ToDictionary(static x => x.Key, static x => x.ToArray());

        foreach (var (_, value) in elements)
        {
            var matchedElement = value
                .OrderByDescending(static x => DistanceFromBorder(new MapPoint(x.Element.Cell.CellId)))
                .ThenBy(static x => Math.Abs(x.Element.PixelOffset.X) + Math.Abs(x.Element.PixelOffset.Y))
                .First();

            var baseElement = value.First();

            _interactives.Add(new InteractiveRecord
            {
                Id = (int)baseElement.Element.Identifier,
                GfxId = baseElement.GfxId,
                Animated = baseElement.Animated,
                ElementId = (int)baseElement.Element.ElementId,
                MapId = (int)matchedElement.Map.Id,
                MapsData = value.Select(x => new InteractiveMapData
                {
                    MapId = (int)x.Map.Id,
                    OnMap = matchedElement.Map.Id == x.Map.Id,
                    CellId = x.Element.Cell.CellId
                }).ToArray(),
                InteractiveActions = []
            });
        }

        foreach (var interactive in _interactives)
        {
            foreach (var _ in interactive.MapsData.Where(static x => x.OnMap))
            {
                if (s_gfxIdToInteractiveId.TryGetValue(interactive.GfxId, out var interactiveId))
                {
                    foreach (var skill in skills.Where(x => x.InteractiveId == (int)interactiveId && x.ClientDisplay))
                    {
                        interactive.InteractiveActions.Add(new InteractiveActionRecord
                        {
                            InteractiveId = interactive.Id,
                            InteractiveTemplateId = (int)interactiveId,
                            SkillId = skill.Id,
                            Type = GenerateTypeBasedOnSkillId(skill),
                            Parameters = [skill.GatheredRessourceItem.ToString(), skill.Id.ToString()]
                        });
                    }
                }
            }
        }

        GameDbContext.Maps.AddRange(_maps);
        GameDbContext.Interactives.AddRange(_interactives);

        await GameDbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Generates a map record and its associated interactives.
    /// </summary>
    /// <param name="d2PFile">The D2P file containing map data.</param>
    /// <param name="mapPosition">The position of the map in the datacenter.</param>
    /// <param name="eleFile">The ELE file containing graphical element data.</param>
    private void GenerateMap(D2PFile d2PFile, MapPosition mapPosition, GraphicalElementFile eleFile)
    {
        if (!d2PFile.TryExtractMap(mapPosition.Id, out var map))
            return;

        var mapRecord = new MapRecord
        {
            Id = mapPosition.Id,
            X = mapPosition.PosX,
            Y = mapPosition.PosY,
            Outdoor = mapPosition.Outdoor,
            Capabilities = (MapPositionFlags)mapPosition.Capabilities,
            SubAreaId = mapPosition.SubAreaId,
            WorldMap = mapPosition.WorldMap,
            HasPriorityOnWorldMap = mapPosition.HasPriorityOnWorldmap,
            PrismAllowed = true,
            PvpDisabled = false,
            PlacementGenDisabled = map.Cells.Any(static x => x.Blue),
            MerchantsMax = 5,
            SpawnDisabled = false,
            RedCells = map.Cells.Where(static x => x.Red).Select(static x => x.Id).ToArray(),
            BlueCells = map.Cells.Where(static x => x.Blue).Select(static x => x.Id).ToArray(),
            Cells = map.Cells.Select(static x => new CellData
            {
                Id = x.Id,
                MapChangeData = x.MapChangeData,
                Floor = (byte)x.Floor,
                LosMov = x.LosMov,
                MoveZone = x.MoveZone,
                Speed = x.Speed
            }).ToArray(),
            Interactives = [],
            BottomNeighbourId = map.BottomNeighbourId,
            TopNeighbourId = map.TopNeighbourId,
            LeftNeighbourId = map.LeftNeighbourId,
            RightNeighbourId = map.RightNeighbourId
        };

        _maps.Add(mapRecord);

        GenerateInteractives(map, eleFile);
    }

    /// <summary>
    /// Generates interactive elements for a given map.
    /// </summary>
    /// <param name="map">The map containing the elements.</param>
    /// <param name="eleFile">The ELE file containing graphical element data.</param>
    private void GenerateInteractives(DlmMap map, GraphicalElementFile eleFile)
    {
        var elements = map.Layers
            .SelectMany(static layer => layer.Cells)
            .SelectMany(static cell => cell.Elements.OfType<DlmGraphicalElement>().Where(static element => element.Identifier is not 0))
            .Select(element => new IdentifiableElement { Element = element, Map = map })
            .ToArray();

        _elements.Add((int)map.Id, elements);

        foreach (var element in elements)
        {
            var eleElement = eleFile.GraphicalElements.Values.FirstOrDefault(x => x.Id == element.Element.ElementId);

            if (eleElement is null)
                continue;

            element.GfxId = GetGfxId(eleElement);
            element.Animated = eleElement is AnimatedGraphicalElementData or EntityGraphicalElementData;
        }
    }

    /// <summary>
    /// Retrieves the GFX ID for a given graphical element.
    /// </summary>
    /// <param name="element">The graphical element.</param>
    /// <returns>The GFX ID of the element, or -1 if not found.</returns>
    private static int GetGfxId(GraphicalElementData element)
    {
        switch (element)
        {
            case EntityGraphicalElementData entity:
                var cleanedLook = entity.EntityLook.Replace("{", string.Empty).Replace("}", string.Empty);

                if (cleanedLook.Contains(','))
                    cleanedLook = cleanedLook.Split(',')[0];

                return int.TryParse(cleanedLook, out var id) ? id : -1;

            case NormalGraphicalElementData normal:
                return normal.GfxId;

            default:
                return -1;
        }
    }

    /// <summary>
    /// Generates a game action type based on the provided skill's ID.
    /// </summary>
    /// <param name="skill">The skill for which to determine the game action type.</param>
    /// <returns>
    /// A <see cref="GameActionTypes"/> value representing the type of action associated with the skill.
    /// </returns>
    private static GameActionTypes GenerateTypeBasedOnSkillId(Skill skill)
    {
        if (skill.GatheredRessourceItem is not -1)
            return GameActionTypes.Gathering;

        return (SkillIds)skill.Id switch
        {
            SkillIds.BriserDesObjets => GameActionTypes.BreakItems,
            SkillIds.FusionnerDesRessources => GameActionTypes.OpenCraft,
            SkillIds.Costumager => GameActionTypes.OpenSmithMagic,
            SkillIds.Joaillomager => GameActionTypes.OpenSmithMagic,
            SkillIds.Cordomager => GameActionTypes.OpenSmithMagic,
            SkillIds.Sculptemager => GameActionTypes.OpenSmithMagic,
            SkillIds.Forgemager => GameActionTypes.OpenSmithMagic,
            SkillIds.Acceder => GameActionTypes.OpenPaddock,
            _ => GameActionTypes.Unknown
        };
    }

    /// <summary>
    /// Calculates the distance of a point from the map border.
    /// </summary>
    /// <param name="point">The point to calculate the distance for.</param>
    /// <returns>The minimum distance from the point to the map border.</returns>
    private static double DistanceFromBorder(MapPoint point)
    {
        var borders = new[]
        {
            new LineSet(new MapPoint(27), new MapPoint(559)),
            new LineSet(new MapPoint(546), new MapPoint(559)),
            new LineSet(new MapPoint(0), new MapPoint(13)),
            new LineSet(new MapPoint(0), new MapPoint(532)),
        };

        return borders.Min(x => x.SquareDistanceToLine(point));
    }

    /// <summary>
    /// Represents an identifiable graphical element associated with a map.
    /// </summary>
    private sealed class IdentifiableElement
    {
        /// <summary>
        /// Gets or sets the graphical element data.
        /// </summary>
        public required DlmGraphicalElement Element { get; init; }

        /// <summary>
        /// Gets or sets the map associated with the element.
        /// </summary>
        public required DlmMap Map { get; init; }

        /// <summary>
        /// Gets or sets the GFX ID of the element.
        /// </summary>
        public int GfxId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the element is animated.
        /// </summary>
        public bool Animated { get; set; }
    }

    /// <summary>
    /// Represents a line segment defined by two points on a map.
    /// </summary>
    private sealed class LineSet
    {
        /// <summary>
        /// Gets the starting point of the line segment.
        /// </summary>
        public MapPoint? A { get; }

        /// <summary>
        /// Gets the ending point of the line segment.
        /// </summary>
        public MapPoint? B { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSet"/> class with the specified points.
        /// </summary>
        /// <param name="a">The starting point of the line segment.</param>
        /// <param name="b">The ending point of the line segment.</param>
        public LineSet(MapPoint a, MapPoint b)
        {
            A = a;
            B = b;
        }

        /// <summary>
        /// Calculates the square of the distance from a given point to the line segment.
        /// </summary>
        /// <param name="point">The point for which to calculate the distance.</param>
        /// <returns>
        /// The square of the perpendicular distance from the point to the line segment,
        /// or <c>0</c> if either endpoint of the line segment is <c>null</c>.
        /// </returns>
        public double SquareDistanceToLine(MapPoint point)
        {
            if (A is null || B is null)
                return 0;

            double dx = B.X - A.X;
            double dy = B.Y - A.Y;

            var projection = dy * point.X - dx * point.Y + B.X * A.Y - B.Y * A.X;

            return projection * projection / (dy * dy + dx * dx);
        }
    }
}
