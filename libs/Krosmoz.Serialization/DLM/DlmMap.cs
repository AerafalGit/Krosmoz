// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Drawing;
using System.Text;
using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.DLM;

/// <summary>
/// Represents a map in the DLM serialization format.
/// </summary>
public sealed class DlmMap
{
    /// <summary>
    /// The encryption key used for the map.
    /// </summary>
    public const string MapEncryptionKey = "649ae451ca33ec53bbcbcc33becf15f4";

    /// <summary>
    /// The number of cells in the map.
    /// </summary>
    private const uint MapCellsCount = 560;

    /// <summary>
    /// Gets or sets the version of the map.
    /// </summary>
    public sbyte Version { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the map.
    /// </summary>
    public uint Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the map is encrypted.
    /// </summary>
    public bool Encrypted { get; set; }

    /// <summary>
    /// Gets or sets the encryption version of the map.
    /// </summary>
    public sbyte EncryptionVersion { get; set; }

    /// <summary>
    /// Gets or sets the relative identifier of the map.
    /// </summary>
    public uint RelativeId { get; set; }

    /// <summary>
    /// Gets or sets the type of the map.
    /// </summary>
    public DlmMapTypes Type { get; set; }

    /// <summary>
    /// Gets or sets the sub-area identifier of the map.
    /// </summary>
    public int SubAreaId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the top neighboring map.
    /// </summary>
    public int TopNeighbourId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the bottom neighboring map.
    /// </summary>
    public int BottomNeighbourId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the left neighboring map.
    /// </summary>
    public int LeftNeighbourId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the right neighboring map.
    /// </summary>
    public int RightNeighbourId { get; set; }

    /// <summary>
    /// Gets or sets the shadow bonus applied to entities on the map.
    /// </summary>
    public int ShadowBonusOnEntities { get; set; }

    /// <summary>
    /// Gets or sets the background color of the map.
    /// </summary>
    public Color Background { get; set; }

    /// <summary>
    /// Gets or sets the zoom scale of the map.
    /// </summary>
    public double ZoomScale { get; set; }

    /// <summary>
    /// Gets or sets the zoom offset of the map.
    /// </summary>
    public Point ZoomOffset { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the map uses a low-pass filter.
    /// </summary>
    public bool UseLowPassFilter { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the map uses reverb.
    /// </summary>
    public bool UseReverb { get; set; }

    /// <summary>
    /// Gets or sets the preset identifier for the map's reverb.
    /// </summary>
    public int PresetId { get; set; }

    /// <summary>
    /// Gets or sets the background fixtures of the map.
    /// </summary>
    public DlmFixture[] BackgroundFixtures { get; set; }

    /// <summary>
    /// Gets or sets the foreground fixtures of the map.
    /// </summary>
    public DlmFixture[] ForegroundFixtures { get; set; }

    /// <summary>
    /// Gets or sets the signature of the map.
    /// </summary>
    public int Signature { get; set; }

    /// <summary>
    /// Gets or sets the ground CRC of the map.
    /// </summary>
    public int GroundCrc { get; set; }

    /// <summary>
    /// Gets or sets the layers of the map.
    /// </summary>
    public DlmLayer[] Layers { get; set; }

    /// <summary>
    /// Gets or sets the cell data of the map.
    /// </summary>
    public DlmCellData[] Cells { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the map uses a new movement system.
    /// </summary>
    public bool UsingNewMovementSystem { get; set; }

    /// <summary>
    /// Gets the list of top arrow cells in the map.
    /// </summary>
    public List<short> TopArrowCells { get; }

    /// <summary>
    /// Gets the list of bottom arrow cells in the map.
    /// </summary>
    public List<short> BottomArrowCells { get; }

    /// <summary>
    /// Gets the list of left arrow cells in the map.
    /// </summary>
    public List<short> LeftArrowCells { get; }

    /// <summary>
    /// Gets the list of right arrow cells in the map.
    /// </summary>
    public List<short> RightArrowCells { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DlmMap"/> class.
    /// </summary>
    public DlmMap()
    {
        BackgroundFixtures = [];
        ForegroundFixtures = [];
        Layers = [];
        Cells = [];
        TopArrowCells = [];
        BottomArrowCells = [];
        LeftArrowCells = [];
        RightArrowCells = [];
        ZoomScale = 1;
    }

    /// <summary>
    /// Serializes the map data to a byte span.
    /// </summary>
    /// <returns>A span of bytes representing the serialized map data.</returns>
    public Span<byte> Serialize()
    {
        var dlmWriter = new BigEndianWriter();

        dlmWriter.WriteUInt32(RelativeId);
        dlmWriter.WriteInt8((sbyte)Type);
        dlmWriter.WriteInt32(SubAreaId);
        dlmWriter.WriteInt32(TopNeighbourId);
        dlmWriter.WriteInt32(BottomNeighbourId);
        dlmWriter.WriteInt32(LeftNeighbourId);
        dlmWriter.WriteInt32(RightNeighbourId);
        dlmWriter.WriteInt32(ShadowBonusOnEntities);

        if (Version >= 3)
        {
            dlmWriter.WriteInt8((sbyte)(Background.ToArgb() >> 16 & byte.MaxValue));
            dlmWriter.WriteInt8((sbyte)(Background.ToArgb() >> 8 & byte.MaxValue));
            dlmWriter.WriteInt8((sbyte)(Background.ToArgb() & byte.MaxValue));
        }

        if (Version >= 4)
        {
            dlmWriter.WriteUInt16((ushort)(ZoomScale * 100));
            dlmWriter.WriteInt16((short)ZoomOffset.X);
            dlmWriter.WriteInt16((short)ZoomOffset.Y);
        }

        dlmWriter.WriteBoolean(UseLowPassFilter);
        dlmWriter.WriteBoolean(UseReverb);

        if (UseReverb)
            dlmWriter.WriteInt32(PresetId);

        dlmWriter.WriteInt8((sbyte)BackgroundFixtures.Length);

        foreach (var fixture in BackgroundFixtures)
            fixture.Serialize(dlmWriter);

        dlmWriter.WriteInt8((sbyte)ForegroundFixtures.Length);

        foreach (var fixture in ForegroundFixtures)
            fixture.Serialize(dlmWriter);

        dlmWriter.WriteInt32(Signature);
        dlmWriter.WriteInt32(GroundCrc);

        dlmWriter.WriteInt8((sbyte)Layers.Length);

        foreach (var layer in Layers)
            layer.Serialize(dlmWriter);

        dlmWriter.WriteInt8((sbyte)Cells.Length);

        foreach (var cell in Cells)
            cell.Serialize(dlmWriter);

        var writer = new BigEndianWriter();

        writer.WriteUInt8(DlmAdapter.DlmFileHeader);
        writer.WriteInt8(Version);
        writer.WriteUInt32(Id);

        if (Version >= 7)
        {
            writer.WriteBoolean(Encrypted);
            writer.WriteInt8(EncryptionVersion);

            var buffer = dlmWriter.ToSpan();

            writer.WriteInt32(buffer.Length);

            if (Encrypted)
            {
                var bytes = Encoding.Default.GetBytes(MapEncryptionKey);

                for (var i = 0; i < buffer.Length; ++i)
                    buffer[i] ^= bytes[i % MapEncryptionKey.Length];
            }

            writer.WriteSpan(buffer);
        }
        else
            writer.WriteSpan(dlmWriter.ToSpan());

        return writer.ToSpan();
    }

    /// <summary>
    /// Deserializes the map data from a binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to deserialize the data from.</param>
    public void Deserialize(BigEndianReader reader)
    {
        Version = reader.ReadInt8();
        Id = reader.ReadUInt32();

        if (Version >= 7)
        {
            Encrypted = reader.ReadBoolean();
            EncryptionVersion = reader.ReadInt8();

            if (Encrypted)
            {
                var buffer = reader.ReadSpan(reader.ReadInt32()).ToArray();
                var bytes = Encoding.Default.GetBytes(MapEncryptionKey);

                for (var i = 0; i < buffer.Length; ++i)
                    buffer[i] ^= bytes[i % MapEncryptionKey.Length];

                reader = new BigEndianReader(buffer);
            }
        }

        RelativeId = reader.ReadUInt32();
        Type = (DlmMapTypes)reader.ReadInt8();
        SubAreaId = reader.ReadInt32();
        TopNeighbourId = reader.ReadInt32();
        BottomNeighbourId = reader.ReadInt32();
        LeftNeighbourId = reader.ReadInt32();
        RightNeighbourId = reader.ReadInt32();
        ShadowBonusOnEntities = reader.ReadInt32();

        if (Version >= 3)
            Background = Color.FromArgb((reader.ReadInt8() & byte.MaxValue) << 16 | (reader.ReadInt8() & byte.MaxValue) << 8 | reader.ReadInt8() & byte.MaxValue);

        if (Version >= 4)
        {
            ZoomScale = reader.ReadUInt16() / 100d;
            ZoomOffset = new Point(reader.ReadInt16(), reader.ReadInt16());

            if (ZoomScale < 1)
            {
                ZoomScale = 1;
                ZoomOffset = new Point(0, 0);
            }
        }

        UseLowPassFilter = reader.ReadBoolean();
        UseReverb = reader.ReadBoolean();
        PresetId = UseReverb ? reader.ReadInt32() : -1;

        BackgroundFixtures = new DlmFixture[reader.ReadInt8()];

        for (var i = 0; i < BackgroundFixtures.Length; ++i)
        {
            var backgroundFixture = new DlmFixture(this);
            backgroundFixture.Deserialize(reader);
            BackgroundFixtures[i] = backgroundFixture;
        }

        ForegroundFixtures = new DlmFixture[reader.ReadInt8()];

        for (var i = 0; i < ForegroundFixtures.Length; ++i)
        {
            var foregroundFixture = new DlmFixture(this);
            foregroundFixture.Deserialize(reader);
            ForegroundFixtures[i] = foregroundFixture;
        }

        Signature = reader.ReadInt32();
        GroundCrc = reader.ReadInt32();
        Layers = new DlmLayer[reader.ReadInt8()];

        for (var i = 0; i < Layers.Length; ++i)
        {
            var layer = new DlmLayer(this);
            layer.Deserialize(reader);
            Layers[i] = layer;
        }

        Cells = new DlmCellData[MapCellsCount];

        byte? oldMvtSystem = null;

        for (short i = 0; i < Cells.Length; ++i)
        {
            var cellData = new DlmCellData(this, i);
            cellData.Deserialize(reader);
            Cells[i] = cellData;

            oldMvtSystem ??= cellData.MoveZone;

            if (cellData.MoveZone != oldMvtSystem.GetValueOrDefault())
                UsingNewMovementSystem = true;
        }
    }
}
