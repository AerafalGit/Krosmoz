// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Krosmoz.Tools.Protocol.Models;
using Krosmoz.Tools.Protocol.Storages.Expressions;

namespace Krosmoz.Tools.Protocol.Parsers;

/// <summary>
/// Represents a parser for extracting information from a class symbol.
/// Implements the <see cref="IParser{T}"/> interface for parsing <see cref="ClassSymbol"/> objects.
/// </summary>
public sealed class ClassParser : IParser<ClassSymbol>
{
    /// <summary>
    /// Parses a class symbol from the provided symbol metadata, extracting its metadata, protocol ID,
    /// using directives, properties, and methods.
    /// </summary>
    /// <param name="symbolMetadata">The metadata of the symbol to parse.</param>
    /// <returns>
    /// A <see cref="ClassSymbol"/> object containing the parsed class information.
    /// </returns>
    public ClassSymbol Parse(SymbolMetadata symbolMetadata)
    {
        var classSymbol = new ClassSymbol
        {
            Metadata = symbolMetadata,
            ProtocolId = ParseProtocolId(symbolMetadata),
            Usings = [],
            Properties = []
        };

        ParseUsings(classSymbol);
        ParseProperties(classSymbol);
        ParseMethods(classSymbol);

        return classSymbol;
    }

    /// <summary>
    /// Parses the protocol ID from the provided symbol metadata.
    /// </summary>
    /// <param name="symbolMetadata">The metadata of the symbol to parse.</param>
    /// <returns>The protocol ID as an unsigned integer.</returns>
    /// <exception cref="Exception">Thrown if the protocol ID is not found in the source.</exception>
    private static uint ParseProtocolId(SymbolMetadata symbolMetadata)
    {
        var match = RegexStorage.ProtocolId().Match(symbolMetadata.Source);

        if (!match.Groups.TryGetValue("value", out var valueGroup))
            throw new Exception($"ProtocolId not found in class {symbolMetadata.Name}.");

        return valueGroup.Value.StartsWith("0x")
            ? Convert.ToUInt32(valueGroup.Value, 16)
            : uint.Parse(valueGroup.Value);
    }

    /// <summary>
    /// Parses the using directives from the class symbol's metadata and adds them to the class symbol.
    /// </summary>
    /// <param name="classSymbol">The class symbol to populate with using directives.</param>
    /// <exception cref="Exception">Thrown if a using directive is not found in the source.</exception>
    private static void ParseUsings(ClassSymbol classSymbol)
    {
        foreach (var match in RegexStorage.Using().Matches(classSymbol.Metadata.Source).Cast<Match>())
        {
            if (!match.Groups.TryGetValue("name", out var nameGroup))
                throw new Exception($"Using not found in class {classSymbol.Metadata.Name}.");

            var usingName = nameGroup.Value;

            if (!usingName.Contains("dofus.network"))
                continue;

            if (usingName.Contains("ProtocolTypeManager"))
                continue;

            classSymbol.Usings.Add(usingName);
        }
    }

    /// <summary>
    /// Parses the properties of a class symbol by delegating to specific parsing methods
    /// for primitive, object, and vector properties.
    /// </summary>
    /// <param name="classSymbol">The class symbol to populate with parsed properties.</param>
    private static void ParseProperties(ClassSymbol classSymbol)
    {
        ParsePropertiesPrimitive(classSymbol);
        ParsePropertiesObject(classSymbol);
        ParsePropertiesVector(classSymbol);
    }

    /// <summary>
    /// Parses primitive properties from the class symbol's metadata and adds them to the class symbol.
    /// </summary>
    /// <param name="classSymbol">The class symbol to populate with parsed primitive properties.</param>
    private static void ParsePropertiesPrimitive(ClassSymbol classSymbol)
    {
        foreach (var match in RegexStorage.PropertyPrimitive().Matches(classSymbol.Metadata.Source).Cast<Match>())
        {
            if (!EnsurePropertyIsValid(classSymbol, match, out var nameGroup, out var typeGroup))
                continue;

            var propertyName = nameGroup.Value;
            var propertyType = typeGroup.Value;

            classSymbol.Properties.Add(propertyName, new PropertySymbol
            {
                Name = propertyName,
                Type = propertyType,
                Index = nameGroup.Index,
                PropertyKind = PropertyKind.Primitive
            });
        }
    }

