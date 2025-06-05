// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Core.Network.Metadata;

/// <summary>
/// Represents an abstract base class for network types.
/// </summary>
public abstract class DofusType
{
    /// <summary>
    /// Gets the static protocol identifier for the network type.
    /// </summary>
    public const ushort StaticProtocolId = 0;

    /// <summary>
    /// Gets the protocol identifier for the specific network type instance.
    /// </summary>
    public abstract ushort ProtocolId { get; }

    /// <summary>
    /// Serializes the network type data to the specified binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to serialize the network type data to.</param>
    public virtual void Serialize(BigEndianWriter writer)
    {
    }

    /// <summary>
    /// Deserializes the network type data from the specified binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the network type data from.</param>
    public virtual void Deserialize(BigEndianReader reader)
    {
    }
}
