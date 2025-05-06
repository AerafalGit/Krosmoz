// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.IO.Binary;

/// <summary>
/// Defines an interface for reading binary data from a stream or buffer.
/// </summary>
public interface IBinaryReader
{
    /// <summary>
    /// Gets the total length of the binary data.
    /// </summary>
    int Length { get; }

    /// <summary>
    /// Gets the current position within the binary data.
    /// </summary>
    int Position { get; }

    /// <summary>
    /// Gets the number of bytes remaining to be read.
    /// </summary>
    int Remaining { get; }

    /// <summary>
    /// Reads a single byte from the binary data.
    /// </summary>
    /// <returns>The byte that was read.</returns>
    byte ReadByte();

    /// <summary>
    /// Reads a signed byte from the binary data.
    /// </summary>
    /// <returns>The signed byte that was read.</returns>
    sbyte ReadSByte();

    /// <summary>
    /// Reads a boolean value from the binary data.
    /// </summary>
    /// <returns>The boolean value that was read.</returns>
    bool ReadBoolean();

    /// <summary>
    /// Reads an unsigned 16-bit integer from the binary data.
    /// </summary>
    /// <returns>The unsigned 16-bit integer that was read.</returns>
    ushort ReadUShort();

    /// <summary>
    /// Reads a signed 16-bit integer from the binary data.
    /// </summary>
    /// <returns>The signed 16-bit integer that was read.</returns>
    short ReadShort();

    /// <summary>
    /// Reads an unsigned 32-bit integer from the binary data.
    /// </summary>
    /// <returns>The unsigned 32-bit integer that was read.</returns>
    uint ReadUInt();

    /// <summary>
    /// Reads a signed 32-bit integer from the binary data.
    /// </summary>
    /// <returns>The signed 32-bit integer that was read.</returns>
    int ReadInt();

    /// <summary>
    /// Reads an unsigned 64-bit integer from the binary data.
    /// </summary>
    /// <returns>The unsigned 64-bit integer that was read.</returns>
    ulong ReadULong();

    /// <summary>
    /// Reads a signed 64-bit integer from the binary data.
    /// </summary>
    /// <returns>The signed 64-bit integer that was read.</returns>
    long ReadLong();

    /// <summary>
    /// Reads a single-precision floating-point value from the binary data.
    /// </summary>
    /// <returns>The single-precision floating-point value that was read.</returns>
    float ReadSingle();

    /// <summary>
    /// Reads a double-precision floating-point value from the binary data.
    /// </summary>
    /// <returns>The double-precision floating-point value that was read.</returns>
    double ReadDouble();

    /// <summary>
    /// Reads a specified number of bytes as a read-only memory block.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <returns>A read-only memory block containing the bytes that were read.</returns>
    ReadOnlyMemory<byte> ReadMemory(int length);

    /// <summary>
    /// Reads a specified number of bytes as a read-only span.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <returns>A read-only span containing the bytes that were read.</returns>
    ReadOnlySpan<byte> ReadSpan(int length);

    /// <summary>
    /// Reads a specified number of bytes as a byte array.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <returns>A byte array containing the bytes that were read.</returns>
    byte[] ReadBytes(int length);

    /// <summary>
    /// Reads a specified number of bytes and decodes them as a UTF-8 string.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <returns>The UTF-8 string that was decoded from the bytes.</returns>
    string ReadUtfBytes(int length);

    /// <summary>
    /// Reads a UTF-8 string prefixed with a 16-bit length.
    /// </summary>
    /// <returns>The UTF-8 string that was read.</returns>
    string ReadUtfLengthPrefixed16();

    /// <summary>
    /// Reads a UTF-8 string prefixed with a 32-bit length.
    /// </summary>
    /// <returns>The UTF-8 string that was read.</returns>
    string ReadUtfLengthPrefixed32();

    /// <summary>
    /// Attempts to read a single byte from the binary data.
    /// </summary>
    /// <param name="value">When this method returns, contains the byte that was read, if the read was successful.</param>
    /// <returns>True if the byte was successfully read; otherwise, false.</returns>
    bool TryReadByte(out byte value);

    /// <summary>
    /// Attempts to read a signed byte from the binary data.
    /// </summary>
    /// <param name="value">When this method returns, contains the signed byte that was read, if the read was successful.</param>
    /// <returns>True if the signed byte was successfully read; otherwise, false.</returns>
    bool TryReadSByte(out sbyte value);

    /// <summary>
    /// Attempts to read a boolean value from the binary data.
    /// </summary>
    /// <param name="value">When this method returns, contains the boolean value that was read, if the read was successful.</param>
    /// <returns>True if the boolean value was successfully read; otherwise, false.</returns>
    bool TryReadBoolean(out bool value);

    /// <summary>
    /// Attempts to read an unsigned 16-bit integer from the binary data.
    /// </summary>
    /// <param name="value">When this method returns, contains the unsigned 16-bit integer that was read, if the read was successful.</param>
    /// <returns>True if the unsigned 16-bit integer was successfully read; otherwise, false.</returns>
    bool TryReadUShort(out ushort value);