    /// <summary>
    /// Parses object properties from the class symbol's metadata and adds them to the class symbol.
    /// Skips properties of type "String" or "ByteArray".
    /// </summary>
    /// <param name="classSymbol">The class symbol to populate with parsed object properties.</param>
    private static void ParsePropertiesObject(ClassSymbol classSymbol)
    {
        foreach (var match in RegexStorage.PropertyObject().Matches(classSymbol.Metadata.Source).Cast<Match>())
        {
            if (!EnsurePropertyIsValid(classSymbol, match, out var nameGroup, out var typeGroup))
                continue;

            var propertyName = nameGroup.Value;
            var propertyType = typeGroup.Value;

            if (propertyType is "String" or "ByteArray")
                continue;

            classSymbol.Properties.Add(propertyName, new PropertySymbol
            {
                Name = propertyName,
                Type = propertyType,
                Index = nameGroup.Index,
                PropertyKind = PropertyKind.Object,
                MethodKind = MethodKind.SerializeOrDeserialize,
                ReadMethod = "Deserialize",
                WriteMethod = "Serialize",
                ObjectType = propertyType
            });
        }
    }

    /// <summary>
    /// Parses vector properties from the class symbol's metadata and adds them to the class symbol.
    /// </summary>
    /// <param name="classSymbol">The class symbol to populate with parsed vector properties.</param>
    private static void ParsePropertiesVector(ClassSymbol classSymbol)
    {
        foreach (var match in RegexStorage.PropertyVector().Matches(classSymbol.Metadata.Source).Cast<Match>())
        {
            if (!EnsurePropertyIsValid(classSymbol, match, out var nameGroup, out var typeGroup))
                continue;

            var propertyName = nameGroup.Value;
            var propertyType = typeGroup.Value;

            classSymbol.Properties.Add(propertyName, new PropertySymbol
            {
                Name = propertyName,
                Type = propertyType,
                Index = nameGroup.Index,
                PropertyKind = PropertyKind.Vector
            });
        }
    }

    /// <summary>
    /// Parses the methods of a class symbol by delegating to specific parsing methods
    /// for different method types, such as vector, primitive, and protocol object methods.
    /// </summary>
    /// <param name="classSymbol">The class symbol to populate with parsed methods.</param>
    private static void ParseMethods(ClassSymbol classSymbol)
    {
        ParseMethodVector(classSymbol);
        ParseMethodPrimitive(classSymbol);
        ParseMethodVectorObject(classSymbol);
        ParseMethodProtocolObject(classSymbol);
        ParseMethodVectorLimitLength(classSymbol);
        ParseBooleanByteWrapper(classSymbol);
    }

    /// <summary>
    /// Parses vector-related methods from the class symbol's metadata and updates the properties
    /// with the corresponding read and write methods.
    /// </summary>
    /// <param name="classSymbol">The class symbol to populate with parsed vector methods.</param>
    private static void ParseMethodVector(ClassSymbol classSymbol)
    {
        var source = GetContentInsideMethod(classSymbol, "serializeAs_");

        foreach (var match in RegexStorage.PropertyVectorFieldWriteLength().Matches(source).Cast<Match>())
        {
            if (!EnsurePropertyExist(classSymbol, match, out var property))
                continue;

            if (!EnsureMethodIsValid(classSymbol, match, out var methodGroup))
                continue;

            var methodName = methodGroup.Value;

            property.VectorFieldRead = methodName;
            property.VectorFieldWrite = methodName;
        }

        foreach (var match in RegexStorage.PropertyVectorFieldWriteMethod().Matches(source).Cast<Match>())
        {
            if (!EnsurePropertyExist(classSymbol, match, out var property))
                continue;

            if (!EnsureMethodIsValid(classSymbol, match, out var methodGroup))
                continue;

            var methodName = methodGroup.Value;

            property.ReadMethod = methodName;
            property.WriteMethod = methodName;
        }
    }

