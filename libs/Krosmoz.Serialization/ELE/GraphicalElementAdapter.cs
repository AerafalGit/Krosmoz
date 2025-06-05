// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;
using Krosmoz.Core.IO.Compression;

namespace Krosmoz.Serialization.ELE;

/// <summary>
/// Provides methods for loading and adapting graphical element files.
/// </summary>
public static class GraphicalElementAdapter
{
    /// <summary>
    /// The header value used to identify graphical element files.
    /// </summary>
    public const sbyte ElementFileHeader = 69;

    /// <summary>
    /// Loads a graphical element file from the predefined path, decompressing it if necessary.
    /// </summary>
    /// <returns>A <see cref="GraphicalElementFile"/> instance containing the loaded data.</returns>
    /// <exception cref="Exception">Thrown if the file header is invalid.</exception>
    public static GraphicalElementFile Load()
    {
        var reader = new BigEndianReader(File.ReadAllBytes(PathConstants.Files.ElementsPath));

        var header = reader.ReadInt8();

        GraphicalElementFile file;

        if (header is ElementFileHeader)
        {
            file = new GraphicalElementFile();
            file.Load(reader);
            return file;
        }

        reader.Seek(SeekOrigin.Begin, 0);

        var deflatedBytes = ZipCompressor.Deflate(reader.ReadSpanToEnd().ToArray());

        if (deflatedBytes.Length is 0 || deflatedBytes[0] is not (byte)ElementFileHeader)
            throw new Exception("Invalid file header.");

        reader = new BigEndianReader(deflatedBytes);
        reader.Seek(SeekOrigin.Current, sizeof(sbyte));

        file = new GraphicalElementFile();
        file.Load(reader);
        return file;
    }
}
