// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers;
using System.Runtime.InteropServices;

namespace Krosmoz.Core.IO.Binary;

/// <inheritdoc />
public sealed class BigEndianBufferReader : BigEndianReader
{
    private readonly ReadOnlyMemory<byte> _buffer;

    /// <inheritdoc />
    public override int Length =>
        _buffer.Length;

    /// <inheritdoc />
    public override int Position { get; protected set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigEndianBufferReader"/> class with a <see cref="ReadOnlySequence{T}"/>.
    /// </summary>
    /// <param name="buffer">The buffer to read from.</param>
    public BigEndianBufferReader(ReadOnlySequence<byte> buffer)
    {
        if (buffer.IsSingleSegment)
            _buffer = buffer.First;
        else if (SequenceMarshal.TryGetReadOnlyMemory(buffer, out var memory))
            _buffer = memory;
        else
            _buffer = buffer.ToArray();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigEndianBufferReader"/> class with a <see cref="ReadOnlyMemory{T}"/>.
    /// </summary>
    /// <param name="buffer">The buffer to read from.</param>
    public BigEndianBufferReader(ReadOnlyMemory<byte> buffer)
    {
        _buffer = buffer;
    }

    /// <inheritdoc />
    public override ReadOnlyMemory<byte> ReadMemory(int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        ArgumentOutOfRangeException.ThrowIfLessThan(Remaining, length);

        var memory = _buffer.Slice(Position, length);
        Position += length;
        return memory;
    }

    /// <inheritdoc />
    public override ReadOnlySpan<byte> ReadSpan(int length)
    {
        return ReadMemory(length).Span;
    }

    /// <inheritdoc />
    public override byte[] ReadBytes(int length)
    {
        return ReadMemory(length).ToArray();
    }
}
