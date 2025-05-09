// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Serialization.D2O;

namespace Krosmoz.Tools.Protocol.Models;

/// <summary>
/// Represents a datacenter symbol containing information about D2O classes.
/// </summary>
public sealed class DatacenterSymbol
{
    /// <summary>
    /// Gets or sets the collection of D2O classes associated with the datacenter symbol.
    /// </summary>
    public required Dictionary<string, Dictionary<int, D2OClass>> D2OClasses { get; set; }

    /// <summary>
    /// Gets or sets the name of the module associated with the datacenter symbol.
    /// </summary>
    public required string ModuleName { get; set; }

    /// <summary>
    /// Gets or sets the primary D2O class associated with the datacenter symbol.
    /// </summary>
    public required D2OClass D2OClass { get; set; }
}
