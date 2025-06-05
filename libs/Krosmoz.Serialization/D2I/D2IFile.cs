// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.D2I;

/// <summary>
/// Represents a D2I file.
/// </summary>
public sealed class D2IFile
{
    /// <summary>
    /// The format string used for unknown text identifiers.
    /// </summary>
    private const string UnknownTextId = "[UNKNOWN_TEXT_ID_{0}]";

    /// <summary>
    /// The format string used for unknown text names.
    /// </summary>
    private const string UnknownTextName = "[UNKNOWN_TEXT_NAME_{0}]";

    /// <summary>
    /// The path to the D2I file.
    /// </summary>
    public string FilePath { get; }

    /// <summary>
    /// Gets the dictionary of indexed text entries, where the key is an integer identifier.
    /// </summary>
    public Dictionary<int, D2IText<int>> IndexedTexts { get; }

    /// <summary>
    /// Gets the dictionary of named text entries, where the key is a string identifier.
    /// </summary>
    public Dictionary<string, D2IText<string>> NamedTexts { get; }

    /// <summary>
    /// Gets the dictionary of sorted indexes, where the key is an integer identifier and the value is the order index.
    /// </summary>
    public Dictionary<int, int> SortedIndexes { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="D2IFile"/> class.
    /// </summary>
    public D2IFile()
    {
        FilePath = PathConstants.Files.D2IFrPath;
        IndexedTexts = [];
        NamedTexts = [];
        SortedIndexes = [];
    }

    /// <summary>
    /// Loads the D2I file and populates the indexed and named text dictionaries.
    /// </summary>
    public void Load()
    {
        var reader = new BigEndianReader(File.ReadAllBytes(FilePath));

        var indexPosition = reader.ReadInt32();
        reader.Seek(SeekOrigin.Begin, indexPosition);
        var indexLength = reader.ReadInt32();

        for (var i = 0; i < indexLength; i += 9)
        {
            var key = reader.ReadInt32();
            var notDiacritical = reader.ReadBoolean();
            var textPosition = reader.ReadInt32();
            var position = reader.Position;

            reader.Seek(SeekOrigin.Begin, textPosition);
            var text = reader.ReadUtfPrefixedLength16();
            reader.Seek(SeekOrigin.Begin, position);

            if (notDiacritical)
            {
                i += sizeof(int);

                var criticalPosition = reader.ReadInt32();

                position = reader.Position;

                reader.Seek(SeekOrigin.Begin, criticalPosition);
                var notDiacriticalText = reader.ReadUtfPrefixedLength16();
                reader.Seek(SeekOrigin.Begin, position);

                IndexedTexts.Add(key, new D2IText<int>(key, text, notDiacriticalText));
            }
            else
                IndexedTexts.Add(key, new D2IText<int>(key, text));
        }

        indexLength = reader.ReadInt32();

        while (indexLength > 0)
        {
            var position = reader.Position;
            var key = reader.ReadUtfPrefixedLength16();
            var textPosition = reader.ReadInt32();

            indexLength -= reader.Position - position;
            position = reader.Position;

            reader.Seek(SeekOrigin.Begin, textPosition);
            NamedTexts.Add(key, new D2IText<string>(key, reader.ReadUtfPrefixedLength16()));
            reader.Seek(SeekOrigin.Begin, position);
        }

        indexLength = reader.ReadInt32();

        var c = 0;

        while (indexLength > 0)
        {
            SortedIndexes.Add(reader.ReadInt32(), c++);
            indexLength -= sizeof(int);
        }
    }

    /// <summary>
    /// Saves the current state of the D2I file, including indexed texts, named texts,
    /// and sorted indexes, to the file specified by <see cref="FilePath"/>.
    /// Creates a backup of the original file before saving.
    /// </summary>
    public void Save()
    {
        var writer = new BigEndianWriter();
        var indexWriter = new BigEndianWriter();
        var namedWriter = new BigEndianWriter();
        var sortedWriter = new BigEndianWriter();

        writer.Seek(SeekOrigin.Begin, sizeof(int));

        foreach (var (id, entry) in IndexedTexts)
        {
            indexWriter.WriteInt32(id);
            indexWriter.WriteBoolean(entry.UseNotDiacriticalText);
            indexWriter.WriteInt32(writer.Position);

            writer.WriteUtfPrefixedLength16(entry.Text);

            if (!entry.UseNotDiacriticalText)
                continue;

            indexWriter.WriteInt32(writer.Position);
            writer.WriteUtfPrefixedLength16(entry.NotDiacriticalText);
        }

        var indexLength = indexWriter.MaxPosition;

        foreach (var (id, entry) in NamedTexts)
        {
            namedWriter.WriteUtfPrefixedLength16(id);
            namedWriter.WriteInt32(writer.Position);
            writer.WriteUtfPrefixedLength16(entry.Text);
        }

        foreach (var index in IndexedTexts.OrderBy(static x => x.Key))
            sortedWriter.WriteInt32(index.Key);

        var position = writer.Position;
        var indexBuffer = indexWriter.ToSpan();

        writer.WriteInt32(indexLength);
        writer.WriteSpan(indexBuffer);
        writer.WriteInt32(namedWriter.MaxPosition);
        writer.WriteSpan(namedWriter.ToSpan());
        writer.WriteInt32(sortedWriter.MaxPosition);
        writer.WriteSpan(sortedWriter.ToSpan());

        var length = writer.Position;

        writer.Seek(SeekOrigin.Begin, 0);
        writer.WriteInt32(position);

        File.Copy(FilePath, string.Concat(FilePath, ".bak"), true);
        File.WriteAllBytes(FilePath, writer.ToSpan(length));
    }

    /// <summary>
    /// Gets the text associated with the specified integer identifier.
    /// </summary>
    /// <param name="id">The integer identifier.</param>
    /// <returns>The associated text, or a placeholder if not found.</returns>
    public string GetText(int id)
    {
        return IndexedTexts.TryGetValue(id, out var entry)
            ? entry.Text
            : string.Format(UnknownTextId, id);
    }

    /// <summary>
    /// Gets the text associated with the specified string identifier.
    /// </summary>
    /// <param name="id">The string identifier.</param>
    /// <returns>The associated text, or a placeholder if not found.</returns>
    public string GetText(string id)
    {
        return NamedTexts.TryGetValue(id, out var entry)
            ? entry.Text
            : string.Format(UnknownTextName, id);
    }

    /// <summary>
    /// Gets the order index of the specified key.
    /// </summary>
    /// <param name="key">The key to find the order index for.</param>
    /// <returns>The order index, or -1 if not found.</returns>
    public int GetOrderIndex(int key)
    {
        return SortedIndexes.GetValueOrDefault(key, -1);
    }

    /// <summary>
    /// Sets the text associated with the specified integer identifier.
    /// If the identifier does not exist, a new entry is added.
    /// If the text contains accents or uppercase characters, a non-diacritical version is also stored.
    /// </summary>
    /// <param name="id">The integer identifier for the text.</param>
    /// <param name="text">The text to associate with the identifier.</param>
    public void SetText(int id, string text)
    {
        if (!IndexedTexts.TryGetValue(id, out var entry))
            IndexedTexts.Add(id, entry = new D2IText<int>(id, text));
        else
            entry.Text = text;

        if (text.HasAccents() || text.Any(char.IsUpper))
        {
            entry.NotDiacriticalText = text.RemoveAccents().ToLower();
            entry.UseNotDiacriticalText = true;
        }
        else
            entry.UseNotDiacriticalText = false;
    }

    /// <summary>
    /// Sets the text associated with the specified string identifier.
    /// If the identifier does not exist, a new entry is added.
    /// </summary>
    /// <param name="id">The string identifier for the text.</param>
    /// <param name="text">The text to associate with the identifier.</param>
    public void SetText(string id, string text)
    {
        if (!NamedTexts.TryGetValue(id, out var entry))
            NamedTexts.Add(id, new D2IText<string>(id, text));
        else
            entry.Text = text;
    }

    /// <summary>
    /// Removes the text associated with the specified integer identifier.
    /// </summary>
    /// <param name="id">The integer identifier of the text to remove.</param>
    /// <returns><c>true</c> if the text was successfully removed; otherwise, <c>false</c>.</returns>
    public bool RemoveText(int id)
    {
        return IndexedTexts.Remove(id);
    }

    /// <summary>
    /// Removes the text associated with the specified string identifier.
    /// </summary>
    /// <param name="id">The string identifier of the text to remove.</param>
    /// <returns><c>true</c> if the text was successfully removed; otherwise, <c>false</c>.</returns>
    public bool RemoveText(string id)
    {
        return NamedTexts.Remove(id);
    }

    /// <summary>
    /// Gets the identifier of the specified text, adding it to the dictionary if it does not already exist.
    /// </summary>
    /// <param name="text">The text to find or add.</param>
    /// <returns>The identifier of the text.</returns>
    public int GetOrAddText(string text)
    {
        var existingTest = IndexedTexts.Values.FirstOrDefault(x => string.Equals(x.Text, text, StringComparison.InvariantCulture));

        if (existingTest is not null)
            return existingTest.Id;

        var lastId = IndexedTexts.Keys.Max() + 1;
        IndexedTexts.TryAdd(lastId, new D2IText<int>(lastId, text));
        return lastId;
    }
}
