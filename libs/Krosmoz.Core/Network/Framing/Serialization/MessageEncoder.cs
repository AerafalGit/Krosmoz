// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers;
using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Core.Network.Metadata;

/// <summary>
/// Encodes Dofus network messages into a sequence of bytes.
/// </summary>
public static class MessageEncoder
{
    private const byte DefaultHeaderSize = 2;
    private const byte DefaultMessageLenTypeSize = 3;
    private const byte MessageHeaderWithSize = DefaultHeaderSize + DefaultMessageLenTypeSize;

    /// <summary>
    /// Encodes a Dofus message into the provided buffer writer.
    /// </summary>
    /// <param name="writer">The buffer writer to write the encoded message to.</param>
    /// <param name="message">The Dofus message to encode.</param>
    /// <returns>The length of the encoded message.</returns>
    public static int EncodeMessage(IBufferWriter<byte> writer, DofusMessage message)
    {
        using var msgWriter = new BigEndianWriter();

        var header = (ushort)((message.ProtocolId << DofusMessage.BitRightShiftLenPacketId) | DefaultMessageLenTypeSize);

        msgWriter.WriteUInt16(header);

        msgWriter.Seek(SeekOrigin.Current, DefaultMessageLenTypeSize);

        message.Serialize(msgWriter);

        var messageLength = msgWriter.Position - MessageHeaderWithSize;

        msgWriter.Seek(SeekOrigin.Begin, DefaultHeaderSize);

        for (var i = DefaultMessageLenTypeSize - 1; i >= 0; i--)
            msgWriter.WriteUInt8((byte)((messageLength >> (i * sizeof(long))) & byte.MaxValue));

        writer.Write(msgWriter.ToSpan());

        return messageLength;
    }
}
