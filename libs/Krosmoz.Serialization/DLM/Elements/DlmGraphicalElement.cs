// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Drawing;
using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.DLM.Elements;

/// <summary>
/// Represents a graphical element in the DLM serialization format.
/// </summary>
public sealed class DlmGraphicalElement : DlmBasicElement
{
    /// <summary>
    /// The half-width of a cell in the map.
    /// </summary>
    private const uint CellHalfWidth = 43;

    /// <summary>
    /// The half-height of a cell in the map.
    /// </summary>
    private const double CellHalfHeight = 21.5;

    /// <summary>
    /// Gets or sets the unique identifier of the graphical element.
    /// </summary>
    public uint ElementId { get; set; }

    /// <summary>
    /// Gets or sets the hue color multiplicator of the element.
    /// </summary>
    public ColorMultiplicator Hue { get; set; }

    /// <summary>
    /// Gets or sets the shadow color multiplicator of the element.
    /// </summary>
    public ColorMultiplicator Shadow { get; set; }

    /// <summary>
    /// Gets or sets the final tint color multiplicator of the element.
    /// </summary>
    public ColorMultiplicator FinalTeint { get; set; }

    /// <summary>
    /// Gets or sets the offset of the element in cell coordinates.
    /// </summary>
    public Point Offset { get; set; }

    /// <summary>
    /// Gets or sets the pixel offset of the element.
    /// </summary>
    public Point PixelOffset { get; set; }

    /// <summary>
    /// Gets or sets the altitude of the element.
    /// </summary>
    public int Altitude { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the element.
    /// </summary>
    public uint Identifier { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DlmGraphicalElement"/> class.
    /// </summary>
    /// <param name="cell">The cell associated with this element.</param>
    /// <param name="type">The type of the element.</param>
    public DlmGraphicalElement(DlmCell cell, DlmElementTypes type) : base(cell, type)
    {
        Hue = new ColorMultiplicator(0, 0, 0);
        Shadow = new ColorMultiplicator(0, 0, 0);
        FinalTeint = new ColorMultiplicator(0, 0, 0);
    }

    /// <summary>
    /// Serializes the graphical element data to a binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to serialize the data to.</param>
    public override void Serialize(BigEndianWriter writer)
    {
        writer.WriteUInt32(ElementId);
        writer.WriteInt8((sbyte)Hue.Red);
        writer.WriteInt8((sbyte)Hue.Green);
        writer.WriteInt8((sbyte)Hue.Blue);
        writer.WriteInt8((sbyte)Shadow.Red);
        writer.WriteInt8((sbyte)Shadow.Green);
        writer.WriteInt8((sbyte)Shadow.Blue);

        if (Cell.Layer.Map.Version <= 4)
        {
            writer.WriteInt8((sbyte)Offset.X);
            writer.WriteInt8((sbyte)Offset.Y);
        }
        else
        {
            writer.WriteInt16((short)PixelOffset.X);
            writer.WriteInt16((short)PixelOffset.Y);
        }

        writer.WriteInt8((sbyte)Altitude);
        writer.WriteUInt32(Identifier);
    }

    /// <summary>
    /// Deserializes the graphical element data from a binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the data from.</param>
    public override void Deserialize(BigEndianReader reader)
    {
        ElementId = reader.ReadUInt32();
        Hue = new ColorMultiplicator(reader.ReadInt8(), reader.ReadInt8(), reader.ReadInt8());
        Shadow = new ColorMultiplicator(reader.ReadInt8(), reader.ReadInt8(), reader.ReadInt8());

        if (Cell.Layer.Map.Version <= 4)
        {
            Offset = new Point(reader.ReadInt8(), reader.ReadInt8());
            PixelOffset = new Point((int)(Offset.X * CellHalfWidth), (int)(Offset.Y * CellHalfHeight));
        }
        else
        {
            PixelOffset = new Point(reader.ReadInt16(), reader.ReadInt16());
            Offset = new Point((int)(PixelOffset.X / CellHalfWidth), (int)(PixelOffset.Y / CellHalfHeight));
        }

        Altitude = reader.ReadInt8();
        Identifier = reader.ReadUInt32();

        var r = ColorMultiplicator.Clamp((Hue.Red + Shadow.Red + 128) * 2, 0, 512);
        var g = ColorMultiplicator.Clamp((Hue.Green + Shadow.Green + 128) * 2, 0, 512);
        var b = ColorMultiplicator.Clamp((Hue.Blue + Shadow.Blue + 128) * 2, 0, 512);

        FinalTeint = new ColorMultiplicator(r, g, b, true);
    }
}
