// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Serialization.D2O.Abstractions;

/// <summary>
/// Represents a datacenter object with a static abstract property for the module name.
/// </summary>
public interface IDatacenterObject
{
    /// <summary>
    /// Gets the name of the module associated with the datacenter object.
    /// </summary>
    static abstract string ModuleName { get; }
}
