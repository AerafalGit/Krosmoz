// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Serialization.D2O.Abstractions;

/// <summary>
/// Defines a factory interface for creating datacenter objects.
/// </summary>
public interface IDatacenterObjectFactory
{
    /// <summary>
    /// Creates a datacenter object based on the specified name.
    /// </summary>
    /// <param name="name">The name of the datacenter object to create.</param>
    /// <returns>An instance of <see cref="IDatacenterObject"/> representing the created object.</returns>
    IDatacenterObject Create(string name);
}
