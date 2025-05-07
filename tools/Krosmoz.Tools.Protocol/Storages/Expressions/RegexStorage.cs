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
    /// Matches import statements in the format `import name;`.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching import statements.</returns>
    [GeneratedRegex(@"import\s+(?<name>[\w|\.]+);", RegexOptions.Multiline)]
    public static partial Regex Using();

    /// <summary>
    /// Matches class declarations, including optional parent and interface information.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching class declarations.</returns>
    [GeneratedRegex(@"public\s*[final]*\s+class\s+(?<name>[\w]+)\s*[extends]*\s*(?<parent>[\w]*)\s[implements\s+]*(?<interface>[\w]*)", RegexOptions.Multiline)]
    public static partial Regex Declaration();

    /// <summary>
    /// Matches static constant protocol ID declarations.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching protocol ID declarations.</returns>
    [GeneratedRegex(@"public\s*static\s*const\s*(?<name>[\w]+)\s*:\s*(?<type>[\w]+)\s*=\s*(?<value>[\w]+);", RegexOptions.Multiline)]
    public static partial Regex ProtocolId();

    /// <summary>
    /// Matches package declarations.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching package declarations.</returns>
    [GeneratedRegex(@"package\s+(?<name>[\w|\.]+)", RegexOptions.Multiline)]
    public static partial Regex Namespace();

    /// <summary>
    /// Matches object property declarations.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching object property declarations.</returns>
    [GeneratedRegex(@"public\s*var\s*(?<name>[\w]+)\s*:\s*(?<type>[\w]+) = new", RegexOptions.Multiline)]
    public static partial Regex PropertyObject();

    /// <summary>
    /// Matches primitive property declarations.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching primitive property declarations.</returns>
    [GeneratedRegex(@"public\s+var\s+(?<name>[\w]+)\s*:\s*(?<type>String|Boolean|int|Number|uint|byte|ByteArray)", RegexOptions.Multiline)]
    public static partial Regex PropertyPrimitive();

    /// <summary>
    /// Matches vector property declarations.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching vector property declarations.</returns>
    [GeneratedRegex(@"public\s+var\s+(?<name>[\w]+)\s*:\s*Vector\.<(?<type>[\w]+)>", RegexOptions.Multiline)]
    public static partial Regex PropertyVector();

    /// <summary>
    /// Matches vector field write length methods.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching vector field write length methods.</returns>
    [GeneratedRegex(@"param1\.(?<method>[\w]+)\(this\.(?<name>[\w]+)\.length\);", RegexOptions.Multiline)]
    public static partial Regex PropertyVectorFieldWriteLength();

    /// <summary>
    /// Matches vector field write methods.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching vector field write methods.</returns>
    [GeneratedRegex(@"param1\.(?<method>[\w]+)\(this.(?<name>[\w]+)\[", RegexOptions.Multiline)]
    public static partial Regex PropertyVectorFieldWriteMethod();

    /// <summary>
    /// Matches primitive read methods.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching primitive read methods.</returns>
    [GeneratedRegex(@"this.(?<name>[\w]+)\s*=\s*param1.(?<method>[\w]+)\(\);", RegexOptions.Multiline)]
    public static partial Regex ReadMethodPrimitive();

    /// <summary>
    /// Matches object read methods.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching object read methods.</returns>
    [GeneratedRegex(@"this.(?<name>[\w]+).deserialize\([\w]+\);", RegexOptions.Multiline)]
    public static partial Regex ReadMethodObject();

    /// <summary>
    /// Matches vector write methods using protocol manager.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching vector write methods using protocol manager.</returns>
    [GeneratedRegex(@"while\s*.*<\s*this\.(?<name>[\w]+)\.length.*\n.*\n.*param1\.(?<method>[\w]+)\(\(this\..*\s*as\s*(?<type>[\w]+)\)\.getTypeId\(\)\);.*\n\s*\(this\.[\w]+.*\s*as\s*.*\.serialize\(param1\);", RegexOptions.Multiline)]
    public static partial Regex WriteMethodVectorProtocolManager();

    /// <summary>
    /// Matches object read methods using protocol manager.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching object read methods using protocol manager.</returns>
    [GeneratedRegex(@"this.(?<name>[\w]+)\s*=\s*ProtocolTypeManager.getInstance\(\s*(?<type>[\w]+),", RegexOptions.Multiline)]
    public static partial Regex ReadMethodObjectProtocolManager();

    /// <summary>
    /// Matches flag read methods.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching flag read methods.</returns>
    [GeneratedRegex(@"this.(?<name>[\w]+)\s*=\s*BooleanByteWrapper.getFlag\(_[\w\d]+,\s*(?<flag>[\d]+)\)", RegexOptions.Multiline)]
    public static partial Regex ReadFlagMethod();

    /// <summary>
    /// Matches throw error statements.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching throw error statements.</returns>
    [GeneratedRegex(@"throw\s+new\s+Error\s*\(", RegexOptions.Multiline)]
    public static partial Regex ThrowError();

    /// <summary>
    /// Matches enumeration property declarations.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching enumeration property declarations.</returns>
    [GeneratedRegex(@"public\s+static\s+const\s+(?<name>[\w|_]+)\s*:\s*(?<type>[\w]+)\s*=\s*(?<value>[-+|\d||\w]+)\s*;", RegexOptions.Multiline)]
    public static partial Regex EnumProperty();

    /// <summary>
    /// Matches enumeration property declarations with string values.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching enumeration property declarations with string values.</returns>
    [GeneratedRegex(@"public\s+static\s+const\s+(?<name>[\w|_]+)\s*:\s*(?<type>[\w]+)\s*=\s*[\w\W](?<value>.*)[\w\W];", RegexOptions.Multiline)]
    public static partial Regex EnumPropertyWithString();

    /// <summary>
    /// Matches enumeration class declarations.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching enumeration class declarations.</returns>
    [GeneratedRegex(@"^\s*public\s+class\s+(?<name>[\w]+)\s*$", RegexOptions.Multiline)]
    public static partial Regex EnumDeclaration();

    /// <summary>
    /// Matches vector property declarations with a length limit.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching vector property declarations with a length limit.</returns>
    [GeneratedRegex(@"while.*<\s*(?<value>[\d]+).*\n\s*.*\n.*this.(?<name>[\w]+)\[", RegexOptions.Multiline)]
    public static partial Regex PropertyVectorLimitLength();

    /// <summary>
    /// Matches vector write methods for objects.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching vector write methods for objects.</returns>
    [GeneratedRegex(@"\(this.(?<name>[\w]+).*\s*\]\s*as\s*(?<type>[\w]+)\)\.serializeAs_[\w]+\(param1\);", RegexOptions.Multiline)]
    public static partial Regex WriteVectorMethodObject();

    /// <summary>
    /// Matches protocol message ID dictionary entries.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching protocol message ID dictionary entries.</returns>
    [GeneratedRegex(@"^\s*_messagesTypes\[(?<protocolId>[\d]+)\]\s+=\s+(?<name>[\w]+)", RegexOptions.Multiline)]
    public static partial Regex ProtocolMessageIdDictionary();

    /// <summary>
    /// Matches protocol type ID dictionary entries.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching protocol type ID dictionary entries.</returns>
    [GeneratedRegex(@"^\s*_typesTypes\[(?<protocolId>[\d]+)\]\s+=\s+(?<name>[\w]+)", RegexOptions.Multiline)]
    public static partial Regex ProtocolTypeIdDictionary();
}
