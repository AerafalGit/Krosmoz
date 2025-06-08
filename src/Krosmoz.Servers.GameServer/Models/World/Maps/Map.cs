// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Krosmoz.Protocol.Constants;
using Krosmoz.Protocol.Datacenter.World;
using Krosmoz.Protocol.Messages.Game.Context.Roleplay;
using Krosmoz.Protocol.Types.Game.Context.Fight;
using Krosmoz.Protocol.Types.Game.House;
using Krosmoz.Protocol.Types.Game.Interactive;
using Krosmoz.Servers.GameServer.Database.Models.Maps;
using Krosmoz.Servers.GameServer.Models.Actors;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;
using Krosmoz.Servers.GameServer.Models.World.Cells;
using Krosmoz.Servers.GameServer.Models.World.Interactives;

namespace Krosmoz.Servers.GameServer.Models.World.Maps;

/// <summary>
/// Represents a map in the game world.
/// </summary>
public sealed class Map
{
    /// <summary>
    /// Gets the record containing map data from the database.
    /// </summary>
    private MapRecord Record { get; }

    /// <summary>
    /// Gets the array of free cells on the map.
    /// </summary>
    private Cell[] FreeCells { get; }

    /// <summary>
    /// Gets the dictionary of interactive elements on the map, indexed by their IDs.
    /// </summary>
    private Dictionary<int, InteractiveWrapper> Interactives { get; }

    /// <summary>
    /// Gets the dictionary of actors on the map, indexed by their IDs.
    /// </summary>
    private Dictionary<int, Actor> Actors { get; }

    /// <summary>
    /// Gets the dictionary of characters on the map, indexed by their IDs.
    /// </summary>
    private ConcurrentDictionary<int, CharacterActor> Characters { get; }

    /// <summary>
    /// Gets the area associated with the map.
    /// </summary>
    public Area Area { get; }

    /// <summary>
    /// Gets the sub-area associated with the map.
    /// </summary>
    public SubArea SubArea { get; }

    /// <summary>
    /// Gets the dictionary of cells on the map, indexed by their IDs.
    /// </summary>
    public Dictionary<short, Cell> Cells { get; }

    /// <summary>
    /// Gets the array of blue cells on the map.
    /// </summary>
    public Cell[] BlueCells { get; }

    /// <summary>
    /// Gets the array of red cells on the map.
    /// </summary>
    public Cell[] RedCells { get; }

    /// <summary>
    /// Gets the unique identifier of the map.
    /// </summary>
    public int Id =>
        Record.Id;

    /// <summary>
    /// Gets the X coordinate of the map.
    /// </summary>
    public int X =>
        Record.X;

    /// <summary>
    /// Gets the Y coordinate of the map.
    /// </summary>
    public int Y =>
        Record.Y;

    /// <summary>
    /// Gets a value indicating whether the map is outdoors.
    /// </summary>
    public bool Outdoor =>
        Record.Outdoor;

    /// <summary>
    /// Gets the capabilities of the map, represented as flags.
    /// </summary>
    public MapPositionFlags Capabilities =>
        Record.Capabilities;

    /// <summary>
    /// Gets the ID of the world map associated with this map.
    /// </summary>
    public int WorldMap =>
        Record.WorldMap;

    /// <summary>
    /// Gets a value indicating whether the map has priority on the world map.
    /// </summary>
    public bool HasPriorityOnWorldMap =>
        Record.HasPriorityOnWorldMap;

    /// <summary>
    /// Gets a value indicating whether prisms are allowed on the map.
    /// </summary>
    public bool PrismAllowed =>
        Record.PrismAllowed;

    /// <summary>
    /// Gets a value indicating whether PvP is disabled on the map.
    /// </summary>
    public bool PvpDisabled =>
        Record.PvpDisabled;

    /// <summary>
    /// Gets a value indicating whether placement generation is disabled on the map.
    /// </summary>
    public bool PlacementGenDisabled =>
        Record.PlacementGenDisabled;

