// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Krosmoz.Core.IO.Binary;
using Krosmoz.Serialization.DLM;

namespace Krosmoz.Serialization.D2P;

/// <summary>
/// Represents a D2P file.
/// </summary>
public sealed class D2PFile
{
    private readonly Dictionary<string, D2PEntry> _entries;
    private readonly List<D2PFile> _links;
    private readonly ConcurrentQueue<D2PFile> _linksToSave;
    private readonly List<D2PProperty> _properties;
    private readonly Dictionary<string, D2PDirectory> _directories;

    private BigEndianReader _reader;

    /// <summary>
    /// Gets a value indicating whether links to other D2P files should be registered.
    /// </summary>
    public bool RegisterLinks { get; }

    /// <summary>
    /// Gets the index table of the D2P file.
    /// </summary>
    public D2PIndexTable IndexTable { get; }

    /// <summary>
    /// Gets the file path of the D2P file.
    /// </summary>
    public string FilePath { get; }

    /// <summary>
    /// Gets the file name of the D2P file.
    /// </summary>
    public string FileName =>
        Path.GetFileName(FilePath);

    /// <summary>
    /// Gets the properties of the D2P file.
    /// </summary>
    public IEnumerable<D2PProperty> Properties =>
        _properties;

    /// <summary>
    /// Gets the entries in the D2P file.
    /// </summary>
    public IEnumerable<D2PEntry> Entries =>
        _entries.Values;

    /// <summary>
    /// Gets the linked D2P files.
    /// </summary>
    public IEnumerable<D2PFile> Links =>
        _links;

    /// <summary>
    /// Gets the root directories in the D2P file.
    /// </summary>
    public IEnumerable<D2PDirectory> RootDirectories =>
        _directories.Values;

    /// <summary>
    /// Gets a value indicating whether the file path is valid.
    /// </summary>
    public bool HasFilePath =>
        !string.IsNullOrEmpty(FilePath);

