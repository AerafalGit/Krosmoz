// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers;
using System.Buffers.Binary;
using System.Text;

namespace Krosmoz.Core.IO.Binary;

/// <summary>
/// A utility class for writing binary data in Big-Endian format.
/// </summary>
public sealed class BigEndianWriter : IDisposable
{
    private const int Mask10000000 = 128;
    private const int Mask01111111 = 127;
    private const int ChunkBitSize = 7;
    private const int Int32Bits = 32;

    private byte[] _buffer;
    private int _position;

    /// <summary>
    /// Gets the total length of the buffer.
    /// </summary>
    public int Length =>
        _buffer.Length;

    /// <summary>
    /// Gets the current position in the buffer.
    /// </summary>
    public int Position
    {
        get => _position;
        private set
        {
            _position = value;

            if (MaxPosition < value)
                MaxPosition = value;
        }
    }

    /// <summary>
    /// Gets the maximum position reached in the buffer.
    /// </summary>
    public int MaxPosition { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigEndianWriter"/> class with an empty buffer.
    /// </summary>
    public BigEndianWriter()
    {
        _buffer = [];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigEndianWriter"/> class with a specified capacity.
    /// </summary>
    /// <param name="capacity">The initial capacity of the buffer.</param>
    public BigEndianWriter(int capacity)
    {
        _buffer = ArrayPool<byte>.Shared.Rent(capacity);
    }

    /// <summary>
    /// Writes an unsigned 8-bit integer to the buffer.
    /// </summary>
    /// <param name="value">The unsigned 8-bit integer to write.</param>
    public void WriteUInt8(byte value)
    {
        GetSpan(sizeof(byte))[0] = value;
    }

    /// <summary>
    /// Writes a signed 8-bit integer to the buffer.
    /// </summary>
    /// <param name="value">The signed 8-bit integer to write.</param>
    public void WriteInt8(sbyte value)
    {
        GetSpan(sizeof(byte))[0] = (byte)value;
    }

    /// <summary>
    /// Writes a boolean value to the buffer.
    /// </summary>
    /// <param name="value">The boolean value to write. <c>true</c> is written as 1, <c>false</c> as 0.</param>
    public void WriteBoolean(bool value)
    {
        GetSpan(sizeof(byte))[0] = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// Writes an unsigned 16-bit integer in Big-Endian format to the buffer.
    /// </summary>
    /// <param name="value">The unsigned 16-bit integer to write.</param>
    public void WriteUInt16(ushort value)
    {
        BinaryPrimitives.WriteUInt16BigEndian(GetSpan(sizeof(ushort)), value);
    }

    /// <summary>
    /// Writes a signed 16-bit integer in Big-Endian format to the buffer.
    /// </summary>
    /// <param name="value">The signed 16-bit integer to write.</param>
    public void WriteInt16(short value)
    {
        BinaryPrimitives.WriteInt16BigEndian(GetSpan(sizeof(short)), value);
    }

    /// <summary>
    /// Writes an unsigned 32-bit integer in Big-Endian format to the buffer.
    /// </summary>
    /// <param name="value">The unsigned 32-bit integer to write.</param>
    public void WriteUInt32(uint value)
    {
        BinaryPrimitives.WriteUInt32BigEndian(GetSpan(sizeof(uint)), value);
    }

    /// <summary>
    /// Writes a signed 32-bit integer in Big-Endian format to the buffer.
    /// </summary>
    /// <param name="value">The signed 32-bit integer to write.</param>
    public void WriteInt32(int value)
    {
        BinaryPrimitives.WriteInt32BigEndian(GetSpan(sizeof(int)), value);
    }

    /// <summary>
    /// Writes an unsigned 64-bit integer in Big-Endian format to the buffer.
    /// </summary>
    /// <param name="value">The unsigned 64-bit integer to write.</param>
    public void WriteUInt64(ulong value)
    {
        BinaryPrimitives.WriteUInt64BigEndian(GetSpan(sizeof(ulong)), value);
    }

    /// <summary>
    /// Writes a signed 64-bit integer in Big-Endian format to the buffer.
    /// </summary>
    /// <param name="value">The signed 64-bit integer to write.</param>
    public void WriteInt64(long value)
    {
        BinaryPrimitives.WriteInt64BigEndian(GetSpan(sizeof(long)), value);
    }

    /// <summary>
    /// Writes a single-precision floating-point number in Big-Endian format to the buffer.
    /// </summary>
    /// <param name="value">The single-precision floating-point number to write.</param>
    public void WriteSingle(float value)
    {
        BinaryPrimitives.WriteSingleBigEndian(GetSpan(sizeof(float)), value);
    }

    /// <summary>
    /// Writes a double-precision floating-point number in Big-Endian format to the buffer.
    /// </summary>
    /// <param name="value">The double-precision floating-point number to write.</param>
    public void WriteDouble(double value)
    {
        BinaryPrimitives.WriteDoubleBigEndian(GetSpan(sizeof(double)), value);
    }

    /// <summary>
    /// Writes an unsigned 16-bit integer in variable-length format to the buffer.
    /// </summary>
    /// <param name="value">The unsigned 16-bit integer to write.</param>
    public void WriteVarUInt16(ushort value)
    {
        WriteVarInt16(unchecked((short)value));
    }

    /// <summary>
    /// Writes a signed 16-bit integer in variable-length format to the buffer.
    /// </summary>
    /// <param name="value">The signed 16-bit integer to write.</param>
    public void WriteVarInt16(short value)
    {
        var v = unchecked((ushort)value);

        if (v <= Mask01111111)
        {
            WriteUInt8((byte)v);
            return;
        }

        while (v is not 0)
        {
            var b = (byte)(v & Mask01111111);

            v >>= ChunkBitSize;

            if (v > 0)
                b |= Mask10000000;

            WriteUInt8(b);
        }
    }

    /// <summary>
    /// Writes an unsigned 32-bit integer in variable-length format to the buffer.
    /// </summary>
    /// <param name="value">The unsigned 32-bit integer to write.</param>
    public void WriteVarUInt32(uint value)
    {
        WriteVarInt32(unchecked((int)value));
    }

    /// <summary>
    /// Writes a signed 32-bit integer in variable-length format to the buffer.
    /// </summary>
    /// <param name="value">The signed 32-bit integer to write.</param>
    public void WriteVarInt32(int value)
    {
        var v = unchecked((uint)value);

        if (v <= Mask01111111)
        {
            WriteUInt8((byte)v);
            return;
        }

        while (v is not 0)
        {
            var b = (byte)(v & Mask01111111);

            v >>= ChunkBitSize;

            if (v > 0)
                b |= Mask10000000;

            WriteUInt8(b);
        }
    }

    /// <summary>
    /// Writes an unsigned 64-bit integer in variable-length format to the buffer.
    /// </summary>
    /// <param name="value">The unsigned 64-bit integer to write.</param>
    public void WriteVarUInt64(ulong value)
    {
        WriteVarInt64(unchecked((long)value));
    }

    /// <summary>
    /// Writes a signed 64-bit integer in variable-length format to the buffer.
    /// </summary>
    /// <param name="value">The signed 64-bit integer to write.</param>
    public void WriteVarInt64(long value)
    {
        var c = unchecked((ulong)value);

        if (c >> Int32Bits is 0)
        {
            WriteVarInt32((int)value);
            return;
        }

        var low  = c & uint.MaxValue;
        var high = c >> Int32Bits;

        for (var i = 0; i < sizeof(int); i++)
        {
            WriteUInt8((byte)((low & Mask01111111) | Mask10000000));
            low >>= ChunkBitSize;
        }

        if ((high & (uint.MaxValue - ChunkBitSize)) is 0)
            WriteUInt8((byte)((high << sizeof(int)) | low));
        else
        {
            WriteUInt8((byte)((((high << sizeof(int)) | low) & Mask01111111) | Mask10000000));

            high >>= ChunkBitSize - sizeof(int);

            while (high >= Mask10000000)
            {
                WriteUInt8((byte)((high & Mask01111111) | Mask10000000));
                high >>= ChunkBitSize;
            }

            WriteUInt8((byte)high);
        }
    }

    /// <summary>
    /// Writes a sequence of bytes to the buffer.
    /// </summary>
    /// <param name="value">The memory containing the bytes to write.</param>
    public void WriteMemory(ReadOnlyMemory<byte> value)
    {
        value.Span.CopyTo(GetSpan(value.Length));
    }

    /// <summary>
    /// Writes a span of bytes to the buffer.
    /// </summary>
    /// <param name="value">The span containing the bytes to write.</param>
    public void WriteSpan(ReadOnlySpan<byte> value)
    {
        value.CopyTo(GetSpan(value.Length));
    }

    /// <summary>
    /// Writes a UTF-8 encoded string to the buffer.
    /// </summary>
    /// <param name="value">The string to write.</param>
    public void WriteUtfSpan(string value)
    {
        WriteSpan(Encoding.UTF8.GetBytes(value));
    }

    /// <summary>
    /// Writes a UTF-8 encoded string prefixed with a 16-bit length to the buffer.
    /// </summary>
    /// <param name="value">The string to write.</param>
    public void WriteUtfPrefixedLength16(string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        WriteUInt16((ushort)bytes.Length);
        WriteSpan(bytes);
    }

    /// <summary>
    /// Writes a UTF-8 encoded string prefixed with a 32-bit length to the buffer.
    /// </summary>
    /// <param name="value">The string to write.</param>
    public void WriteUtfPrefixedLength32(string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        WriteInt32(bytes.Length);
        WriteSpan(bytes);
    }

    /// <summary>
    /// Moves the current position in the buffer based on the specified origin and offset.
    /// </summary>
    /// <param name="origin">The origin from which to calculate the new position.</param>
    /// <param name="offset">The offset to apply to the position.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the origin is invalid.</exception>
    public void Seek(SeekOrigin origin, int offset)
    {
        switch (origin)
        {
            case SeekOrigin.Begin:
                CheckAndResizeBuffer(offset, offset);
                Position = offset;
                break;
            case SeekOrigin.Current:
                CheckAndResizeBuffer(offset);
                Position += offset;
                break;
            case SeekOrigin.End:
                Position = Length - Math.Abs(offset);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(origin));
        }
    }

    /// <summary>
    /// Copies the contents of the buffer to the specified memory destination.
    /// </summary>
    /// <param name="destination">The memory destination to copy the buffer to.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the destination length is less than the maximum position in the buffer.
    /// </exception>
    public void CopyTo(Memory<byte> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, MaxPosition);
        new ReadOnlyMemory<byte>(_buffer, 0, MaxPosition).CopyTo(destination);
    }

    /// <summary>
    /// Copies the contents of the buffer to the specified span destination.
    /// </summary>
    /// <param name="destination">The span destination to copy the buffer to.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the destination length is less than the maximum position in the buffer.
    /// </exception>
    public void CopyTo(Span<byte> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, MaxPosition);
        new ReadOnlySpan<byte>(_buffer, 0, MaxPosition).CopyTo(destination);
    }

