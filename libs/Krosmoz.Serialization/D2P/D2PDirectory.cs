// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Serialization.D2P;

/// <summary>
/// Represents a directory in a D2P file structure.
/// </summary>
public sealed class D2PDirectory
{
    /// <summary>
    /// Gets or sets the container file associated with this directory.
    /// </summary>
    public D2PFile Container { get; set; }

    private string? _name;
    private D2PDirectory? _directory;

    /// <summary>
    /// Gets or sets the name of the directory.
    /// Updates the full name when set.
    /// </summary>
    public string Name
    {
        get => _name ?? string.Empty;
        set
        {
            _name = value;
            UpdateFullName();
        }
    }

    /// <summary>
    /// Gets or sets the full name of the directory, including its path.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Gets or sets the parent directory of this directory.
    /// Updates the full name when set.
    /// </summary>
    public D2PDirectory? Parent
    {
        get => _directory;
        set
        {
            _directory = value;
            UpdateFullName();
        }
    }

    /// <summary>
    /// Gets or sets the list of entries in this directory.
    /// </summary>
    public List<D2PEntry> Entries { get; set; }

    /// <summary>
    /// Gets or sets the dictionary of subdirectories within this directory.
    /// </summary>
    public Dictionary<string, D2PDirectory> Directories { get; set; }

    /// <summary>
    /// Gets a value indicating whether this directory is the root directory.
    /// </summary>
    public bool IsRoot =>
        Parent is null;

    /// <summary>
    /// Initializes a new instance of the <see cref="D2PDirectory"/> class.
    /// </summary>
    /// <param name="container">The container file associated with this directory.</param>
    /// <param name="name">The name of the directory.</param>
    public D2PDirectory(D2PFile container, string name)
    {
        Container = container;
        Name = name;
        FullName = name;
        Entries = [];
        Directories = [];
    }

    /// <summary>
    /// Updates the full name of the directory based on its parent directories.
    /// </summary>
    private void UpdateFullName()
    {
        FullName = Name;

        for (var current = this; current.Parent is not null; current = current.Parent)
            FullName = FullName.Insert(0, current.Parent.Name + "\\");
    }

    /// <summary>
    /// Determines whether a subdirectory with the specified name exists.
    /// </summary>
    /// <param name="directory">The name of the subdirectory to check.</param>
    /// <returns><c>true</c> if the subdirectory exists; otherwise, <c>false</c>.</returns>
    public bool HasDirectory(string directory)
    {
        return Directories.ContainsKey(directory);
    }

    /// <summary>
    /// Gets the subdirectory with the specified name.
    /// </summary>
    /// <param name="name">The name of the subdirectory to retrieve.</param>
    /// <returns>The subdirectory if found; otherwise, <c>null</c>.</returns>
    public D2PDirectory? GetDirectory(string name)
    {
        return Directories.GetValueOrDefault(name);
    }

    /// <summary>
    /// Determines whether an entry with the specified name exists in this directory.
    /// </summary>
    /// <param name="entryName">The name of the entry to check.</param>
    /// <returns><c>true</c> if the entry exists; otherwise, <c>false</c>.</returns>
    public bool HasEntry(string entryName)
    {
        return Entries.Any(entry => entry.FullFileName == entryName);
    }

    /// <summary>
    /// Gets the entry with the specified name.
    /// </summary>
    /// <param name="entryName">The name of the entry to retrieve.</param>
    /// <returns>The entry if found; otherwise, <c>null</c>.</returns>
    public D2PEntry? GetEntry(string entryName)
    {
        return Entries.SingleOrDefault(entry => entry.FullFileName == entryName);
    }

    /// <summary>
    /// Gets all entries in this directory and its subdirectories.
    /// </summary>
    /// <returns>An enumerable collection of all entries.</returns>
    public IEnumerable<D2PEntry> GetAllEntries()
    {
        return Entries.Concat(Directories.SelectMany(static x => x.Value.GetAllEntries()));
    }
}
