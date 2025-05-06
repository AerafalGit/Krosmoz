// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.Contracts;
using System.IO.Compression;
using ComponentAce.Compression.Libs.zlib;

namespace Krosmoz.Core.IO.Compression;

/// <summary>
/// Provides methods for compressing and decompressing data using GZip and Deflate algorithms.
/// </summary>
public static class ZipCompressor
{
    /// <summary>
    /// Decompresses data from the input stream and writes the decompressed data to the output stream.
    /// </summary>
    /// <param name="input">The input stream containing compressed data.</param>
    /// <param name="output">The output stream to write the decompressed data to.</param>
    public static void Decompress(Stream input, Stream output)
    {
        using var zipStream = new GZipStream(input, CompressionMode.Decompress);
        zipStream.CopyTo(output);
    }

    /// <summary>
    /// Compresses data from the input stream using the Deflate algorithm and writes it to the output stream.
    /// </summary>
    /// <param name="input">The input stream containing uncompressed data.</param>
    /// <param name="output">The output stream to write the compressed data to.</param>
    public static void Deflate(Stream input, Stream output)
    {
        using var zOutput = new ZOutputStream(output);
        using var reader = new BinaryReader(input);
        zOutput.Write(reader.ReadBytes((int)input.Length), 0, (int)input.Length);
        zOutput.Flush();
    }

    /// <summary>
    /// Decompresses a byte array containing compressed data and returns the decompressed data as a byte array.
    /// </summary>
    /// <param name="buffer">The byte array containing compressed data.</param>
    /// <returns>A byte array containing the decompressed data.</returns>
    [Pure]
    public static byte[] Decompress(byte[] buffer)
    {
        var input = new MemoryStream(buffer);
        var output = new MemoryStream();
        Decompress(input, output);
        return output.ToArray();
    }

    /// <summary>
    /// Compresses a byte array using the Deflate algorithm and returns the compressed data as a byte array.
    /// </summary>
    /// <param name="buffer">The byte array containing uncompressed data.</param>
    /// <returns>A byte array containing the compressed data.</returns>
    [Pure]
    public static byte[] Deflate(byte[] buffer)
    {
        var input = new MemoryStream(buffer);
        var output = new MemoryStream();
        Deflate(input, output);
        return output.ToArray();
    }
}