    /// <summary>
    /// Copies the contents of the buffer to the specified stream destination.
    /// </summary>
    /// <param name="destination">The stream destination to copy the buffer to.</param>
    /// <exception cref="ArgumentNullException">Thrown if the destination stream is null.</exception>
    public void CopyTo(Stream destination)
    {
        ArgumentNullException.ThrowIfNull(destination);
        destination.Write(_buffer, 0, MaxPosition);
    }

    /// <summary>
    /// Returns the buffer as a memory segment up to the maximum position.
    /// </summary>
    /// <returns>A memory segment containing the buffer data.</returns>
    public Memory<byte> ToMemory()
    {
        return _buffer.AsMemory(0, MaxPosition);
    }

    /// <summary>
    /// Returns the buffer as a memory segment of the specified length.
    /// </summary>
    /// <param name="length">The length of the memory segment to return.</param>
    /// <returns>A memory segment containing the buffer data.</returns>
    public Memory<byte> ToMemory(int length)
    {
        return _buffer.AsMemory(0, length);
    }

    /// <summary>
    /// Returns the buffer as a span up to the maximum position.
    /// </summary>
    /// <returns>A span containing the buffer data.</returns>
    public Span<byte> ToSpan()
    {
        return _buffer.AsSpan(0, MaxPosition);
    }

