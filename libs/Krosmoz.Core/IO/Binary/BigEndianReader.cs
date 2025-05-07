// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers.Binary;
using System.Text;

namespace Krosmoz.Core.IO.Binary;

/// <summary>
/// Represents an abstract base class for reading binary data from a buffer or stream.
/// </summary>
public abstract class BigEndianReader : IDisposable
{
    /// <summary>
    /// Gets the total length of the buffer.
    /// </summary>
    public abstract int Length { get; }

    /// <summary>
    /// Gets or sets the current position within the buffer.
    /// </summary>
    public abstract int Position { get; protected set; }

    /// <summary>
    /// Gets the number of bytes remaining to be read from the buffer.
    /// </summary>
    public int Remaining =>
        Length - Position;

    /// <summary>
    /// Reads a single byte from the buffer.
    /// </summary>
    /// <returns>The byte that was read.</returns>
    public byte ReadByte()
    {
        return ReadSpan(sizeof(byte))[0];
    }

    /// <summary>
    /// Reads a signed byte from the buffer.
    /// </summary>
    /// <returns>The signed byte that was read.</returns>
    public sbyte ReadSByte()
    {
        return (sbyte)ReadSpan(sizeof(sbyte))[0];
    }

    /// <summary>
    /// Reads a boolean value from the buffer.
    /// </summary>
    /// <returns>True if the value is 1; otherwise, false.</returns>
    public bool ReadBoolean()
    {
        return ReadSpan(sizeof(bool))[0] is 1;
    }

    /// <summary>
    /// Reads an unsigned 16-bit integer from the buffer.
    /// </summary>
    /// <returns>The unsigned 16-bit integer that was read.</returns>
    public ushort ReadUShort()
    {
        return BinaryPrimitives.ReadUInt16BigEndian(ReadSpan(sizeof(ushort)));
    }

    /// <summary>
    /// Reads a signed 16-bit integer from the buffer.
    /// </summary>
    /// <returns>The signed 16-bit integer that was read.</returns>
    public short ReadShort()
    {
        return BinaryPrimitives.ReadInt16BigEndian(ReadSpan(sizeof(short)));
    }

    /// <summary>
    /// Reads an unsigned 32-bit integer from the buffer.
    /// </summary>
    /// <returns>The unsigned 32-bit integer that was read.</returns>
    public uint ReadUInt()
    {
        return BinaryPrimitives.ReadUInt32BigEndian(ReadSpan(sizeof(uint)));
    }

    /// <summary>
    /// Reads a signed 32-bit integer from the buffer.
    /// </summary>
    /// <returns>The signed 32-bit integer that was read.</returns>
    public int ReadInt()
    {
        return BinaryPrimitives.ReadInt32BigEndian(ReadSpan(sizeof(int)));
    }

    /// <summary>
    /// Reads an unsigned 64-bit integer from the buffer.
    /// </summary>
    /// <returns>The unsigned 64-bit integer that was read.</returns>
    public ulong ReadULong()
    {
        return BinaryPrimitives.ReadUInt64BigEndian(ReadSpan(sizeof(ulong)));
    }

    /// <summary>
    /// Reads a signed 64-bit integer from the buffer.
    /// </summary>
    /// <returns>The signed 64-bit integer that was read.</returns>
    public long ReadLong()
    {
        return BinaryPrimitives.ReadInt64BigEndian(ReadSpan(sizeof(long)));
    }

    /// <summary>
    /// Reads a single-precision floating-point value from the buffer.
    /// </summary>
    /// <returns>The single-precision floating-point value that was read.</returns>
    public float ReadSingle()
    {
        return BinaryPrimitives.ReadSingleBigEndian(ReadSpan(sizeof(float)));
    }

    /// <summary>
    /// Reads a double-precision floating-point value from the buffer.
    /// </summary>
    /// <returns>The double-precision floating-point value that was read.</returns>
    public double ReadDouble()
    {
        return BinaryPrimitives.ReadDoubleBigEndian(ReadSpan(sizeof(double)));
    }

    /// <summary>
    /// Reads a specified number of bytes as a read-only memory block.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <returns>A read-only memory block containing the bytes that were read.</returns>
    public abstract ReadOnlyMemory<byte> ReadMemory(int length);

    /// <summary>
    /// Reads a specified number of bytes as a read-only span.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <returns>A read-only span containing the bytes that were read.</returns>
    public abstract ReadOnlySpan<byte> ReadSpan(int length);

    /// <summary>
    /// Reads a specified number of bytes as a byte array.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <returns>A byte array containing the bytes that were read.</returns>
    public abstract byte[] ReadBytes(int length);

    /// <summary>
    /// Reads a specified number of bytes and decodes them as a UTF-8 string.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <returns>The UTF-8 string that was decoded from the bytes.</returns>
    public string ReadUtfBytes(int length)
    {
        return Encoding.UTF8.GetString(ReadSpan(length));
    }

