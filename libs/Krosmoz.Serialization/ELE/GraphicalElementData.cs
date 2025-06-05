// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.ELE;

/// <summary>
/// Represents the base class for graphical element data in the serialization format.
/// </summary>
public abstract class GraphicalElementData
{
    /// <summary>
    /// Gets or sets the unique identifier of the graphical element.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the type of the graphical element.
    /// </summary>
    public GraphicalElementTypes Type { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GraphicalElementData"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the graphical element.</param>
    /// <param name="type">The type of the graphical element.</param>
    public GraphicalElementData(int id, GraphicalElementTypes type)
    {
        Id = id;
        Type = type;
    }

    /// <summary>
    /// Serializes the graphical element data to a binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to serialize the data to.</param>
    /// <param name="version">The version of the serialization format.</param>
    public virtual void Serialize(BigEndianWriter writer, sbyte version)
    {
    }

    /// <summary>
    /// Deserializes the graphical element data from a binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the data from.</param>
    /// <param name="version">The version of the serialization format.</param>
    public virtual void Deserialize(BigEndianReader reader, sbyte version)
    {
    }
}
