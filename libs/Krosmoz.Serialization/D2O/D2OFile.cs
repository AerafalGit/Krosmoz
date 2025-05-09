﻿// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Text;
using Krosmoz.Core.IO.Binary;
using Krosmoz.Serialization.D2O.Abstractions;

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
    private readonly IDatacenterObjectFactory _datacenterObjectFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="D2OFile"/> class.
    /// </summary>
    public D2OFile(IDatacenterObjectFactory datacenterObjectFactory)
    {
        _datacenterObjectFactory = datacenterObjectFactory;
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
            var classDefinition = ReadClassDefinition(reader, moduleName);

            _classes[moduleName][classId] = classDefinition;
        }
    }

    /// <summary>
    /// Retrieves all objects of type <typeparamref name="T"/> from the D2O file.
    /// </summary>
    /// <typeparam name="T">The type of objects to retrieve, which must implement <see cref="IDatacenterObject"/>.</typeparam>
    /// <param name="clearReader">
    /// A boolean value indicating whether to clear the reader after retrieving the objects.
    /// If <c>true</c>, the reader will be reset for the specified module.
    /// </param>
    /// <returns>
    /// An enumerable collection of objects of type <typeparamref name="T"/>.
    /// If the module is not found, an empty collection is returned.
    /// </returns>
    public IList<T> GetObjects<T>(bool clearReader = false)
        where T : class, IDatacenterObject
    {
        var moduleName = T.ModuleName;

        if (!_counters.TryGetValue(moduleName, out var length) ||
            !_readers.TryGetValue(moduleName, out var reader) ||
            !_classes.TryGetValue(moduleName, out var classes) ||
            !_readersStartIndexes.TryGetValue(moduleName, out var startIndex))
            return [];

        reader.Seek(SeekOrigin.Begin, startIndex);

        var objects = new T[length];

        for (var i = 0; i < length; i++)
            objects[i] = classes[reader.ReadInt()].Deserialize<T>(reader);

        if (clearReader)
            ResetReader(moduleName);

        return objects;
    }

    /// <summary>
    /// Resets the reader and removes all associated data for the specified module.
    /// </summary>
    /// <param name="moduleName">The name of the module to reset.</param>
    public void ResetReader(string moduleName)
    {
        _classes.Remove(moduleName);
        _counters.Remove(moduleName);
        _readers.Remove(moduleName);
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
    /// <param name="moduleName">The name of the module containing the class.</param>
    /// <returns>A <see cref="D2OClass"/> representing the class definition.</returns>
    private D2OClass ReadClassDefinition(BigEndianReader reader, string moduleName)
    {
        var name = reader.ReadUtfLengthPrefixed16();
        var @namespace = reader.ReadUtfLengthPrefixed16();

        var classDefinition = new D2OClass(this, _datacenterObjectFactory, moduleName, @namespace, name);

        var fieldsCount = reader.ReadInt();

        for (var i = 0; i < fieldsCount; i++)
            classDefinition.AddField(reader, reader.ReadUtfLengthPrefixed16());

        return classDefinition;
    }
}
