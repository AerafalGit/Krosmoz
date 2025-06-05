// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.ELE.Types;

/// <summary>
/// Represents graphical element data for animated elements in the serialization format.
/// </summary>
public sealed class AnimatedGraphicalElementData : NormalGraphicalElementData
{
    /// <summary>
    /// Gets or sets the minimum delay for the animation.
    /// </summary>
    public int MinDelay { get; set; }

    /// <summary>
    /// Gets or sets the maximum delay for the animation.
    /// </summary>
    public int MaxDelay { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AnimatedGraphicalElementData"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the graphical element.</param>
    /// <param name="type">The type of the graphical element.</param>
    public AnimatedGraphicalElementData(int id, GraphicalElementTypes type) : base(id, type)
    {
    }

    /// <summary>
    /// Serializes the animated graphical element data to a binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to serialize the data to.</param>
    /// <param name="version">The version of the serialization format.</param>
    public override void Serialize(BigEndianWriter writer, sbyte version)
    {
        base.Serialize(writer, version);

        if (version is 4)
        {
            writer.WriteInt32(MinDelay);
            writer.WriteInt32(MaxDelay);
        }
    }

    /// <summary>
    /// Deserializes the animated graphical element data from a binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the data from.</param>
    /// <param name="version">The version of the serialization format.</param>
    public override void Deserialize(BigEndianReader reader, sbyte version)
    {
        base.Deserialize(reader, version);

        if (version is 4)
        {
            MinDelay = reader.ReadInt32();
            MaxDelay = reader.ReadInt32();
        }
    }
}
