// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Drawing;
using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.DLM;

/// <summary>
/// Represents a fixture in the DLM serialization format.
/// </summary>
public sealed class DlmFixture
{
    /// <summary>
    /// Gets the map associated with this fixture.
    /// </summary>
    public DlmMap Map { get; }

    /// <summary>
    /// Gets or sets the unique identifier of the fixture.
    /// </summary>
    public int FixtureId { get; set; }

    /// <summary>
    /// Gets or sets the offset of the fixture in cell coordinates.
    /// </summary>
    public Point Offset { get; set; }

    /// <summary>
    /// Gets or sets the hue of the fixture.
    /// </summary>
    public int Hue { get; set; }

    /// <summary>
    /// Gets or sets the red color multiplier of the fixture.
    /// </summary>
    public byte RedMultiplier { get; set; }

    /// <summary>
    /// Gets or sets the green color multiplier of the fixture.
    /// </summary>
    public byte GreenMultiplier { get; set; }

    /// <summary>
    /// Gets or sets the blue color multiplier of the fixture.
    /// </summary>
    public byte BlueMultiplier { get; set; }

    /// <summary>
    /// Gets or sets the alpha (transparency) value of the fixture.
    /// </summary>
    public byte Alpha { get; set; }

    /// <summary>
    /// Gets or sets the scale of the fixture in cell coordinates.
    /// </summary>
    public Point Scale { get; set; }

    /// <summary>
    /// Gets or sets the rotation of the fixture in degrees.
    /// </summary>
    public short Rotation { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DlmFixture"/> class.
    /// </summary>
    /// <param name="map">The map associated with this fixture.</param>
    public DlmFixture(DlmMap map)
    {
        Map = map;
    }

    /// <summary>
    /// Serializes the fixture data to a binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to serialize the data to.</param>
    public void Serialize(BigEndianWriter writer)
    {
        writer.WriteInt32(FixtureId);
        writer.WriteInt16((short)Offset.X);
        writer.WriteInt16((short)Offset.Y);
        writer.WriteInt16(Rotation);
        writer.WriteInt16((short)Scale.X);
        writer.WriteInt16((short)Scale.Y);
        writer.WriteUInt8(RedMultiplier);
        writer.WriteUInt8(GreenMultiplier);
        writer.WriteUInt8(BlueMultiplier);
        writer.WriteUInt8(Alpha);
    }

    /// <summary>
    /// Deserializes the fixture data from a binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the data from.</param>
    public void Deserialize(BigEndianReader reader)
    {
        FixtureId = reader.ReadInt32();
        Offset = new Point(reader.ReadInt16(), reader.ReadInt16());
        Rotation = reader.ReadInt16();
        Scale = new Point(reader.ReadInt16(), reader.ReadInt16());
        RedMultiplier = reader.ReadUInt8();
        GreenMultiplier = reader.ReadUInt8();
        BlueMultiplier = reader.ReadUInt8();
        Hue = RedMultiplier | GreenMultiplier | BlueMultiplier;
        Alpha = reader.ReadUInt8();
    }
}
