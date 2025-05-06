// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.IO.Binary;

/// <summary>
/// Defines an interface for writing binary data to a stream or buffer.
/// </summary>
public interface IBinaryWriter
{
    /// <summary>
    /// Gets the total length of the binary data.
    /// </summary>
    int Length { get; }

    /// <summary>
    /// Gets or sets the current position within the binary data.
    /// </summary>
    int Position { get; set; }

    /// <summary>
    /// Gets the binary data as a memory block.
    /// </summary>
    Memory<byte> BufferAsMemory { get; }

    /// <summary>
    /// Gets the binary data as a span.
    /// </summary>
    Span<byte> BufferAsSpan { get; }

    /// <summary>
    /// Gets the binary data as a byte array.
    /// </summary>
    byte[] BufferAsArray { get; }

    /// <summary>
    /// Writes a single byte to the binary data.
    /// </summary>
    /// <param name="value">The byte to write.</param>
    void WriteByte(byte value);

    /// <summary>
    /// Writes a signed byte to the binary data.
    /// </summary>
    /// <param name="value">The signed byte to write.</param>
    void WriteSByte(sbyte value);

    /// <summary>
    /// Writes a boolean value to the binary data.
    /// </summary>
    /// <param name="value">The boolean value to write.</param>
    void WriteBoolean(bool value);

    /// <summary>
    /// Writes an unsigned 16-bit integer to the binary data.
    /// </summary>
    /// <param name="value">The unsigned 16-bit integer to write.</param>
    void WriteUShort(ushort value);

    /// <summary>
    /// Writes a signed 16-bit integer to the binary data.
    /// </summary>
    /// <param name="value">The signed 16-bit integer to write.</param>
    void WriteShort(short value);

    /// <summary>
    /// Writes an unsigned 32-bit integer to the binary data.
    /// </summary>
    /// <param name="value">The unsigned 32-bit integer to write.</param>
    void WriteUInt(uint value);

    /// <summary>
    /// Writes a signed 32-bit integer to the binary data.
    /// </summary>
    /// <param name="value">The signed 32-bit integer to write.</param>
    void WriteInt(int value);

    /// <summary>
    /// Writes an unsigned 64-bit integer to the binary data.
    /// </summary>
    /// <param name="value">The unsigned 64-bit integer to write.</param>
    void WriteULong(ulong value);

    /// <summary>
    /// Writes a signed 64-bit integer to the binary data.
    /// </summary>
    /// <param name="value">The signed 64-bit integer to write.</param>
    void WriteLong(long value);

    /// <summary>
    /// Writes a single-precision floating-point value to the binary data.
    /// </summary>
    /// <param name="value">The single-precision floating-point value to write.</param>
    void WriteSingle(float value);

    /// <summary>
    /// Writes a double-precision floating-point value to the binary data.
    /// </summary>
    /// <param name="value">The double-precision floating-point value to write.</param>
    void WriteDouble(double value);

    /// <summary>
    /// Writes a memory block to the binary data.
    /// </summary>
    /// <param name="value">The memory block to write.</param>
    void WriteMemory(ReadOnlyMemory<byte> value);

    /// <summary>
    /// Writes a span to the binary data.
    /// </summary>
    /// <param name="value">The span to write.</param>
    void WriteSpan(ReadOnlySpan<byte> value);

    /// <summary>
    /// Writes a byte array to the binary data.
    /// </summary>
    /// <param name="value">The byte array to write.</param>
    void WriteBytes(byte[] value);

    /// <summary>
    /// Writes a UTF-8 string to the binary data.
    /// </summary>
    /// <param name="value">The UTF-8 string to write.</param>
    void WriteUtfBytes(string value);

    /// <summary>
    /// Writes a UTF-8 string prefixed with a 16-bit length to the binary data.
    /// </summary>
    /// <param name="value">The UTF-8 string to write.</param>
    void WriteUtfLengthPrefxed16(string value);

    /// <summary>
    /// Writes a UTF-8 string prefixed with a 32-bit length to the binary data.
    /// </summary>
    /// <param name="value">The UTF-8 string to write.</param>
    void WriteUtfLengthPrefxed32(string value);

    /// <summary>
    /// Moves the current position within the binary data by a specified offset.
    /// </summary>
    /// <param name="origin">The reference point for the offset.</param>
    /// <param name="offset">The number of bytes to move the position by.</param>
    void Seek(SeekOrigin origin, int offset);
}
