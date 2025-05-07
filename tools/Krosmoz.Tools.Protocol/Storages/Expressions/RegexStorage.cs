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
}
