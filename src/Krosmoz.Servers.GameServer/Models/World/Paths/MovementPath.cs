// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Constants;
using Krosmoz.Protocol.Enums;
using Krosmoz.Servers.GameServer.Models.World.Cells;
using Krosmoz.Servers.GameServer.Models.World.Maps;

namespace Krosmoz.Servers.GameServer.Models.World.Paths;

/// <summary>
/// Represents a movement path within a game world, including its cells, positions, and associated metadata.
/// </summary>
public sealed class MovementPath
{
    private Cell[] _cellsPath;
    private WorldPosition[]? _compressedPath;
    private WorldPosition? _endPathPosition;
    private MapPoint[] _path;

    /// <summary>
    /// Gets the map associated with this movement path.
    /// </summary>
    private Map Map { get; }

    /// <summary>
    /// Gets the starting cell of the movement path.
    /// </summary>
    public Cell StartCell =>
        _cellsPath[0];

    /// <summary>
    /// Gets the ending cell of the movement path.
    /// </summary>
    public Cell EndCell =>
        _cellsPath[^1];

    /// <summary>
    /// Gets the world position of the end of the path, including its direction.
    /// </summary>
    public WorldPosition EndPathPosition =>
        _endPathPosition ??= new WorldPosition(Map, EndCell, GetEndCellDirection());

    /// <summary>
    /// Gets the movement point (MP) cost of the path.
    /// </summary>
    public int MpCost =>
        _cellsPath.Length - 1;

    /// <summary>
    /// Gets or sets the cells that make up the movement path.
    /// Setting this property recalculates the path and resets the end path position.
    /// </summary>
    public Cell[] Cells
    {
        get => _cellsPath;
        set
        {
            _cellsPath = value;
            _endPathPosition = null;
            _path = _cellsPath.Select(static x => new MapPoint(x)).ToArray();
        }
    }

