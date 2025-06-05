// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.DLM;

/// <summary>
/// Represents the data of a cell in the DLM serialization format.
/// </summary>
public sealed class DlmCellData
{
    private short? _floor;
    private sbyte _rawFloor;
    private sbyte _rawArrow;
    private short? _arrow;

    /// <summary>
    /// Gets the map associated with this cell.
    /// </summary>
    public DlmMap Map { get; }

    /// <summary>
    /// Gets the unique identifier of the cell.
    /// </summary>
    public short Id { get; }

    /// <summary>
    /// Gets or sets the speed of the cell.
    /// </summary>
    public sbyte Speed { get; set; }

    /// <summary>
    /// Gets or sets the map change data of the cell.
    /// </summary>
    public byte MapChangeData { get; set; }

    /// <summary>
    /// Gets or sets the move zone of the cell.
    /// </summary>
    public byte MoveZone { get; set; }

    /// <summary>
    /// Gets or sets the line-of-sight and movement data of the cell.
    /// </summary>
    public byte LosMov { get; set; }

    /// <summary>
    /// Gets the arrow value of the cell.
    /// </summary>
    public short Arrow =>
        _arrow ??= (short)(15 & _rawArrow);

    /// <summary>
    /// Gets a value indicating whether the cell is movable.
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
    /// Gets the floor value of the cell.
    /// </summary>
    public short Floor =>
        _floor ??= (short)(_rawFloor * 10);

    /// <summary>
    /// Gets a value indicating whether the cell has a linked zone during roleplay.
    /// </summary>
    public bool HasLinkedZoneRp =>
        Mov && !FarmCell;

    /// <summary>
    /// Gets a value indicating whether the cell has a linked zone during a fight.
    /// </summary>
    public bool HasLinkedZoneFight =>
        Mov && !NonWalkableDuringFight && !FarmCell;

    /// <summary>
    /// Gets a value indicating whether the top arrow is used.
    /// </summary>
    public bool UseTopArrow =>
        (Arrow & 1) is not 0;

    /// <summary>
    /// Gets a value indicating whether the bottom arrow is used.
    /// </summary>
    public bool UseBottomArrow =>
        (Arrow & 2) is not 0;

    /// <summary>
    /// Gets a value indicating whether the right arrow is used.
    /// </summary>
    public bool UseRightArrow =>
        (Arrow & 4) is not 0;

    /// <summary>
    /// Gets a value indicating whether the left arrow is used.
    /// </summary>
    public bool UseLeftArrow =>
        (Arrow & 8) is not 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="DlmCellData"/> class.
    /// </summary>
    /// <param name="map">The map associated with this cell.</param>
    /// <param name="id">The unique identifier of the cell.</param>
    public DlmCellData(DlmMap map, short id)
    {
        Map = map;
        Id = id;
    }

    /// <summary>
    /// Serializes the cell data to a binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to serialize the data to.</param>
    public void Serialize(BigEndianWriter writer)
    {
        writer.WriteInt8((sbyte)Floor);

        if (_rawFloor is sbyte.MinValue)
            return;

        writer.WriteUInt8(LosMov);
        writer.WriteInt8(Speed);
        writer.WriteUInt8(MapChangeData);

        if (Map.Version > 5)
            writer.WriteUInt8(MoveZone);

        if (Map.Version > 7)
            writer.WriteInt8((sbyte)Arrow);
    }

    /// <summary>
    /// Deserializes the cell data from a binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the data from.</param>
    public void Deserialize(BigEndianReader reader)
    {
        _rawFloor = reader.ReadInt8();

        if (_rawFloor is sbyte.MinValue)
            return;

        LosMov = reader.ReadUInt8();
        Speed = reader.ReadInt8();
        MapChangeData = reader.ReadUInt8();

        if (Map.Version > 5)
            MoveZone = reader.ReadUInt8();

        if (Map.Version > 7)
        {
            _rawArrow = reader.ReadInt8();

            if (UseTopArrow)
                Map.TopArrowCells.Add(Id);

            if (UseBottomArrow)
                Map.BottomArrowCells.Add(Id);

            if (UseLeftArrow)
                Map.LeftArrowCells.Add(Id);

            if (UseRightArrow)
                Map.RightArrowCells.Add(Id);
        }
    }
}