    /// <summary>
    /// Reads a UTF-8 string prefixed with a 16-bit length.
    /// </summary>
    /// <returns>The UTF-8 string that was read.</returns>
    public string ReadUtfLengthPrefixed16()
    {
        return Encoding.UTF8.GetString(ReadSpan(ReadUShort()));
    }

    /// <summary>
    /// Reads a UTF-8 string prefixed with a 32-bit length.
    /// </summary>
    /// <returns>The UTF-8 string that was read.</returns>
    public string ReadUtfLengthPrefixed32()
    {
        return Encoding.UTF8.GetString(ReadSpan(ReadInt()));
    }

    /// <summary>
    /// Attempts to read a single byte from the buffer.
    /// </summary>
    /// <param name="value">When this method returns, contains the byte that was read, if the read was successful.</param>
    /// <returns>True if the byte was successfully read; otherwise, false.</returns>
    public bool TryReadByte(out byte value)
    {
        if (TryReadSpan(sizeof(byte), out var span))
        {
            value = span[0];
            return true;
        }

        value = 0;
        return false;
    }

    /// <summary>
    /// Attempts to read a signed byte from the buffer.
    /// </summary>
    /// <param name="value">When this method returns, contains the signed byte that was read, if the read was successful.</param>
    /// <returns>True if the signed byte was successfully read; otherwise, false.</returns>
    public bool TryReadSByte(out sbyte value)
    {
        if (TryReadSpan(sizeof(sbyte), out var span))
        {
            value = (sbyte)span[0];
            return true;
        }

        value = 0;
        return false;
    }

    /// <summary>
    /// Attempts to read a boolean value from the buffer.
    /// </summary>
    /// <param name="value">When this method returns, contains the boolean value that was read, if the read was successful.</param>
    /// <returns>True if the boolean value was successfully read; otherwise, false.</returns>
    public bool TryReadBoolean(out bool value)
    {
        if (TryReadSpan(sizeof(bool), out var span))
        {
            value = span[0] is 1;
            return true;
        }

        value = false;
        return false;
    }

    /// <summary>
    /// Attempts to read an unsigned 16-bit integer from the buffer.
    /// </summary>
    /// <param name="value">When this method returns, contains the unsigned 16-bit integer that was read, if the read was successful.</param>
    /// <returns>True if the unsigned 16-bit integer was successfully read; otherwise, false.</returns>
    public bool TryReadUShort(out ushort value)
    {
        if (TryReadSpan(sizeof(ushort), out var span))
        {
            value = BinaryPrimitives.ReadUInt16BigEndian(span);
            return true;
        }

        value = 0;
        return false;
    }

    /// <summary>
    /// Attempts to read a signed 16-bit integer from the buffer.
    /// </summary>
    /// <param name="value">When this method returns, contains the signed 16-bit integer that was read, if the read was successful.</param>
    /// <returns>True if the signed 16-bit integer was successfully read; otherwise, false.</returns>
    public bool TryReadShort(out short value)
    {
        if (TryReadSpan(sizeof(short), out var span))
        {
            value = BinaryPrimitives.ReadInt16BigEndian(span);
            return true;
        }

        value = 0;
        return false;
    }

    /// <summary>
    /// Attempts to read an unsigned 32-bit integer from the buffer.
    /// </summary>
    /// <param name="value">When this method returns, contains the unsigned 32-bit integer that was read, if the read was successful.</param>
    /// <returns>True if the unsigned 32-bit integer was successfully read; otherwise, false.</returns>
    public bool TryReadUInt(out uint value)
    {
        if (TryReadSpan(sizeof(uint), out var span))
        {
            value = BinaryPrimitives.ReadUInt32BigEndian(span);
            return true;
        }

        value = 0;
        return false;
    }

    /// <summary>
    /// Attempts to read a signed 32-bit integer from the buffer.
    /// </summary>
    /// <param name="value">When this method returns, contains the signed 32-bit integer that was read, if the read was successful.</param>
    /// <returns>True if the signed 32-bit integer was successfully read; otherwise, false.</returns>
    public bool TryReadInt(out int value)
    {
        if (TryReadSpan(sizeof(int), out var span))
        {
            value = BinaryPrimitives.ReadInt32BigEndian(span);
            return true;
        }

        value = 0;
        return false;
    }

    /// <summary>
    /// Attempts to read an unsigned 64-bit integer from the buffer.
    /// </summary>
    /// <param name="value">When this method returns, contains the unsigned 64-bit integer that was read, if the read was successful.</param>
    /// <returns>True if the unsigned 64-bit integer was successfully read; otherwise, false.</returns>
    public bool TryReadULong(out ulong value)
    {
        if (TryReadSpan(sizeof(ulong), out var span))
        {
            value = BinaryPrimitives.ReadUInt64BigEndian(span);
            return true;
        }

        value = 0;
        return false;
    }

    /// <summary>
    /// Attempts to read a signed 64-bit integer from the buffer.
    /// </summary>
    /// <param name="value">When this method returns, contains the signed 64-bit integer that was read, if the read was successful.</param>
    /// <returns>True if the signed 64-bit integer was successfully read; otherwise, false.</returns>
    public bool TryReadLong(out long value)
    {
        if (TryReadSpan(sizeof(long), out var span))
        {
            value = BinaryPrimitives.ReadInt64BigEndian(span);
            return true;
        }

        value = 0;
        return false;
    }

