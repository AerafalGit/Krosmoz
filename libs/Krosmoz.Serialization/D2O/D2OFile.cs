// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Krosmoz.Core.Extensions;
using Krosmoz.Core.IO.Binary;
using Krosmoz.Serialization.D2I;
using Krosmoz.Serialization.D2O.Abstractions;

namespace Krosmoz.Serialization.D2O;

/// <summary>
/// Represents a D2O file.
/// </summary>
public sealed class D2OFile
{
    private readonly Dictionary<string, Dictionary<int, D2OClass>> _classes;
    private readonly Dictionary<string, int> _counters;
    private readonly Dictionary<string, BigEndianReader> _readers;
    private readonly Dictionary<string, int> _readersStartIndexes;
    private readonly Dictionary<string, D2OProcessor> _processors;
    private readonly Dictionary<string, Dictionary<object, List<int>>> _searchTable;
    private readonly IDatacenterObjectFactory _datacenterObjectFactory;
    private readonly D2IFile _d2IFile;

    /// <summary>
    /// Initializes a new instance of the <see cref="D2OFile"/> class.
    /// </summary>
    /// <param name="datacenterObjectFactory">The factory for creating datacenter objects.</param>
    /// <param name="d2IFile">The associated D2I file for internationalized text.</param>
    public D2OFile(IDatacenterObjectFactory datacenterObjectFactory, D2IFile d2IFile)
    {
        _datacenterObjectFactory = datacenterObjectFactory;
        _d2IFile = d2IFile;
        _classes = [];
        _counters = [];
        _readers = [];
        _readersStartIndexes = [];
        _processors = [];
        _searchTable = [];
    }

    /// <summary>
    /// Loads a D2O module by its name, initializing its reader, indexes, and classes.
    /// </summary>
    /// <param name="filePath">The path to the D2O file to load.</param>
    public void Load(string filePath)
    {
        var moduleName = Path.GetFileNameWithoutExtension(filePath);

        if (!_readers.TryGetValue(moduleName, out var reader))
        {
            _readers[moduleName] = reader = new BigEndianReader(File.ReadAllBytes(filePath));
            _readersStartIndexes[moduleName] = 7;
        }
        else
            reader.Seek(SeekOrigin.Begin, 0);

        var header = reader.ReadUtfSpan(3);
        var contentOffset = 0;

        if (header is not "D2O")
        {
            reader.Seek(SeekOrigin.Begin, 0);

            header = reader.ReadUtfPrefixedLength16();

            if (header is not "AKSF")
                throw new Exception("Unexpected file format.");

            reader.Seek(SeekOrigin.Current, sizeof(short));
            reader.Seek(SeekOrigin.Current, reader.ReadInt32());

            contentOffset = reader.Position;

            _readersStartIndexes[moduleName] = contentOffset + 7;

            header = reader.ReadUtfSpan(3);

            if (header is not "D2O")
                throw new Exception("Unexpected file format.");
        }

        var indexesPointer = reader.ReadInt32();
        reader.Seek(SeekOrigin.Begin, contentOffset + indexesPointer);
        var indexesCount = reader.ReadInt32();

        var count = 0;

        for (var i = 0; i < indexesCount; i += sizeof(long))
        {
            reader.Seek(SeekOrigin.Current, sizeof(long));
            count++;
        }

        _counters[moduleName] = count;

        var classes = new Dictionary<int, D2OClass>();

        _classes[moduleName] = classes;

        var classesCount = reader.ReadInt32();

        for (var i = 0; i < classesCount; i++)
        {
            var classId = reader.ReadInt32();
            var classDefinition = ReadClassDefinition(reader, moduleName);

            _classes[moduleName][classId] = classDefinition;
        }

        if (reader.Remaining > 0)
            _processors[moduleName] = new D2OProcessor(reader);
    }

