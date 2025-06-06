// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Models.World.Cells;

/// <summary>
/// Represents a cell on the map.
/// </summary>
public sealed class Cell
{
    /// <summary>
    /// Gets or sets the unique identifier of the cell.
    /// </summary>
    public short Id { get; set; }

    /// <summary>
    /// Gets or sets the map change data associated with the cell.
    /// </summary>
    public byte MapChangeData { get; set; }

    /// <summary>
    /// Gets or sets the movement zone of the cell.
    /// </summary>
    public byte MoveZone { get; set; }

    /// <summary>
    /// Gets or sets the speed modifier for the cell.
    /// </summary>
    public sbyte Speed { get; set; }

    /// <summary>
    /// Gets or sets the floor height of the cell.
    /// </summary>
    public byte Floor { get; set; }

    /// <summary>
    /// Gets or sets the line-of-sight and movement data for the cell.
    /// </summary>
    public byte LosMov { get; set; }

    /// <summary>
    /// Gets a value indicating whether the cell is walkable.
    /// </summary>
    public bool Mov =>
        (LosMov & 1) is 1;

    /// <summary>
    /// Gets a value indicating whether the cell has line of sight.
    /// </summary>
    public bool Los =>
        (LosMov & 2) >> 1 is 1;

    /// <summary>
    /// Gets a value indicating whether the cell is non-walkable during a fight.
    /// </summary>
    public bool NonWalkableDuringFight =>
        (LosMov & 4) >> 2 is 1;

    /// <summary>
    /// Gets a value indicating whether the cell is marked as red.
    /// </summary>
    public bool Red =>
        (LosMov & 8) >> 3 is 1;

    /// <summary>
    /// Gets a value indicating whether the cell is marked as blue.
    /// </summary>
    public bool Blue =>
        (LosMov & 16) >> 4 is 1;

    /// <summary>
    /// Gets a value indicating whether the cell is a farm cell.
    /// </summary>
    public bool FarmCell =>
        (LosMov & 32) >> 5 is 1;

    /// <summary>
    /// Gets a value indicating whether the cell is visible.
    /// </summary>
    public bool Visible =>
        (LosMov & 64) >> 6 is 1;

    /// <summary>
    /// Gets a value indicating whether the cell is non-walkable during roleplay.
    /// </summary>
    public bool NonWalkableDuringRp =>
        (LosMov & 128) >> 7 is 1;

    /// <summary>
    /// Gets a value indicating whether the cell has a linked zone for roleplay.
    /// </summary>
    public bool HasLinkedZoneRp =>
        Mov && !FarmCell;

    /// <summary>
    /// Gets a value indicating whether the cell has a linked zone for fights.
    /// </summary>
    public bool HasLinkedZoneFight =>
        Mov && !NonWalkableDuringFight && !FarmCell;
}