    /// <summary>
    /// Attempts to read a single-precision floating-point value from the buffer.
    /// </summary>
    /// <param name="value">When this method returns, contains the single-precision floating-point value that was read, if the read was successful.</param>
    /// <returns>True if the single-precision floating-point value was successfully read; otherwise, false.</returns>
    public bool TryReadSingle(out float value)
    {
        if (TryReadSpan(sizeof(float), out var span))
        {
            value = BinaryPrimitives.ReadSingleBigEndian(span);
            return true;
        }

        value = 0;
        return false;
    }

    /// <summary>
    /// Attempts to read a double-precision floating-point value from the buffer.
    /// </summary>
    /// <param name="value">When this method returns, contains the double-precision floating-point value that was read, if the read was successful.</param>
    /// <returns>True if the double-precision floating-point value was successfully read; otherwise, false.</returns>
    public bool TryReadDouble(out double value)
    {
        if (TryReadSpan(sizeof(double), out var span))
        {
            value = BinaryPrimitives.ReadDoubleBigEndian(span);
            return true;
        }

        value = 0;
        return false;
    }

    /// <summary>
    /// Attempts to read a specified number of bytes as a read-only memory block.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="value">When this method returns, contains the read-only memory block that was read, if the read was successful.</param>
    /// <returns>True if the memory block was successfully read; otherwise, false.</returns>
    public bool TryReadMemory(int length, out ReadOnlyMemory<byte> value)
    {
        if (Remaining >= length)
        {
            value = ReadMemory(length);
            return true;
        }

        value = ReadOnlyMemory<byte>.Empty;
        return false;
    }

    /// <summary>
    /// Attempts to read a specified number of bytes as a read-only span.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="value">When this method returns, contains the read-only span that was read, if the read was successful.</param>
    /// <returns>True if the span was successfully read; otherwise, false.</returns>
    public bool TryReadSpan(int length, out ReadOnlySpan<byte> value)
    {
        if (TryReadMemory(length, out var memory))
        {
            value = memory.Span;
            return true;
        }

        value = ReadOnlySpan<byte>.Empty;
        return false;
    }

    /// <summary>
    /// Attempts to read a specified number of bytes as a byte array.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="value">When this method returns, contains the byte array that was read, if the read was successful.</param>
    /// <returns>True if the byte array was successfully read; otherwise, false.</returns>
    public bool TryReadBytes(int length, out byte[] value)
    {
        if (TryReadMemory(length, out var memory))
        {
            value = memory.ToArray();
            return true;
        }

        value = [];
        return false;
    }

    /// <summary>
    /// Attempts to read a specified number of bytes and decode them as a UTF-8 string.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="value">When this method returns, contains the UTF-8 string that was decoded, if the read was successful.</param>
    /// <returns>True if the UTF-8 string was successfully read; otherwise, false.</returns>
    public bool TryReadUtfBytes(int length, out string value)
    {
        if (TryReadSpan(length, out var span))
        {
            value = Encoding.UTF8.GetString(span);
            return true;
        }

        value = string.Empty;
        return false;
    }

    /// <summary>
    /// Attempts to read a UTF-8 string prefixed with a 16-bit length.
    /// </summary>
    /// <param name="value">When this method returns, contains the UTF-8 string that was read, if the read was successful.</param>
    /// <returns>True if the UTF-8 string was successfully read; otherwise, false.</returns>
    public bool TryReadUtfLengthPrefixed16(out string value)
    {
        if (TryReadUShort(out var length) && TryReadUtfBytes(length, out var utf))
        {
            value = utf;
            return true;
        }

        value = string.Empty;
        return false;
    }

    /// <summary>
    /// Attempts to read a UTF-8 string prefixed with a 32-bit length.
    /// </summary>
    /// <param name="value">When this method returns, contains the UTF-8 string that was read, if the read was successful.</param>
    /// <returns>True if the UTF-8 string was successfully read; otherwise, false.</returns>
    public bool TryReadUtfLengthPrefixed32(out string value)
    {
        if (TryReadInt(out var length) && TryReadUtfBytes(length, out var utf))
        {
            value = utf;
            return true;
        }

        value = string.Empty;
        return false;
    }

    /// <summary>
    /// Moves the current position within the buffer by a specified offset.
    /// </summary>
    /// <param name="origin">The reference point for the offset.</param>
    /// <param name="offset">The number of bytes to move the position by.</param>
    public void Seek(SeekOrigin origin, int offset)
    {
        Position = origin switch
        {
            SeekOrigin.Begin => offset,
            SeekOrigin.Current => Position + offset,
            SeekOrigin.End => Length - Math.Abs(offset),
            _ => throw new ArgumentOutOfRangeException(nameof(origin))
        };
    }

    /// <summary>
    /// Releases all resources used by the current instance of the <see cref="BigEndianReader"/> class.
    /// </summary>
    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
