// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers;
using System.Buffers.Binary;

namespace Krosmoz.Core.IO.Binary;

/// <inheritdoc />
public sealed class BigEndianBufferWriter : BigEndianWriter
{
    private byte[] _buffer;
    private int _position;

    /// <inheritdoc />
    public override int Length =>
        _buffer.Length;

    /// <inheritdoc />
    public override int Position
    {
        get => _position;
        protected set
        {
            _position = value;

            if (MaxPosition < value)
                MaxPosition = value;
        }
    }

    /// <summary>
    /// Gets or sets the maximum position reached in the buffer.
    /// </summary>
    private int MaxPosition { get; set; }

    /// <inheritdoc />
    public override Memory<byte> BufferAsMemory =>
        _buffer.AsMemory(0, MaxPosition);

    /// <inheritdoc />
    public override Span<byte> BufferAsSpan =>
        _buffer.AsSpan(0, MaxPosition);

    /// <inheritdoc />
    public override byte[] BufferAsArray =>
        _buffer.AsSpan(0, MaxPosition).ToArray();

    /// <summary>
    /// Initializes a new instance of the <see cref="BigEndianBufferWriter"/> class with an empty buffer.
    /// </summary>
    public BigEndianBufferWriter()
    {
        _buffer = [];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigEndianBufferWriter"/> class with a buffer of the specified length.
    /// </summary>
    /// <param name="length">The initial length of the buffer.</param>
    public BigEndianBufferWriter(int length)
    {
        _buffer = ArrayPool<byte>.Shared.Rent(length);
    }

    /// <inheritdoc />
    public override void WriteByte(byte value)
    {
        GetSpan(sizeof(byte))[0] = value;
    }

    /// <inheritdoc />
    public override void WriteSByte(sbyte value)
    {
        GetSpan(sizeof(sbyte))[0] = (byte)value;
    }

    /// <inheritdoc />
    public override void WriteBoolean(bool value)
    {
        GetSpan(sizeof(bool))[0] = (byte)(value ? 1 : 0);
    }

    /// <inheritdoc />
    public override void WriteUShort(ushort value)
    {
        BinaryPrimitives.WriteUInt16BigEndian(GetSpan(sizeof(ushort)), value);
    }

    /// <inheritdoc />
    public override void WriteShort(short value)
    {
        BinaryPrimitives.WriteInt16BigEndian(GetSpan(sizeof(short)), value);
    }

    /// <inheritdoc />
    public override void WriteUInt(uint value)
    {
        BinaryPrimitives.WriteUInt32BigEndian(GetSpan(sizeof(uint)), value);
    }

    /// <inheritdoc />
    public override void WriteInt(int value)
    {
        BinaryPrimitives.WriteInt32BigEndian(GetSpan(sizeof(int)), value);
    }

    /// <inheritdoc />
    public override void WriteULong(ulong value)
    {
        BinaryPrimitives.WriteUInt64BigEndian(GetSpan(sizeof(ulong)), value);
    }

    /// <inheritdoc />
    public override void WriteLong(long value)
    {
        BinaryPrimitives.WriteInt64BigEndian(GetSpan(sizeof(long)), value);
    }

    /// <inheritdoc />
    public override void WriteSingle(float value)
    {
        BinaryPrimitives.WriteSingleBigEndian(GetSpan(sizeof(float)), value);
    }

    /// <inheritdoc />
    public override void WriteDouble(double value)
    {
        BinaryPrimitives.WriteDoubleBigEndian(GetSpan(sizeof(double)), value);
    }

    /// <inheritdoc />
    public override void WriteMemory(ReadOnlyMemory<byte> value)
    {
        value.Span.CopyTo(GetSpan(value.Length));
    }

    /// <inheritdoc />
    public override void WriteSpan(ReadOnlySpan<byte> value)
    {
        value.CopyTo(GetSpan(value.Length));
    }

    /// <inheritdoc />
    public override void WriteBytes(byte[] value)
    {
        value.CopyTo(GetSpan(value.Length));
    }

    /// <inheritdoc />
    public override void Seek(SeekOrigin origin, int offset)
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
    /// Retrieves a span of the specified length from the buffer, ensuring the buffer has enough capacity.
    /// Updates the current position within the buffer after retrieving the span.
    /// </summary>
    /// <param name="length">The length of the span to retrieve.</param>
    /// <returns>A span of the specified length starting from the current position.</returns>
    private Span<byte> GetSpan(int length)
    {
        CheckAndResizeBuffer(length);
        var span = _buffer.AsSpan(Position, length);
        Position += length;
        return span;
    }

    /// <summary>
    /// Ensures the buffer has enough capacity to accommodate the specified number of bytes.
    /// If the buffer is too small, it resizes the buffer to a larger size.
    /// </summary>
    /// <param name="length">The number of bytes to accommodate.</param>
    /// <param name="position">The position to check from. If null, the current position is used.</param>
    /// <exception cref="OutOfMemoryException">Thrown when the requested operation exceeds the maximum array length.</exception>
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

    /// <inheritdoc />
    public override void Dispose()
    {
        ArrayPool<byte>.Shared.Return(_buffer, true);
    }
}
