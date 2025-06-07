// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Servers.GameServer.Database.Models.Characters;
using Krosmoz.Servers.GameServer.Models.World.Cells;
using Krosmoz.Servers.GameServer.Models.World.Maps;

namespace Krosmoz.Servers.GameServer.Models.World;

/// <summary>
/// Represents a position in the game world.
/// </summary>
public sealed class WorldPosition
{
    /// <summary>
    /// The cell associated with the position.
    /// </summary>
    private Cell _cell;

    /// <summary>
    /// Gets the map associated with the position.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// Gets or sets the point representation of the position.
    /// </summary>
    public MapPoint Point { get; private set; }

    /// <summary>
    /// Gets or sets the cell associated with the position.
    /// Updates the <see cref="Point"/> property when set.
    /// </summary>
    public Cell Cell
    {
        get => _cell;
        set
        {
            _cell = value;
            Point = MapPoint.GetPoint(value)!;
        }
    }

    /// <summary>
    /// Gets or sets the orientation of the position.
    /// </summary>
    public Directions Orientation { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WorldPosition"/> class.
    /// </summary>
    /// <param name="map">The map associated with the position.</param>
    /// <param name="cell">The cell associated with the position.</param>
    /// <param name="orientation">The orientation of the position.</param>
    public WorldPosition(Map map, Cell cell, Directions orientation)
    {
        Map = map;
        Orientation = orientation;
        Point = MapPoint.GetPoint(cell.Id)!;
        _cell = cell;
    }

    /// <summary>
    /// Creates a clone of the current <see cref="WorldPosition"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="WorldPosition"/> with the same properties.</returns>
    public WorldPosition Clone()
    {
        return new WorldPosition(Map, Cell, Orientation);
    }

    /// <summary>
    /// Implicitly converts a <see cref="WorldPosition"/> to a <see cref="CharacterPosition"/>.
    /// </summary>
    /// <param name="position">The world position to convert.</param>
    /// <returns>A new instance of <see cref="CharacterPosition"/> representing the world position.</returns>
    public static implicit operator CharacterPosition(WorldPosition position)
    {
        return new CharacterPosition(position.Map.Id, position.Cell.Id, (sbyte)position.Orientation);
    }
}
