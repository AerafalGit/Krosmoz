// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Drawing;
using Krosmoz.Protocol.Constants;
using Krosmoz.Protocol.Enums;

namespace Krosmoz.Servers.GameServer.Models.World.Cells;

/// <summary>
/// Represents a point on the map.
/// Provides various utility methods for map-related calculations.
/// </summary>
public sealed class MapPoint
{
    private static readonly Point s_vectorRight = new(1, 1);
    private static readonly Point s_vectorDownRight = new(1, 0);
    private static readonly Point s_vectorDown = new(1, -1);
    private static readonly Point s_vectorDownLeft = new(0, -1);
    private static readonly Point s_vectorLeft = new(-1, -1);
    private static readonly Point s_vectorUpLeft = new(-1, 0);
    private static readonly Point s_vectorUp = new(-1, 1);
    private static readonly Point s_vectorUpRight = new(0, 1);
    private static readonly MapPoint[] s_orthogonalGridReference = new MapPoint[AtouinConstants.MapCellsCount];

    private static bool s_initialized;

    /// <summary>
    /// Gets the middle point of the map.
    /// </summary>
    public static MapPoint Middle =>
        Points[315];

    /// <summary>
    /// Gets all points on the map.
    /// </summary>
    public static MapPoint[] Points { get; }

    /// <summary>
    /// Static constructor to initialize map points.
    /// </summary>
    static MapPoint()
    {
        Points = new MapPoint[AtouinConstants.MapCellsCount];

        for (short i = 0; i < Points.Length; i++)
            Points[i] = new MapPoint(i);
    }

    /// <summary>
    /// Gets or sets the cell ID of the map point.
    /// </summary>
    public short CellId { get; private set; }

    /// <summary>
    /// Gets or sets the X coordinate of the map point.
    /// </summary>
    public int X { get; private set; }

