// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;
using Krosmoz.Core.IO.Compression;

namespace Krosmoz.Serialization.DLM;

/// <summary>
/// Provides methods to adapt and load DLM map.
/// </summary>
public static class DlmAdapter
{
    /// <summary>
    /// Represents the header byte value for a DLM file.
    /// </summary>
    public const byte DlmFileHeader = 77;

    /// <summary>
    /// Loads a DLM map from the provided binary buffer.
    /// </summary>
    /// <param name="buffer">The binary buffer containing the DLM map data.</param>
    /// <returns>A <see cref="DlmMap"/> object representing the loaded map.</returns>
    /// <exception cref="Exception">Thrown when the file header is invalid.</exception>
    public static DlmMap Load(ReadOnlyMemory<byte> buffer)
    {
        var reader = new BigEndianReader(buffer);

        DlmMap map;

        if (reader.ReadUInt8() is DlmFileHeader)
        {
            map = new DlmMap();
            map.Deserialize(reader);
            return map;
        }

        reader.Seek(SeekOrigin.Begin, 0);

        reader = new BigEndianReader(ZipCompressor.UncompressV2(reader.ReadSpanToEnd().ToArray()));

        if (reader.ReadUInt8() is not DlmFileHeader)
            throw new Exception("Invalid file header.");

        map = new DlmMap();
        map.Deserialize(reader);
        return map;
    }
}