    /// <summary>
    /// Parses primitive-related methods from the class symbol's metadata and updates the properties
    /// with the corresponding read and write methods.
    /// </summary>
    /// <param name="classSymbol">The class symbol to populate with parsed primitive methods.</param>
    private static void ParseMethodPrimitive(ClassSymbol classSymbol)
    {
        foreach (var match in RegexStorage.ReadVectorMethodPrimitive().Matches(classSymbol.Metadata.Source).Cast<Match>())
        {
            if (!EnsurePropertyExist(classSymbol, match, out var property))
                continue;

            if (!EnsureMethodIsValid(classSymbol, match, out var methodGroup))
                continue;

            var methodName = methodGroup.Value;

            property.ReadMethod = methodName;
            property.WriteMethod = methodName;
            property.ObjectType = methodName;
            property.MethodKind = MethodKind.VectorPrimitive;
        }

        foreach (var match in RegexStorage.ReadVectorMethodPrimitive2().Matches(classSymbol.Metadata.Source).Cast<Match>())
        {
            if (!EnsurePropertyExist(classSymbol, match, out var property))
                continue;

            if (!EnsureMethodIsValid(classSymbol, match, out var methodGroup))
                continue;

            var methodName = methodGroup.Value;

            property.ReadMethod = methodName;
            property.WriteMethod = methodName;
            property.MethodKind = MethodKind.VectorPrimitive;
        }

        foreach (var match in RegexStorage.ReadMethodPrimitive().Matches(classSymbol.Metadata.Source).Cast<Match>())
        {
            if (!EnsurePropertyExist(classSymbol, match, out var property))
                continue;

            if (!EnsureMethodIsValid(classSymbol, match, out var methodGroup))
                continue;

            var methodName = methodGroup.Value;

            property.ReadMethod = methodName;
            property.WriteMethod = methodName;
            property.MethodKind = MethodKind.Primitive;
        }
    }

    /// <summary>
    /// Parses vector object-related methods from the class symbol's metadata and updates the properties
    /// with the corresponding read and write methods.
    /// </summary>
    /// <param name="classSymbol">The class symbol to populate with parsed vector object methods.</param>
    private static void ParseMethodVectorObject(ClassSymbol classSymbol)
    {
        foreach (var match in RegexStorage.ReadVectorMethodObject().Matches(classSymbol.Metadata.Source).Cast<Match>())
        {
            if (!EnsurePropertyExist(classSymbol, match, out var property))
                continue;

            if (!match.Groups.TryGetValue("type", out var typeGroup))
                throw new Exception($"Property type not found in class {classSymbol.Metadata.Name}.");

            var typeName = typeGroup.Value;

            property.ReadMethod = "Deserialize";
            property.WriteMethod = "Serialize";
            property.ObjectType = typeName;
            property.MethodKind = MethodKind.SerializeOrDeserialize;
        }
    }