    /// <summary>
    /// Gets or sets the Y coordinate of the map point.
    /// </summary>
    public int Y { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MapPoint"/> class with a cell ID.
    /// </summary>
    /// <param name="cellId">The cell ID of the map point.</param>
    public MapPoint(short cellId)
    {
        CellId = cellId;
        SetFromCellId();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MapPoint"/> class with a <see cref="Cell"/>.
    /// </summary>
    /// <param name="cell">The cell to initialize the map point from.</param>
    public MapPoint(Cell cell)
    {
        CellId = cell.Id;
        SetFromCellId();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MapPoint"/> class with X and Y coordinates.
    /// </summary>
    /// <param name="x">The X coordinate of the map point.</param>
    /// <param name="y">The Y coordinate of the map point.</param>
    public MapPoint(int x, int y)
    {
        X = x;
        Y = y;
        SetFromCoords();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MapPoint"/> class with a <see cref="Point"/>.
    /// </summary>
    /// <param name="point">The point to initialize the map point from.</param>
    public MapPoint(Point point)
    {
        X = point.X;
        Y = point.Y;
        SetFromCoords();
    }

    /// <summary>
    /// Sets the cell ID based on the current coordinates.
    /// </summary>
    private void SetFromCoords()
    {
        if (!s_initialized)
            InitializeStaticGrid();

        CellId = (short)((X - Y) * AtouinConstants.MapWidth + Y + (X - Y) / 2);
    }

    /// <summary>
    /// Sets the coordinates based on the current cell ID.
    /// </summary>
    private void SetFromCellId()
    {
        if (!s_initialized)
            InitializeStaticGrid();

        ArgumentOutOfRangeException.ThrowIfNegative(CellId);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(CellId, (short)AtouinConstants.MapCellsCount);

        (X, Y) = s_orthogonalGridReference[CellId];
    }

    /// <summary>
    /// Calculates the Euclidean distance to another map point.
    /// </summary>
    /// <param name="point">The target map point.</param>
    /// <returns>The Euclidean distance.</returns>
    public uint EuclideanDistanceTo(MapPoint point)
    {
        return (uint)Math.Sqrt((point.X - X) * (point.X - X) + (point.Y - Y) * (point.Y - Y));
    }

    /// <summary>
    /// Calculates the Manhattan distance to another map point.
    /// </summary>
    /// <param name="point">The target map point.</param>
    /// <returns>The Manhattan distance.</returns>
    public uint ManhattanDistanceTo(MapPoint point)
    {
        return (uint)(Math.Abs(X - point.X) + Math.Abs(Y - point.Y));
    }

    /// <summary>
    /// Calculates the adjusted Manhattan distance to another map point.
    /// </summary>
    /// <param name="point">The target map point.</param>
    /// <returns>The adjusted Manhattan distance.</returns>
    public int AdjustedManhattanDistance(MapPoint point)
    {
        var dx = Math.Abs(X - point.X);
        var dy = Math.Abs(Y - point.Y);

        var diagonalSteps = Math.Min(dx, dy);
        var straightSteps = Math.Max(dx, dy) - diagonalSteps;

        return 2 * diagonalSteps + straightSteps;
    }

    /// <summary>
    /// Calculates the Manhattan distance along the X-axis to another map point.
    /// </summary>
    /// <param name="point">The target map point.</param>
    /// <returns>The Manhattan distance along the X-axis.</returns>
    public uint ManhattanDistanceToX(MapPoint point)
    {
        return (uint)Math.Abs(X - point.X);
    }

    /// <summary>
    /// Calculates the Manhattan distance along the Y-axis to another map point.
    /// </summary>
    /// <param name="point">The target map point.</param>
    /// <returns>The Manhattan distance along the Y-axis.</returns>
    public uint ManhattanDistanceToY(MapPoint point)
    {
        return (uint)Math.Abs(Y - point.Y);
    }

    /// <summary>
    /// Calculates the square distance to another map point.
    /// </summary>
    /// <param name="point">The target map point.</param>
    /// <returns>The square distance.</returns>
    public uint SquareDistanceTo(MapPoint point)
    {
        return (uint)Math.Max(Math.Abs(X - point.X), Math.Abs(Y - point.Y));
    }

    /// <summary>
    /// Determines if the map point is adjacent to another map point.
    /// </summary>
    /// <param name="point">The target map point.</param>
    /// <returns><c>true</c> if the map point is adjacent; otherwise, <c>false</c>.</returns>
    public bool IsAdjacentTo(MapPoint point)
    {
        return ManhattanDistanceTo(point) is 1;
    }

    /// <summary>
    /// Determines if the map point is adjacent or equal to another map point.
    /// </summary>
    /// <param name="point">The target map point.</param>
    /// <returns><c>true</c> if the map point is adjacent or equal; otherwise, <c>false</c>.</returns>
    public bool IsAdjacentOrEqualTo(MapPoint point)
    {
        return ManhattanDistanceTo(point) is 1 or 0;
    }

    /// <summary>
    /// Determines the orientation to an adjacent map point.
    /// </summary>
    /// <param name="point">The target map point.</param>
    /// <returns>The orientation as a <see cref="Directions"/> value.</returns>
    public Directions OrientationToAdjacent(MapPoint point)
    {
        var vector = new Point
        {
            X = point.X > X ? 1 : point.X < X ? -1 : 0,
            Y = point.Y > Y ? 1 : point.Y < Y ? -1 : 0
        };

        return vector switch
        {
            _ when vector == s_vectorRight => Directions.East,
            _ when vector == s_vectorDownRight => Directions.SouthEast,
            _ when vector == s_vectorDown => Directions.South,
            _ when vector == s_vectorDownLeft => Directions.SouthWest,
            _ when vector == s_vectorLeft => Directions.West,
            _ when vector == s_vectorUpLeft => Directions.NorthWest,
            _ when vector == s_vectorUp => Directions.North,
            _ when vector == s_vectorUpRight => Directions.NorthEast,
            _ => Directions.East
        };
    }

    /// <summary>
    /// Determines the orientation to a target map point.
    /// </summary>
    /// <param name="point">The target map point.</param>
    /// <param name="diagonal">
    /// A boolean indicating whether diagonal directions should be considered.
    /// </param>
    /// <returns>The orientation as a <see cref="Directions"/> value.</returns>
    public Directions OrientationTo(MapPoint point, bool diagonal = true)
    {
        var dx = point.X - X;
        var dy = Y - point.Y;

        var distance = Math.Sqrt(dx * dx + dy * dy);
        var angleInRadians = Math.Acos(dx / distance);

        var angleInDegrees = angleInRadians * 180 / Math.PI;
        var transformedAngle = angleInDegrees * (point.Y > Y ? -1 : 1);

        var orientation = !diagonal
            ? (int)Math.Round(transformedAngle / 90, MidpointRounding.AwayFromZero) * 2 + 1
            : (int)Math.Round(transformedAngle / 45, MidpointRounding.AwayFromZero) + 1;

        if (orientation < 0)
            orientation += 8;

        return (Directions)orientation;
    }

    /// <summary>
    /// Determines the orientation along the X-axis to a target map point.
    /// </summary>
    /// <param name="point">The target map point.</param>
    /// <param name="diagonal">
    /// A boolean indicating whether diagonal directions should be considered.
    /// </param>
    /// <returns>The orientation as a <see cref="Directions"/> value.</returns>
    public Directions OrientationToX(MapPoint point, bool diagonal = false)
    {
        var dx = point.X - X;

        var distance = Math.Sqrt(dx * dx);
        var angleInRadians = Math.Acos(dx / distance);

        var angleInDegrees = angleInRadians * 180 / Math.PI;

        var orientation = !diagonal
            ? (int)Math.Round(angleInDegrees / 90, MidpointRounding.AwayFromZero) * 2 + 1
            : (int)Math.Round(angleInDegrees / 45, MidpointRounding.AwayFromZero) + 1;

        if (orientation < 0)
            orientation += 8;

        return (Directions)orientation;
    }

    /// <summary>
    /// Determines the orientation along the Y-axis to a target map point.
    /// </summary>
    /// <param name="point">The target map point.</param>
    /// <param name="diagonal">
    /// A boolean indicating whether diagonal directions should be considered.
    /// </param>
    /// <returns>The orientation as a <see cref="Directions"/> value.</returns>
    public Directions OrientationToY(MapPoint point, bool diagonal = false)
    {
        const int dx = 0;
        var dy = Y - point.Y;

        var distance = Math.Sqrt(dx * dx + dy * dy);
        var angleInRadians = Math.Acos(dx / distance);

        var angleInDegrees = angleInRadians * 180 / Math.PI;
        var transformedAngle = angleInDegrees * (point.Y > Y ? -1 : 1);

        var orientation = !diagonal
            ? (int)Math.Round(transformedAngle / 90, MidpointRounding.AwayFromZero) * 2 + 1
            : (int)Math.Round(transformedAngle / 45, MidpointRounding.AwayFromZero) + 1;

        if (orientation < 0)
            orientation += 8;

        return (Directions)orientation;
    }

    /// <summary>
    /// Retrieves all cells within a rectangle defined by the current map point
    /// and an opposite map point.
    /// </summary>
    /// <param name="oppositeCell">The opposite corner of the rectangle.</param>
    /// <param name="skipStartAndEndCells">
    /// A boolean indicating whether to skip the start and end cells.
    /// </param>
    /// <param name="predicate">
    /// An optional predicate to filter the cells.
    /// </param>
    /// <returns>An enumerable of <see cref="MapPoint"/> within the rectangle.</returns>
    public IEnumerable<MapPoint> GetAllCellsInRectangle(MapPoint oppositeCell, bool skipStartAndEndCells = true, Func<MapPoint, bool>? predicate = null)
    {
        var x1 = Math.Min(oppositeCell.X, X);
        var y1 = Math.Min(oppositeCell.Y, Y);
        var x2 = Math.Max(oppositeCell.X, X);
        var y2 = Math.Max(oppositeCell.Y, Y);

        for (var x = x1; x <= x2; x++)
        {
            for (var y = y1; y <= y2; y++)
            {
                if (skipStartAndEndCells && (x == X && y == Y || x == oppositeCell.X && y == oppositeCell.Y))
                    continue;

                var cell = GetPoint(x, y);

                if (cell is not null && (predicate is null || predicate(cell)))
                    yield return cell;
            }
        }
    }

    /// <summary>
    /// Determines if the current map point is on the same line as another map point.
    /// </summary>
    /// <param name="point">The target map point.</param>
    /// <returns>
    /// <c>true</c> if the points are on the same line; otherwise, <c>false</c>.
    /// </returns>
    public bool IsOnSameLine(MapPoint point)
    {
        return point.X == X || point.Y == Y;
    }

    /// <summary>
    /// Determines if the current map point is on the same diagonal as another map point.
    /// </summary>
    /// <param name="point">The target map point.</param>
    /// <returns>
    /// <c>true</c> if the points are on the same diagonal; otherwise, <c>false</c>.
    /// </returns>
    public bool IsOnSameDiagonal(MapPoint point)
    {
        return point.X + point.Y == X + Y ||
               point.X - point.Y == X - Y;
    }

    /// <summary>
    /// Determines if the current map point is between two other map points.
    /// </summary>
    /// <param name="a">The first map point.</param>
    /// <param name="b">The second map point.</param>
    /// <returns>
    /// <c>true</c> if the current map point is between the two points; otherwise, <c>false</c>.
    /// </returns>
    public bool IsBetween(MapPoint a, MapPoint b)
    {
        if ((X - a.X) * (b.Y - Y) - (Y - a.Y) * (b.X - X) is not 0)
            return false;

        var min = new Point(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));
        var max = new Point(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));

        return X >= min.X && X <= max.X && Y >= min.Y && Y <= max.Y;
    }

    /// <summary>
    /// Retrieves all cells on the line between the current map point and a destination map point.
    /// </summary>
    /// <param name="destination">The destination map point.</param>
    /// <returns>An array of <see cref="MapPoint"/> on the line.</returns>
    public MapPoint[] GetCellsOnLineBetween(MapPoint destination)
    {
        var result = new List<MapPoint>();
        var direction = OrientationTo(destination);
        var current = this;

        for (var i = 0; i < AtouinConstants.MapHeight * AtouinConstants.MapWidth / 2; i++)
        {
            current = current.GetCellInDirection(direction, 1);

            if (current is null)
                break;

            if (current.CellId == destination.CellId)
                break;

            result.Add(current);
        }

        return result.ToArray();
    }

    /// <summary>
    /// Retrieves all cells in a line between the current map point and a destination map point.
    /// </summary>
    /// <param name="destination">The destination map point.</param>
    /// <returns>An enumerable of <see cref="MapPoint"/> in the line.</returns>
    public IEnumerable<MapPoint> GetCellsInLine(MapPoint destination)
    {
        return GetCellsInLine(this, destination);
    }

    /// <summary>
    /// Retrieves all cells in a line between two map points.
    /// </summary>
    /// <param name="source">The source map point.</param>
    /// <param name="destination">The destination map point.</param>
    /// <returns>An enumerable of <see cref="MapPoint"/> in the line.</returns>
    public static IEnumerable<MapPoint> GetCellsInLine(MapPoint source, MapPoint destination)
    {
        var dx = Math.Abs(destination.X - source.X);
        var dy = Math.Abs(destination.Y - source.Y);
        var x = source.X;
        var y = source.Y;
        var n = 1 + dx + dy;
        var vectorX = destination.X > source.X ? 1 : -1;
        var vectorY = destination.Y > source.Y ? 1 : -1;
        var error = dx - dy;
        dx *= 2;
        dy *= 2;

        for (; n > 0; --n)
        {
            var cell = GetPoint(x, y);

            if (cell is not null)
                yield return cell;

            switch (error)
            {
                case > 0:
                    x += vectorX;
                    error -= dy;
                    break;
                case 0:
                    x += vectorX;
                    y += vectorY;
                    n--;
                    break;
                default:
                    y += vectorY;
                    error += dx;
                    break;
            }
        }
    }

    /// <summary>
    /// Retrieves the border cells of the map based on the specified map neighbor.
    /// </summary>
    /// <param name="mapNeighbour">The map neighbor (e.g., Top, Left, Bottom, Right).</param>
    /// <returns>An enumerable of <see cref="MapPoint"/> representing the border cells.</returns>
    public static IEnumerable<MapPoint> GetBorderCells(MapNeighbours mapNeighbour)
    {
        return mapNeighbour switch
        {
            MapNeighbours.Top => GetCellsInLine(new MapPoint(546), new MapPoint(559)),
            MapNeighbours.Left => GetCellsInLine(new MapPoint(27), new MapPoint(559)),
            MapNeighbours.Bottom => GetCellsInLine(new MapPoint(0), new MapPoint(13)),
            MapNeighbours.Right => GetCellsInLine(new MapPoint(0), new MapPoint(532)),
            _ => []
        };
    }

    /// <summary>
    /// Retrieves the border cells of the map based on the specified direction.
    /// </summary>
    /// <param name="mapNeighbour">The direction (e.g., North, West, South, East).</param>
    /// <returns>An enumerable of <see cref="MapPoint"/> representing the border cells.</returns>
    public static IEnumerable<MapPoint> GetBorderCells(Directions mapNeighbour)
    {
        return mapNeighbour switch
        {
            Directions.North => GetCellsInLine(new MapPoint(546), new MapPoint(559)),
            Directions.West => GetCellsInLine(new MapPoint(27), new MapPoint(559)),
            Directions.South => GetCellsInLine(new MapPoint(0), new MapPoint(13)),
            Directions.East => GetCellsInLine(new MapPoint(0), new MapPoint(532)),
            _ => []
        };
    }

    /// <summary>
    /// Retrieves the cell in the specified direction and step distance from the current map point.
    /// </summary>
    /// <param name="direction">The direction to move (e.g., East, SouthEast, etc.).</param>
    /// <param name="step">The number of steps to move in the specified direction.</param>
    /// <returns>
    /// A <see cref="MapPoint"/> representing the cell in the specified direction and step distance,
    /// or <c>null</c> if the cell is out of bounds.
    /// </returns>
    public MapPoint? GetCellInDirection(Directions direction, int step)
    {
        var mapPoint = direction switch
        {
            Directions.East => GetPoint(X + step, Y + step),
            Directions.SouthEast => GetPoint(X + step, Y),
            Directions.South => GetPoint(X + step, Y - step),
            Directions.SouthWest => GetPoint(X, Y - step),
            Directions.West => GetPoint(X - step, Y - step),
            Directions.NorthWest => GetPoint(X - step, Y),
            Directions.North => GetPoint(X - step, Y + step),
            Directions.NorthEast => GetPoint(X, Y + step),
            _ => null
        };

        if (mapPoint is not null)
            return IsInMap(mapPoint.X, mapPoint.Y) ? mapPoint : null;

        return null;
    }

    /// <summary>
    /// Retrieves the nearest cell in the specified direction from the current map point.
    /// </summary>
    /// <param name="direction">The direction to move (e.g., East, SouthEast, etc.).</param>
    /// <returns>
    /// A <see cref="MapPoint"/> representing the nearest cell in the specified direction,
    /// or <c>null</c> if the cell is out of bounds.
    /// </returns>
    public MapPoint? GetNearestCellInDirection(Directions direction)
    {
        return GetCellInDirection(direction, 1);
    }

    /// <summary>
    /// Retrieves all adjacent cells to the current map point.
    /// </summary>
    /// <param name="diagonal">
    /// A boolean indicating whether diagonal neighbors should be included.
    /// </param>
    /// <returns>An enumerable of <see cref="MapPoint"/> representing the adjacent cells.</returns>
    public IEnumerable<MapPoint> GetAdjacentCells(bool diagonal = false)
    {
        return GetAdjacentCells(_ => true, diagonal);
    }

    /// <summary>
    /// Retrieves all adjacent cells within a specified range to the current map point.
    /// </summary>
    /// <param name="range">The range of cells to retrieve.</param>
    /// <param name="diagonal">
    /// A boolean indicating whether diagonal neighbors should be included.
    /// </param>
    /// <returns>An enumerable of <see cref="MapPoint"/> representing the adjacent cells.</returns>
    public IEnumerable<MapPoint> GetAdjacentCells(byte range, bool diagonal = false)
    {
        return GetAdjacentCells(_ => true, diagonal, range);
    }

    /// <summary>
    /// Retrieves all adjacent cells to the current map point that satisfy a given predicate.
    /// </summary>
    /// <param name="predicate">A predicate to filter the adjacent cells.</param>
    /// <param name="diagonal">
    /// A boolean indicating whether diagonal neighbors should be included.
    /// </param>
    /// <param name="range">The range of cells to retrieve.</param>
    /// <returns>An enumerable of <see cref="MapPoint"/> representing the adjacent cells.</returns>
    public IEnumerable<MapPoint> GetAdjacentCells(Func<short, bool> predicate, bool diagonal = false, byte range = 1)
    {
        for (var i = 1; i <= range; i++)
        {
            var northEast = new MapPoint(X, Y + i);

            if (IsInMap(northEast.X, northEast.Y) && predicate(northEast.CellId))
                yield return northEast;

            var southEast = new MapPoint(X + i, Y);

            if (IsInMap(southEast.X, southEast.Y) && predicate(southEast.CellId))
                yield return southEast;

            var southWest = new MapPoint(X, Y - i);

            if (IsInMap(southWest.X, southWest.Y) && predicate(southWest.CellId))
                yield return southWest;

            var northWest = new MapPoint(X - i, Y);

            if (IsInMap(northWest.X, northWest.Y) && predicate(northWest.CellId))
                yield return northWest;

            if (!diagonal)
                continue;

            var south = new MapPoint(X + i, Y - i);

            if (IsInMap(south.X, south.Y) && predicate(south.CellId))
                yield return south;

            var west = new MapPoint(X - i, Y - i);

            if (IsInMap(west.X, west.Y) && predicate(west.CellId))
                yield return west;

            var north = new MapPoint(X - i, Y + i);

            if (IsInMap(north.X, north.Y) && predicate(north.CellId))
                yield return north;

            var east = new MapPoint(X + i, Y + i);

            if (IsInMap(east.X, east.Y) && predicate(east.CellId))
                yield return east;
        }
    }

    /// <summary>
    /// Determines the advanced orientation to a target map point.
    /// </summary>
    /// <param name="target">The target map point.</param>
    /// <param name="fourDir">
    /// A boolean indicating whether to use four directions (North, East, South, West).
    /// </param>
    /// <returns>
    /// An unsigned integer representing the orientation to the target map point.
    /// </returns>
    public uint AdvancedOrientationTo(MapPoint? target, bool fourDir = true)
    {
        if (target is null)
            return 0;

        var xDifference = target.X - X;
        var yDifference = Y - target.Y;

        var angle = Math.Acos(xDifference / Math.Sqrt(Math.Pow(xDifference, 2) + Math.Pow(yDifference, 2))) * 180 / Math.PI * (target.Y > Y ? -1 : 1);

        angle = fourDir
            ? (int)Math.Round(angle / 90, MidpointRounding.AwayFromZero) * 2 + 1
            : (int)Math.Round(angle / 45, MidpointRounding.AwayFromZero) + 1;

        if (angle < 0)
            angle += 8;

        return (uint)angle;
    }

    /// <summary>
    /// Determines if the current map point is within the map boundaries.
    /// </summary>
    /// <returns><c>true</c> if the map point is within the map boundaries; otherwise, <c>false</c>.</returns>
    public bool IsInMap()
    {
        return IsInMap(X, Y);
    }

    /// <summary>
    /// Determines if the specified coordinates are within the map boundaries.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <returns><c>true</c> if the coordinates are within the map boundaries; otherwise, <c>false</c>.</returns>
    private static bool IsInMap(int x, int y)
    {
        return x + y >= 0 && x - y >= 0 && x - y < AtouinConstants.MapHeight * 2 && x + y < AtouinConstants.MapWidth * 2;
    }

    /// <summary>
    /// Converts the specified coordinates to a cell ID.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <returns>The cell ID corresponding to the specified coordinates.</returns>
    private static uint CoordToCellId(int x, int y)
    {
        if (!s_initialized)
            InitializeStaticGrid();

        return (uint)((x - y) * AtouinConstants.MapWidth + y + (x - y) / 2);
    }

    /// <summary>
    /// Initializes the static grid for map points.
    /// </summary>
    private static void InitializeStaticGrid()
    {
        if (s_initialized)
            return;

        s_initialized = true;

        var posX = 0;
        var posY = 0;
        var cellCount = 0;

        for (var x = 0; x < AtouinConstants.MapHeight; x++)
        {
            for (var y = 0; y < AtouinConstants.MapWidth; y++)
                s_orthogonalGridReference[cellCount++] = new MapPoint(posX + y, posY + y);

            posX++;

            for (var y = 0; y < AtouinConstants.MapWidth; y++)
                s_orthogonalGridReference[cellCount++] = new MapPoint(posX + y, posY + y);

            posY--;
        }
    }

    /// <summary>
    /// Retrieves a map point based on the specified cell ID.
    /// </summary>
    /// <param name="cellId">The cell ID to retrieve the map point for.</param>
    /// <returns>
    /// A <see cref="MapPoint"/> corresponding to the specified cell ID, or <c>null</c> if not found.
    /// </returns>
    public static MapPoint? GetPoint(int cellId)
    {
        return GetPoint((short)cellId);
    }

    /// <summary>
    /// Retrieves a map point based on the specified X and Y coordinates.
    /// </summary>
    /// <param name="x">The X coordinate of the map point.</param>
    /// <param name="y">The Y coordinate of the map point.</param>
    /// <returns>
    /// A <see cref="MapPoint"/> corresponding to the specified coordinates, or <c>null</c> if not found.
    /// </returns>
    public static MapPoint? GetPoint(int x, int y)
    {
        return GetPoint(CoordToCellId(x, y));
    }

    /// <summary>
    /// Retrieves a map point based on the specified cell ID as an unsigned integer.
    /// </summary>
    /// <param name="cell">The cell ID as an unsigned integer.</param>
    /// <returns>
    /// A <see cref="MapPoint"/> corresponding to the specified cell ID, or <c>null</c> if not found.
    /// </returns>
    public static MapPoint? GetPoint(uint cell)
    {
        return GetPoint((short)cell);
    }

    /// <summary>
    /// Retrieves a map point based on the specified cell ID as a short.
    /// </summary>
    /// <param name="cell">The cell ID as a short.</param>
    /// <returns>
    /// A <see cref="MapPoint"/> corresponding to the specified cell ID, or <c>null</c> if not found.
    /// </returns>
    public static MapPoint? GetPoint(short cell)
    {
        if (cell is < 0 or >= (short)AtouinConstants.MapCellsCount)
            return null;

        return s_orthogonalGridReference.All(x => x.CellId != cell) ? null : s_orthogonalGridReference[cell];
    }

    /// <summary>
    /// Retrieves a map point based on the specified <see cref="Cell"/> object.
    /// </summary>
    /// <param name="cell">The <see cref="Cell"/> object to retrieve the map point for.</param>
    /// <returns>
    /// A <see cref="MapPoint"/> corresponding to the specified cell, or <c>null</c> if the cell is <c>null</c>.
    /// </returns>
    public static MapPoint? GetPoint(Cell? cell)
    {
        return cell is null ? null : GetPoint(cell.Id);
    }

    /// <summary>
    /// Retrieves all map points on the map.
    /// </summary>
    /// <returns>An array of all <see cref="MapPoint"/> objects on the map.</returns>
    public static MapPoint[] GetAllPoints()
    {
        if (!s_initialized)
            InitializeStaticGrid();

        return s_orthogonalGridReference;
    }

    /// <summary>
    /// Implicitly converts a <see cref="Cell"/> object to a <see cref="MapPoint"/>.
    /// </summary>
    /// <param name="cell">The <see cref="Cell"/> object to convert.</param>
    /// <returns>A <see cref="MapPoint"/> corresponding to the specified cell.</returns>
    public static implicit operator MapPoint(Cell cell)
    {
        return new MapPoint(cell);
    }

    /// <summary>
    /// Returns a string representation of the map point.
    /// </summary>
    /// <returns>A string containing the cell ID, X, and Y coordinates of the map point.</returns>
    public override string ToString()
    {
        return $"CellId: {CellId}, X: {X}, Y: {Y}";
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current map point.
    /// </summary>
    /// <param name="obj">The object to compare with the current map point.</param>
    /// <returns>
    /// <c>true</c> if the specified object is equal to the current map point; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        return obj is MapPoint point && Equals(point);
    }

    /// <summary>
    /// Determines whether the specified map point is equal to the current map point.
    /// </summary>
    /// <param name="other">The map point to compare with the current map point.</param>
    /// <returns>
    /// <c>true</c> if the specified map point is equal to the current map point; otherwise, <c>false</c>.
    /// </returns>
    private bool Equals(MapPoint? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return other.CellId == CellId && other.X == X && other.Y == Y;
    }

    /// <summary>
    /// Returns the hash code for the current map point.
    /// </summary>
    /// <returns>The hash code for the current map point.</returns>
    public override int GetHashCode()
    {
        unchecked
        {
            int result = CellId;
            result = (result * 397) ^ X;
            result = (result * 397) ^ Y;
            return result;
        }
    }

    /// <summary>
    /// Deconstructs the map point into its X and Y coordinates.
    /// </summary>
    /// <param name="x">The X coordinate of the map point.</param>
    /// <param name="y">The Y coordinate of the map point.</param>
    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }
}