    /// <summary>
    /// Gets the maximum number of merchants allowed on the map.
    /// </summary>
    public int MerchantsMax =>
        Record.MerchantsMax;

    /// <summary>
    /// Gets a value indicating whether spawning is disabled on the map.
    /// </summary>
    public bool SpawnDisabled =>
        Record.SpawnDisabled;

    /// <summary>
    /// Gets the ID of the top neighboring map.
    /// </summary>
    public int TopNeighbourId =>
        Record.TopNeighbourId;

    /// <summary>
    /// Gets the ID of the bottom neighboring map.
    /// </summary>
    public int BottomNeighbourId =>
        Record.BottomNeighbourId;

    /// <summary>
    /// Gets the ID of the left neighboring map.
    /// </summary>
    public int LeftNeighbourId =>
        Record.LeftNeighbourId;

    /// <summary>
    /// Gets the ID of the right neighboring map.
    /// </summary>
    public int RightNeighbourId =>
        Record.RightNeighbourId;

    /// <summary>
    /// Gets the ID of the top cell on the map, if available.
    /// </summary>
    public short? TopCellId =>
        Record.TopCellId;

    /// <summary>
    /// Gets the ID of the bottom cell on the map, if available.
    /// </summary>
    public short? BottomCellId =>
        Record.BottomCellId;

    /// <summary>
    /// Gets the ID of the left cell on the map, if available.
    /// </summary>
    public short? LeftCellId =>
        Record.LeftCellId;

    /// <summary>
    /// Gets the ID of the right cell on the map, if available.
    /// </summary>
    public short? RightCellId =>
        Record.RightCellId;

    /// <summary>
    /// Gets a value indicating whether challenges are allowed on the map.
    /// </summary>
    public bool AllowChallenge =>
        (Capabilities & MapPositionFlags.AllowChallenge) is not 0;

    /// <summary>
    /// Gets a value indicating whether aggression is allowed on the map.
    /// </summary>
    public bool AllowAggression =>
        (Capabilities & MapPositionFlags.AllowAggression) is not 0;

    /// <summary>
    /// Gets a value indicating whether teleportation to the map is allowed.
    /// </summary>
    public bool AllowTeleportTo =>
        (Capabilities & MapPositionFlags.AllowTeleportTo) is not 0;

    /// <summary>
    /// Gets a value indicating whether teleportation from the map is allowed.
    /// </summary>
    public bool AllowTeleportFrom =>
        (Capabilities & MapPositionFlags.AllowTeleportFrom) is not 0;

    /// <summary>
    /// Gets a value indicating whether exchanges between players are allowed on the map.
    /// </summary>
    public bool AllowExchangesBetweenPlayers =>
        (Capabilities & MapPositionFlags.AllowExchangesBetweenPlayers) is not 0;

    /// <summary>
    /// Gets a value indicating whether human vendors are allowed on the map.
    /// </summary>
    public bool AllowHumanVendor =>
        (Capabilities & MapPositionFlags.AllowHumanVendor) is not 0;

    /// <summary>
    /// Gets a value indicating whether collectors are allowed on the map.
    /// </summary>
    public bool AllowCollector =>
        (Capabilities & MapPositionFlags.AllowCollector) is not 0;

    /// <summary>
    /// Gets a value indicating whether soul capture is allowed on the map.
    /// </summary>
    public bool AllowSoulCapture =>
        (Capabilities & MapPositionFlags.AllowSoulCapture) is not 0;

    /// <summary>
    /// Gets a value indicating whether soul summoning is allowed on the map.
    /// </summary>
    public bool AllowSoulSummon =>
        (Capabilities & MapPositionFlags.AllowSoulSummon) is not 0;

    /// <summary>
    /// Gets a value indicating whether tavern regeneration is allowed on the map.
    /// </summary>
    public bool AllowTavernRegen =>
        (Capabilities & MapPositionFlags.AllowTavernRegen) is not 0;

    /// <summary>
    /// Gets a value indicating whether tomb mode is allowed on the map.
    /// </summary>
    public bool AllowTombMode =>
        (Capabilities & MapPositionFlags.AllowTombMode) is not 0;