    /// <summary>
    /// Retrieves a list of objects of the specified type from the D2O file.
    /// </summary>
    /// <typeparam name="T">The type of objects to retrieve.</typeparam>
    /// <param name="clearReader">Whether to clear the reader after retrieving objects.</param>
    /// <returns>A list of objects of the specified type.</returns>
    public IList<T> GetObjects<T>(bool clearReader = false)
        where T : class, IDatacenterObject
    {
        var moduleName = T.ModuleName;

        if (!TryExtractInformations(moduleName, out var length, out var reader, out var classes, out var startIndex))
        {
            var filePath = moduleName is "ActionDescriptions"
                ? Path.Combine(PathConstants.Directories.CommonPath, string.Concat(moduleName, ".d2os"))
                : Path.Combine(PathConstants.Directories.CommonPath, string.Concat(moduleName, ".d2o"));

            Load(filePath);

            if (!TryExtractInformations(moduleName, out length, out reader, out classes, out startIndex))
                throw new Exception($"Module {moduleName} not found.");
        }

        reader.Seek(SeekOrigin.Begin, startIndex);

        var objects = new T[length];

        for (var i = 0; i < length; i++)
            objects[i] = classes[reader.ReadInt32()].Deserialize<T>(reader);

        if (clearReader)
            ResetReader(moduleName);

        return objects;
    }

    /// <summary>
    /// Writes a list of objects to a D2O file, creating indexes and serializing the data.
    /// </summary>
    /// <typeparam name="T">The type of objects to write, which must implement <see cref="IDatacenterObject"/>.</typeparam>
    /// <param name="objects">The list of objects to write to the D2O file.</param>
    /// <remarks>
    /// This method creates a search table for the objects, writes the D2O header,
    /// serializes the objects and their associated class definitions, and writes the search table.
    /// </remarks>
    public void Write<T>(IList<T> objects)
        where T : class, IDatacenterObject
    {
        var moduleName = T.ModuleName;

        CreateSearchTable(moduleName, objects, string.Empty);

        var writer = new BigEndianWriter();

        writer.WriteUtfSpan("D2O");

        var indexPosition = writer.Position;

        writer.WriteInt32(0);

        var indexes = new Dictionary<int, int>();
        var indexObjects = new Dictionary<int, object>();

        var classes = _classes[moduleName];

        foreach (var obj in objects)
        {
            if (obj is null)
                continue;

            var objectType = obj.GetType();

            var (classId, classDefinition) = classes.First(x => x.Value.Name.Equals(objectType.Name));

            BuildClassIndexKey(moduleName, objectType, obj, indexes, indexObjects, writer);

            writer.WriteInt32(classId);

            classDefinition.Serialize(writer, obj);
        }

        var position = writer.Position;
        writer.Seek(SeekOrigin.Begin, indexPosition);
        writer.WriteInt32(position);
        writer.Seek(SeekOrigin.Begin, position);

        writer.WriteInt32(objects.Count * sizeof(long));

        foreach (var (key, value) in indexes)
        {
            writer.WriteInt32(key);
            writer.WriteInt32(value);
        }

        writer.WriteInt32(classes.Count);

        foreach (var (key, value) in classes)
        {
            writer.WriteInt32(key);
            value.Serialize(writer);
        }

        WriteSearchTable(moduleName, writer);

        var filePath = moduleName is "ActionDescriptions"
            ? Path.Combine(PathConstants.Directories.CommonPath, string.Concat(moduleName, ".d2os"))
            : Path.Combine(PathConstants.Directories.CommonPath, string.Concat(moduleName, ".d2o"));

        File.Copy(filePath, string.Concat(filePath, ".bak"), true);
        File.WriteAllBytes(filePath, writer.ToSpan());
    }

    /// <summary>
    /// Retrieves the dictionary of classes defined in the D2O file.
    /// </summary>
    /// <returns>A dictionary of classes organized by module name and class ID.</returns>
    public Dictionary<string, Dictionary<int, D2OClass>> GetClasses()
    {
        return _classes;
    }

