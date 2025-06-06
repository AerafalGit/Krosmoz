// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.SourceGenerators.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for string manipulation.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Capitalizes the first character of the specified string.
    /// </summary>
    /// <param name="text">The string to capitalize.</param>
    /// <returns>
    /// A new string with the first character converted to uppercase,
    /// or the original string if it is null or empty.
    /// </returns>
    public static string Capitalize(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        return char.ToUpperInvariant(text[0]) + text[1..];
    }

    /// <summary>
    /// Converts the first character of the specified string to lowercase.
    /// </summary>
    /// <param name="text">The string to uncapitalize.</param>
    /// <returns>
    /// A new string with the first character converted to lowercase,
    /// or the original string if it is null or empty.
    /// </returns>
    public static string UnCapitalize(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        return char.ToLowerInvariant(text[0]) + text[1..];
    }
}
