// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Tools.Protocol.Models;

/// <summary>
/// Represents a symbol for an enumeration.
/// </summary>
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
}
