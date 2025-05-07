// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Tools.Protocol.Models;

/// <summary>
/// Represents metadata for an ActionScript class.
/// </summary>
public sealed class SymbolMetadata
{
    /// <summary>
    /// Gets or sets the namespace of the ActionScript class.
    /// </summary>
    public required string Namespace { get; set; }

    /// <summary>
    /// Gets or sets the name of the ActionScript class.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the name of the parent class of the ActionScript class.
    /// </summary>
    public required string ParentName { get; set; }

    /// <summary>
    /// Gets or sets the source file of the ActionScript class.
    /// </summary>
    public required string Source { get; set; }

    /// <summary>
    /// Gets or sets the type of the ActionScript class.
    /// </summary>
    public required SymbolKind Kind { get; set; }
}
