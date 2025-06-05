// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers;
using Krosmoz.Core.IO.Binary;
using Krosmoz.Core.Network.Framing.Serialization;

namespace Krosmoz.Core.Network.Metadata;

/// <summary>
/// Encodes Dofus network messages into a sequence of bytes.
/// </summary>
public sealed class DofusMessageEncoder : IMessageEncoder<DofusMessage>
{
    /// <summary>
    /// Encodes a Dofus message into the provided buffer writer.
    /// </summary>
    /// <param name="writer">The buffer writer to write the encoded message to.</param>
    /// <param name="message">The Dofus message to encode.</param>
    public void EncodeMessage(IBufferWriter<byte> writer, DofusMessage message)
    {
        using var contentWriter = new BigEndianWriter();

        message.Serialize(contentWriter);

        var contentBuffer = contentWriter.ToSpan();

        byte messageSize = contentBuffer.Length switch
        {
            > ushort.MaxValue => 3,
            > byte.MaxValue => 2,
            > 0 => 1,
            _ => 0
        };

        var header = (ushort)((message.ProtocolId << DofusMessage.BitRightShiftLenPacketId) | messageSize);

        using var messageWriter = new BigEndianWriter();

        messageWriter.WriteUInt16(header);

        for (var i = messageSize - 1; i >= 0; i--)
            messageWriter.WriteUInt8((byte)((contentBuffer.Length >> (i * sizeof(long))) & byte.MaxValue));

        if (contentBuffer.Length > 0)
            messageWriter.WriteSpan(contentBuffer);

        writer.Write(messageWriter.ToSpan());
    }
}
