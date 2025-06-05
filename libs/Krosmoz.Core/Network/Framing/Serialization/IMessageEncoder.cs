// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers;

namespace Krosmoz.Core.Network.Framing.Serialization;

/// <summary>
/// Defines an interface for encoding messages into a sequence of bytes.
/// </summary>
/// <typeparam name="TMessage">The type of the message to encode.</typeparam>
public interface IMessageEncoder<in TMessage>
    where TMessage : class
{
    /// <summary>
    /// Encodes a message into the provided output buffer.
    /// </summary>
    /// <param name="output">The buffer writer to which the encoded message will be written.</param>
    /// <param name="message">The message to encode.</param>
    void EncodeMessage(IBufferWriter<byte> output, TMessage message);
}
