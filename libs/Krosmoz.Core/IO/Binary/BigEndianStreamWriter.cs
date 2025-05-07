// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers.Binary;

namespace Krosmoz.Core.IO.Binary;

/// <inheritdoc />
public sealed class BigEndianStreamWriter : BigEndianWriter
{
    private readonly Stream _stream;

    /// <inheritdoc />
    public override int Length =>
        (int)_stream.Length;

    /// <inheritdoc />
    public override int Position
    {
        get => (int)_stream.Position;
        protected set => _stream.Position = value;
    }

    /// <inheritdoc />
    public override Memory<byte> BufferAsMemory =>
        BufferAsArray.AsMemory();

    /// <inheritdoc />
    public override Span<byte> BufferAsSpan =>
        BufferAsArray.AsSpan();

    /// <inheritdoc />
    public override byte[] BufferAsArray
    {
        get
        {
            var buffer = new byte[Position];
            _stream.Position = 0;
            var bytesRead = _stream.Read(buffer, 0, buffer.Length);
            if (bytesRead != buffer.Length)
                throw new OutOfMemoryException("Buffer is not large enough to hold the data.");
            return buffer;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigEndianStreamWriter"/> class with the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write data to.</param>
    public BigEndianStreamWriter(Stream stream)
    {
        _stream = stream;
    }

    /// <inheritdoc />
    public override void WriteByte(byte value)
    {
        WriteBytes([value]);
    }

    /// <inheritdoc />
    public override void WriteSByte(sbyte value)
    {
        WriteBytes([(byte)value]);
    }

    /// <inheritdoc />
    public override void WriteBoolean(bool value)
    {
        WriteBytes([(byte)(value ? 1 : 0)]);
    }

    /// <inheritdoc />
    public override void WriteUShort(ushort value)
    {
        var buffer = new byte[sizeof(ushort)];
        BinaryPrimitives.WriteUInt16BigEndian(buffer, value);
        WriteBytes(buffer);
    }

    /// <inheritdoc />
    public override void WriteShort(short value)
    {
        var buffer = new byte[sizeof(short)];
        BinaryPrimitives.WriteInt16BigEndian(buffer, value);
        WriteBytes(buffer);
    }

    /// <inheritdoc />
    public override void WriteUInt(uint value)
    {
        var buffer = new byte[sizeof(uint)];
        BinaryPrimitives.WriteUInt32BigEndian(buffer, value);
        WriteBytes(buffer);
    }

    /// <inheritdoc />
    public override void WriteInt(int value)
    {
        var buffer = new byte[sizeof(int)];
        BinaryPrimitives.WriteInt32BigEndian(buffer, value);
        WriteBytes(buffer);
    }

    /// <inheritdoc />
    public override void WriteULong(ulong value)
    {
        var buffer = new byte[sizeof(ulong)];
        BinaryPrimitives.WriteUInt64BigEndian(buffer, value);
        WriteBytes(buffer);
    }

    /// <inheritdoc />
    public override void WriteLong(long value)
    {
        var buffer = new byte[sizeof(long)];
        BinaryPrimitives.WriteInt64BigEndian(buffer, value);
        WriteBytes(buffer);
    }

    /// <inheritdoc />
    public override void WriteSingle(float value)
    {
        var buffer = new byte[sizeof(float)];
        BinaryPrimitives.WriteSingleBigEndian(buffer, value);
        WriteBytes(buffer);
    }

    /// <inheritdoc />
    public override void WriteDouble(double value)
    {
        var buffer = new byte[sizeof(double)];
        BinaryPrimitives.WriteDoubleBigEndian(buffer, value);
        WriteBytes(buffer);
    }

    /// <inheritdoc />
    public override void WriteMemory(ReadOnlyMemory<byte> value)
    {
        _stream.Write(value.Span);
    }

    /// <inheritdoc />
    public override void WriteSpan(ReadOnlySpan<byte> value)
    {
        _stream.Write(value);
    }

    /// <inheritdoc />
    public override void WriteBytes(byte[] value)
    {
        _stream.Write(value);
    }

    /// <inheritdoc />
    public override void Seek(SeekOrigin origin, int offset)
    {
        Position = origin switch
        {
            SeekOrigin.Begin => offset,
            SeekOrigin.Current => Position + offset,
            SeekOrigin.End => Length - Math.Abs(offset),
            _ => throw new ArgumentOutOfRangeException(nameof(origin))
        };
    }

    /// <inheritdoc />
    public override void Dispose()
    {
        _stream.Dispose();
    }
}
