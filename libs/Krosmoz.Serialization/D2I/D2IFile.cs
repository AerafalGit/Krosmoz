// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.D2I;

/// <summary>
/// Represents a D2I file, providing functionality to load, save, and manage D2I text entries.
/// </summary>
public sealed class D2IFile
{
    private const string UnknownTextId = "[UNKNOWN_TEXT_ID_{0}]";

    /// <summary>
    /// Gets the file path associated with the D2I file.
    /// </summary>
    public string FilePath { get; }

    /// <summary>
    /// Gets the dictionary of indexed texts, where the key is an integer identifier.
    /// </summary>
    public Dictionary<int, D2IText<int>> IndexedTexts { get; }

    /// <summary>
    /// Gets the dictionary of named texts, where the key is a string identifier.
    /// </summary>
    public Dictionary<string, D2IText<string>> NamedTexts { get; }

    /// <summary>
    /// Gets the dictionary of sorted indexes, where the key is an integer identifier and the value is the order index.
    /// </summary>
    public Dictionary<int, int> SortedIndexes { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="D2IFile"/> class with the specified file path.
    /// </summary>
    /// <param name="filePath">The file path associated with the D2I file.</param>
    public D2IFile(string filePath)
    {
        FilePath = filePath;
        IndexedTexts = [];
        NamedTexts = [];
        SortedIndexes = [];
    }

    /// <summary>
    /// Loads the D2I file and populates the indexed texts, named texts, and sorted indexes.
    /// </summary>
    public void Load()
    {
        using var reader = new BigEndianBufferReader(File.ReadAllBytes(FilePath));

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

                IndexedTexts.Add(key, new D2IText<int>(key, text, notDiacriticalText));
            }
            else
                IndexedTexts.Add(key, new D2IText<int>(key, text));
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
            NamedTexts.Add(key, new D2IText<string>(key, reader.ReadUtfLengthPrefixed16()));
            reader.Seek(SeekOrigin.Begin, position);
        }

        indexLength = reader.ReadInt();

        var c = 0;

        while (indexLength > 0)
        {
            SortedIndexes.Add(reader.ReadInt(), c++);
            indexLength -= sizeof(int);
        }
    }

    /// <summary>
    /// Saves the current state of the D2I file to disk.
    /// </summary>
    public void Save()
    {
        using var writer = new BigEndianBufferWriter();
        using var indexWriter = new BigEndianBufferWriter();
        using var namedWriter = new BigEndianBufferWriter();
        using var sortedWriter = new BigEndianBufferWriter();

        writer.Seek(SeekOrigin.Begin, sizeof(int));

        foreach (var (id, entry) in IndexedTexts)
        {
            indexWriter.WriteInt(id);
            indexWriter.WriteBoolean(entry.UseNotDiacriticalText);
            indexWriter.WriteInt(writer.Position);

            writer.WriteUtfLengthPrefixed16(entry.Text);

            if (!entry.UseNotDiacriticalText)
                continue;

            indexWriter.WriteInt(writer.Position);
            writer.WriteUtfLengthPrefixed16(entry.NotDiacriticalText);
        }

        var indexLength = indexWriter.BufferAsSpan.Length;

        foreach (var (id, entry) in NamedTexts)
        {
            namedWriter.WriteUtfLengthPrefixed16(id);
            namedWriter.WriteInt(writer.Position);
            writer.WriteUtfLengthPrefixed16(entry.Text);
        }

        foreach (var index in IndexedTexts.OrderBy(static x => x.Key))
            sortedWriter.WriteInt(index.Key);

        var position = writer.Position;
        var indexBuffer = indexWriter.BufferAsSpan;

        writer.WriteInt(indexLength);
        writer.WriteSpan(indexBuffer);
        writer.WriteInt(namedWriter.BufferAsSpan.Length);
        writer.WriteSpan(namedWriter.BufferAsSpan);
        writer.WriteInt(sortedWriter.BufferAsSpan.Length);
        writer.WriteSpan(sortedWriter.BufferAsSpan);

        var length = writer.Position;

        writer.Seek(SeekOrigin.Begin, 0);
        writer.WriteInt(position);

        File.WriteAllBytes(FilePath, writer.BufferAsSpan[..length]);
    }

    /// <summary>
    /// Retrieves the text associated with the specified integer identifier.
    /// </summary>
    /// <param name="id">The integer identifier of the text.</param>
    /// <returns>The text associated with the identifier, or a placeholder if not found.</returns>
    public string GetText(int id)
    {
        return IndexedTexts.TryGetValue(id, out var entry)
            ? entry.Text
            : string.Format(UnknownTextId, id);
    }

    /// <summary>
    /// Retrieves the text associated with the specified string identifier.
    /// </summary>
    /// <param name="id">The string identifier of the text.</param>
    /// <returns>The text associated with the identifier, or a placeholder if not found.</returns>
    public string GetText(string id)
    {
        return NamedTexts.TryGetValue(id, out var entry)
            ? entry.Text
            : string.Format(UnknownTextId, id);
    }

    /// <summary>
    /// Sets or updates the text for the specified integer identifier.
    /// </summary>
    /// <param name="id">The integer identifier of the text.</param>
    /// <param name="text">The text to associate with the identifier.</param>
    public void SetText(int id, string text)
    {
        if (!IndexedTexts.TryGetValue(id, out var entry))
            IndexedTexts.Add(id, entry = new D2IText<int>(id, text));
        else
            entry.Text = text;

        if (text.HasAccents() || text.Any(char.IsUpper))
        {
            entry.NotDiacriticalText    = text.RemoveAccents().ToLower();
            entry.UseNotDiacriticalText = true;
        }
        else
            entry.UseNotDiacriticalText = false;
    }

    /// <summary>
    /// Sets or updates the text for the specified string identifier.
    /// </summary>
    /// <param name="id">The string identifier of the text.</param>
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
    /// <returns><c>true</c> if the text was removed; otherwise, <c>false</c>.</returns>
    public bool RemoveText(int id)
    {
        return IndexedTexts.Remove(id);
    }

    /// <summary>
    /// Removes the text associated with the specified string identifier.
    /// </summary>
    /// <param name="id">The string identifier of the text to remove.</param>
    /// <returns><c>true</c> if the text was removed; otherwise, <c>false</c>.</returns>
    public bool RemoveText(string id)
    {
        return NamedTexts.Remove(id);
    }

    /// <summary>
    /// Retrieves the identifier of an existing text or adds a new text with a generated identifier.
    /// </summary>
    /// <param name="text">The text to retrieve or add.</param>
    /// <returns>The identifier of the existing or newly added text.</returns>
    public int GetOrAddText(string text)
    {
        var existingTest = IndexedTexts.Values.FirstOrDefault(x => string.Equals(x.Text, text, StringComparison.InvariantCulture));

        if (existingTest is not null)
            return existingTest.Id;

        var nextId = IndexedTexts.Keys.Max() + 1;
        IndexedTexts.TryAdd(nextId, new D2IText<int>(nextId, text));
        return nextId;
    }

    /// <summary>
    /// Retrieves the order index associated with the specified key.
    /// </summary>
    /// <param name="key">The key to retrieve the order index for.</param>
    /// <returns>The order index, or -1 if not found.</returns>
    public int GetOrderIndex(int key)
    {
        return SortedIndexes.GetValueOrDefault(key, -1);
    }
}