    /// <summary>
    /// Initializes a new instance of the <see cref="D2PFile"/> class with default values.
    /// </summary>
    public D2PFile()
    {
        IndexTable = new D2PIndexTable(this);
        FilePath = string.Empty;
        _reader = new BigEndianReader(Array.Empty<byte>());
        _entries = [];
        _links = [];
        _linksToSave = [];
        _properties = [];
        _directories = [];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="D2PFile"/> class.
    /// </summary>
    /// <param name="filePath">The file path of the D2P file.</param>
    /// <param name="registerLinks">Indicates whether links to other D2P files should be registered.</param>
    public D2PFile(string filePath, bool registerLinks = true)
    {
        RegisterLinks = registerLinks;
        IndexTable = new D2PIndexTable(this);
        FilePath = filePath;

        _reader = new BigEndianReader(File.ReadAllBytes(filePath));
        _entries = [];
        _links = [];
        _linksToSave = [];
        _properties = [];
        _directories = [];

        InternalOpen();
    }

    /// <summary>
    /// Saves the current D2P file to its file path.
    /// </summary>
    /// <exception cref="Exception">
    /// Thrown if the file path is not set. Use <see cref="SaveAs"/> instead.
    /// </exception>
    public void Save()
    {
        if (!HasFilePath)
            throw new Exception($"Cannot save a D2P file without a file path, use {nameof(SaveAs)} instead.");

        while (_linksToSave.TryDequeue(out var link))
            link.Save();

        InternalSave(FilePath);

        _reader = new BigEndianReader(File.ReadAllBytes(FilePath));
    }

    /// <summary>
    /// Saves the current D2P file to a specified file path.
    /// </summary>
    /// <param name="filePath">The file path to save the D2P file to.</param>
    /// <param name="saveLinks">
    /// Indicates whether to save linked D2P files to the same directory as the specified file path.
    /// </param>
    public void SaveAs(string filePath, bool saveLinks = true)
    {
        if (filePath == FilePath)
        {
            Save();
            return;
        }

        if (saveLinks)
        {
            foreach (var link in Links)
                link.SaveAs(Path.Combine(Path.GetDirectoryName(filePath)!, link.FileName));
        }

        InternalSave(filePath);

        _reader = new BigEndianReader(File.ReadAllBytes(filePath));
    }

    /// <summary>
    /// Reads the file data for a specified file path from the D2P file.
    /// </summary>
    /// <param name="filePath">The file path of the file to read.</param>
    /// <returns>A byte array containing the file data.</returns>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the specified file does not exist in the D2P file.
    /// </exception>
    public byte[] ReadFile(string filePath)
    {
        if (!Exists(filePath))
            throw new FileNotFoundException(filePath);

        return ReadFile(GetEntry(filePath));
    }

    /// <summary>
    /// Reads the file data for the specified entry from the D2P file.
    /// </summary>
    /// <param name="entry">The entry to read the file data for.</param>
    /// <returns>A byte array containing the file data.</returns>
    public byte[] ReadFile(D2PEntry entry)
    {
        if (entry.Container != this)
            return entry.Container.ReadFile(entry);

        if (entry.Index >= 0 && IndexTable.OffsetBase + entry.Index >= 0)
            _reader.Seek(SeekOrigin.Begin, (int)IndexTable.OffsetBase + entry.Index);

        return entry.ReadEntry(_reader);
    }

    /// <summary>
    /// Extracts a file from the D2P file to the current directory.
    /// </summary>
    /// <param name="filePath">The file path of the file to extract.</param>
    /// <param name="overwrite">
    /// Indicates whether to overwrite the file if it already exists in the destination.
    /// </param>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the specified file does not exist in the D2P file.
    /// </exception>
    public void ExtractFile(string filePath, bool overwrite = false)
    {
        if (!Exists(filePath))
            throw new FileNotFoundException(filePath);

        var entry = GetEntry(filePath);

        var destinationPath = Path.Combine("./", entry.FullFileName);

        if (!Directory.Exists(Path.GetDirectoryName(destinationPath)))
            Directory.CreateDirectory(destinationPath);

        ExtractFile(filePath, destinationPath, overwrite);
    }

    /// <summary>
    /// Extracts a file from the D2P file to a specified destination path.
    /// </summary>
    /// <param name="filePath">The file path of the file to extract.</param>
    /// <param name="destinationPath">The destination path to extract the file to.</param>
    /// <param name="overwrite">
    /// Indicates whether to overwrite the file if it already exists in the destination.
    /// </param>
    /// <exception cref="IOException">
    /// Thrown if the file already exists and overwrite is set to false.
    /// </exception>
    public void ExtractFile(string filePath, string destinationPath, bool overwrite = false)
    {
        var bytes = ReadFile(filePath);

        if (Directory.Exists(destinationPath))
        {
            var attributes = File.GetAttributes(destinationPath);

            if ((attributes & FileAttributes.Directory) is FileAttributes.Directory)
                destinationPath = Path.Combine(destinationPath, Path.GetFileName(filePath));
        }

        if (File.Exists(destinationPath))
        {
            if (!overwrite)
                throw new IOException($"File already exists: {destinationPath}, use overwrite parameter to replace it.");

            File.Copy(destinationPath, string.Concat(destinationPath, ".bak"), true);
        }

        if (!Directory.Exists(Path.GetDirectoryName(destinationPath)))
            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

        File.WriteAllBytes(destinationPath, bytes);
    }

    /// <summary>
    /// Extracts a directory and its contents from the D2P file to a specified destination path.
    /// </summary>
    /// <param name="directoryPath">The path of the directory to extract.</param>
    /// <param name="destinationPath">The destination path to extract the directory to.</param>
    /// <exception cref="DirectoryNotFoundException">
    /// Thrown if the specified directory does not exist in the D2P file.
    /// </exception>
    public void ExtractDirectory(string directoryPath, string destinationPath)
    {
        if (!HasDirectory(directoryPath))
            throw new DirectoryNotFoundException(directoryPath);

        if (!TryGetDirectory(directoryPath, out var directory))
            return;

        if (!Directory.Exists(Path.Combine(destinationPath, directory.FullName)))
            Directory.CreateDirectory(Path.Combine(destinationPath, directory.FullName));

        foreach (var entry in directory.Entries)
            ExtractFile(entry.FullFileName, Path.Combine(destinationPath, entry.FullFileName));

        foreach (var subDirectory in directory.Directories)
            ExtractDirectory(subDirectory.Value.FullName, destinationPath);
    }

    /// <summary>
    /// Extracts all files from the D2P file to a specified destination path.
    /// </summary>
    /// <param name="destinationPath">The destination path to extract all files to.</param>
    /// <param name="overwrite">
    /// Indicates whether to overwrite files if they already exist in the destination.
    /// </param>
    /// <exception cref="IOException">
    /// Thrown if a file already exists and overwrite is set to false.
    /// </exception>
    public void ExtractAllFiles(string destinationPath, bool overwrite = false)
    {
        if (!Directory.Exists(Path.GetDirectoryName(destinationPath)))
            Directory.CreateDirectory(destinationPath);

        foreach (var directoryName in _entries.Select(static x => x.Value.GetDirectoryNames()).Distinct())
        {
            var destionationPath = Path.Combine(Path.GetFullPath(destinationPath), Path.Combine(directoryName));

            if (!Directory.Exists(destionationPath))
                Directory.CreateDirectory(destionationPath);
        }

        foreach (var entry in _entries)
        {
            if (File.Exists(Path.GetFullPath(destinationPath)))
            {
                if (!overwrite)
                    throw new IOException($"File already exists: {Path.GetFullPath(destinationPath)}, use overwrite parameter to replace it.");

                File.Copy(Path.GetFullPath(destinationPath), string.Concat(Path.GetFullPath(destinationPath), ".bak"), true);
            }

            var destinationFilePath = Path.Combine(Path.GetFullPath(destinationPath), entry.Value.FullFileName);

            File.WriteAllBytes(destinationFilePath, ReadFile(entry.Value));
        }
    }

    /// <summary>
    /// Attempts to extract a map from the D2P file using the specified map ID.
    /// </summary>
    /// <param name="id">The ID of the map to extract.</param>
    /// <param name="map">When this method returns, contains the extracted map if found; otherwise, <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> if the map was successfully extracted; otherwise, <c>false</c>.
    /// </returns>
    public bool TryExtractMap(int id, [NotNullWhen(true)] out DlmMap? map)
    {
        if (TryGetEntry($"{id.ToString(CultureInfo.InvariantCulture).Last()}/{id}.dlm", out var entry))
        {
            map = DlmAdapter.Load(ReadFile(entry));
            return true;
        }

        map = null;
        return false;
    }

    /// <summary>
    /// Gets the entries that belong only to this D2P file instance.
    /// </summary>
    /// <returns>An enumerable collection of entries.</returns>
    public IEnumerable<D2PEntry> GetEntriesOfInstanceOnly()
    {
        return _entries.Values.Where(entry => entry.Container == this);
    }

    /// <summary>
    /// Gets an entry by its file name.
    /// </summary>
    /// <param name="fileName">The file name of the entry.</param>
    /// <returns>The entry with the specified file name.</returns>
    public D2PEntry GetEntry(string fileName)
    {
        return _entries[fileName];
    }

    /// <summary>
    /// Tries to get an entry by its file name.
    /// </summary>
    /// <param name="fileName">The file name of the entry.</param>
    /// <param name="entry">When this method returns, contains the entry if found; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the entry was found; otherwise, <c>false</c>.</returns>
    public bool TryGetEntry(string fileName, [NotNullWhen(true)] out D2PEntry? entry)
    {
        return _entries.TryGetValue(fileName, out entry);
    }

    /// <summary>
    /// Gets the latest linked D2P file in the chain of links.
    /// </summary>
    /// <returns>The latest linked D2P file.</returns>
    public D2PFile GetLatestLink()
    {
        var lastLink = this;

        for (var link = _links.FirstOrDefault(); link is not null; link = link.Links.FirstOrDefault())
            lastLink = link;

        return lastLink;
    }

    /// <summary>
    /// Clears all entries and directories from the D2P file.
    /// </summary>
    public void Clear()
    {
        _entries.Clear();
        _directories.Clear();
    }

    /// <summary>
    /// Adds a file to the D2P file using its file path.
    /// </summary>
    /// <param name="filePath">The full path of the file to add.</param>
    /// <returns>The added D2P entry.</returns>
    public D2PEntry AddFile(string filePath)
    {
        var bytes = File.ReadAllBytes(filePath);

        var destinationPath = filePath;

        if (HasFilePath)
            destinationPath = GetRelativePath(filePath, Path.GetDirectoryName(FilePath)!);

        return AddFile(destinationPath, bytes);
    }

    /// <summary>
    /// Adds a file to the D2P file using its file path and a specified relative path.
    /// </summary>
    /// <param name="filePath">The full path of the file to add.</param>
    /// <param name="relativePath">The relative path to use for the file in the D2P file.</param>
    /// <returns>The added D2P entry.</returns>
    public D2PEntry AddFile(string filePath, string relativePath)
    {
        var bytes = File.ReadAllBytes(filePath);
        return AddFile(relativePath, bytes);
    }

    /// <summary>
    /// Adds a file to the D2P file using its byte data and a specified relative path.
    /// </summary>
    /// <param name="bytes">The byte array representing the file data.</param>
    /// <param name="relativePath">The relative path to use for the file in the D2P file.</param>
    /// <returns>The added D2P entry.</returns>
    public D2PEntry AddFile(byte[] bytes, string relativePath)
    {
        return AddFile(relativePath, bytes);
    }

    /// <summary>
    /// Adds a file to the D2P file using a relative path and its byte data.
    /// </summary>
    /// <param name="relativePath">The relative path to use for the file in the D2P file.</param>
    /// <param name="bytes">The byte array representing the file data.</param>
    /// <returns>The added D2P entry.</returns>
    public D2PEntry AddFile(string relativePath, byte[] bytes)
    {
        var entry = new D2PEntry(this, relativePath, bytes);

        AddEntry(entry);

        return entry;
    }

    /// <summary>
    /// Removes a file from the D2P file.
    /// </summary>
    /// <param name="filePath">The file path of the file to remove.</param>
    /// <returns>
    /// <c>true</c> if the file was successfully removed; otherwise, <c>false</c>.
    /// </returns>
    public bool RemoveFile(string filePath)
    {
        return TryGetEntry(filePath, out var entry) && RemoveEntry(entry);
    }

    /// <summary>
    /// Modifies an existing file in the D2P file with new byte data.
    /// </summary>
    /// <param name="filePath">The file path of the file to modify.</param>
    /// <param name="bytes">The new byte data to replace the file's content.</param>
    /// <returns>
    /// <c>true</c> if the file was successfully modified; otherwise, <c>false</c>.
    /// </returns>
    public bool ModifyFile(string filePath, byte[] bytes)
    {
        if (!TryGetEntry(filePath, out var entry))
            return false;

        entry.ModifyEntry(bytes);

        lock (_linksToSave)
        {
            if (entry.Container != this && !_linksToSave.Contains(entry.Container))
                _linksToSave.Enqueue(entry.Container);
        }

        return true;
    }

    /// <summary>
    /// Adds an entry to the D2P file.
    /// </summary>
    /// <param name="entry">The entry to add.</param>
    public void AddEntry(D2PEntry entry)
    {
        entry.State = D2PEntryState.Added;
        InternalAddEntry(entry);
        IndexTable.EntriesCount++;
    }

    /// <summary>
    /// Removes an entry from the D2P file.
    /// </summary>
    /// <param name="entry">The entry to remove.</param>
    /// <returns>
    /// <c>true</c> if the entry was successfully removed; otherwise, <c>false</c>.
    /// </returns>
    public bool RemoveEntry(D2PEntry entry)
    {
        if (entry.Container != this)
        {
            if (!entry.Container.RemoveEntry(entry))
                return false;

            if (!_linksToSave.Contains(entry.Container))
                _linksToSave.Enqueue(entry.Container);
        }

        if (!_entries.Remove(entry.FullFileName))
            return false;

        entry.State = D2PEntryState.Removed;
        InternalRemoveDirectories(entry);

        if (entry.Container == this)
            IndexTable.EntriesCount--;

        return true;
    }

    /// <summary>
    /// Checks if a directory exists in the D2P file.
    /// </summary>
    /// <param name="directoryPath">The path of the directory to check.</param>
    /// <returns>
    /// <c>true</c> if the directory exists; otherwise, <c>false</c>.
    /// </returns>
    public bool HasDirectory(string directoryPath)
    {
        var directoriesName = directoryPath.Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries);

        if (directoriesName.Length is 0)
            return false;

        if (!_directories.TryGetValue(directoriesName[0], out var directory))
            return false;

        foreach (var directoryName in directoriesName.Skip(1))
        {
            if (directory is null)
                return false;

            if (!directory.HasDirectory(directoryName))
                return false;

            directory = directory.GetDirectory(directoryName);
        }

        return true;
    }

