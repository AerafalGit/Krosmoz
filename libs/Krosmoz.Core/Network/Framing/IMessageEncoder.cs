// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers;

namespace Krosmoz.Core.Network.Framing;

/// <summary>
/// Defines a contract for encoding network messages into a sequence of bytes.
/// </summary>
/// <typeparam name="TMessage">The type of the network message to encode.</typeparam>
public interface IMessageEncoder<in TMessage>
    where TMessage : class
{
    /// <summary>
    /// Encodes a network message into the provided buffer writer.
    /// </summary>
    /// <param name="writer">The buffer writer to write the encoded message to.</param>
    /// <param name="message">The network message to encode.</param>
    void EncodeMessage(IBufferWriter<byte> writer, TMessage message);
}
