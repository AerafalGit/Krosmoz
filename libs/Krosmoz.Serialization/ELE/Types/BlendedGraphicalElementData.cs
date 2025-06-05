// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.ELE.Types;

/// <summary>
/// Represents graphical element data with blending capabilities in the serialization format.
/// </summary>
public sealed class BlendedGraphicalElementData : NormalGraphicalElementData
{
    /// <summary>
    /// Gets or sets the blend mode of the graphical element.
    /// </summary>
    public string BlendMode { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlendedGraphicalElementData"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the graphical element.</param>
    /// <param name="type">The type of the graphical element.</param>
    public BlendedGraphicalElementData(int id, GraphicalElementTypes type) : base(id, type)
    {
        BlendMode = string.Empty;
    }

    /// <summary>
    /// Serializes the blended graphical element data to a binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to serialize the data to.</param>
    /// <param name="version">The version of the serialization format.</param>
    public override void Serialize(BigEndianWriter writer, sbyte version)
    {
        base.Serialize(writer, version);

        writer.WriteUtfPrefixedLength32(BlendMode);
    }

    /// <summary>
    /// Deserializes the blended graphical element data from a binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the data from.</param>
    /// <param name="version">The version of the serialization format.</param>
    public override void Deserialize(BigEndianReader reader, sbyte version)
    {
        base.Deserialize(reader, version);

        BlendMode = reader.ReadUtfPrefixedLength32();
    }
}
