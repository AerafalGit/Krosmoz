// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;

namespace Krosmoz.Tools.Protocol.Models;

/// <summary>
/// Represents a symbol for a property.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public sealed class PropertySymbol
{
    /// <summary>
    /// Gets or sets the name of the property.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the type of the property.
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Gets or sets the index of the property.
    /// </summary>
    public required int Index { get; set; }

    /// <summary>
    /// Gets or sets the kind of the property, if applicable.
    /// </summary>
    public PropertyKind? PropertyKind { get; set; }

    /// <summary>
    /// Gets or sets the kind of method associated with the property, if applicable.
    /// </summary>
    public MethodKind? MethodKind { get; set; }

    /// <summary>
    /// Gets or sets the name of the method used to read the property, if applicable.
    /// </summary>
    public string? ReadMethod { get; set; }

    /// <summary>
    /// Gets or sets the name of the method used to write the property, if applicable.
    /// </summary>
    public string? WriteMethod { get; set; }

    /// <summary>
    /// Gets or sets the name of the method used to read a vector field, if applicable.
    /// </summary>
    public string? VectorFieldRead { get; set; }

    /// <summary>
    /// Gets or sets the name of the method used to write a vector field, if applicable.
    /// </summary>
    public string? VectorFieldWrite { get; set; }

    /// <summary>
    /// Gets or sets the object type associated with the property, if applicable.
    /// </summary>
    public string? ObjectType { get; set; }

    /// <summary>
    /// Gets or sets the length of the vector, if the property represents a vector.
    /// </summary>
    public int? VectorLength { get; set; }

    /// <summary>
    /// Returns a string representation of the property symbol, including its name, type, and index.
    /// </summary>
    /// <returns>A string describing the property symbol.</returns>
    public override string ToString()
    {
        return PropertyKind is not null && MethodKind is not null
            ? $"Name: {Name}, Type: {Type}, Index: {Index}, PropertyKind: {PropertyKind}, MethodKind: {MethodKind}"
            : $"Name: {Name}, Type: {Type}, Index: {Index}";
    }
}
