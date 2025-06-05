// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.ELE.Types;

/// <summary>
/// Represents graphical element data for entities in the serialization format.
/// </summary>
public sealed class EntityGraphicalElementData : GraphicalElementData
{
    /// <summary>
    /// Gets or sets the entity look, represented as a string.
    /// </summary>
    public string EntityLook { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity has horizontal symmetry.
    /// </summary>
    public bool HorizontalSymmetry { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity should play an animation.
    /// </summary>
    public bool PlayAnimation { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity should play a static animation.
    /// </summary>
    public bool PlayAnimStatic { get; set; }

    /// <summary>
    /// Gets or sets the minimum delay for animations.
    /// </summary>
    public int MinDelay { get; set; }

    /// <summary>
    /// Gets or sets the maximum delay for animations.
    /// </summary>
    public int MaxDelay { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityGraphicalElementData"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the graphical element.</param>
    /// <param name="type">The type of the graphical element.</param>
    public EntityGraphicalElementData(int id, GraphicalElementTypes type) : base(id, type)
    {
        EntityLook = string.Empty;
    }

    /// <summary>
    /// Serializes the entity graphical element data to a binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to serialize the data to.</param>
    /// <param name="version">The version of the serialization format.</param>
    public override void Serialize(BigEndianWriter writer, sbyte version)
    {
        writer.WriteUtfPrefixedLength32(EntityLook);
        writer.WriteBoolean(HorizontalSymmetry);

        if (version >= 7)
            writer.WriteBoolean(PlayAnimation);

        if (version >= 6)
            writer.WriteBoolean(PlayAnimStatic);

        if (version >= 5)
        {
            writer.WriteInt32(MinDelay);
            writer.WriteInt32(MaxDelay);
        }
    }

    /// <summary>
    /// Deserializes the entity graphical element data from a binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the data from.</param>
    /// <param name="version">The version of the serialization format.</param>
    public override void Deserialize(BigEndianReader reader, sbyte version)
    {
        EntityLook = reader.ReadUtfPrefixedLength32();
        HorizontalSymmetry = reader.ReadBoolean();

        if (version >= 7)
            PlayAnimation = reader.ReadBoolean();

        if (version >= 6)
            PlayAnimStatic = reader.ReadBoolean();

        if (version >= 5)
        {
            MinDelay = reader.ReadInt32();
            MaxDelay = reader.ReadInt32();
        }
    }
}
