// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Frozen;
using Krosmoz.Core.Extensions;
using Krosmoz.Tools.Protocol.Models;

namespace Krosmoz.Tools.Protocol.Converters;

/// <summary>
/// A converter class for transforming ActionScript class symbols into their C# equivalents.
/// </summary>
public sealed class ClassConverter : IConverter<ClassSymbol>
{
    /// <summary>
    /// A dictionary mapping ActionScript method names to their corresponding C# method names.
    /// </summary>
    private static readonly FrozenDictionary<string, string> s_actionScriptMethodsToCSharpMethods = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["boolean"] = "Boolean",
        ["byte"] = "SByte",
        ["double"] = "Double",
        ["float"] = "Single",
        ["int"] = "Int",
        ["short"] = "Short",
        ["unsignedint"] = "UInt",
        ["unsignedshort"] = "UShort",
        ["unsignedbyte"] = "Byte",
        ["utf"] = "UtfLengthPrefixed16",
        ["utfbytes"] = "UtfBytes",
        ["string"] = "UtfLengthPrefixed16",
        ["uint"] = "UInt",
        ["number"] = "Double",
        ["bytearray"] = "Bytes"
    }.ToFrozenDictionary();

    /// <summary>
    /// A dictionary mapping ActionScript method names to their corresponding C# types.
    /// </summary>
    private static readonly FrozenDictionary<string, string> s_methodsToTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["boolean"] = "bool",
        ["byte"] = "byte",
        ["sbyte"] = "sbyte",
        ["double"] = "double",
        ["single"] = "float",
        ["int"] = "int",
        ["uint"] = "uint",
        ["short"] = "short",
        ["ushort"] = "ushort",
        ["long"] = "long",
        ["ulong"] = "ulong",
        ["utfbytes"] = "string",
        ["utflengthprefixed16"] = "string"
    }.ToFrozenDictionary();

    /// <summary>
    /// A set of reserved C# keywords that need to be escaped when used as property names.
    /// </summary>
    private static readonly FrozenSet<string> s_cSharpReservedKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "abstract",
        "as",
        "base",
        "bool",
        "break",
        "byte",
        "case",
        "catch",
        "char",
        "checked",
        "class",
        "const",
        "continue",
        "decimal",
        "default",
        "delegate",
        "do",
        "double",
        "else",
        "enum",
        "event",
        "explicit",
        "extern",
        "false",
        "finally",
        "fixed",
        "float",
        "for",
        "foreach",
        "goto",
        "if",
        "implicit",
        "in",
        "int",
        "interface",
        "internal",
        "is",
        "lock",
        "long",
        "namespace",
        "new",
        "null",
        "object",
        "operator",
        "out",
        "override",
        "params",
        "private",
        "protected",
        "public",
        "readonly",
        "ref",
        "return",
        "sbyte",
        "sealed",
        "short",
        "sizeof",
        "stackalloc",
        "static",
        "string",
        "struct",
        "switch",
        "this",
        "throw",
        "true",
        "try",
        "typeof",
        "uint",
        "ulong",
        "unchecked",
        "unsafe",
        "ushort",
        "using",
        "virtual",
        "void",
        "volatile",
        "while"
    }.ToFrozenSet();

    /// <summary>
    /// Converts a given class symbol by transforming its properties and methods
    /// to their C# equivalents, handling reserved keywords and specific type mappings.
    /// </summary>
    /// <param name="symbol">The class symbol to be converted.</param>
    public void Convert(ClassSymbol symbol)
    {
        symbol.Metadata.Namespace = string.Concat("Krosmoz.Protocol", string.Join('.', symbol.Metadata.Namespace.Split('.').Select(static x => x.Capitalize())));

        symbol.Metadata.ParentName = symbol.Metadata.ParentName switch
        {
            "NetworkMessage" => "DofusMessage",
            "NetworkType" => "DofusType",
            _ => symbol.Metadata.ParentName
        };

        foreach (var property in symbol.Properties.Values)
        {
            property.Type = property.Type switch
            {
                "Boolean" => "bool",
                "ByteArray" => "byte[]",
                "String" => "string",
                _ => property.Type
            };

            if (property.ObjectType is "ByteArray" || property.Type is "byte[]")
                property.ObjectType = "byte[]";

            if (property.PropertyKind is PropertyKind.Vector)
            {
                if (!string.IsNullOrEmpty(property.VectorFieldRead) && !string.IsNullOrEmpty(property.VectorFieldWrite))
                {
                    property.VectorFieldRead = GetMethod(property.VectorFieldRead, "write", "write");
                    property.VectorFieldWrite = GetMethod(property.VectorFieldWrite, "write", "read");
                }
            }

            if (property.MethodKind is MethodKind.VectorPrimitive or MethodKind.Primitive &&
                !string.IsNullOrEmpty(property.ReadMethod) && !string.IsNullOrEmpty(property.WriteMethod))
            {
                property.ReadMethod = GetMethod(property.ReadMethod, "read", "read");
                property.WriteMethod = GetMethod(property.WriteMethod, "read", "write");
                property.ObjectType = GetType(property.ReadMethod, "read");
            }

            property.Name = s_cSharpReservedKeywords.Contains(property.Name)
                ? string.Concat('@', property.Name.Capitalize())
                : property.Name.Capitalize();

            property.ObjectType = property.ObjectType switch
            {
                "Achievement" => "Types.Game.Achievement.Achievement",
                "Shortcut" => "Types.Game.Shortcut.Shortcut",
                _ => property.ObjectType
            };
        }

        symbol.Properties = symbol.Properties
            .OrderBy(static x => x.Value.Index)
            .ToDictionary(static x => x.Key, static x => x.Value);
    }

    /// <summary>
    /// Converts a given ActionScript method name to its corresponding C# method name,
    /// replacing the specified method prefix with the desired replacement.
    /// </summary>
    /// <param name="as3">The ActionScript method name to convert.</param>
    /// <param name="method">The method prefix to be replaced (e.g., "read" or "write").</param>
    /// <param name="replaceWith">The replacement prefix for the method (e.g., "read" or "write").</param>
    /// <returns>
    /// A string representing the converted C# method name with the specified prefix replaced.
    /// </returns>
    private static string GetMethod(string as3, string method, string replaceWith)
    {
        return s_actionScriptMethodsToCSharpMethods.TryGetValue(as3.ToLowerInvariant().Replace(method, string.Empty), out var csharp)
            ? $"{replaceWith.Capitalize()}{csharp}"
            : $"{replaceWith.Capitalize()}{as3}";
    }

    /// <summary>
    /// Retrieves the C# type corresponding to a given method name, removing the specified prefix.
    /// </summary>
    /// <param name="method">The method name to analyze.</param>
    /// <param name="prefix">The prefix to be removed from the method name (e.g., "read").</param>
    /// <returns>
    /// A string representing the C# type corresponding to the method name, or the original method name
    /// in lowercase if no match is found.
    /// </returns>
    private static string GetType(string method, string prefix)
    {
        return s_methodsToTypes.TryGetValue(method.ToLowerInvariant().Replace(prefix, string.Empty), out var type)
            ? type
            : method.ToLowerInvariant();
    }
}
