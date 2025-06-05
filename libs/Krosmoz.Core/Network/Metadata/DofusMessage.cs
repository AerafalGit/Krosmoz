// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Core.Network.Metadata;

/// <summary>
/// Represents an abstract base class for network messages.
/// </summary>
public abstract class DofusMessage
{
    /// <summary>
    /// The number of bits to right-shift the length of the packet ID.
    /// </summary>
    public const byte BitRightShiftLenPacketId = 2;

    /// <summary>
    /// The bitmask used for extracting specific bits for the packet length.
    /// </summary>
    public const byte BitMask = 3;

    /// <summary>
    /// Gets the static protocol identifier for the message.
    /// </summary>
    public const uint StaticProtocolId = 0;

    /// <summary>
    /// Gets the protocol identifier for the specific message instance.
    /// </summary>
    public abstract uint ProtocolId { get; }

    /// <summary>
    /// Serializes the message data to the specified binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to serialize the message data to.</param>
    public virtual void Serialize(BigEndianWriter writer)
    {
    }

    /// <summary>
    /// Deserializes the message data from the specified binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the message data from.</param>
    public virtual void Deserialize(BigEndianReader reader)
    {
    }
}
