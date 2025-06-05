// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Text.RegularExpressions;

namespace Krosmoz.Tools.Protocol.Storages.Expressions;

/// <summary>
/// Provides a collection of precompiled regular expressions for parsing protocol-related constructs.
/// </summary>
public static partial class RegexStorage
{
    /// <summary>
    /// Matches throw error statements in the source code.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching throw error statements.</returns>
    [GeneratedRegex(@"^\s*throw\s+new\s+Error\s*\(", RegexOptions.Multiline)]
    public static partial Regex ThrowError();

    /// <summary>
    /// Matches class declarations, including optional parent and interface information.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching class declarations.</returns>
    [GeneratedRegex(@"public\s*[final]*\s+class\s+(?<name>[\w]+)\s*[extends]*\s*(?<parent>[\w]*)\s[implements\s+]*(?<interface>[\w]*)\s*,*\s*(?<interface2>[\w]*)", RegexOptions.Multiline)]
    public static partial Regex ClassDeclaration();

    /// <summary>
    /// Matches package declarations in the source code.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching package declarations.</returns>
    [GeneratedRegex(@"package\s+(?<name>[\w|\.]+)", RegexOptions.Multiline)]
    public static partial Regex NamespaceDeclaration();

    /// <summary>
    /// Matches enumeration property declarations in the source code.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching enumeration property declarations,
    /// including the name, type, and value of the property.
    /// </returns>
    [GeneratedRegex(@"public\s+static\s+const\s+(?<name>[\w|_]+)\s*:\s*(?<type>[\w]+)\s*=\s*(?<value>[-+|\d||\w]+)\s*;", RegexOptions.Multiline)]
    public static partial Regex EnumProperty();

    /// <summary>
    /// Matches protocol identifier declarations in the source code.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching protocol identifier declarations,
    /// including the name, type, and value of the identifier.
    /// </returns>
    [GeneratedRegex(@"public\s*static\s*const\s*protocolId\s*:\s*uint\s*=\s*(?<value>[\w]+)\s*;", RegexOptions.Multiline)]
    public static partial Regex ProtocolId();

    /// <summary>
    /// Matches import statements in the source code.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching import statements,
    /// capturing the name of the imported namespace or module.
    /// </returns>
    [GeneratedRegex(@"import\s+(?<name>[\w|\.]+);", RegexOptions.Multiline)]
    public static partial Regex Using();

    /// <summary>
    /// Matches primitive property declarations in the source code.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching primitive property declarations,
    /// capturing the name and type of the property.
    /// </returns>
    [GeneratedRegex(@"public\s+var\s+(?<name>[\w]+)\s*:\s*(?<type>String|Boolean|int|Number|uint|byte|ByteArray)", RegexOptions.Multiline)]
    public static partial Regex PropertyPrimitive();

    /// <summary>
    /// Matches object property declarations in the source code.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching object property declarations,
    /// capturing the name and type of the property.
    /// </returns>
    [GeneratedRegex(@"public\s*var\s*(?<name>[\w]+)\s*:\s*(?<type>[\w]+) = new", RegexOptions.Multiline)]
    public static partial Regex PropertyObject();

    /// <summary>
    /// Matches vector property declarations in the source code.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching vector property declarations,
    /// capturing the name and type of the property.
    /// </returns>
    [GeneratedRegex(@"public\s+var\s+(?<name>[\w]+)\s*:\s*Vector.\s*<\s*(?<type>[\w]+)\s*>", RegexOptions.Multiline)]
    public static partial Regex PropertyVector();

    /// <summary>
    /// Matches vector field write length operations in the source code.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching vector field write length operations,
    /// capturing the method name and the field name being accessed.
    /// </returns>
    [GeneratedRegex(@"output\.(?<method>[\w]+)\((this\.)?(?<name>[\w]+).length\);", RegexOptions.Multiline)]
    public static partial Regex PropertyVectorFieldWriteLength();

    /// <summary>
    /// Matches vector field write method operations in the source code.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching vector field write method operations,
    /// capturing the method name and the field name being accessed.
    /// </returns>
    [GeneratedRegex(@"output\.(?<method>[\w]+)\((this\.)?(?<name>[\w]+)\[", RegexOptions.Multiline)]
    public static partial Regex PropertyVectorFieldWriteMethod();

