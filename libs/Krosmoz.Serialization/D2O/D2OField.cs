// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;
using Krosmoz.Core.IO.Binary;
using Krosmoz.Serialization.D2O.Abstractions;

namespace Krosmoz.Serialization.D2O;

/// <summary>
/// Represents a field in a D2O file, containing metadata and methods for deserialization.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public sealed class D2OField
{
    /// <summary>
    /// The identifier used to represent a null value in the D2O file.
    /// </summary>
    private const int NullIdentifier = -1431655766;

    /// <summary>
    /// Gets the D2O file associated with this field.
    /// </summary>
    private D2OFile D2OFile { get; }

    /// <summary>
    /// Gets the name of the module associated with this field.
    /// </summary>
    private string ModuleName { get; }

    /// <summary>
    /// Gets or sets the name of the field.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the type identifier of the field.
    /// </summary>
    public int Type { get; private set; }

    /// <summary>
    /// Gets the list of inner type names, if applicable.
    /// </summary>
    public List<string>? InnerTypeNames { get; private set; }

    /// <summary>
    /// Gets the list of inner type identifiers, if applicable.
    /// </summary>
    public List<int>? InnerTypeIds { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="D2OField"/> class.
    /// </summary>
    /// <param name="d2OFile">The D2O file associated with this field.</param>
    /// <param name="moduleName">The name of the module.</param>
    /// <param name="name">The name of the field.</param>
    public D2OField(D2OFile d2OFile, string moduleName, string name)
    {
        D2OFile = d2OFile;
        ModuleName = moduleName;
        Name = name;
    }

    /// <summary>
    /// Deserializes the field data from the specified binary reader.
    /// </summary>
    /// <param name="reader">The binary reader used to read the field data.</param>
    public void Deserialize(BigEndianReader reader)
    {
        Type = reader.ReadInt();

        var currentType = Type;

        while (currentType is -99)
        {
            InnerTypeNames ??= [];
            InnerTypeIds ??= [];

            InnerTypeNames.Add(reader.ReadUtfLengthPrefixed16());
            InnerTypeIds.Add(currentType = reader.ReadInt());
        }
    }

    /// <summary>
    /// Reads the field value as an integer.
    /// </summary>
    /// <param name="reader">The binary reader used to read the value.</param>
    /// <returns>The integer value of the field.</returns>
    public int AsInt(BigEndianReader reader)
    {
        return reader.ReadInt();
    }

    /// <summary>
    /// Reads the field value as an unsigned integer.
    /// </summary>
    /// <param name="reader">The binary reader used to read the value.</param>
    /// <returns>The unsigned integer value of the field.</returns>
    public uint AsUInt(BigEndianReader reader)
    {
        return reader.ReadUInt();
    }

    /// <summary>
    /// Reads the field value as a boolean.
    /// </summary>
    /// <param name="reader">The binary reader used to read the value.</param>
    /// <returns>The boolean value of the field.</returns>
    public bool AsBoolean(BigEndianReader reader)
    {
        return reader.ReadBoolean();
    }

    /// <summary>
    /// Reads the field value as an internationalized (I18N) identifier.
    /// </summary>
    /// <param name="reader">The binary reader used to read the value.</param>
    /// <returns>The I18N identifier value of the field.</returns>
    public int AsI18N(BigEndianReader reader)
    {
        return reader.ReadInt();
    }

    /// <summary>
    /// Reads the field value as a string.
    /// </summary>
    /// <param name="reader">The binary reader used to read the value.</param>
    /// <returns>The string value of the field.</returns>
    public string AsString(BigEndianReader reader)
    {
        return reader.ReadUtfLengthPrefixed16();
    }

    /// <summary>
    /// Reads the field value as a double.
    /// </summary>
    /// <param name="reader">The binary reader used to read the value.</param>
    /// <returns>The double value of the field.</returns>
    public double AsDouble(BigEndianReader reader)
    {
        return reader.ReadDouble();
    }

    /// <summary>
    /// Reads the field value as a list of elements.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="reader">The binary reader used to read the list.</param>
    /// <param name="converter">A function to convert each element in the list.</param>
    /// <returns>A list of elements of type <typeparamref name="T"/>.</returns>
    public List<T> AsList<T>(BigEndianReader reader, Func<D2OField, BigEndianReader, T?> converter)
    {
        var length = reader.ReadInt();

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
    /// Reads the field value as a list of lists of elements.
    /// </summary>
    /// <typeparam name="T">The type of elements in the inner lists.</typeparam>
    /// <param name="reader">The binary reader used to read the list of lists.</param>
    /// <param name="converter">A function to convert each element in the inner lists.</param>
    /// <returns>A list of lists of elements of type <typeparamref name="T"/>.</returns>
    public List<List<T>> AsListOfList<T>(BigEndianReader reader, Func<D2OField, BigEndianReader, T> converter)
    {
        var length = reader.ReadInt();

        var content = new List<List<T>>(length);

        for (var i = 0; i < length; i++)
            content.Add(AsList(reader, converter));

        return content;
    }

    /// <summary>
    /// Reads the field value as an object of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the object, which must implement <see cref="IDatacenterObject"/>.</typeparam>
    /// <param name="reader">The binary reader used to read the object.</param>
    /// <returns>An object of type <typeparamref name="T"/>.</returns>
    /// <exception cref="Exception">Thrown if the class identifier is null.</exception>
    public T AsObject<T>(BigEndianReader reader)
        where T : class, IDatacenterObject
    {
        var classIdentifier = reader.ReadInt();

        return classIdentifier is NullIdentifier
            ? null!
            : D2OFile.GetClass(ModuleName, classIdentifier).Deserialize<T>(reader);
    }

    /// <summary>
    /// Returns a string representation of the field.
    /// </summary>
    /// <returns>A string containing the name, type, and inner type information of the field.</returns>
    public override string ToString()
    {
        return InnerTypeNames is not null && InnerTypeIds is not null
            ? $"Name: {Name}, Type: {(D2OFieldTypes)Type}, InnerTypeNames: {InnerTypeNames.Count}, InnerTypeIds: {InnerTypeIds.Count}"
            : $"Name: {Name}, Type: {(D2OFieldTypes)Type}";
    }
}
