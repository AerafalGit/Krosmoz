// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;

namespace Krosmoz.Tools.Protocol.Models;

/// <summary>
/// Represents a symbol for an enumeration property.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public sealed class EnumPropertySymbol
{
    /// <summary>
    /// Gets or sets the name of the enumeration property.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the type of the enumeration property.
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Gets or sets the value of the enumeration property.
    /// </summary>
    public required long Value { get; set; }

    /// <summary>
    /// Returns a string representation of the enumeration property symbol,
    /// including its name, type, and value.
    /// </summary>
    /// <returns>A string describing the enumeration property symbol.</returns>
    public override string ToString()
    {
        return $"Name: {Name}, Type: {Type}, Value: {Value}";
    }
}