    /// <summary>
    /// Matches vector read operations for primitive types in the source code,
    /// capturing the method name and the field name being accessed.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching vector read operations for primitive types,
    /// including the method name and the field name being pushed to the vector.
    /// </returns>
    [GeneratedRegex(@"while\s*\(\s*\w+\s*<\s*\w+\)\s*\n\s*\{\s*\n\s*\w+\s*=\s*(\w+\()?input\.(?<method>[\w]+)\(\)(\))?;\s*\s*.*\s*.*\s*.*\s*.*\s*this\.(?<name>[\w]+)\.push", RegexOptions.Multiline)]
    public static partial Regex ReadVectorMethodPrimitive();

    /// <summary>
    /// Matches vector read operations for primitive types in the source code,
    /// capturing the method name and the field name being assigned within the vector.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching vector read operations for primitive types,
    /// including the method name and the field name being assigned.
    /// </returns>
    [GeneratedRegex(@"for\s*\(.*<\s*(_)?(?<name>[\w]+)Len\s*.*\s*.*\s*.*=\s*(\w+\()?input\.(?<method>[\w]+)\(\)(\))?;", RegexOptions.Multiline)]
    public static partial Regex ReadVectorMethodPrimitive2();

    /// <summary>
    /// Matches read operations for primitive types in the source code,
    /// capturing the method name and the field name being assigned.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching read operations for primitive types,
    /// including the method name and the field name being assigned.
    /// </returns>
    [GeneratedRegex(@"(this\.)?(?<name>\w+)\s*=\s*input\.(?<method>\w+)\(\);", RegexOptions.Multiline)]
    public static partial Regex ReadMethodPrimitive();

    /// <summary>
    /// Matches vector read operations for object types in the source code,
    /// capturing the type of the object being deserialized and the field name
    /// being pushed to the vector.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching vector read operations for object types,
    /// including the type of the object and the field name being accessed.
    /// </returns>
    [GeneratedRegex(@"^\s*_\w+\s*=\s*new\s+(?<type>[\w]+)\(\)\s*;\s*\n\s*_\w+.deserialize\(\s*input\s*\)\s*;\s*\n\s*(this\.)?(?<name>[\w]+).push\(_\w+\)\s*;\s*$", RegexOptions.Multiline)]
    public static partial Regex ReadVectorMethodObject();

    /// <summary>
    /// Matches vector read operations for protocol-managed object types in the source code,
    /// capturing the type of the object being deserialized and the field name being pushed to the vector.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching vector read operations for protocol-managed object types,
    /// including the type of the object and the field name being accessed.
    /// </returns>
    [GeneratedRegex(@"for.*<.*Len\s*;.*\s*.*\s*.*\s*(\w+\()?input\.\w+\(\)(\))?;\s*\n\s*\w+\s*=\s*ProtocolTypeManager\.getInstance\((?<type>[\w]+)\s*,\s*\w+\s*\)\s*;\s*\n\s*\w+\.deserialize\(input\);\s*\n\s*(this\.)?(?<name>[\w]+)\.push", RegexOptions.Multiline)]
    public static partial Regex ReadVectorMethodProtocolManager();

    /// <summary>
    /// Matches read operations for protocol-managed object types in the source code,
    /// capturing the field name being assigned and the type of the object being instantiated.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching read operations for protocol-managed object types,
    /// including the field name and the type of the object being accessed.
    /// </returns>
    [GeneratedRegex(@"(this\.)?(?<name>[\w]+)\s*=\s*ProtocolTypeManager.getInstance\(\s*(?<type>[\w]+),", RegexOptions.Multiline)]
    public static partial Regex ReadMethodObjectProtocolManager();

    /// <summary>
    /// Matches read operations with a limited length for vector fields in the source code,
    /// capturing the maximum length value and the field name being accessed.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching read operations with a limited length for vector fields,
    /// including the maximum length value and the field name being accessed.
    /// </returns>
    [GeneratedRegex(@"for.*<\s*(?<value>[\d]+).*\n\s*.*\n\s*(this\.)?(?<name>[\w]+)\[", RegexOptions.Multiline)]
    public static partial Regex ReadVectorMethodLimitLength();

    /// <summary>
    /// Matches read operations for boolean flags in the source code,
    /// capturing the field name being assigned and the flag index being accessed.
    /// </summary>
    /// <returns>
    /// A <see cref="Regex"/> for matching read operations for boolean flags,
    /// including the field name and the flag index.
    /// </returns>
    [GeneratedRegex(@"^\s*(this\.)?(?<name>[\w]+)\s*=\s*BooleanByteWrapper.getFlag\(_[\w\d]+,\s*(?<flag>[\d]+)\)", RegexOptions.Multiline)]
    public static partial Regex ReadFlagMethod();
}