    /// <summary>
    /// Returns the buffer as a span of the specified length.
    /// </summary>
    /// <param name="length">The length of the span to return.</param>
    /// <returns>A span containing the buffer data.</returns>
    public Span<byte> ToSpan(int length)
    {
        return _buffer.AsSpan(0, length);
    }

    /// <summary>
    /// Converts the buffer content up to the maximum position into a byte array.
    /// </summary>
    /// <returns>A byte array containing the buffer data up to the maximum position.</returns>
    public byte[] ToArray()
    {
        return _buffer.AsSpan(0, MaxPosition).ToArray();
    }

    /// <summary>
    /// Converts the buffer content up to the specified length into a byte array.
    /// </summary>
    /// <param name="length">The length of the buffer to include in the byte array.</param>
    /// <returns>A byte array containing the buffer data up to the specified length.</returns>
    public byte[] ToArray(int length)
    {
        return _buffer.AsSpan(0, length).ToArray();
    }

    /// <summary>
    /// Gets a span of the specified length from the buffer, resizing if necessary.
    /// </summary>
    /// <param name="length">The length of the span to retrieve.</param>
    /// <returns>A span of the specified length.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the length is negative.</exception>
    private Span<byte> GetSpan(int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        CheckAndResizeBuffer(length);
        var span = _buffer.AsSpan(Position, length);
        Position += length;
        return span;
    }

    /// <summary>
    /// Checks and resizes the buffer if the specified length exceeds the remaining capacity.
    /// </summary>
    /// <param name="length">The required length to accommodate.</param>
    /// <param name="position">The position to check from, defaults to the current position.</param>
    /// <exception cref="OutOfMemoryException">
    /// Thrown if the requested operation exceeds the maximum array length.
    /// </exception>
    private void CheckAndResizeBuffer(int length, int? position = null)
    {
        position ??= Position;

        var remaining = Length - position.Value;

        if (length <= remaining)
            return;

        var currentCount = Length;
        var growBy = Math.Max(length, currentCount);

        if (length is 0)
            growBy = Math.Max(growBy, 256);

        var newCount = currentCount + growBy;

        if ((uint)newCount > int.MaxValue)
        {
            var needed = (uint)(currentCount - remaining + length);

            if (needed > Array.MaxLength)
                throw new OutOfMemoryException("The requested operation would exceed the maximum array length.");

            newCount = Array.MaxLength;
        }

        var newBuffer = ArrayPool<byte>.Shared.Rent(newCount);

        if (currentCount > 0)
        {
            Buffer.BlockCopy(_buffer, 0, newBuffer, 0, currentCount);
            ArrayPool<byte>.Shared.Return(_buffer, true);
        }

        _buffer = newBuffer;
    }

    /// <summary>
    /// Releases the buffer back to the shared array pool.
    /// </summary>
    public void Dispose()
    {
        ArrayPool<byte>.Shared.Return(_buffer, true);
    }
}