    /// <summary>
    /// Attempts to read a signed 16-bit integer from the binary data.
    /// </summary>
    /// <param name="value">When this method returns, contains the signed 16-bit integer that was read, if the read was successful.</param>
    /// <returns>True if the signed 16-bit integer was successfully read; otherwise, false.</returns>
    bool TryReadShort(out short value);

    /// <summary>
    /// Attempts to read an unsigned 32-bit integer from the binary data.
    /// </summary>
    /// <param name="value">When this method returns, contains the unsigned 32-bit integer that was read, if the read was successful.</param>
    /// <returns>True if the unsigned 32-bit integer was successfully read; otherwise, false.</returns>
    bool TryReadUInt(out uint value);

    /// <summary>
    /// Attempts to read a signed 32-bit integer from the binary data.
    /// </summary>
    /// <param name="value">When this method returns, contains the signed 32-bit integer that was read, if the read was successful.</param>
    /// <returns>True if the signed 32-bit integer was successfully read; otherwise, false.</returns>
    bool TryReadInt(out int value);

    /// <summary>
    /// Attempts to read an unsigned 64-bit integer from the binary data.
    /// </summary>
    /// <param name="value">When this method returns, contains the unsigned 64-bit integer that was read, if the read was successful.</param>
    /// <returns>True if the unsigned 64-bit integer was successfully read; otherwise, false.</returns>
    bool TryReadULong(out ulong value);

    /// <summary>
    /// Attempts to read a signed 64-bit integer from the binary data.
    /// </summary>
    /// <param name="value">When this method returns, contains the signed 64-bit integer that was read, if the read was successful.</param>
    /// <returns>True if the signed 64-bit integer was successfully read; otherwise, false.</returns>
    bool TryReadLong(out long value);

    /// <summary>
    /// Attempts to read a single-precision floating-point value from the binary data.
    /// </summary>
    /// <param name="value">When this method returns, contains the single-precision floating-point value that was read, if the read was successful.</param>
    /// <returns>True if the single-precision floating-point value was successfully read; otherwise, false.</returns>
    bool TryReadSingle(out float value);

    /// <summary>
    /// Attempts to read a double-precision floating-point value from the binary data.
    /// </summary>
    /// <param name="value">When this method returns, contains the double-precision floating-point value that was read, if the read was successful.</param>
    /// <returns>True if the double-precision floating-point value was successfully read; otherwise, false.</returns>
    bool TryReadDouble(out double value);

    /// <summary>
    /// Attempts to read a specified number of bytes as a read-only memory block.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="value">When this method returns, contains the read-only memory block that was read, if the read was successful.</param>
    /// <returns>True if the memory block was successfully read; otherwise, false.</returns>
    bool TryReadMemory(int length, out ReadOnlyMemory<byte> value);

    /// <summary>
    /// Attempts to read a specified number of bytes as a read-only span.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="value">When this method returns, contains the read-only span that was read, if the read was successful.</param>
    /// <returns>True if the span was successfully read; otherwise, false.</returns>
    bool TryReadSpan(int length, out ReadOnlySpan<byte> value);

    /// <summary>
    /// Attempts to read a specified number of bytes as a byte array.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="value">When this method returns, contains the byte array that was read, if the read was successful.</param>
    /// <returns>True if the byte array was successfully read; otherwise, false.</returns>
    bool TryReadBytes(int length, out byte[] value);

    /// <summary>
    /// Attempts to read a specified number of bytes and decode them as a UTF-8 string.
    /// </summary>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="value">When this method returns, contains the UTF-8 string that was decoded, if the read was successful.</param>
    /// <returns>True if the UTF-8 string was successfully read; otherwise, false.</returns>
    bool TryReadUtfBytes(int length, out string value);

    /// <summary>
    /// Attempts to read a UTF-8 string prefixed with a 16-bit length.
    /// </summary>
    /// <param name="value">When this method returns, contains the UTF-8 string that was read, if the read was successful.</param>
    /// <returns>True if the UTF-8 string was successfully read; otherwise, false.</returns>
    bool TryReadUtfLengthPrefixed16(out string value);

    /// <summary>
    /// Attempts to read a UTF-8 string prefixed with a 32-bit length.
    /// </summary>
    /// <param name="value">When this method returns, contains the UTF-8 string that was read, if the read was successful.</param>
    /// <returns>True if the UTF-8 string was successfully read; otherwise, false.</returns>
    bool TryReadUtfLengthPrefixed32(out string value);

    /// <summary>
    /// Moves the current position within the binary data by a specified offset.
    /// </summary>
    /// <param name="origin">The reference point for the offset.</param>
    /// <param name="offset">The number of bytes to move the position by.</param>
    void Seek(SeekOrigin origin, int offset);
}
