// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.D2O.Abstractions;

/// <summary>
/// Represents a datacenter object with a static abstract property for the module name
/// and a method for deserializing the object from a D2O class and binary reader.
/// </summary>
/// <typeparam name="T">
/// The type of the datacenter object, which must implement <see cref="IDatacenterObject{T}"/>.
/// </typeparam>
public interface IDatacenterObject<out T>
    where T : class, IDatacenterObject<T>
{
    /// <summary>
    /// Gets the name of the module associated with the datacenter object.
    /// </summary>
    static abstract string ModuleName { get; }

    /// <summary>
    /// Deserializes the datacenter object from the specified D2O class and binary reader.
    /// </summary>
    /// <param name="d2OClass">The D2O class containing metadata for deserialization.</param>
    /// <param name="reader">The binary reader used to read the object's data.</param>
    /// <returns>An instance of the datacenter object.</returns>
    static abstract T Deserialize(D2OClass d2OClass, BigEndianReader reader);
}