    /// <summary>
    /// Parses protocol object-related methods from the class symbol's metadata and updates the properties
    /// with the corresponding object types.
    /// </summary>
    /// <param name="classSymbol">The class symbol to populate with parsed protocol object methods.</param>
    private static void ParseMethodProtocolObject(ClassSymbol classSymbol)
    {
        foreach (var match in RegexStorage.ReadVectorMethodProtocolManager().Matches(classSymbol.Metadata.Source).Cast<Match>())
        {
            if (!EnsurePropertyExist(classSymbol, match, out var property))
                continue;

            if (!match.Groups.TryGetValue("type", out var typeGroup))
                throw new Exception($"Property type not found in class {classSymbol.Metadata.Name}.");

            var typeName = typeGroup.Value;

            property.ObjectType = typeName;
            property.MethodKind = MethodKind.ProtocolTypeManager;
        }

        foreach (var match in RegexStorage.ReadMethodObjectProtocolManager().Matches(classSymbol.Metadata.Source).Cast<Match>())
        {
            if (!EnsurePropertyExist(classSymbol, match, out var property))
                continue;

            if (!match.Groups.TryGetValue("type", out var typeGroup))
                throw new Exception($"Property type not found in class {classSymbol.Metadata.Name}.");

            var typeName = typeGroup.Value;

            property.ObjectType = typeName;
            property.MethodKind = MethodKind.ProtocolTypeManager;
        }
    }

    /// <summary>
    /// Parses vector methods with length limits from the class symbol's metadata and updates the properties
    /// with the corresponding vector lengths.
    /// </summary>
    /// <param name="classSymbol">The class symbol to populate with parsed vector length methods.</param>
    private static void ParseMethodVectorLimitLength(ClassSymbol classSymbol)
    {
        foreach (var match in RegexStorage.ReadVectorMethodLimitLength().Matches(classSymbol.Metadata.Source).Cast<Match>())
        {
            if (!EnsurePropertyExist(classSymbol, match, out var property))
                continue;

            if (match.Groups.TryGetValue("value", out var valueGroup))
                property.VectorLength = int.Parse(valueGroup.Value);
        }
    }

    /// <summary>
    /// Parses boolean byte wrapper methods from the class symbol's metadata and updates the properties
    /// with the corresponding flags and methods.
    /// </summary>
    /// <param name="classSymbol">The class symbol to populate with parsed boolean byte wrapper methods.</param>
    private static void ParseBooleanByteWrapper(ClassSymbol classSymbol)
    {
        var lines = (classSymbol.Metadata.Source + "\n\n\n\n").Split('\n');

        for (var index = 0; index < lines.Length - 4; index++)
        {
            var line = lines[index];

            var match = RegexStorage.ReadFlagMethod().Match(line);

            if (!match.Success)
                continue;

            if (!EnsurePropertyExist(classSymbol, match, out var property))
                continue;

            if (!match.Groups.TryGetValue("name", out var nameGroup))
                throw new Exception($"Property name not found in class {classSymbol.Metadata.Name}.");

            if (!match.Groups.TryGetValue("flag", out var flagGroup))
                throw new Exception($"Property flag not found in class {classSymbol.Metadata.Name}.");

            var propertyIndex = nameGroup.Index;
            var flag = flagGroup.Value;

            property.ObjectType = "bool";
            property.MethodKind = MethodKind.BooleanByteWrapper;
            property.ReadMethod = flag;
            property.WriteMethod = flag;
            property.Index = propertyIndex;
        }
    }

    /// <summary>
    /// Extracts the content inside a specified method by locating its opening and closing brackets
    /// and returning the lines of code within the method.
    /// </summary>
    /// <param name="classSymbol">The class symbol containing the metadata of the source file.</param>
    /// <param name="methodName">The name of the method to extract content from.</param>
    /// <returns>
    /// A string containing the content inside the specified method, with empty lines removed.
    /// </returns>
    private static string GetContentInsideMethod(ClassSymbol classSymbol, string methodName)
    {
        var fileLines = classSymbol.Metadata.Source.Split("\n");
        var brackets = FindBracketsIndexesByLines(fileLines, '{', '}');

        var bracketOpen = Array.FindIndex(fileLines, entry => entry.Contains($"function {methodName}"));

        if (!fileLines[bracketOpen].EndsWith('{'))
            bracketOpen++;

        var bracketClose = brackets[bracketOpen];
        var methodLines  = new string[bracketClose - 1 - bracketOpen];

        Array.Copy(fileLines, bracketOpen + 1, methodLines, 0, bracketClose - 1 - bracketOpen);

        return methodLines
            .Where(static line => line.Trim() != string.Empty)
            .Aggregate(string.Empty, static (current, line) => current + (line + (char)10));
    }