    /// <summary>
    /// Retrieves the associated D2I file for internationalized text.
    /// </summary>
    /// <returns>The associated D2I file.</returns>
    internal D2IFile GetD2IFile()
    {
        return _d2IFile;
    }

    /// <summary>
    /// Retrieves a class definition by its module name and ID.
    /// </summary>
    /// <param name="moduleName">The name of the module containing the class.</param>
    /// <param name="id">The ID of the class to retrieve.</param>
    /// <returns>The class definition.</returns>
    internal D2OClass GetClass(string moduleName, int id)
    {
        return _classes[moduleName][id];
    }

    /// <summary>
    /// Retrieves the class ID for a given module and class name.
    /// </summary>
    /// <param name="moduleName">The name of the module containing the class.</param>
    /// <param name="className">The name of the class to retrieve the ID for.</param>
    /// <returns>The ID of the class.</returns>
    internal int GetClassId(string moduleName, string className)
    {
        return _classes[moduleName].First(x => x.Value.Name.Equals(className)).Key;
    }

    /// <summary>
    /// Reads a class definition from the binary reader for the specified module.
    /// </summary>
    /// <param name="reader">The binary reader to read data from.</param>
    /// <param name="moduleName">The name of the module containing the class.</param>
    /// <returns>The class definition read from the reader.</returns>
    private D2OClass ReadClassDefinition(BigEndianReader reader, string moduleName)
    {
        var name = reader.ReadUtfPrefixedLength16();

        if (moduleName is "MapPositions" && name is "AmbientSound")
            name = "MapAmbientSound";

        var @namespace = reader.ReadUtfPrefixedLength16();

        var classDefinition = new D2OClass(this, _datacenterObjectFactory, moduleName,@namespace, name);

        var fieldsCount = reader.ReadInt32();

        for (var i = 0; i < fieldsCount; i++)
        {
            var fieldName = reader.ReadUtfPrefixedLength16();
            var fieldType = reader.ReadInt32();

            var innerTypeNames = new List<string>();
            var innerTypeIds = new List<int>();

            var currentFieldType = fieldType;

            while (currentFieldType is (int)D2OFieldTypes.Vector)
            {
                innerTypeNames.Add(reader.ReadUtfPrefixedLength16());
                innerTypeIds.Add(currentFieldType = reader.ReadInt32());
            }

            classDefinition.Fields.Add(new D2OField(fieldName, fieldType, innerTypeNames, innerTypeIds));
        }

        return classDefinition;
    }

    /// <summary>
    /// Attempts to extract information about a module, including its length, reader, classes, and start index.
    /// </summary>
    /// <param name="moduleName">The name of the module to extract information for.</param>
    /// <param name="length">The length of the module.</param>
    /// <param name="reader">The binary reader for the module.</param>
    /// <param name="classes">The dictionary of classes for the module.</param>
    /// <param name="startIndex">The start index for the module's reader.</param>
    /// <returns><c>true</c> if the information was successfully extracted; otherwise, <c>false</c>.</returns>
    private bool TryExtractInformations(
        string moduleName,
        out int length,
        [NotNullWhen(true)] out BigEndianReader? reader,
        [NotNullWhen(true)] out Dictionary<int, D2OClass>? classes,
        out int startIndex)
    {
        length = 0;
        reader = null;
        classes = null;
        startIndex = 0;

        return _counters.TryGetValue(moduleName, out length) &&
               _readers.TryGetValue(moduleName, out reader) &&
               _classes.TryGetValue(moduleName, out classes) &&
               _readersStartIndexes.TryGetValue(moduleName, out startIndex);
    }

    /// <summary>
    /// Resets the reader and clears cached data for the specified module.
    /// </summary>
    /// <param name="moduleName">The name of the module to reset.</param>
    private void ResetReader(string moduleName)
    {
        _classes.Remove(moduleName);
        _counters.Remove(moduleName);
        _readers.Remove(moduleName);
    }

