﻿// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;
using Krosmoz.Core.IO.Binary;
using Krosmoz.Serialization.D2O.Abstractions;

namespace Krosmoz.Serialization.D2O;

/// <summary>
/// Represents a class in a D2O file, containing metadata and methods for deserialization.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public sealed class D2OClass
{
    /// <summary>
    /// The identifier used to represent a null value.
    /// </summary>
    private const int NullIdentifier = -1431655766;

    /// <summary>
    /// Gets the associated D2O file.
    /// </summary>
    private D2OFile D2OFile { get; }

    /// <summary>
    /// Gets the factory used to create instances of datacenter objects.
    /// </summary>
    private IDatacenterObjectFactory D2OFactory { get; }

    /// <summary>
    /// Gets the name of the module.
    /// </summary>
    private string ModuleName { get; }

    /// <summary>
    /// Gets or sets the namespace of the class.
    /// </summary>
    public string Namespace { get; set; }

    /// <summary>
    /// Gets the name of the class.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the list of fields defined in the class.
    /// </summary>
    public List<D2OField> Fields { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="D2OClass"/> class.
    /// </summary>
    /// <param name="d2OFile">The associated D2O file.</param>
    /// <param name="d2OFactory">The factory for creating datacenter objects.</param>
    /// <param name="moduleName">The name of the module.</param>
    /// <param name="namespace">The namespace of the class.</param>
    /// <param name="name">The name of the class.</param>
    public D2OClass(D2OFile d2OFile, IDatacenterObjectFactory d2OFactory, string moduleName, string @namespace, string name)
    {
        D2OFile = d2OFile;
        D2OFactory = d2OFactory;
        ModuleName = moduleName;
        Namespace = @namespace;
        Name = name;
        Fields = [];
    }

    /// <summary>
    /// Deserializes an object of type <typeparamref name="T"/> from the provided reader.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="reader">The binary reader to read data from.</param>
    /// <returns>The deserialized object.</returns>
    internal T Deserialize<T>(BigEndianReader reader)
        where T : class, IDatacenterObject
    {
        var instance = D2OFactory.CreateInstance(Name);
        instance.Deserialize(this, reader);
        return (T)instance;
    }

    /// <summary>
    /// Reads a field as an integer from the reader.
    /// </summary>
    /// <param name="reader">The binary reader to read data from.</param>
    /// <returns>The integer value read.</returns>
    public int ReadFieldAsInt(BigEndianReader reader)
    {
        return reader.ReadInt32();
    }

    /// <summary>
    /// Reads a field as an unsigned integer from the reader.
    /// </summary>
    /// <param name="reader">The binary reader to read data from.</param>
    /// <returns>The unsigned integer value read.</returns>
    public uint ReadFieldAsUInt(BigEndianReader reader)
    {
        return reader.ReadUInt32();
    }

    /// <summary>
    /// Reads a field as a boolean from the reader.
    /// </summary>
    /// <param name="reader">The binary reader to read data from.</param>
    /// <returns>The boolean value read.</returns>
    public bool ReadFieldAsBoolean(BigEndianReader reader)
    {
        return reader.ReadBoolean();
    }

    /// <summary>
    /// Reads a field as an internationalized text identifier from the reader.
    /// </summary>
    /// <param name="reader">The binary reader to read data from.</param>
    /// <returns>The internationalized text identifier read.</returns>
    public int ReadFieldAsI18N(BigEndianReader reader)
    {
        return reader.ReadInt32();
    }

    /// <summary>
    /// Reads an internationalized text string associated with the given identifier.
    /// </summary>
    /// <param name="id">The identifier of the internationalized text.</param>
    /// <returns>The text string associated with the identifier.</returns>
    public string ReadFieldAsI18NString(int id)
    {
        return D2OFile.GetD2IFile().GetText(id);
    }

    /// <summary>
    /// Reads a field as a string from the reader.
    /// </summary>
    /// <param name="reader">The binary reader to read data from.</param>
    /// <returns>The string value read.</returns>
    public string ReadFieldAsString(BigEndianReader reader)
    {
        return reader.ReadUtfPrefixedLength16();
    }

    /// <summary>
    /// Reads a field as a double from the reader.
    /// </summary>
    /// <param name="reader">The binary reader to read data from.</param>
    /// <returns>The double value read.</returns>
    public double ReadFieldAsNumber(BigEndianReader reader)
    {
        return reader.ReadDouble();
    }

    /// <summary>
    /// Reads a field as an object of type <typeparamref name="T"/> from the reader.
    /// </summary>
    /// <typeparam name="T">The type of the object to read.</typeparam>
    /// <param name="reader">The binary reader to read data from.</param>
    /// <returns>The object read, or <c>null</c> if the identifier is null.</returns>
    public T ReadFieldAsObject<T>(BigEndianReader reader)
        where T : class, IDatacenterObject
    {
        var id = reader.ReadInt32();

        return id is NullIdentifier
            ? null!
            : D2OFile.GetClass(ModuleName, id).Deserialize<T>(reader);
    }

    /// <summary>
    /// Reads a field as a list of items from the reader.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    /// <param name="reader">The binary reader to read data from.</param>
    /// <param name="converter">The function to convert each item.</param>
    /// <returns>The list of items read.</returns>
    public List<T> ReadFieldAsList<T>(BigEndianReader reader, Func<D2OClass, BigEndianReader, T?> converter)
    {
        var length = reader.ReadInt32();

        var content = new List<T>(length);

        for (var i = 0; i < length; i++)
        {
            var value = converter(this, reader);

            if (value is null)
                continue;

            content.Add(value);
        }

        return content;
    }

    /// <summary>
    /// Reads a field as a list of lists of items from the reader.
    /// </summary>
    /// <typeparam name="T">The type of the items in the lists.</typeparam>
    /// <param name="reader">The binary reader to read data from.</param>
    /// <param name="converter">The function to convert each item.</param>
    /// <returns>The list of lists of items read.</returns>
    public List<List<T>> ReadFieldAsListOfList<T>(BigEndianReader reader, Func<D2OClass, BigEndianReader, T> converter)
    {
        var length = reader.ReadInt32();

        var content = new List<List<T>>(length);

        for (var i = 0; i < length; i++)
            content.Add(ReadFieldAsList(reader, converter));

        return content;
    }

    /// <summary>
    /// Returns a string representation of the D2O class.
    /// </summary>
    /// <returns>A string containing the namespace, name, and field count.</returns>
    public override string ToString()
    {
        return $"Namespace: {Namespace}, Name: {Name}, Fields: {Fields.Count}";
    }
}