    /// <summary>
    /// Gets a value indicating whether the path is empty.
    /// </summary>
    public bool IsEmpty =>
        _cellsPath.Length is 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="MovementPath"/> class with a map and a sequence of cells.
    /// </summary>
    /// <param name="map">The map associated with the path.</param>
    /// <param name="path">The sequence of cells that make up the path.</param>
    public MovementPath(Map map, IEnumerable<Cell> path)
    {
        Map = map;
        _cellsPath = path.ToArray();
        _path = _cellsPath.Select(static x => MapPoint.GetPoint(x)!).ToArray();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MovementPath"/> class with a map and a compressed path.
    /// </summary>
    /// <param name="map">The map associated with the path.</param>
    /// <param name="compressedPath">The compressed path represented as world positions.</param>
    private MovementPath(Map map, IEnumerable<WorldPosition> compressedPath)
    {
        Map = map;
        _compressedPath = compressedPath.ToArray();
        _cellsPath = BuildCompletePath();
        _path = _cellsPath.Select(static x => MapPoint.GetPoint(x)!).ToArray();
    }

    /// <summary>
    /// Determines the direction of the end cell in the path.
    /// </summary>
    /// <returns>The direction of the end cell.</returns>
    private Directions GetEndCellDirection()
    {
        if (_cellsPath.Length <= 1)
            return Directions.East;

        return _compressedPath is not null
            ? _compressedPath.Last().Orientation
            : _path[^2].OrientationToAdjacent(_path[^1]);
    }

    /// <summary>
    /// Gets the compressed representation of the movement path.
    /// </summary>
    /// <returns>An array of <see cref="WorldPosition"/> representing the compressed path.</returns>
    public WorldPosition[] GetCompressedPath()
    {
        return _compressedPath ??= BuildCompressedPath();
    }

    /// <summary>
    /// Gets the sequence of cells that make up the movement path.
    /// </summary>
    /// <returns>An enumerable of <see cref="Cell"/> objects.</returns>
    public IEnumerable<Cell> GetPath()
    {
        return _cellsPath;
    }

    /// <summary>
    /// Determines whether the path contains a specific cell by its ID.
    /// </summary>
    /// <param name="cellId">The ID of the cell to check.</param>
    /// <returns>True if the cell is in the path; otherwise, false.</returns>
    public bool Contains(short cellId)
    {
        return _cellsPath.Any(x => x.Id == cellId);
    }

    /// <summary>
    /// Gets the server path keys for the movement path.
    /// </summary>
    /// <returns>An enumerable of cell IDs representing the server path keys.</returns>
    public IEnumerable<short> GetServerPathKeys()
    {
        return _cellsPath.Select(static x => x.Id);
    }

    /// <summary>
    /// Cuts the path at a specified index, optionally skipping the index.
    /// </summary>
    /// <param name="index">The index at which to cut the path.</param>
    /// <param name="skip">Whether to skip the index when cutting.</param>
    public void CutPath(int index, bool skip = false)
    {
        if (index >= _cellsPath.Length || index < 0)
            return;

        _cellsPath = skip ? _cellsPath.Skip(index).ToArray() : _cellsPath.Take(index).ToArray();

        _path = _cellsPath.Select(static x => new MapPoint(x)).ToArray();

        _endPathPosition = new WorldPosition(Map, EndCell, GetEndCellDirection());
    }

    /// <summary>
    /// Builds the compressed representation of the movement path.
    /// </summary>
    /// <returns>An array of <see cref="WorldPosition"/> representing the compressed path.</returns>
    private WorldPosition[] BuildCompressedPath()
    {
        switch (_cellsPath.Length)
        {
            case <= 0:
                return [];
            case <= 1:
                return [new WorldPosition(Map, _cellsPath[0], Directions.East)];
        }

        var path = new List<WorldPosition>();

        for (var i = 1; i < _cellsPath.Length; i++)
            path.Add(new WorldPosition(Map, _cellsPath[i - 1], _path[i - 1].OrientationToAdjacent(_path[i])));

        path.Add(new WorldPosition(Map, _cellsPath[^1], path[^1].Orientation));

        if (path.Count <= 0)
            return path.ToArray();

        var i2 = path.Count - 2;

        while (i2 > 0)
        {
            if (path[i2].Orientation == path[i2 - 1].Orientation)
                path.RemoveAt(i2);

            i2--;
        }

        return path.ToArray();
    }

    /// <summary>
    /// Builds the complete path from the compressed path.
    /// </summary>
    /// <returns>An array of <see cref="Cell"/> objects representing the complete path.</returns>
    private Cell[] BuildCompletePath()
    {
        var completePath = new List<Cell>();

        if (_compressedPath is null)
            return [];

        for (var i = 0; i < _compressedPath.Length - 1; i++)
        {
            completePath.Add(_compressedPath[i].Cell);

            var l = 0;
            var nextPoint = MapPoint.GetPoint(_compressedPath[i].Cell);

            while ((nextPoint = nextPoint?.GetNearestCellInDirection(_compressedPath[i].Orientation)) is not null && nextPoint.CellId != _compressedPath[i + 1].Cell.Id)
            {
                if (l > AtouinConstants.MapHeight * 2 + AtouinConstants.MapWidth)
                    throw new Exception("MovementPath too long. Maybe an orientation problem ?");

                var cell = Map.Cells[nextPoint.CellId];

                if (!Map.IsWalkableCell(cell))
                    return completePath.ToArray();

                completePath.Add(cell);

                l++;
            }
        }

        completePath.Add(_compressedPath[^1].Cell);

        return completePath.ToArray();
    }

    /// <summary>
    /// Builds a movement path from a compressed path represented by server path keys.
    /// </summary>
    /// <param name="map">The map associated with the path.</param>
    /// <param name="keys">The server path keys representing the compressed path.</param>
    /// <returns>A new instance of <see cref="MovementPath"/>.</returns>
    public static MovementPath BuildFromCompressedPath(Map map, IEnumerable<short> keys)
    {
        var path = new List<WorldPosition>();

        foreach (var key in keys)
        {
            var cellId = (short)(key & 4095);
            var direction = (Directions)((key >> 12) & 7);

            if (map.IsWalkableCell(cellId))
                path.Add(new WorldPosition(map, map.Cells[cellId], direction));
        }

        return new MovementPath(map, path);
    }

    /// <summary>
    /// Creates an empty movement path starting at a specified cell.
    /// </summary>
    /// <param name="map">The map associated with the path.</param>
    /// <param name="startCell">The starting cell of the path.</param>
    /// <returns>A new instance of <see cref="MovementPath"/>.</returns>
    public static MovementPath GetEmptyPath(Map map, Cell startCell)
    {
        return new MovementPath(map, [startCell]);
    }

    /// <summary>
    /// Cancels the path at a specified cell, cutting it at that point.
    /// </summary>
    /// <param name="cell">The cell at which to cancel the path.</param>
    public void CancelAt(Cell cell)
    {
        if (cell.Id == EndCell.Id)
            return;

        var index = Array.FindIndex(Cells, entry => entry.Id == cell.Id);

        if (index is -1)
            return;

        CutPath(index + 1);
    }
}
