// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.D2P;

/// <summary>
/// Represents the index table of a D2P file.
/// </summary>
public sealed class D2PIndexTable
{
    /// <summary>
    /// The offset of the index table relative to the end of the file.
    /// </summary>
    public const int TableOffset = -24;

    /// <summary>
    /// The seek origin used to locate the index table in the file.
    /// </summary>
    public const SeekOrigin TableSeekOrigin = SeekOrigin.End;

    /// <summary>
    /// Gets the container file associated with this index table.
    /// </summary>
    public D2PFile Container { get; }

    /// <summary>
    /// Gets or sets the base offset for entries in the index table.
    /// </summary>
    public uint OffsetBase { get; set; }

    /// <summary>
    /// Gets or sets the size of the index table in bytes.
    /// </summary>
    public uint Size { get; set; }

    /// <summary>
    /// Gets or sets the offset of the entries definition section in the index table.
    /// </summary>
    public uint EntriesDefinitionOffset { get; set; }

    /// <summary>
    /// Gets or sets the number of entries in the index table.
    /// </summary>
    public uint EntriesCount { get; set; }

    /// <summary>
    /// Gets or sets the offset of the properties section in the index table.
    /// </summary>
    public uint PropertiesOffset { get; set; }

    /// <summary>
    /// Gets or sets the number of properties in the index table.
    /// </summary>
    public uint PropertiesCount { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="D2PIndexTable"/> class.
    /// </summary>
    /// <param name="container">The container file associated with this index table.</param>
    public D2PIndexTable(D2PFile container)
    {
        Container = container;
    }

    /// <summary>
    /// Writes the index table data to the specified binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to write the index table data to.</param>
    public void WriteTable(BigEndianWriter writer)
    {
        writer.WriteUInt32(OffsetBase);
        writer.WriteUInt32(Size);
        writer.WriteUInt32(EntriesDefinitionOffset);
        writer.WriteUInt32(EntriesCount);
        writer.WriteUInt32(PropertiesOffset);
        writer.WriteUInt32(PropertiesCount);
    }

    /// <summary>
    /// Reads the index table data from the specified binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to read the index table data from.</param>
    public void ReadTable(BigEndianReader reader)
    {
        OffsetBase = reader.ReadUInt32();
        Size = reader.ReadUInt32();
        EntriesDefinitionOffset = reader.ReadUInt32();
        EntriesCount = reader.ReadUInt32();
        PropertiesOffset = reader.ReadUInt32();
        PropertiesCount = reader.ReadUInt32();
    }
}
