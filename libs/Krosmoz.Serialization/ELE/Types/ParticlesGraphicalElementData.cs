// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.ELE.Types;

/// <summary>
/// Represents graphical element data for particles in the serialization format.
/// </summary>
public sealed class ParticlesGraphicalElementData : GraphicalElementData
{
    /// <summary>
    /// Gets or sets the script identifier associated with the particle element.
    /// </summary>
    public short ScriptId { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParticlesGraphicalElementData"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the graphical element.</param>
    /// <param name="type">The type of the graphical element.</param>
    public ParticlesGraphicalElementData(int id, GraphicalElementTypes type) : base(id, type)
    {
    }

    /// <summary>
    /// Serializes the particle graphical element data to a binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to serialize the data to.</param>
    /// <param name="version">The version of the serialization format.</param>
    public override void Serialize(BigEndianWriter writer, sbyte version)
    {
        writer.WriteInt16(ScriptId);
    }

    /// <summary>
    /// Deserializes the particle graphical element data from a binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the data from.</param>
    /// <param name="version">The version of the serialization format.</param>
    public override void Deserialize(BigEndianReader reader, sbyte version)
    {
        ScriptId = reader.ReadInt16();
    }
}
