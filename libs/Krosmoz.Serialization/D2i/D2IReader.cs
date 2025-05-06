// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.D2i;

/// <summary>
/// Provides functionality to read and parse D2i files into a <see cref="D2IContext"/>.
/// </summary>
public sealed class D2IReader
{
    private D2IContext? _context;

    /// <summary>
    /// Retrieves the <see cref="D2IContext"/> for the specified file path.
    /// If the context is already initialized, it returns the existing context.
    /// </summary>
    /// <param name="filePath">The file path of the D2i file to read.</param>
    /// <returns>The <see cref="D2IContext"/> containing the parsed data.</returns>
    public D2IContext GetContext(string filePath)
    {
        if (_context is not null)
            return _context;

        _context = new D2IContext(filePath);

        using var reader = new BinaryBufferReader(File.ReadAllBytes(_context.FilePath));

        var indexPosition = reader.ReadInt();
        reader.Seek(SeekOrigin.Begin, indexPosition);
        var indexLength = reader.ReadInt();

        for (var i = 0; i < indexLength; i += 9)
        {
            var key = reader.ReadInt();
            var notDiacritical = reader.ReadBoolean();
            var textPosition = reader.ReadInt();
            var position = reader.Position;

            reader.Seek(SeekOrigin.Begin, textPosition);
            var text = reader.ReadUtfLengthPrefixed16();
            reader.Seek(SeekOrigin.Begin, position);

            if (notDiacritical)
            {
                i += sizeof(int);

                var criticalPosition = reader.ReadInt();

                position = reader.Position;

                reader.Seek(SeekOrigin.Begin, criticalPosition);
                var notDiacriticalText = reader.ReadUtfLengthPrefixed16();
                reader.Seek(SeekOrigin.Begin, position);

                _context.IndexedTexts.Add(key, new D2IText<int>(key, text, notDiacriticalText));
            }
            else
                _context.IndexedTexts.Add(key, new D2IText<int>(key, text));
        }

        indexLength = reader.ReadInt();

        while (indexLength > 0)
        {
            var position = reader.Position;
            var key = reader.ReadUtfLengthPrefixed16();
            var textPosition = reader.ReadInt();

            indexLength -= reader.Position - position;
            position = reader.Position;

            reader.Seek(SeekOrigin.Begin, textPosition);
            _context.NamedTexts.Add(key, new D2IText<string>(key, reader.ReadUtfLengthPrefixed16()));
            reader.Seek(SeekOrigin.Begin, position);
        }

        indexLength = reader.ReadInt();

        var c = 0;

        while (indexLength > 0)
        {
            _context.SortedIndexes.Add(reader.ReadInt(), c++);
            indexLength -= sizeof(int);
        }

        return _context;
    }
}
