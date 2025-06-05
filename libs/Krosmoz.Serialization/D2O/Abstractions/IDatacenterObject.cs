// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.D2O.Abstractions;

/// <summary>
/// Represents a datacenter object that can be deserialized from a D2O file.
/// </summary>
public interface IDatacenterObject
{
    /// <summary>
    /// Gets the module name associated with the datacenter object.
    /// </summary>
    static abstract string ModuleName { get; }

    /// <summary>
    /// Serializes the datacenter object using the specified D2O class and binary writer.
    /// </summary>
    /// <param name="d2OClass">The D2O class containing metadata for serialization.</param>
    /// <param name="writer">The binary writer to write data to.</param>
    void Serialize(D2OClass d2OClass, BigEndianWriter writer);

    /// <summary>
    /// Deserializes the datacenter object using the specified D2O class and binary reader.
    /// </summary>
    /// <param name="d2OClass">The D2O class containing metadata for deserialization.</param>
    /// <param name="reader">The binary reader to read data from.</param>
    void Deserialize(D2OClass d2OClass, BigEndianReader reader);
}
