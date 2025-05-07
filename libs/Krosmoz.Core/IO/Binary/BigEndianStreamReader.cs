// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.IO.Binary;

/// <inheritdoc />
public sealed class BigEndianStreamReader : BigEndianReader
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

    /// <summary>
    /// Initializes a new instance of the <see cref="BigEndianStreamReader"/> class with the specified stream.
    /// </summary>
    /// <param name="stream">The stream to read from.</param>
    public BigEndianStreamReader(Stream stream)
    {
        _stream = stream;
    }

    /// <inheritdoc />
    public override ReadOnlyMemory<byte> ReadMemory(int length)
    {
        var buffer = new byte[length];

        var bytesRead = _stream.Read(buffer, 0, length);

        if (bytesRead != length)
            throw new OutOfMemoryException("Not enough data to read the requested length.");

        return new ReadOnlyMemory<byte>(buffer, 0, bytesRead);
    }

    /// <inheritdoc />
    public override ReadOnlySpan<byte> ReadSpan(int length)
    {
        var buffer = new byte[length];

        var bytesRead = _stream.Read(buffer, 0, length);

        if (bytesRead != length)
            throw new OutOfMemoryException("Not enough data to read the requested length.");

        return new ReadOnlySpan<byte>(buffer, 0, bytesRead);
    }

    /// <inheritdoc />
    public override byte[] ReadBytes(int length)
    {
        var buffer = new byte[length];

        var bytesRead = _stream.Read(buffer, 0, length);

        if (bytesRead != length)
            throw new OutOfMemoryException("Not enough data to read the requested length.");

        return buffer;
    }

    /// <inheritdoc />
    public override void Dispose()
    {
        _stream.Dispose();
    }
}