    /// <summary>
    /// Tries to get a directory by its path.
    /// </summary>
    /// <param name="directoryPath">The path of the directory to retrieve.</param>
    /// <param name="directory">
    /// When this method returns, contains the directory if found; otherwise, <c>null</c>.
    /// </param>
    /// <returns>
    /// <c>true</c> if the directory was found; otherwise, <c>false</c>.
    /// </returns>
    public bool TryGetDirectory(string directoryPath, [NotNullWhen(true)] out D2PDirectory? directory)
    {
        directory = null;

        var directoriesName = directoryPath.Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries);

        if (directoriesName.Length is 0)
            return false;

        if (!_directories.TryGetValue(directoriesName[0], out var currentDirectory))
            return false;

        foreach (var directoryName in directoriesName.Skip(1))
        {
            if (currentDirectory is not null && !currentDirectory.HasDirectory(directoryName))
                return false;

            currentDirectory = currentDirectory?.GetDirectory(directoryName);
        }

        directory = currentDirectory!;
        return true;
    }

    /// <summary>
    /// Gets the directory tree for a specified directory path.
    /// </summary>
    /// <param name="directoryPath">The path of the directory to retrieve the tree for.</param>
    /// <returns>
    /// A collection of directories representing the directory tree.
    /// </returns>
    public IEnumerable<D2PDirectory> GetDirectoryTree(string directoryPath)
    {
        var tree = new List<D2PDirectory>();

        var directoriesName = directoryPath.Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries);

        if (directoriesName.Length is 0 || !_directories.TryGetValue(directoriesName[0], out var currentDirectory))
            return tree;

        tree.Add(currentDirectory);

        foreach (var directoryName in directoriesName.Skip(1))
        {
            if (currentDirectory is null || !currentDirectory.HasDirectory(directoryName))
                return tree;

            currentDirectory = currentDirectory.GetDirectory(directoryName);

            if (currentDirectory is null)
                return tree;

            tree.Add(currentDirectory);
        }

        return tree;
    }

    /// <summary>
    /// Checks if a file exists in the D2P file.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <returns>
    /// <c>true</c> if the file exists; otherwise, <c>false</c>.
    /// </returns>
    public bool Exists(string filePath)
    {
        return _entries.ContainsKey(filePath);
    }

    /// <summary>
    /// Reads all files from the D2P file.
    /// </summary>
    /// <returns>
    /// A dictionary containing all entries and their corresponding byte data.
    /// </returns>
    public Dictionary<D2PEntry, byte[]> ReadAllFiles()
    {
        return _entries.ToDictionary(static x => x.Value, x => ReadFile(x.Value));
    }

    /// <summary>
    /// Opens the D2P file and reads its contents.
    /// </summary>
    private void InternalOpen()
    {
        if (_reader.ReadUInt8() is not 2 || _reader.ReadUInt8() is not 1)
            throw new Exception("Invalid D2P file.");

        ReadTable();
        ReadProperties();
        ReadEntryDefinitions();
    }

    /// <summary>
    /// Saves the current D2P file to the specified file path.
    /// </summary>
    /// <param name="filePath">The file path to save the D2P file to.</param>
    private void InternalSave(string filePath)
    {
        var writer = new BigEndianWriter();

        writer.WriteUInt8(2);
        writer.WriteUInt8(1);

        var entries = GetEntriesOfInstanceOnly().ToArray();

        foreach (var entry in entries)
        {
            var data = ReadFile(entry);
            entry.Index = writer.Position - sizeof(short);
            writer.WriteSpan(data);
        }

        var entriesDefinitionOffset = (uint)writer.Position;

        foreach (var entry in entries)
            entry.WriteEntryDefinition(writer);

        var propertiesOffset = (uint)writer.Position;

        foreach (var property in _properties)
            property.Serialize(writer);

        IndexTable.OffsetBase = sizeof(short);
        IndexTable.EntriesCount = (uint)entries.Length;
        IndexTable.EntriesDefinitionOffset = entriesDefinitionOffset;
        IndexTable.PropertiesCount = (uint)_properties.Count;
        IndexTable.PropertiesOffset = propertiesOffset;
        IndexTable.Size = IndexTable.EntriesDefinitionOffset - IndexTable.OffsetBase;

        IndexTable.WriteTable(writer);

        File.Copy(filePath, string.Concat(filePath, ".bak"), true);
        File.WriteAllBytes(filePath, writer.ToSpan());
    }

    /// <summary>
    /// Reads the index table from the D2P file.
    /// </summary>
    private void ReadTable()
    {
        _reader.Seek(D2PIndexTable.TableSeekOrigin, D2PIndexTable.TableOffset);

        IndexTable.ReadTable(_reader);
    }

    /// <summary>
    /// Reads the properties from the D2P file.
    /// </summary>
    private void ReadProperties()
    {
        _reader.Seek(SeekOrigin.Begin, (int)IndexTable.PropertiesOffset);

        for (var i = 0; i < IndexTable.PropertiesCount; i++)
        {
            var property = new D2PProperty();
            property.Deserialize(_reader);

            if (property.Key is "link")
                InternalAddLink(property.Value);

            _properties.Add(property);
        }
    }

    /// <summary>
    /// Reads the entry definitions from the D2P file.
    /// </summary>
    private void ReadEntryDefinitions()
    {
        _reader.Seek(SeekOrigin.Begin, (int)IndexTable.EntriesDefinitionOffset);

        for (var i = 0; i < IndexTable.EntriesCount; i++)
        {
            var entry = new D2PEntry(this);
            entry.ReadEntryDefinition(_reader);
            InternalAddEntry(entry);
        }
    }

    /// <summary>
    /// Adds a link to another D2P file.
    /// </summary>
    /// <param name="linkFile">The file path of the linked D2P file.</param>
    private void InternalAddLink(string linkFile)
    {
        var path = GetLinkFileAbsolutePath(linkFile);

        if (!File.Exists(path))
            throw new FileNotFoundException(linkFile);

        var link = new D2PFile(path);

        if (RegisterLinks)
        {
            foreach (var entry in link.Entries)
                InternalAddEntry(entry);
        }

        _links.Add(link);
    }

    /// <summary>
    /// Removes directories associated with the specified entry from the D2P file.
    /// </summary>
    /// <param name="entry">The entry whose directories are to be removed.</param>
    private void InternalRemoveDirectories(D2PEntry entry)
    {
        for (var current = entry.Directory; current is not null; current = current.Parent)
        {
            current.Entries.Remove(entry);

            if (current.Parent is { Entries.Count: 0 })
                current.Parent.Directories.Remove(current.Name);

            else if (current is { IsRoot: true, Entries.Count: 0 })
                _directories.Remove(current.Name);
        }
    }

    /// <summary>
    /// Gets the absolute path of a linked file.
    /// </summary>
    /// <param name="linkFile">The relative or absolute path of the linked file.</param>
    /// <returns>The absolute path of the linked file.</returns>
    private string GetLinkFileAbsolutePath(string linkFile)
    {
        if (File.Exists(linkFile) || !HasFilePath)
            return linkFile;

        var absolutePath = Path.Combine(Path.GetDirectoryName(FilePath)!, linkFile);

        return File.Exists(absolutePath) ? absolutePath : linkFile;
    }

    /// <summary>
    /// Adds an entry to the D2P file.
    /// </summary>
    /// <param name="entry">The entry to add.</param>
    private void InternalAddEntry(D2PEntry entry)
    {
        if (TryGetEntry(entry.FullFileName, out var registerdEntry))
            _entries[registerdEntry.FullFileName] = entry;
        else
            _entries.TryAdd(entry.FullFileName, entry);

        InternalAddDirectories(entry);
    }

    /// <summary>
    /// Adds directories for an entry to the D2P file.
    /// </summary>
    /// <param name="entry">The entry for which directories are added.</param>
    private void InternalAddDirectories(D2PEntry entry)
    {
        var directories = entry.GetDirectoryNames();

        if (directories.Length is 0)
            return;

        D2PDirectory? current;

        if (!_directories.TryGetValue(directories[0], out var value))
            _directories.TryAdd(directories[0], current = new D2PDirectory(this, directories[0]));
        else
            current = value;

        if (directories.Length == 1)
            current.Entries.Add(entry);

        for (var i = 1; i < directories.Length; i++)
        {
            var directory = directories[i];
            var next= current.GetDirectory(directory);

            if (next is null)
            {
                var dir = new D2PDirectory(this, directory) { Parent = current };
                current.Directories.Add(directory, dir);
                current = dir;
            }
            else
                current = next;

            if (i == directories.Length - 1)
                current.Entries.Add(entry);
        }

        entry.Directory = current;
    }

    /// <summary>
    /// Gets the relative path of a file with respect to a directory.
    /// </summary>
    /// <param name="filePath">The full path of the file.</param>
    /// <param name="directoryPath">The full path of the directory.</param>
    /// <returns>The relative path of the file from the directory.</returns>
    private static string GetRelativePath(string filePath, string directoryPath)
    {
        var uri = new Uri(Path.GetFullPath(filePath));
        var directoryUri = new Uri(Path.GetFullPath(directoryPath));
        return directoryUri.MakeRelativeUri(uri).ToString();
    }
}
