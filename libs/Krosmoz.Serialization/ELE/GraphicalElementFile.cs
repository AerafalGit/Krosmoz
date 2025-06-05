// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.ELE;

/// <summary>
/// Represents a Ele file.
/// </summary>
public sealed class GraphicalElementFile
{
    /// <summary>
    /// Gets the version of the graphical element file.
    /// </summary>
    public sbyte Version { get; private set; }

    /// <summary>
    /// Gets the dictionary of graphical elements, where the key is the element ID
    /// and the value is the corresponding graphical element data.
    /// </summary>
    public Dictionary<int, GraphicalElementData> GraphicalElements { get; }

    /// <summary>
    /// Gets the list of gfxId used in the file.
    /// </summary>
    public List<int> GfxIds { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GraphicalElementFile"/> class.
    /// </summary>
    public GraphicalElementFile()
    {
        GraphicalElements = [];
        GfxIds = [];
    }

    /// <summary>
    /// Loads the graphical element file from the specified binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to read data from.</param>
    /// <exception cref="Exception">Thrown if the file header is invalid.</exception>
    public void Load(BigEndianReader reader)
    {
        Version = reader.ReadInt8();

        var elementsCount = reader.ReadInt32();

        for (var i = 0; i < elementsCount; i++)
        {
            if (Version >= 9)
                reader.Seek(SeekOrigin.Current, sizeof(ushort));

            var elementId = reader.ReadInt32();

            var elementType = (GraphicalElementTypes)reader.ReadInt8();

            var element = GraphicalElementFactory.Create(elementId, elementType);

            element.Deserialize(reader, Version);

            GraphicalElements.Add(elementId, element);
        }

        if (Version >= 8)
        {
            var gfxCount = reader.ReadInt32();

            for (var i = 0; i < gfxCount; i++)
                GfxIds.Add(reader.ReadInt32());
        }
    }

    /// <summary>
    /// Saves the current state of the graphical element file, including its version,
    /// graphical elements, and GfxIds.
    /// Creates a backup of the original file before saving.
    /// </summary>
    public void Save()
    {
        var writer = new BigEndianWriter();

        writer.WriteInt8(GraphicalElementAdapter.ElementFileHeader);
        writer.WriteInt8(Version);
        writer.WriteInt32(GraphicalElements.Count);

        foreach (var graphicalElement in GraphicalElements.Values)
        {
            if (Version >= 9)
            {
                var temporaryWriter = new BigEndianWriter();
                graphicalElement.Serialize(temporaryWriter, Version);
                var length = temporaryWriter.MaxPosition + sizeof(int);
                writer.WriteInt16((short)length);
            }

            writer.WriteInt32(graphicalElement.Id);
            writer.WriteInt8((sbyte)graphicalElement.Type);
            graphicalElement.Serialize(writer, Version);
        }

        if (Version >= 8)
        {
            writer.WriteInt32(GfxIds.Count);

            foreach (var gfxId in GfxIds)
                writer.WriteInt32(gfxId);
        }

        File.Copy(PathConstants.Files.ElementsPath, string.Concat(PathConstants.Files.ElementsPath, ".bak", true));
        File.WriteAllBytes(PathConstants.Files.ElementsPath, writer.ToSpan());
    }

    /// <summary>
    /// Retrieves the graphical element data for the specified element ID.
    /// </summary>
    /// <param name="elementId">The ID of the graphical element to retrieve.</param>
    /// <returns>The graphical element data, or <c>null</c> if not found.</returns>
    public GraphicalElementData? GetGraphicalElementData(int elementId)
    {
        return GraphicalElements.GetValueOrDefault(elementId);
    }

    /// <summary>
    /// Determines whether the specified graphical ID (GfxId) is a JPG.
    /// </summary>
    /// <param name="gfxId">The graphical ID to check.</param>
    /// <returns><c>true</c> if the graphical ID is a JPG; otherwise, <c>false</c>.</returns>
    public bool IsJpg(int gfxId)
    {
        return GfxIds.Contains(gfxId);
    }
}