    /// <summary>
    /// Gets a value indicating whether teleportation everywhere is allowed on the map.
    /// </summary>
    public bool AllowTeleportEverywhere =>
        (Capabilities & MapPositionFlags.AllowTeleportEverywhere) is not 0;

    /// <summary>
    /// Gets a value indicating whether fight challenges are allowed on the map.
    /// </summary>
    public bool AllowFightChallenges =>
        (Capabilities & MapPositionFlags.AllowFightChallenges) is not 0;

    /// <summary>
    /// Determines whether the map contains a Zaap interactive element.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the map contains a Zaap; otherwise, <c>false</c>.
    /// </returns>
    [MemberNotNullWhen(true, nameof(Zaap))]
    public bool ContainsZaap =>
        Zaap is not null;

    /// <summary>
    /// Determines whether the map contains a Zaapi interactive element.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the map contains a Zaapi; otherwise, <c>false</c>.
    /// </returns>
    [MemberNotNullWhen(true, nameof(Zaapi))]
    public bool ContainsZaapi =>
        Zaapi is not null;

    /// <summary>
    /// Determines whether the map contains a Paddock interactive element.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the map contains a Paddock; otherwise, <c>false</c>.
    /// </returns>
    [MemberNotNullWhen(true, nameof(Paddock))]
    public bool ContainsPaddock =>
        Paddock is not null;

    /// <summary>
    /// Gets or sets the interactive wrapper for the Zaap element on the map.
    /// </summary>
    public InteractiveWrapper? Zaap { get; set; }

    /// <summary>
    /// Gets or sets the interactive wrapper for the Zaapi element on the map.
    /// </summary>
    public InteractiveWrapper? Zaapi { get; set; }

