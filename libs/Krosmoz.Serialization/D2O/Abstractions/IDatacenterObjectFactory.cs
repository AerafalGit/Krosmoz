// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Serialization.D2O.Abstractions;

/// <summary>
/// Defines a factory for creating instances of datacenter objects.
/// </summary>
public interface IDatacenterObjectFactory
{
    /// <summary>
    /// Creates an instance of a datacenter object based on the specified class name.
    /// </summary>
    /// <param name="className">The name of the class to create an instance of.</param>
    /// <returns>An instance of the datacenter object.</returns>
    IDatacenterObject CreateInstance(string className);
}

/// <summary>
/// Represents a null implementation of the <see cref="IDatacenterObjectFactory"/> interface.
/// This factory throws a <see cref="NotImplementedException"/> for all operations.
/// </summary>
public sealed class NullDatacenterObjectFactory : IDatacenterObjectFactory
{
    /// <summary>
    /// Throws a <see cref="NotImplementedException"/> indicating that the factory is not implemented
    /// for the specified class name.
    /// </summary>
    /// <param name="className">The name of the class for which an instance was requested.</param>
    /// <returns>None. This method always throws an exception.</returns>
    /// <exception cref="NotImplementedException">Thrown to indicate that the factory is not implemented.</exception>
    public IDatacenterObject CreateInstance(string className)
    {
        throw new Exception($"Datacenter object factory not implemented for class: {className}.");
    }
}
