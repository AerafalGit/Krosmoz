// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;
using Krosmoz.Core.IO.Binary;
using Krosmoz.Serialization.D2O.Abstractions;

namespace Krosmoz.Serialization.D2O;

/// <summary>
/// Represents a D2O class containing metadata and fields for deserialization.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public sealed class D2OClass
{
    /// <summary>
    /// Gets the D2O file associated with this class.
    /// </summary>
    private D2OFile D2OFile { get; }

    /// <summary>
    /// Gets the factory used to create datacenter objects.
    /// </summary>
    private IDatacenterObjectFactory D2OFactory { get; }

    /// <summary>
    /// Gets the name of the module associated with this field.
    /// </summary>
    private string ModuleName { get; }

    /// <summary>
    /// Gets or sets the namespace of the D2O class.
    /// </summary>
    public string Namespace { get; set; }

    /// <summary>
    /// Gets the name of the D2O class.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the list of fields associated with the D2O class.
    /// </summary>
    public List<D2OField> Fields { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="D2OClass"/> class.
    /// </summary>
    /// <param name="d2OFile">The D2O file associated with this class.</param>
    /// <param name="d2OFactory">The factory used to create datacenter objects.</param>
    /// <param name="moduleName">The name of the module.</param>
    /// <param name="namespace">The namespace of the D2O class.</param>
    /// <param name="name">The name of the D2O class.</param>
    public D2OClass(D2OFile d2OFile, IDatacenterObjectFactory d2OFactory, string moduleName, string @namespace, string name)
    {
        D2OFile = d2OFile;
        D2OFactory = d2OFactory;
        ModuleName = moduleName;
        Name = name;
        Namespace = @namespace;
        Fields = [];
    }

    /// <summary>
    /// Deserializes an object of type <typeparamref name="T"/> from the specified binary reader.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize, which must implement <see cref="IDatacenterObject"/>.</typeparam>
    /// <param name="reader">The binary reader used to read the object's data.</param>
    /// <returns>An instance of type <typeparamref name="T"/>.</returns>
    public T Deserialize<T>(BigEndianReader reader)
        where T : class, IDatacenterObject
    {
        var instance = D2OFactory.Create(Name);
        instance.Deserialize(this, reader);
        return (T)instance;
    }

    /// <summary>
    /// Adds a field to the D2O class by deserializing it from the specified binary reader.
    /// </summary>
    /// <param name="reader">The binary reader used to read the field's data.</param>
    /// <param name="fieldName">The name of the field to add.</param>
    public void AddField(BigEndianReader reader, string fieldName)
    {
        var field = new D2OField(D2OFile, ModuleName, fieldName);
        field.Deserialize(reader);
        Fields.Add(field);
    }

    /// <summary>
    /// Returns a string representation of the D2O class.
    /// </summary>
    /// <returns>A string containing the namespace, name, and field count of the class.</returns>
    public override string ToString()
    {
        return $"Namespace: {Namespace}, Name: {Name}, Fields: {Fields.Count}";
    }
}
