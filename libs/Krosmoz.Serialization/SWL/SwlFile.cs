// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.SWL;

/// <summary>
/// Represents a SWL file and provides methods for loading its data.
/// </summary>
public sealed class SwlFile
{
    /// <summary>
    /// The header value for SWL files.
    /// </summary>
    public const sbyte SwlHeader = 76;

    private readonly BigEndianReader _reader;

    /// <summary>
    /// Gets or sets the version of the SWL file.
    /// </summary>
    public sbyte Version { get; set; }

    /// <summary>
    /// Gets or sets the frame rate of the SWL file.
    /// </summary>
    public uint FrameRate { get; set; }

    /// <summary>
    /// Gets or sets the list of class names defined in the SWL file.
    /// </summary>
    public List<string> Classes { get; set; }

    /// <summary>
    /// Gets or sets the raw SWF data contained in the SWL file.
    /// </summary>
    public byte[] SwfData { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SwlFile"/> class with the specified stream.
    /// </summary>
    /// <param name="stream">The stream containing the SWL file data.</param>
    public SwlFile(Stream stream)
    {
        _reader = new BigEndianReader(stream);

        Classes = [];
        SwfData = [];
    }

    /// <summary>
    /// Loads the SWL file data from the stream and extracts relevant information such as version, frame rate, class names, and SWF data.
    /// </summary>
    /// <exception cref="Exception">Thrown when the SWL header is invalid.</exception>
    public void Load()
    {
        if (_reader.ReadInt8() is not SwlHeader)
            throw new Exception("Invalid SWL header.");

        Version = _reader.ReadInt8();
        FrameRate = _reader.ReadUInt32();

        var classesCount = _reader.ReadInt32();

        for (var i = 0; i < classesCount; i++)
            Classes.Add(_reader.ReadUtfPrefixedLength16());

        SwfData = _reader.ReadSpanToEnd().ToArray();
    }
}
