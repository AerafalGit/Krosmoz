// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.D2P;

/// <summary>
/// Represents an entry in a D2P file.
/// </summary>
public sealed class D2PEntry
{
    private byte[] _newData;

    /// <summary>
    /// Gets the container file associated with this entry.
    /// </summary>
    public D2PFile Container { get; }

    /// <summary>
    /// Gets or sets the full file name of the entry, including its path.
    /// </summary>
    public string FullFileName { get; set; }

    /// <summary>
    /// Gets or sets the file name of the entry (without the path).
    /// Updates the full file name when set.
    /// </summary>
    public string FileName
    {
        get => Path.GetFileName(FullFileName);
        set => FullFileName = Path.Combine(Path.GetDirectoryName(FullFileName)!, value);
    }

    /// <summary>
    /// Gets or sets the directory associated with this entry.
    /// </summary>
    public D2PDirectory Directory { get; set; }

    /// <summary>
    /// Gets or sets the index of the entry in the D2P file.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Gets or sets the size of the entry in bytes.
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Gets or sets the state of the entry (e.g., added, removed, dirty).
    /// </summary>
    public D2PEntryState State { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="D2PEntry"/> class with the specified container.
    /// </summary>
    /// <param name="container">The container file associated with this entry.</param>
    public D2PEntry(D2PFile container)
    {
        Container = container;
        Index = -1;
        _newData = [];
        FullFileName = string.Empty;
        Directory = null!;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="D2PEntry"/> class with the specified container and file name.
    /// </summary>
    /// <param name="container">The container file associated with this entry.</param>
    /// <param name="fileName">The full file name of the entry.</param>
    public D2PEntry(D2PFile container, string fileName)
    {
        Container = container;
        FullFileName = fileName;
        Index = -1;
        _newData = [];
        Directory = null!;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="D2PEntry"/> class with the specified container, file name, and data.
    /// </summary>
    /// <param name="container">The container file associated with this entry.</param>
    /// <param name="fileName">The full file name of the entry.</param>
    /// <param name="data">The data associated with the entry.</param>
    public D2PEntry(D2PFile container, string fileName, byte[] data)
    {
        Container = container;
        FullFileName = fileName;
        _newData = data;
        State = D2PEntryState.Added;
        Size = data.Length;
        Index = -1;
        Directory = null!;
    }

    /// <summary>
    /// Writes the entry definition to the specified binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to write the entry definition to.</param>
    public void WriteEntryDefinition(BigEndianWriter writer)
    {
        writer.WriteUtfPrefixedLength16(FullFileName);
        writer.WriteInt32(Index);
        writer.WriteInt32(Size);
    }

    /// <summary>
    /// Reads the entry definition from the specified binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to read the entry definition from.</param>
    public void ReadEntryDefinition(BigEndianReader reader)
    {
        FullFileName = reader.ReadUtfPrefixedLength16();
        Index = reader.ReadInt32();
        Size = reader.ReadInt32();
    }

    /// <summary>
    /// Reads the entry data from the specified binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to read the entry data from.</param>
    /// <returns>The entry data as a byte array.</returns>
    /// <exception cref="Exception">Thrown if the entry is in a removed state.</exception>
    public byte[] ReadEntry(BigEndianReader reader)
    {
        return State switch
        {
            D2PEntryState.None => reader.ReadSpan(Size).ToArray(),
            D2PEntryState.Dirty or D2PEntryState.Added => _newData,
            D2PEntryState.Removed => throw new Exception("Cannot read a removed entry."),
            _ => reader.ReadSpan(Size).ToArray()
        };
    }

    /// <summary>
    /// Modifies the entry by updating its data, marking it as dirty, and adjusting its size.
    /// </summary>
    /// <param name="data">The new data to replace the current entry data.</param>
    public void ModifyEntry(byte[] data)
    {
        _newData = data;
        State = D2PEntryState.Dirty;
        Size = data.Length;
    }

    /// <summary>
    /// Gets the directory names in the path of the entry.
    /// </summary>
    /// <returns>An array of directory names in the entry's path.</returns>
    public string[] GetDirectoryNames()
    {
        return Path.GetDirectoryName(FullFileName)!.Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries);
    }
}