    /// <summary>
    /// Creates a search table for the specified module, processing objects and their fields recursively.
    /// </summary>
    /// <param name="moduleName">The name of the module to create the search table for.</param>
    /// <param name="objects">The collection of objects to process.</param>
    /// <param name="parentFieldName">The name of the parent field, used for nested fields (optional).</param>
    /// <param name="index">The index of the current object (optional, defaults to -1).</param>
    private void CreateSearchTable(string moduleName, IEnumerable objects, string parentFieldName = "", int index = -1)
    {
        var classes = _classes[moduleName];
        var processor = _processors[moduleName];

        foreach (var obj in objects)
        {
            var objectType = obj.GetType();

            var currentIndex = index > -1
                ? index
                : GetClassObjectKey(moduleName, objectType, obj);

            var classDefinition = classes.First(x => x.Value.Name.Equals(objectType.Name)).Value;

            foreach (var field in classDefinition.Fields.Where(x => processor.QueryableFields!.Contains(GetFullFieldName(parentFieldName, x.Name)) || x.IsQueryable))
            {
                var fieldValue = objectType.GetProperty(field.Name.Capitalize())!.GetValue(obj)!;
                var fullFieldName = GetFullFieldName(parentFieldName, field.Name);

                if (field.IsQueryable)
                    CreateSearchTable(moduleName, field.Type > 0 ? new[] { fieldValue } : (IEnumerable)fieldValue, fullFieldName, currentIndex);
                else switch (field.Type)
                {
                    case (int)D2OFieldTypes.Vector when
                        field.InnerTypeIds[0] is not (int)D2OFieldTypes.Vector &&
                        field.InnerTypeIds[0] == processor.SearchFieldTypes![fullFieldName]:
                    {
                        foreach (var o in (IEnumerable)fieldValue)
                            AddToSearchTable(o, fullFieldName, currentIndex);
                        break;
                    }
                    case < 0 and > (int)D2OFieldTypes.Vector:
                        AddToSearchTable(fieldValue, fullFieldName, currentIndex);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Writes the search table for the specified module to the provided binary writer.
    /// </summary>
    /// <param name="moduleName">The name of the module whose search table is being written.</param>
    /// <param name="writer">The binary writer to write the search table to.</param>
    private void WriteSearchTable(string moduleName, BigEndianWriter writer)
    {
        var processor = _processors[moduleName];

        int currentPosition;
        var size = 0;

        var startPosition = writer.Position;

        writer.WriteInt32(0);

        var fieldPositions = new List<int>();

        foreach (var field in processor.QueryableFields!)
        {
            if (!_searchTable.TryGetValue(field, out var searchTable))
                continue;

            var fieldStartPosition = writer.Position;
            writer.WriteUtfPrefixedLength16(field);
            fieldPositions.Add(writer.Position);
            writer.WriteInt32(0);
            writer.WriteInt32(processor.SearchFieldTypes![field]);
            writer.WriteInt32(searchTable.Count);
            size += writer.Position - fieldStartPosition;
        }

        var dataStartPosition = writer.Position;
        writer.WriteInt32(0);
        var realIndex = 0;

        foreach (var field in processor.QueryableFields)
        {
            if (!_searchTable.TryGetValue(field, out var searchTable))
                continue;

            var fieldStartPosition = writer.Position;

            foreach (var (key, obj) in searchTable.OrderBy(static x => x.Key))
            {
                Action? writeFn = (D2OFieldTypes)processor.SearchFieldTypes![field] switch
                {
                    D2OFieldTypes.Int => () => writer.WriteInt32((int)key),
                    D2OFieldTypes.Boolean => () => writer.WriteBoolean((bool)key),
                    D2OFieldTypes.String => () => writer.WriteUtfPrefixedLength16((string)key),
                    D2OFieldTypes.Number => () => writer.WriteDouble((double)key),
                    D2OFieldTypes.I18N => () => writer.WriteInt32((int)key),
                    D2OFieldTypes.UInt => () => writer.WriteUInt32((uint)key),
                    _ => null
                };

                if (writeFn is null)
                    continue;

                writer.WriteInt32(obj.Count * sizeof(int));

                foreach (var i in obj)
                    writer.WriteInt32(i);
            }

            currentPosition = writer.Position;
            writer.Seek(SeekOrigin.Begin, fieldPositions[realIndex]);
            writer.WriteInt32(fieldStartPosition - dataStartPosition - sizeof(int));
            writer.Seek(SeekOrigin.Begin, currentPosition);
            realIndex++;
        }

        currentPosition = writer.Position;
        writer.Seek(SeekOrigin.Begin, dataStartPosition);
        writer.WriteInt32(currentPosition - dataStartPosition);
        writer.Seek(SeekOrigin.Begin, startPosition);
        writer.WriteInt32(size);
        writer.Seek(SeekOrigin.Begin, currentPosition);
    }

    /// <summary>
    /// Adds a value to the search table for the specified field and index.
    /// </summary>
    /// <param name="value">The value to add to the search table.</param>
    /// <param name="fullFieldName">The full name of the field.</param>
    /// <param name="index">The index of the object associated with the value.</param>
    private void AddToSearchTable(object value, string fullFieldName, int index)
    {
        if (!_searchTable.TryGetValue(fullFieldName, out var searchTable))
            _searchTable[fullFieldName] = searchTable = [];

        if (!searchTable.ContainsKey(value))
            _searchTable[fullFieldName][value] = [];

        searchTable[value].Add(index);
    }

    /// <summary>
    /// Builds a class index key for the specified object and adds it to the provided dictionaries.
    /// </summary>
    /// <param name="moduleName">The name of the module containing the object.</param>
    /// <param name="objectType">The type of the object.</param>
    /// <param name="obj">The object to build the index key for.</param>
    /// <param name="indexes">The dictionary to store the index key and position.</param>
    /// <param name="indexObjects">The dictionary to store the index key and object.</param>
    /// <param name="writer">The binary writer used to determine the position.</param>
    private static void BuildClassIndexKey(
        string moduleName,
        Type objectType,
        object obj,
        Dictionary<int, int> indexes,
        Dictionary<int, object> indexObjects,
        BigEndianWriter writer)
    {
        var key = GetClassObjectKey(moduleName, objectType, obj);
        indexes.Add(key, writer.Position);
        indexObjects.Add(key, obj);
    }

    /// <summary>
    /// Retrieves the class object key for the specified module and object.
    /// </summary>
    /// <param name="moduleName">The name of the module containing the object.</param>
    /// <param name="objectType">The type of the object.</param>
    /// <param name="obj">The object to retrieve the key for.</param>
    /// <returns>The class object key.</returns>
    private static int GetClassObjectKey(string moduleName, Type objectType, object obj)
    {
        int i;

        switch (moduleName)
        {
            case "InfoMessages":
            {
                var messageId = (int)objectType.GetProperty("MessageId")!.GetValue(obj)!;
                var typeId    = (int)objectType.GetProperty("TypeId")!.GetValue(obj)!;
                i = typeId > 0 ? messageId + typeId * 10000 : messageId;
                break;
            }
            case "MapScrollActions":
            case "MapPositions":
            {
                var id = objectType.GetProperty("Id")!.GetValue(obj);
                var indexId = (double)id!;
                i = (int)indexId;
                break;
            }
            default:
            {
                var id = objectType.GetProperty("Id")!.GetValue(obj);
                var indexId = (int)id!;
                i = indexId;
                break;
            }
        }

        return i;
    }

    /// <summary>
    /// Constructs the full field name by combining the parent field name and the current field name.
    /// </summary>
    /// <param name="parentFieldName">The name of the parent field.</param>
    /// <param name="fieldName">The name of the current field.</param>
    /// <returns>The full field name.</returns>
    private static string GetFullFieldName(string parentFieldName, string fieldName)
    {
        return string.IsNullOrEmpty(parentFieldName) ? fieldName : string.Concat(parentFieldName, '.', fieldName);
    }
}
