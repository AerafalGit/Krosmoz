// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;
using Krosmoz.Serialization.DLM.Elements;

namespace Krosmoz.Serialization.DLM;

/// <summary>
/// Represents a cell in the DLM serialization format.
/// </summary>
public sealed class DlmCell
{
    /// <summary>
    /// Gets the layer associated with this cell.
    /// </summary>
    public DlmLayer Layer { get; }

    /// <summary>
    /// Gets or sets the unique identifier of the cell.
    /// </summary>
    public short CellId { get; set; }

    /// <summary>
    /// Gets or sets the elements contained within the cell.
    /// </summary>
    public DlmBasicElement[] Elements { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DlmCell"/> class.
    /// </summary>
    /// <param name="layer">The layer associated with this cell.</param>
    public DlmCell(DlmLayer layer)
    {
        Layer = layer;
        Elements = [];
    }

    /// <summary>
    /// Serializes the cell data to a binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to serialize the data to.</param>
    public void Serialize(BigEndianWriter writer)
    {
        writer.WriteInt16(CellId);
        writer.WriteInt16((short)Elements.Length);

        foreach (var element in Elements)
        {
            writer.WriteInt8((sbyte)element.Type);
            element.Serialize(writer);
        }
    }

    /// <summary>
    /// Deserializes the cell data from a binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the data from.</param>
    public void Deserialize(BigEndianReader reader)
    {
        CellId = reader.ReadInt16();
        Elements = new DlmBasicElement[reader.ReadInt16()];

        for (var i = 0; i < Elements.Length; i++)
        {
            var element = DlmBasicElement.GetElementFromType(reader.ReadInt8(), this);
            element.Deserialize(reader);
            Elements[i] = element;
        }
    }
}
