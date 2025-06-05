// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.D2O;

/// <summary>
/// Represents a processor for D2O files, providing methods to parse and build sort indexes for fields.
/// </summary>
public sealed class D2OProcessor
{
    /// <summary>
    /// Gets the list of fields that can be queried.
    /// </summary>
    public List<string>? QueryableFields { get; private set; }

    /// <summary>
    /// Gets the dictionary mapping field names to their search index offsets.
    /// </summary>
    public Dictionary<string, long>? SearchFieldIndexes { get; private set; }

    /// <summary>
    /// Gets the dictionary mapping field names to their data types.
    /// </summary>
    public Dictionary<string, int>? SearchFieldTypes { get; private set; }

    /// <summary>
    /// Gets the dictionary mapping field names to their counts.
    /// </summary>
    public Dictionary<string, int>? SearchFieldCounts { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="D2OProcessor"/> class.
    /// </summary>
    /// <param name="reader">The binary reader to read data from the D2O file.</param>
    public D2OProcessor(BigEndianReader reader)
    {
        ParseReader(reader);
    }

    /// <summary>
    /// Parses the D2O file to initialize queryable fields and their associated metadata.
    /// </summary>
    /// <param name="reader">The binary reader to read data from the D2O file.</param>
    private void ParseReader(BigEndianReader reader)
    {
        QueryableFields ??= [];
        SearchFieldIndexes ??= [];
        SearchFieldTypes ??= [];
        SearchFieldCounts ??= [];

        var fieldListSize = reader.ReadInt32();
        var indexSearchOffset = reader.Position + fieldListSize + sizeof(int);

        while (fieldListSize > 0)
        {
            var size = reader.Remaining;
            var fieldName = reader.ReadUtfPrefixedLength16();
            var index = reader.ReadInt32();

            QueryableFields.Add(fieldName);
            SearchFieldIndexes[fieldName] = index + indexSearchOffset;
            SearchFieldTypes[fieldName] = reader.ReadInt32();
            SearchFieldCounts[fieldName] = reader.ReadInt32();

            fieldListSize -= size - reader.Remaining;
        }
    }
}
