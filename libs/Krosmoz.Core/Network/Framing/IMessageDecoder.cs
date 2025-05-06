// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using Krosmoz.Core.Network.Metadata;

namespace Krosmoz.Core.Network.Framing;

/// <summary>
/// Defines a contract for decoding network messages from a sequence of bytes.
/// </summary>
/// <typeparam name="TMessage">The type of the network message to decode.</typeparam>
public interface IMessageDecoder<TMessage>
    where TMessage : NetworkMessage
{
    /// <summary>
    /// Attempts to decode a network message from the provided byte sequence.
    /// </summary>
    /// <param name="sequence">The sequence of bytes to decode the message from.</param>
    /// <param name="consumed">The position in the sequence up to which data has been consumed.</param>
    /// <param name="examined">The position in the sequence up to which data has been examined.</param>
    /// <param name="message">
    /// When this method returns <c>true</c>, contains the decoded message; otherwise, <c>null</c>.
    /// </param>
    /// <returns>
    /// <c>true</c> if a message was successfully decoded; otherwise, <c>false</c>.
    /// </returns>
    bool TryDecodeMessage(in ReadOnlySequence<byte> sequence, ref SequencePosition consumed, ref SequencePosition examined, [NotNullWhen(true)] out TMessage? message);
}
