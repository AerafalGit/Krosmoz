// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using Krosmoz.Core.IO.Binary;
using Krosmoz.Core.Network.Framing.Serialization;

namespace Krosmoz.Core.Network.Metadata;

/// <summary>
/// Decodes Dofus network messages from a sequence of bytes.
/// </summary>
public sealed class DofusMessageDecoder : IMessageDecoder<DofusMessage>
{
    private readonly IMessageFactory _messageFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="DofusMessageDecoder"/> class.
    /// </summary>
    /// <param name="messageFactory">The factory used to create Dofus messages.</param>
    public DofusMessageDecoder(IMessageFactory messageFactory)
    {
        _messageFactory = messageFactory;
    }

    /// <summary>
    /// Attempts to decode a Dofus message from the provided byte sequence.
    /// </summary>
    /// <param name="sequence">The sequence of bytes to decode the message from.</param>
    /// <param name="consumed">The position in the sequence up to which data has been consumed.</param>
    /// <param name="examined">The position in the sequence up to which data has been examined.</param>
    /// <param name="message">The decoded Dofus message, or null if decoding fails.</param>
    /// <returns><c>true</c> if the message was successfully decoded; otherwise, <c>false</c>.</returns>
    public bool TryDecodeMessage(in ReadOnlySequence<byte> sequence, ref SequencePosition consumed, ref SequencePosition examined, [NotNullWhen(true)] out DofusMessage? message)
    {
        message = null;

        var reader = new BigEndianReader(sequence);

        if (reader.Remaining < sizeof(ushort))
            return false;

        var header = reader.ReadUInt16();

        var messageId = (uint)(header >> DofusMessage.BitRightShiftLenPacketId);
        var messageSize = (byte)(header & DofusMessage.BitMask);

        message = _messageFactory.CreateMessage(messageId);

        if (reader.Remaining < messageSize)
            return false;

        var messageLength = 0;

        for (var i = messageSize - 1; i >= 0; i--)
            messageLength |= reader.ReadUInt8() << (i * sizeof(long));

        if (reader.Remaining < messageLength)
            return false;

        message.Deserialize(reader);

        consumed = sequence.GetPosition(reader.Position);
        examined = consumed;
        return true;
    }
}
