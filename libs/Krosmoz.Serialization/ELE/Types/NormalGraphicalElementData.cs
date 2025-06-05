// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Drawing;
using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.ELE.Types;

/// <summary>
/// Represents graphical element data for normal elements in the serialization format.
/// </summary>
public class NormalGraphicalElementData : GraphicalElementData
{
    /// <summary>
    /// Gets or sets the graphical identifier of the element.
    /// </summary>
    public int GfxId { get; set; }

    /// <summary>
    /// Gets or sets the height of the graphical element.
    /// </summary>
    public sbyte Height { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the element has horizontal symmetry.
    /// </summary>
    public bool HorizontalSymmetry { get; set; }

    /// <summary>
    /// Gets or sets the origin point of the graphical element.
    /// </summary>
    public Point Origin { get; set; }

    /// <summary>
    /// Gets or sets the size of the graphical element.
    /// </summary>
    public Point Size { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NormalGraphicalElementData"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the graphical element.</param>
    /// <param name="type">The type of the graphical element.</param>
    public NormalGraphicalElementData(int id, GraphicalElementTypes type) : base(id, type)
    {
    }

    /// <summary>
    /// Serializes the normal graphical element data to a binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to serialize the data to.</param>
    /// <param name="version">The version of the serialization format.</param>
    public override void Serialize(BigEndianWriter writer, sbyte version)
    {
        writer.WriteInt32(GfxId);
        writer.WriteInt8(Height);
        writer.WriteBoolean(HorizontalSymmetry);
        writer.WriteInt16((short)Origin.X);
        writer.WriteInt16((short)Origin.Y);
        writer.WriteInt16((short)Size.X);
        writer.WriteInt16((short)Size.Y);
    }

    /// <summary>
    /// Deserializes the normal graphical element data from a binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the data from.</param>
    /// <param name="version">The version of the serialization format.</param>
    public override void Deserialize(BigEndianReader reader, sbyte version)
    {
        GfxId = reader.ReadInt32();
        Height = reader.ReadInt8();
        HorizontalSymmetry = reader.ReadBoolean();
        Origin = new Point(reader.ReadInt16(), reader.ReadInt16());
        Size = new Point(reader.ReadInt16(), reader.ReadInt16());
    }
}
