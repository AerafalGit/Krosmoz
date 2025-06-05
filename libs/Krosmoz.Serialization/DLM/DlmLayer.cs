// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.DLM;

/// <summary>
/// Represents a layer in the DLM serialization format.
/// </summary>
public sealed class DlmLayer
{
    /// <summary>
    /// Gets the map associated with this layer.
    /// </summary>
    public DlmMap Map { get; }

    /// <summary>
    /// Gets or sets the unique identifier of the layer.
    /// </summary>
    public int LayerId { get; set; }

    /// <summary>
    /// Gets or sets the cells contained in this layer.
    /// </summary>
    public DlmCell[] Cells { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DlmLayer"/> class.
    /// </summary>
    /// <param name="map">The map associated with this layer.</param>
    public DlmLayer(DlmMap map)
    {
        Map = map;
        Cells = [];
    }

    /// <summary>
    /// Serializes the layer data to a binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to serialize the data to.</param>
    public void Serialize(BigEndianWriter writer)
    {
        writer.WriteInt32(LayerId);
        writer.WriteInt16((short)Cells.Length);

        foreach (var cell in Cells)
            cell.Serialize(writer);
    }

    /// <summary>
    /// Deserializes the layer data from a binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the data from.</param>
    public void Deserialize(BigEndianReader reader)
    {
        LayerId = reader.ReadInt32();
        Cells = new DlmCell[reader.ReadInt16()];
        for (var i = 0; i < Cells.Length; i++)
        {
            Cells[i] = new DlmCell(this);
            Cells[i].Deserialize(reader);
        }
    }
}
