// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;

namespace Krosmoz.Tools.Protocol.Models;

/// <summary>
/// Represents a symbol for an enumeration.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public sealed class EnumSymbol
{
    /// <summary>
    /// Gets or sets the metadata associated with the enumeration symbol.
    /// </summary>
    public required SymbolMetadata Metadata { get; set; }

    /// <summary>
    /// Gets or sets the list of properties associated with the enumeration symbol.
    /// Each property represents a specific value in the enumeration.
    /// </summary>
    public required List<EnumPropertySymbol> Properties { get; set; }

    /// <summary>
    /// Returns a string representation of the enumeration symbol, including its metadata
    /// and the count of associated properties.
    /// </summary>
    /// <returns>A string describing the enumeration symbol.</returns>
    public override string ToString()
    {
        return $"{Metadata}, Properties: {Properties.Count}";
    }
}
