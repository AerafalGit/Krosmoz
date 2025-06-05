// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;

namespace Krosmoz.Tools.Protocol.Models;

/// <summary>
/// Represents an ActionScript class.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public sealed class ClassSymbol
{
    /// <summary>
    /// Gets or sets the metadata associated with the ActionScript class.
    /// </summary>
    public required SymbolMetadata Metadata { get; set; }

    /// <summary>
    /// Gets or sets the protocol identifier for the ActionScript class.
    /// </summary>
    public required uint ProtocolId { get; set; }

    /// <summary>
    /// Gets or sets the list of namespaces or libraries used by the ActionScript class.
    /// </summary>
    public required List<string> Usings { get; set; }

    /// <summary>
    /// Gets or sets the dictionary of properties defined in the ActionScript class.
    /// The key represents the property name, and the value represents the property details.
    /// </summary>
    public required Dictionary<string, PropertySymbol> Properties { get; set; }

    /// <summary>
    /// Returns a string representation of the ActionScript class, including its metadata,
    /// protocol identifier, the count of namespaces or libraries used, and the count of properties.
    /// </summary>
    /// <returns>A string describing the ActionScript class.</returns>
    public override string ToString()
    {
        return $"{Metadata}, ProtocolId: {ProtocolId}, Usings: {Usings.Count}, Properties: {Properties.Count}";
    }
}
