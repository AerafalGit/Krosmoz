// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Tools.Protocol.Models;

/// <summary>
/// Represents a symbol for an enumeration property.
/// </summary>
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
}
