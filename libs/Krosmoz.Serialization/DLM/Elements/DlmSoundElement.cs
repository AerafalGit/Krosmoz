// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.DLM.Elements;

/// <summary>
/// Represents a sound element in the DLM serialization format.
/// </summary>
public sealed class DlmSoundElement : DlmBasicElement
{
    /// <summary>
    /// Gets or sets the unique identifier of the sound.
    /// </summary>
    public int SoundId { get; set; }

    /// <summary>
    /// Gets or sets the minimum delay between sound loops, in milliseconds.
    /// </summary>
    public short MinDelayBetweenLoops { get; set; }

    /// <summary>
    /// Gets or sets the maximum delay between sound loops, in milliseconds.
    /// </summary>
    public short MaxDelayBetweenLoops { get; set; }

    /// <summary>
    /// Gets or sets the base volume of the sound.
    /// </summary>
    public short BaseVolume { get; set; }

    /// <summary>
    /// Gets or sets the distance at which the sound is played at full volume.
    /// </summary>
    public int FullVolumeDistance { get; set; }

    /// <summary>
    /// Gets or sets the distance at which the sound volume becomes zero.
    /// </summary>
    public int NullVolumeDistance { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DlmSoundElement"/> class.
    /// </summary>
    /// <param name="cell">The cell associated with this sound element.</param>
    /// <param name="type">The type of the element.</param>
    public DlmSoundElement(DlmCell cell, DlmElementTypes type) : base(cell, type)
    {
    }

    /// <summary>
    /// Serializes the sound element data to a binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to serialize the data to.</param>
    public override void Serialize(BigEndianWriter writer)
    {
        writer.WriteInt32(SoundId);
        writer.WriteInt16(BaseVolume);
        writer.WriteInt32(FullVolumeDistance);
        writer.WriteInt32(NullVolumeDistance);
        writer.WriteInt16(MinDelayBetweenLoops);
        writer.WriteInt16(MaxDelayBetweenLoops);
    }

    /// <summary>
    /// Deserializes the sound element data from a binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the data from.</param>
    public override void Deserialize(BigEndianReader reader)
    {
        SoundId = reader.ReadInt32();
        BaseVolume = reader.ReadInt16();
        FullVolumeDistance = reader.ReadInt32();
        NullVolumeDistance = reader.ReadInt32();
        MinDelayBetweenLoops = reader.ReadInt16();
        MaxDelayBetweenLoops = reader.ReadInt16();
    }
}
