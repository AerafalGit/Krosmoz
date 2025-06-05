// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers;
using System.Buffers.Binary;
using System.Text;

namespace Krosmoz.Core.IO.Binary;

/// <summary>
/// A utility class for reading binary data in Big-Endian format.
/// </summary>
public sealed class BigEndianReader
{
    private const int Mask10000000 = 128;
    private const int Mask01111111 = 127;
    private const int ChunkBitSize = 7;

    private readonly ReadOnlyMemory<byte> _buffer;

    /// <summary>
    /// Gets the total length of the buffer.
    /// </summary>
    public int Length => _buffer.Length;

    /// <summary>
    /// Gets the current position in the buffer.
    /// </summary>
    public int Position { get; private set; }

    /// <summary>
    /// Gets the number of bytes remaining in the buffer.
    /// </summary>
    public int Remaining => Length - Position;

    /// <summary>
    /// Initializes a new instance of the <see cref="BigEndianReader"/> class from a stream.
    /// </summary>
    /// <param name="stream">The input stream to read from.</param>
    public BigEndianReader(Stream stream)
    {
        if (stream is not MemoryStream ms)
        {
            ms = new MemoryStream();
            stream.CopyTo(ms);
        }
        _buffer = ms.ToArray();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigEndianReader"/> class from a <see cref="ReadOnlySequence{T}"/>.
    /// </summary>
    /// <param name="buffer">The input buffer.</param>
    public BigEndianReader(ReadOnlySequence<byte> buffer)
    {
        _buffer = buffer.IsSingleSegment ? buffer.First : buffer.ToArray();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigEndianReader"/> class from a <see cref="ReadOnlyMemory{T}"/>.
    /// </summary>
    /// <param name="buffer">The input buffer.</param>
    public BigEndianReader(ReadOnlyMemory<byte> buffer)
    {
        _buffer = buffer;
    }

    /// <summary>
    /// Reads an unsigned 8-bit integer from the buffer.
    /// </summary>
    /// <returns>The read byte.</returns>
    public byte ReadUInt8()
    {
        return ReadSpan(sizeof(byte))[0];
    }

    /// <summary>
    /// Reads a signed 8-bit integer from the buffer.
    /// </summary>
    /// <returns>The read signed byte.</returns>
    public sbyte ReadInt8()
    {
        return (sbyte)ReadSpan(sizeof(byte))[0];
    }

    /// <summary>
    /// Reads a boolean value from the buffer.
    /// </summary>
    /// <returns><c>true</c> if the value is non-zero; otherwise, <c>false</c>.</returns>
    public bool ReadBoolean()
    {
        return ReadUInt8() is not 0;
    }

    /// <summary>
    /// Reads an unsigned 16-bit integer in Big-Endian format.
    /// </summary>
    /// <returns>The read unsigned 16-bit integer.</returns>
    public ushort ReadUInt16()
    {
        return BinaryPrimitives.ReadUInt16BigEndian(ReadSpan(sizeof(ushort)));
    }

    /// <summary>
    /// Reads a signed 16-bit integer in Big-Endian format.
    /// </summary>
    /// <returns>The read signed 16-bit integer.</returns>
    public short ReadInt16()
    {
        return BinaryPrimitives.ReadInt16BigEndian(ReadSpan(sizeof(short)));
    }

    /// <summary>
    /// Reads an unsigned 32-bit integer in Big-Endian format.
    /// </summary>
    /// <returns>The read unsigned 32-bit integer.</returns>
    public uint ReadUInt32()
    {
        return BinaryPrimitives.ReadUInt32BigEndian(ReadSpan(sizeof(uint)));
    }

    /// <summary>
    /// Reads a signed 32-bit integer in Big-Endian format.
    /// </summary>
    /// <returns>The read signed 32-bit integer.</returns>
    public int ReadInt32()
    {
        return BinaryPrimitives.ReadInt32BigEndian(ReadSpan(sizeof(int)));
    }

    /// <summary>
    /// Reads an unsigned 64-bit integer in Big-Endian format.
    /// </summary>
    /// <returns>The read unsigned 64-bit integer.</returns>
    public ulong ReadUInt64()
    {
        return BinaryPrimitives.ReadUInt64BigEndian(ReadSpan(sizeof(ulong)));
    }

    /// <summary>
    /// Reads a signed 64-bit integer in Big-Endian format.
    /// </summary>
    /// <returns>The read signed 64-bit integer.</returns>
    public long ReadInt64()
    {
        return BinaryPrimitives.ReadInt64BigEndian(ReadSpan(sizeof(long)));
    }

    /// <summary>
    /// Reads a single-precision floating-point number in Big-Endian format.
    /// </summary>
    /// <returns>The read float.</returns>
    public float ReadSingle()
    {
        return BinaryPrimitives.ReadSingleBigEndian(ReadSpan(sizeof(float)));
    }

    /// <summary>
    /// Reads a double-precision floating-point number in Big-Endian format.
    /// </summary>
    /// <returns>The read double.</returns>
    public double ReadDouble()
    {
        return BinaryPrimitives.ReadDoubleBigEndian(ReadSpan(sizeof(double)));
    }

    /// <summary>
    /// Reads a variable-length unsigned 16-bit integer from the buffer.
    /// </summary>
    /// <returns>The read unsigned 16-bit integer.</returns>
    public ushort ReadVarUInt16()
    {
        return (ushort)ReadVarInt16();
    }

    /// <summary>
    /// Reads a variable-length signed 16-bit integer from the buffer.
    /// </summary>
    /// <returns>The read signed 16-bit integer.</returns>
    public short ReadVarInt16()
    {
        ushort result = 0;
        byte b;
        short shift = 0;

        do
        {
            b = ReadUInt8();
            result |= (ushort)((b & Mask01111111) << shift);
            shift += ChunkBitSize;
        } while ((b & Mask10000000) is not 0);

        return (short)result;
    }

    /// <summary>
    /// Reads a variable-length unsigned 32-bit integer from the buffer.
    /// </summary>
    /// <returns>The read unsigned 32-bit integer.</returns>
    public uint ReadVarUInt32()
    {
        return (uint)ReadVarInt32();
    }

    /// <summary>
    /// Reads a variable-length signed 32-bit integer from the buffer.
    /// </summary>
    /// <returns>The read signed 32-bit integer.</returns>
    public int ReadVarInt32()
    {
        uint result = 0;
        byte b;
        var shift = 0;

        do
        {
            b = ReadUInt8();
            result |= (uint)(b & Mask01111111) << shift;
            shift += ChunkBitSize;
        } while ((b & Mask10000000) is not 0);

        return (int)result;
    }

    /// <summary>
    /// Reads a variable-length unsigned 64-bit integer from the buffer.
    /// </summary>
    /// <returns>The read unsigned 64-bit integer.</returns>
    public ulong ReadVarUInt64()
    {
        return (ulong)ReadVarInt64();
    }

    /// <summary>
    /// Reads a variable-length signed 64-bit integer from the buffer.
    /// </summary>
    /// <returns>The read signed 64-bit integer.</returns>
    public long ReadVarInt64()
    {
        ulong result = 0;
        byte b;
        var shift = 0;

        do
        {
            b = ReadUInt8();
            result |= (ulong)(b & Mask01111111) << shift;
            shift += ChunkBitSize;
        } while ((b & Mask10000000) is not 0);

        return (long)result;
    }

    /// <summary>
    /// Reads a specified number of bytes as a <see cref="ReadOnlyMemory{T}"/>.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <returns>The read memory.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the length is negative or greater than the remaining bytes in the buffer.
    /// </exception>
    public ReadOnlyMemory<byte> ReadMemory(int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(length, Remaining);

        var memory = _buffer.Slice(Position, length);
        Position += length;
        return memory;
    }

    /// <summary>
    /// Reads all remaining bytes in the buffer as a <see cref="ReadOnlyMemory{T}"/>.
    /// </summary>
    /// <returns>The read memory containing all remaining bytes.</returns>
    public ReadOnlyMemory<byte> ReadMemoryToEnd()
    {
        return ReadMemory(Remaining);
    }

    /// <summary>
    /// Reads a specified number of bytes as a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <returns>The read span.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the length is negative or greater than the remaining bytes in the buffer.
    /// </exception>
    public ReadOnlySpan<byte> ReadSpan(int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(length, Remaining);

        var span = _buffer.Span.Slice(Position, length);
        Position += length;
        return span;
    }

    /// <summary>
    /// Reads all remaining bytes in the buffer as a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <returns>The read span containing all remaining bytes.</returns>
    public ReadOnlySpan<byte> ReadSpanToEnd()
    {
        return ReadSpan(Remaining);
    }

    /// <summary>
    /// Reads a specified number of bytes from the buffer.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <returns>An array containing the read bytes.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the length is negative or greater than the remaining bytes in the buffer.
    /// </exception>
    public byte[] ReadBytes(int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(length, Remaining);

        var bytes = new byte[length];
        ReadSpan(length).CopyTo(bytes);
        Position += length;
        return bytes;
    }

    /// <summary>
    /// Reads all remaining bytes from the buffer.
    /// </summary>
    /// <returns>An array containing all remaining bytes.</returns>
    public byte[] ReadBytesToEnd()
    {
        return ReadBytes(Remaining);
    }

    /// <summary>
    /// Reads a UTF-8 encoded string of the specified length.
    /// </summary>
    /// <param name="length">The length of the string to read.</param>
    /// <returns>The read string.</returns>
    public string ReadUtfSpan(int length)
    {
        return Encoding.UTF8.GetString(ReadSpan(length));
    }

    /// <summary>
    /// Reads a UTF-8 encoded string prefixed with a 16-bit length.
    /// </summary>
    /// <returns>The read string.</returns>
    public string ReadUtfPrefixedLength16()
    {
        return ReadUtfSpan(ReadUInt16());
    }

    /// <summary>
    /// Reads a UTF-8 encoded string prefixed with a 32-bit length.
    /// </summary>
    /// <returns>The read string.</returns>
    public string ReadUtfPrefixedLength32()
    {
        return ReadUtfSpan(ReadInt32());
    }

    /// <summary>
    /// Moves the current position in the buffer.
    /// </summary>
    /// <param name="origin">The origin from which to calculate the new position.</param>
    /// <param name="offset">The offset to apply.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the origin is invalid.</exception>
    public void Seek(SeekOrigin origin, int offset)
    {
        Position = origin switch
        {
            SeekOrigin.Begin => offset,
            SeekOrigin.Current => Position + offset,
            SeekOrigin.End => Length - Math.Abs(offset),
            _ => throw new ArgumentOutOfRangeException(nameof(origin)),
        };
    }
}
