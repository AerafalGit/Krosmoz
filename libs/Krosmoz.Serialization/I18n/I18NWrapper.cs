// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Concurrent;
using Krosmoz.Serialization.D2I;

namespace Krosmoz.Serialization.I18n;

/// <summary>
/// Provides functionality to manage internationalization (I18N) by wrapping multiple D2I files.
/// </summary>
public sealed class I18NWrapper
{
    private readonly ConcurrentDictionary<Languages, D2IFile> _files;
    private readonly string _directoryPath;

    /// <summary>
    /// Initializes a new instance of the <see cref="I18NWrapper"/> class with the specified directory path.
    /// </summary>
    /// <param name="directoryPath">The directory path containing the D2I files.</param>
    public I18NWrapper(string directoryPath)
    {
        _directoryPath = directoryPath;
        _files = [];
    }

    /// <summary>
    /// Loads all D2I files from the specified directory into memory.
    /// </summary>
    public void Load()
    {
        Parallel.ForEach(Directory.EnumerateFiles(_directoryPath, "*.d2i"), filePath =>
        {
            var file = new D2IFile(filePath);
            file.Load();
            _files[ShortNameToLanguage(Path.GetFileNameWithoutExtension(filePath)[^2..])] = file;
        });
    }

    /// <summary>
    /// Saves all loaded D2I files back to disk.
    /// </summary>
    public void Save()
    {
        Parallel.ForEach(_files.Values, static file => file.Save());
    }

    /// <summary>
    /// Retrieves the text associated with the specified integer identifier for a given language.
    /// </summary>
    /// <param name="language">The language of the text.</param>
    /// <param name="id">The integer identifier of the text.</param>
    /// <returns>The text associated with the identifier.</returns>
    public string GetText(Languages language, int id)
    {
        return _files[language].GetText(id);
    }

    /// <summary>
    /// Retrieves the text associated with the specified string identifier for a given language.
    /// </summary>
    /// <param name="language">The language of the text.</param>
    /// <param name="id">The string identifier of the text.</param>
    /// <returns>The text associated with the identifier.</returns>
    public string GetText(Languages language, string id)
    {
        return _files[language].GetText(id);
    }

    /// <summary>
    /// Sets or updates the text for the specified integer identifier for a given language.
    /// </summary>
    /// <param name="language">The language of the text.</param>
    /// <param name="id">The integer identifier of the text.</param>
    /// <param name="text">The text to associate with the identifier.</param>
    public void SetText(Languages language, int id, string text)
    {
        _files[language].SetText(id, text);
    }

    /// <summary>
    /// Sets or updates the text for the specified string identifier for a given language.
    /// </summary>
    /// <param name="language">The language of the text.</param>
    /// <param name="id">The string identifier of the text.</param>
    /// <param name="text">The text to associate with the identifier.</param>
    public void SetText(Languages language, string id, string text)
    {
        _files[language].SetText(id, text);
    }

    /// <summary>
    /// Removes the text associated with the specified integer identifier for a given language.
    /// </summary>
    /// <param name="language">The language of the text.</param>
    /// <param name="id">The integer identifier of the text to remove.</param>
    /// <returns><c>true</c> if the text was removed; otherwise, <c>false</c>.</returns>
    public bool RemoveText(Languages language, int id)
    {
        return _files[language].RemoveText(id);
    }

    /// <summary>
    /// Removes the text associated with the specified string identifier for a given language.
    /// </summary>
    /// <param name="language">The language of the text.</param>
    /// <param name="id">The string identifier of the text to remove.</param>
    /// <returns><c>true</c> if the text was removed; otherwise, <c>false</c>.</returns>
    public bool RemoveText(Languages language, string id)
    {
        return _files[language].RemoveText(id);
    }

    /// <summary>
    /// Retrieves the identifier of an existing text or adds a new text with a generated identifier for a given language.
    /// </summary>
    /// <param name="language">The language of the text.</param>
    /// <param name="text">The text to retrieve or add.</param>
    /// <returns>The identifier of the existing or newly added text.</returns>
    public int GetOrAddText(Languages language, string text)
    {
        return _files[language].GetOrAddText(text);
    }

    /// <summary>
    /// Retrieves the order index associated with the specified key for a given language.
    /// </summary>
    /// <param name="language">The language of the text.</param>
    /// <param name="key">The key to retrieve the order index for.</param>
    /// <returns>The order index, or -1 if not found.</returns>
    public int GetOrderIndex(Languages language, int key)
    {
        return _files[language].GetOrderIndex(key);
    }

    /// <summary>
    /// Converts a short language name to the corresponding <see cref="Languages"/> enumeration value.
    /// </summary>
    /// <param name="shortName">The short name of the language (e.g., "fr" for French).</param>
    /// <returns>The corresponding <see cref="Languages"/> value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the short name is not recognized.</exception>
    private static Languages ShortNameToLanguage(string shortName)
    {
        return shortName switch
        {
            "fr" => Languages.French,
            "en" => Languages.English,
            "de" => Languages.German,
            "ja" => Languages.Japanese,
            "es" => Languages.Spanish,
            "ru" => Languages.Russian,
            "it" => Languages.Italian,
            "pt" => Languages.Portuguese,
            "nl" => Languages.Dutch,
            _ => throw new ArgumentOutOfRangeException(nameof(shortName))
        };
    }
}
