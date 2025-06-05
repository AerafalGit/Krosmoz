// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers;
using System.Diagnostics.CodeAnalysis;

namespace Krosmoz.Core.Network.Framing.Serialization;

/// <summary>
/// Defines an interface for decoding messages from a sequence of bytes.
/// </summary>
/// <typeparam name="TMessage">The type of the message to decode.</typeparam>
public interface IMessageDecoder<TMessage>
    where TMessage : class
{
    /// <summary>
    /// Attempts to decode a message from the provided input sequence.
    /// </summary>
    /// <param name="input">The input sequence of bytes to decode.</param>
    /// <param name="consumed">The position in the sequence that has been consumed.</param>
    /// <param name="examined">The position in the sequence that has been examined.</param>
    /// <param name="message">The decoded message if the operation succeeds; otherwise, null.</param>
    /// <returns><c>true</c> if the message was successfully decoded; otherwise, <c>false</c>.</returns>
    bool TryDecodeMessage(in ReadOnlySequence<byte> input, ref SequencePosition consumed, ref SequencePosition examined, [NotNullWhen(true)] out TMessage? message);
}
