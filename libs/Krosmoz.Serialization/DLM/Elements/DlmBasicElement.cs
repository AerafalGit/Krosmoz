// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.DLM.Elements;

/// <summary>
/// Represents the base class for all DLM elements.
/// </summary>
public abstract class DlmBasicElement
{
    /// <summary>
    /// Gets the cell associated with this element.
    /// </summary>
    public DlmCell Cell { get; }

    /// <summary>
    /// Gets the type of the element.
    /// </summary>
    public DlmElementTypes Type { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DlmBasicElement"/> class.
    /// </summary>
    /// <param name="cell">The cell associated with this element.</param>
    /// <param name="type">The type of the element.</param>
    protected DlmBasicElement(DlmCell cell, DlmElementTypes type)
    {
        Cell = cell;
        Type = type;
    }

    /// <summary>
    /// Serializes the element data to a binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to serialize the data to.</param>
    public virtual void Serialize(BigEndianWriter writer)
    {
    }

    /// <summary>
    /// Deserializes the element data from a binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the data from.</param>
    public virtual void Deserialize(BigEndianReader reader)
    {
    }

    /// <summary>
    /// Creates a specific DLM element based on the provided type.
    /// </summary>
    /// <param name="type">The type of the element as a signed byte.</param>
    /// <param name="cell">The cell associated with the element.</param>
    /// <returns>A <see cref="DlmBasicElement"/> instance corresponding to the specified type.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the type is not recognized.</exception>
    public static DlmBasicElement GetElementFromType(sbyte type, DlmCell cell)
    {
        var elementType = (DlmElementTypes)type;

        return elementType switch
        {
            DlmElementTypes.Graphical => new DlmGraphicalElement(cell, elementType),
            DlmElementTypes.Sound => new DlmSoundElement(cell, elementType),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }
}
