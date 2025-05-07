// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Text;

namespace Krosmoz.Core.IO.Binary;

/// <summary>
/// Represents an abstract base class for writing binary data to a buffer or stream.
/// </summary>
public abstract class BigEndianWriter : IDisposable
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
    /// Gets the buffer as a memory block.
    /// </summary>
    public abstract Memory<byte> BufferAsMemory { get; }

    /// <summary>
    /// Gets the buffer as a span.
    /// </summary>
    public abstract Span<byte> BufferAsSpan { get; }

    /// <summary>
    /// Gets the buffer as a byte array.
    /// </summary>
    public abstract byte[] BufferAsArray { get; }

    /// <summary>
    /// Writes a single byte to the buffer.
    /// </summary>
    /// <param name="value">The byte to write.</param>
    public abstract void WriteByte(byte value);

    /// <summary>
    /// Writes a signed byte to the buffer.
    /// </summary>
    /// <param name="value">The signed byte to write.</param>
    public abstract void WriteSByte(sbyte value);

    /// <summary>
    /// Writes a boolean value to the buffer.
    /// </summary>
    /// <param name="value">The boolean value to write.</param>
    public abstract void WriteBoolean(bool value);

    /// <summary>
    /// Writes an unsigned 16-bit integer to the buffer.
    /// </summary>
    /// <param name="value">The unsigned 16-bit integer to write.</param>
    public abstract void WriteUShort(ushort value);

    /// <summary>
    /// Writes a signed 16-bit integer to the buffer.
    /// </summary>
    /// <param name="value">The signed 16-bit integer to write.</param>
    public abstract void WriteShort(short value);

    /// <summary>
    /// Writes an unsigned 32-bit integer to the buffer.
    /// </summary>
    /// <param name="value">The unsigned 32-bit integer to write.</param>
    public abstract void WriteUInt(uint value);

    /// <summary>
    /// Writes a signed 32-bit integer to the buffer.
    /// </summary>
    /// <param name="value">The signed 32-bit integer to write.</param>
    public abstract void WriteInt(int value);

    /// <summary>
    /// Writes an unsigned 64-bit integer to the buffer.
    /// </summary>
    /// <param name="value">The unsigned 64-bit integer to write.</param>
    public abstract void WriteULong(ulong value);

    /// <summary>
    /// Writes a signed 64-bit integer to the buffer.
    /// </summary>
    /// <param name="value">The signed 64-bit integer to write.</param>
    public abstract void WriteLong(long value);

    /// <summary>
    /// Writes a single-precision floating-point value to the buffer.
    /// </summary>
    /// <param name="value">The single-precision floating-point value to write.</param>
    public abstract void WriteSingle(float value);

    /// <summary>
    /// Writes a double-precision floating-point value to the buffer.
    /// </summary>
    /// <param name="value">The double-precision floating-point value to write.</param>
    public abstract void WriteDouble(double value);

    /// <summary>
    /// Writes a memory block to the buffer.
    /// </summary>
    /// <param name="value">The memory block to write.</param>
    public abstract void WriteMemory(ReadOnlyMemory<byte> value);

    /// <summary>
    /// Writes a span to the buffer.
    /// </summary>
    /// <param name="value">The span to write.</param>
    public abstract void WriteSpan(ReadOnlySpan<byte> value);

    /// <summary>
    /// Writes a byte array to the buffer.
    /// </summary>
    /// <param name="value">The byte array to write.</param>
    public abstract void WriteBytes(byte[] value);

    /// <summary>
    /// Writes a UTF-8 encoded string to the buffer.
    /// </summary>
    /// <param name="value">The string to write.</param>
    public void WriteUtfBytes(string value)
    {
        WriteBytes(Encoding.UTF8.GetBytes(value));
    }

    /// <summary>
    /// Writes a UTF-8 encoded string prefixed with a 16-bit length to the buffer.
    /// </summary>
    /// <param name="value">The string to write.</param>
    public void WriteUtfLengthPrefixed16(string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        var length = (ushort)bytes.Length;
        WriteUShort(length);
        WriteBytes(bytes);
    }

    /// <summary>
    /// Writes a UTF-8 encoded string prefixed with a 32-bit length to the buffer.
    /// </summary>
    /// <param name="value">The string to write.</param>
    public void WriteUtfLengthPrefixed32(string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        var length = bytes.Length;
        WriteInt(length);
        WriteBytes(bytes);
    }

    /// <summary>
    /// Moves the current position within the buffer by a specified offset.
    /// </summary>
    /// <param name="origin">The reference point for the offset.</param>
    /// <param name="offset">The number of bytes to move the position by.</param>
    public abstract void Seek(SeekOrigin origin, int offset);

    /// <summary>
    /// Releases all resources used by the current instance of the <see cref="BigEndianWriter"/> class.
    /// </summary>
    public abstract void Dispose();
}
