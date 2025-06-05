// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.Contracts;
using ComponentAce.Compression.Libs.zlib;

namespace Krosmoz.Core.IO.Compression;

/// <summary>
/// Provides methods for compressing data using the Deflate algorithm.
/// </summary>
public static class ZipCompressor
{
    /// <summary>
    /// Compresses the input byte array using the Deflate algorithm.
    /// </summary>
    /// <param name="input">The byte array to compress.</param>
    /// <returns>A compressed byte array.</returns>
    [Pure]
    public static byte[] Deflate(byte[] input)
    {
        var zInput = new BinaryReader(new MemoryStream(input));
        var output = new MemoryStream();
        var zOutput = new ZOutputStream(output);

        zOutput.Write(zInput.ReadBytes(input.Length), 0, input.Length);
        zOutput.Flush();

        return output.ToArray();
    }

    /// <summary>
    /// Decompresses the input byte array using the Deflate algorithm.
    /// </summary>
    /// <param name="input">The compressed byte array to decompress.</param>
    /// <returns>A decompressed byte array.</returns>
    [Pure]
    public static byte[] UncompressV2(byte[] input)
    {
        using var output = new MemoryStream();
        using var zOutput = new ZOutputStream(output);

        zOutput.Write(input);
        zOutput.Flush();

        return output.ToArray();
    }
}
