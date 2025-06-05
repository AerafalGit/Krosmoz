// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.Contracts;

namespace Krosmoz.Tools.Protocol.Extensions;

/// <summary>
/// Provides extension methods for string manipulation.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Converts a namespace string into a file path by replacing dots with directory separators.
    /// </summary>
    /// <param name="text">The namespace string to convert.</param>
    /// <returns>
    /// A file path representation of the namespace, or the original string if it is null or empty.
    /// </returns>
    [Pure]
    public static string NamespaceToPath(this string text)
    {
        return string.IsNullOrEmpty(text)
            ? text
            : string.Join(Path.DirectorySeparatorChar, text.Split(['.'], StringSplitOptions.RemoveEmptyEntries));
    }

    /// <summary>
    /// Converts a string to PascalCase by capitalizing the first letter of each word
    /// and removing delimiters such as underscores, hyphens, and spaces.
    /// </summary>
    /// <param name="text">The input string to convert.</param>
    /// <returns>
    /// A PascalCase version of the input string, or the original string if it is null or empty.
    /// </returns>
    [Pure]
    public static string ToPascalCase(this string text)
    {
        return string.IsNullOrEmpty(text)
            ? text
            : string.Join(string.Empty, text.Split(['_', '-', ' '], StringSplitOptions.RemoveEmptyEntries).Select(static x => x.Capitalize()));
    }

    /// <summary>
    /// Capitalizes the first letter of the input string and converts the rest to lowercase.
    /// </summary>
    /// <param name="text">The input string to capitalize.</param>
    /// <returns>
    /// A string with the first character in uppercase and the remaining characters in lowercase,
    /// or the original string if it is null or empty.
    /// </returns>
    [Pure]
    private static string Capitalize(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        return char.ToUpperInvariant(text[0]) + text[1..].ToLowerInvariant();
    }

    /// <summary>
    /// Converts the first character of the input string to lowercase while keeping the rest unchanged.
    /// </summary>
    /// <param name="text">The input string to uncapitalize.</param>
    /// <returns>
    /// A string with the first character in lowercase and the remaining characters unchanged,
    /// or the original string if it is null or empty.
    /// </returns>
    [Pure]
    public static string Uncapitalize(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        return text[0] is '@'
            ? '@' + char.ToLowerInvariant(text[1]) + text[2..]
            : char.ToLowerInvariant(text[0]) + text[1..];
    }
}
