// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Text;
using Krosmoz.Core.IO.Binary;

namespace Krosmoz.Serialization.D2O;

/// <summary>
/// Represents a D2O file, which contains serialized data for datacenter objects.
/// </summary>
public sealed class D2OFile
{
    private readonly Dictionary<string, Dictionary<int, D2OClass>> _classes;
    private readonly Dictionary<string, int> _counters;
    private readonly Dictionary<string, Dictionary<int, int>> _indexes;
    private readonly Dictionary<string, BigEndianReader> _readers;
    private readonly Dictionary<string, int> _readersStartIndexes;

    /// <summary>
    /// Initializes a new instance of the <see cref="D2OFile"/> class.
    /// </summary>
    public D2OFile()
    {
        _classes = [];
        _counters = [];
        _indexes = [];
        _readers = [];
        _readersStartIndexes = [];
    }

    /// <summary>
    /// Registers a D2O file definition by reading its structure and metadata.
    /// </summary>
    /// <param name="filePath">The path to the D2O file to register.</param>
    public void RegisterDefinition(string filePath)
    {
        var moduleName = Path.GetFileNameWithoutExtension(filePath);

        if (!_readers.TryGetValue(moduleName, out var reader))
        {
            _readers[moduleName] = reader = new BigEndianBufferReader(File.ReadAllBytes(filePath));
            _readersStartIndexes[moduleName] = 7;
        }
        else
            reader.Seek(SeekOrigin.Begin, 0);

        var indexes = new Dictionary<int, int>();

        _indexes[moduleName] = indexes;

        var header = Encoding.ASCII.GetString(reader.ReadSpan(3));
        var contentOffset = 0;

        if (header is not "D2O")
        {
            reader.Seek(SeekOrigin.Begin, 0);

            header = reader.ReadUtfLengthPrefixed16();

            if (header is not "AKSF")
                throw new Exception("Unexpected file format.");

            reader.Seek(SeekOrigin.Current, sizeof(short));
            reader.Seek(SeekOrigin.Current, reader.ReadInt());

            contentOffset = reader.Position;

            _readersStartIndexes[moduleName] = contentOffset + 7;

            header = Encoding.ASCII.GetString(reader.ReadSpan(3));

            if (header is not "D2O")
                throw new Exception("Unexpected file format.");
        }

        var indexesPointer = reader.ReadInt();
        reader.Seek(SeekOrigin.Begin, contentOffset + indexesPointer);
        var indexesCount = reader.ReadInt();

        var count = 0;

        for (var i = 0; i < indexesCount; i += 8)
        {
            var key = reader.ReadInt();
            var pointer = reader.ReadInt();

            indexes[key] = contentOffset + pointer;
            count++;
        }

        _counters[moduleName] = count;

        var classes = new Dictionary<int, D2OClass>();

        _classes[moduleName] = classes;

        var classesCount = reader.ReadInt();

        for (var i = 0; i < classesCount; i++)
        {
            var classId = reader.ReadInt();
            var classDefinition = ReadClassDefinition(reader);

            _classes[moduleName][classId] = classDefinition;
        }

        if (reader.Remaining > 0)
        {
            // TODO: Handle remaining data
        }
    }

    /// <summary>
    /// Gets all the classes defined in the D2O file.
    /// </summary>
    /// <returns>A dictionary containing the classes, organized by module name and class ID.</returns>
    public Dictionary<string, Dictionary<int, D2OClass>> GetClasses()
    {
        return _classes;
    }

    /// <summary>
    /// Gets a specific class definition from the D2O file.
    /// </summary>
    /// <param name="moduleName">The name of the module containing the class.</param>
    /// <param name="index">The ID of the class to retrieve.</param>
    /// <returns>The <see cref="D2OClass"/> corresponding to the specified module and index.</returns>
    public D2OClass GetClass(string moduleName, int index)
    {
        return _classes[moduleName][index];
    }

    /// <summary>
    /// Reads a class definition from the binary reader.
    /// </summary>
    /// <param name="reader">The binary reader used to read the class definition.</param>
    /// <returns>A <see cref="D2OClass"/> representing the class definition.</returns>
    private D2OClass ReadClassDefinition(BigEndianReader reader)
    {
        var name = reader.ReadUtfLengthPrefixed16();
        var @namespace = reader.ReadUtfLengthPrefixed16();

        var classDefinition = new D2OClass(this, @namespace, name);

        var fieldsCount = reader.ReadInt();

        for (var i = 0; i < fieldsCount; i++)
            classDefinition.AddField(reader, reader.ReadUtfLengthPrefixed16());

        return classDefinition;
    }
}