    /// <summary>
    /// Finds the indexes of matching opening and closing brackets in a list of lines.
    /// </summary>
    /// <param name="lines">The lines of code to analyze.</param>
    /// <param name="start">The character representing the opening bracket.</param>
    /// <param name="end">The character representing the closing bracket.</param>
    /// <returns>
    /// A dictionary where the key is the line index of an opening bracket and the value is the line index
    /// of the corresponding closing bracket.
    /// </returns>
    private static Dictionary<int, int> FindBracketsIndexesByLines(IReadOnlyList<string> lines, char start, char end)
    {
        var elementsStack = new Stack<int>();
        var result = new Dictionary<int, int>();

        for (var i = 0; i < lines.Count; i++)
        {
            if (lines[i].Contains(start))
                elementsStack.Push(i);

            if (!lines[i].Contains(end))
                continue;

            result.Add(elementsStack.Pop(), i);
        }

        return result;
    }

    /// <summary>
    /// Ensures that a property is valid by checking if the match contains the required name and type groups.
    /// </summary>
    /// <param name="classSymbol">The class symbol being validated.</param>
    /// <param name="match">The match object containing the property information.</param>
    /// <param name="nameGroup">The output group containing the property name.</param>
    /// <param name="typeGroup">The output group containing the property type.</param>
    /// <returns>True if the property is valid; otherwise, false.</returns>
    /// <exception cref="Exception">Thrown if the name or type group is not found in the match.</exception>
    private static bool EnsurePropertyIsValid(ClassSymbol classSymbol, Match match, [NotNullWhen(true)] out Group? nameGroup, [NotNullWhen(true)] out Group? typeGroup)
    {
        if (!match.Groups.TryGetValue("name", out nameGroup))
            throw new Exception($"Property name not found in class {classSymbol.Metadata.Name}.");

        if (!match.Groups.TryGetValue("type", out typeGroup))
            throw new Exception($"Property type not found in class {classSymbol.Metadata.Name}.");

        return true;
    }

    /// <summary>
    /// Ensures that a property exists in the class symbol's properties dictionary.
    /// </summary>
    /// <param name="classSymbol">The class symbol containing the properties.</param>
    /// <param name="match">The match object containing the property information.</param>
    /// <param name="property">The output property symbol if it exists.</param>
    /// <returns>True if the property exists; otherwise, false.</returns>
    /// <exception cref="Exception">Thrown if the property name is not found in the match or the property does not exist in the dictionary.</exception>
    private static bool EnsurePropertyExist(ClassSymbol classSymbol, Match match, [NotNullWhen(true)] out PropertySymbol? property)
    {
        if (!match.Groups.TryGetValue("name", out var nameGroup))
            throw new Exception($"Property name not found in class {classSymbol.Metadata.Name}.");

        return classSymbol.Properties.TryGetValue(nameGroup.Value, out property);
    }

    /// <summary>
    /// Ensures that a method is valid by checking if the match contains the required method group.
    /// </summary>
    /// <param name="classSymbol">The class symbol being validated.</param>
    /// <param name="match">The match object containing the method information.</param>
    /// <param name="methodGroup">The output group containing the method name.</param>
    /// <returns>True if the method is valid; otherwise, false.</returns>
    /// <exception cref="Exception">Thrown if the method group is not found in the match.</exception>
    private static bool EnsureMethodIsValid(ClassSymbol classSymbol, Match match, [NotNullWhen(true)] out Group? methodGroup)
    {
        if (!match.Groups.TryGetValue("method", out methodGroup))
            throw new Exception($"Method name not found in class {classSymbol.Metadata.Name}.");

        return true;
    }
}
