// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Flazzy;
using Flazzy.Tags;
using Krosmoz.Core.IO.Binary;
using Krosmoz.Serialization.SWL;

namespace Krosmoz.Serialization.SWF;

/// <summary>
/// Represents a SWFfile and provides methods for loading and converting it to SWL format.
/// </summary>
public sealed class SwfFile
{
    private readonly Stream _stream;

    /// <summary>
    /// Gets or sets the version of the SWF file.
    /// </summary>
    public byte Version { get; set; }

    /// <summary>
    /// Gets or sets the frame rate of the SWF file.
    /// </summary>
    public uint FrameRate { get; set; }

    /// <summary>
    /// Gets or sets the list of class names defined in the SWF file.
    /// </summary>
    public List<string> Classes { get; set; }

    /// <summary>
    /// Gets or sets the raw SWF file data.
    /// </summary>
    public byte[] SwfData { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SwfFile"/> class with the specified stream.
    /// </summary>
    /// <param name="stream">The stream containing the SWF file data.</param>
    public SwfFile(Stream stream)
    {
        _stream = stream;

        Classes = [];
        SwfData = [];
    }

    /// <summary>
    /// Loads the SWF file data from the stream and extracts relevant information such as version, frame rate, and class names.
    /// </summary>
    public void Load()
    {
        if (_stream is not MemoryStream ms)
        {
            ms = new MemoryStream();
            _stream.CopyTo(ms);
        }

        SwfData = ms.ToArray();

        ms.Seek(0, SeekOrigin.Begin);

        var swf = new ShockwaveFlash(ms);
        swf.Disassemble();

        if (swf.Tags.FirstOrDefault(static x => x is SymbolClassTag) is not SymbolClassTag tag)
            return;

        FrameRate = swf.Frame.Rate;
        Version = swf.Version;
        Classes.AddRange(tag.Names);
    }

    /// <summary>
    /// Converts the SWF file data to SWL format and returns it as a span of bytes.
    /// </summary>
    /// <returns>A span of bytes representing the SWL file data.</returns>
    public Span<byte> ToSwl()
    {
        var writer = new BigEndianWriter();

        writer.WriteInt8(SwlFile.SwlHeader);
        writer.WriteInt8((sbyte)Version);
        writer.WriteUInt32(FrameRate);
        writer.WriteInt32(Classes.Count);

        foreach (var className in Classes)
            writer.WriteUtfPrefixedLength16(className);

        writer.WriteSpan(SwfData);

        return writer.ToSpan();
    }
}