    /// <summary>
    /// Gets or sets the interactive wrapper for the Paddock element on the map.
    /// </summary>
    public InteractiveWrapper? Paddock { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Map"/> class.
    /// </summary>
    /// <param name="record">The map record containing database data.</param>
    /// <param name="area">The area associated with the map.</param>
    /// <param name="subArea">The sub-area associated with the map.</param>
    public Map(MapRecord record, Area area, SubArea subArea)
    {
        Record = record;
        Area = area;
        SubArea = subArea;
        Cells = Record.Cells.ToDictionary(static x => x.Id, static x => x.ToCell());
        BlueCells = Cells.Values.Where(static x => x.Blue).ToArray();
        RedCells = Cells.Values.Where(static x => x.Red).ToArray();
        FreeCells = Cells.Values
            .Where(x => IsWalkableCell(x) && !x.FarmCell)
            .OrderBy(static x => MapPoint.Middle.ManhattanDistanceTo(MapPoint.Points[x.Id]))
            .ToArray();

        Interactives = [];
        Actors = [];
        Characters = [];

        foreach (var interactive in record.Interactives)
        {
            foreach (var interactiveMapData in interactive.MapsData)
            {
                if (Interactives.ContainsKey(interactive.Id))
                    continue;

                Interactives.Add(interactive.Id, new InteractiveWrapper(interactive, this, interactiveMapData, interactive.InteractiveActions.ToList()));
            }
        }
    }

    /// <summary>
    /// Retrieves a cell by its ID.
    /// </summary>
    /// <param name="cellId">The ID of the cell to retrieve.</param>
    /// <returns>The cell with the specified ID.</returns>
    public Cell GetCell(short cellId)
    {
        EnsureValidCellId(cellId);

        return Cells[cellId];
    }

    /// <summary>
    /// Retrieves a cell by its map point.
    /// </summary>
    /// <param name="point">The map point of the cell to retrieve.</param>
    /// <returns>The cell at the specified map point.</returns>
    public Cell GetCell(MapPoint point)
    {
        EnsureValidCellId(point.CellId);

        return Cells[point.CellId];
    }

    /// <summary>
    /// Determines whether a cell is walkable by its ID.
    /// </summary>
    /// <param name="cellId">The ID of the cell to check.</param>
    /// <returns><c>true</c> if the cell is walkable; otherwise, <c>false</c>.</returns>
    public bool IsWalkableCell(short cellId)
    {
        EnsureValidCellId(cellId);

        return Cells[cellId].Mov;
    }

    /// <summary>
    /// Determines whether a cell is walkable.
    /// </summary>
    /// <param name="cell">The cell to check.</param>
    /// <returns><c>true</c> if the cell is walkable; otherwise, <c>false</c>.</returns>
    public bool IsWalkableCell(Cell cell)
    {
        return cell.Mov;
    }

    /// <summary>
    /// Determines whether a cell is walkable during a fight by its ID.
    /// </summary>
    /// <param name="cellId">The ID of the cell to check.</param>
    /// <returns><c>true</c> if the cell is walkable during a fight; otherwise, <c>false</c>.</returns>
    public bool IsWalkableCellDuringFight(short cellId)
    {
        EnsureValidCellId(cellId);

        return Cells[cellId].Mov && Cells[cellId].NonWalkableDuringFight;
    }

    /// <summary>
    /// Determines whether a cell is walkable during a fight.
    /// </summary>
    /// <param name="cell">The cell to check.</param>
    /// <returns><c>true</c> if the cell is walkable during a fight; otherwise, <c>false</c>.</returns>
    public bool IsWalkableCellDuringFight(Cell cell)
    {
        return cell is { Mov: true, NonWalkableDuringFight: true };
    }

    /// <summary>
    /// Retrieves a random free cell from the map.
    /// </summary>
    /// <returns>A <see cref="Cell"/> object representing a random free cell on the map.</returns>
    /// <exception cref="InvalidOperationException">Thrown if there are no free cells available on the map.</exception>
    public Cell GetRandomFreeCell()
    {
        if (FreeCells.Length == 0)
            throw new InvalidOperationException("No free cells available on the map.");

        return FreeCells[Random.Shared.Next(FreeCells.Length)];
    }

    /// <summary>
    /// Retrieves a walkable cell by its ID or returns the first free cell if the specified cell is not walkable.
    /// </summary>
    /// <param name="cellId">The ID of the cell to retrieve.</param>
    /// <returns>
    /// The cell with the specified ID if it is walkable; otherwise, the first free cell on the map.
    /// </returns>
    public Cell GetCellWalkableOrDefault(short cellId)
    {
        var cell = GetCell(cellId);

        return IsWalkableCell(cell) ? cell : FreeCells[0];
    }

    /// <summary>
    /// Adds an actor to the map. If the actor is a character, it is also added to the characters dictionary.
    /// </summary>
    /// <param name="actor">The actor to add to the map.</param>
    public void AddActor(Actor actor)
    {
        if (actor is CharacterActor character)
            Characters.TryAdd(character.Id, character);

        Actors.Add(actor.Id, actor);
    }

    /// <summary>
    /// Removes an actor from the map. If the actor is a character, it is also removed from the characters dictionary.
    /// </summary>
    /// <param name="actor">The actor to remove from the map.</param>
    public void RemoveActor(Actor actor)
    {
        if (actor is CharacterActor character)
            Characters.Remove(character.Id, out _);

        Actors.Remove(actor.Id);
    }

    /// <summary>
    /// Retrieves an actor from the map by its unique identifier.
    /// </summary>
    /// <param name="actorId">The unique identifier of the actor to retrieve.</param>
    /// <returns>The actor with the specified identifier, or <c>null</c> if not found.</returns>
    public Actor? GetActor(int actorId)
    {
        return Actors.GetValueOrDefault(actorId);
    }

    /// <summary>
    /// Attempts to retrieve an actor from the map by its unique identifier.
    /// </summary>
    /// <param name="actorId">The unique identifier of the actor to retrieve.</param>
    /// <param name="actor">
    /// When this method returns, contains the actor with the specified identifier if found; otherwise, <c>null</c>.
    /// </param>
    /// <returns><c>true</c> if the actor was found; otherwise, <c>false</c>.</returns>
    public bool TryGetActor(int actorId, [NotNullWhen(true)] out Actor? actor)
    {
        return Actors.TryGetValue(actorId, out actor);
    }

    /// <summary>
    /// Retrieves all character actors present on the map.
    /// </summary>
    /// <returns>An enumerable collection of character actors.</returns>
    public IEnumerable<CharacterActor> GetCharacters()
    {
        return Characters.Values;
    }

    /// <summary>
    /// Retrieves all interactive elements present on the map.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="InteractiveWrapper"/> objects representing the interactive elements.</returns>
    public IEnumerable<InteractiveWrapper> GetInteractives()
    {
        return Interactives.Values;
    }

    /// <summary>
    /// Creates a message containing complementary information about the map.
    /// </summary>
    /// <returns>
    /// A <see cref="MapComplementaryInformationsDataMessage"/> object containing details about the map,
    /// including its ID, sub-area ID, actors, fights, houses, interactive elements, obstacles, and stated elements.
    /// </returns>
    public MapComplementaryInformationsDataMessage GetMapComplementaryInformationsDataMessage(CharacterActor character)
    {
        return new MapComplementaryInformationsDataMessage
        {
            MapId = Id,
            SubAreaId = (ushort)SubArea.Id,
            Actors = Actors.Values.Select(static x => x.GetGameRolePlayActorInformations()),
            Fights = GetFightsInformations(),
            Houses = GetHousesInformations(),
            InteractiveElements = GetInteractiveElements(character),
            Obstacles = GetObstaclesInformations(),
            StatedElements = GetStatedElements()
        };
    }

    /// <summary>
    /// Retrieves fight information for the map.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{FightCommonInformations}"/> containing fight details.
    /// Currently, it returns an empty list as a placeholder.
    /// </returns>
    private IEnumerable<FightCommonInformations> GetFightsInformations()
    {
        // This method should return fight information for the map.
        // Currently, it returns an empty list as a placeholder.
        return [];
    }

    /// <summary>
    /// Retrieves house information for the map.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{HouseInformations}"/> containing house details.
    /// Currently, it returns an empty list as a placeholder.
    /// </returns>
    private IEnumerable<HouseInformations> GetHousesInformations()
    {
        // This method should return house information for the map.
        // Currently, it returns an empty list as a placeholder.
        return [];
    }

    /// <summary>
    /// Retrieves obstacle information for the map.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{MapObstacle}"/> containing obstacle details.
    /// Currently, it returns an empty list as a placeholder.
    /// </returns>
    private IEnumerable<MapObstacle> GetObstaclesInformations()
    {
        // This method should return obstacle information for the map.
        // Currently, it returns an empty list as a placeholder.
        return [];
    }

    /// <summary>
    /// Retrieves interactive elements for the map based on the specified character.
    /// </summary>
    /// <param name="character">The character interacting with the elements.</param>
    /// <returns>
    /// An <see cref="IEnumerable{InteractiveElement}"/> containing interactive element details.
    /// </returns>
    private IEnumerable<InteractiveElement> GetInteractiveElements(CharacterActor character)
    {
        return Interactives.Values
            //.Where(x => x.Actions.Count > 0 && x.Actions.Any(y => y.Action is null || y.Action.CanBeExecuted(character)))
            .Select(x => x.GetInteractiveElement(character));
    }

    /// <summary>
    /// Retrieves stated elements for the map.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{StatedElement}"/> containing stated element details.
    /// </returns>
    private IEnumerable<StatedElement> GetStatedElements()
    {
        return Interactives.Values.Where(x => x.Animated).Select(x => x.GetStatedElement());
    }

    /// <summary>
    /// Ensures that the specified cell ID is valid.
    /// </summary>
    /// <param name="cellId">The cell ID to validate.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the cell ID is invalid.</exception>
    private static void EnsureValidCellId(short cellId)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(cellId);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(cellId, (short)AtouinConstants.MapCellsCount);
    }
}
