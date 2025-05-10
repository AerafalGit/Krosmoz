// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.Contracts;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Krosmoz.Core.Extensions;

/// <summary>
/// Provides extension methods for string manipulation.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Capitalizes the first character of the specified string.
    /// </summary>
    /// <param name="text">The string to capitalize.</param>
    /// <returns>The input string with its first character converted to uppercase.</returns>
    [Pure]
    public static string Capitalize(this string text)
    {
        return string.IsNullOrEmpty(text)
            ? text
            : string.Concat(char.ToUpperInvariant(text[0]), text[1..]);
    }

    /// <summary>
    /// Determines whether the specified string contains any accented characters.
    /// </summary>
    /// <param name="text">The string to check for accents.</param>
    /// <returns><c>true</c> if the string contains accented characters; otherwise, <c>false</c>.</returns>
    [Pure]
    public static bool HasAccents(this string text)
    {
        return text
            .Normalize(NormalizationForm.FormD)
            .Any(static c => CharUnicodeInfo.GetUnicodeCategory(c) is UnicodeCategory.NonSpacingMark);
    }

    /// <summary>
    /// Removes all accented characters from the specified string.
    /// </summary>
    /// <param name="text">The string from which to remove accents.</param>
    /// <returns>A new string with all accented characters removed.</returns>
    [Pure]
    public static string RemoveAccents(this string text)
    {
        return string.Concat(text
                .Normalize(NormalizationForm.FormD)
                .Where(static c => CharUnicodeInfo.GetUnicodeCategory(c) is not UnicodeCategory.NonSpacingMark))
            .Normalize(NormalizationForm.FormC);
    }

    /// <summary>
    /// Converts the specified string to its MD5 hash representation in lowercase hexadecimal format.
    /// </summary>
    /// <param name="text">The input string to hash.</param>
    /// <returns>A string containing the MD5 hash of the input, represented in lowercase hexadecimal format.</returns>
    [Pure]
    public static string ToMd5(this string text)
    {
        return Convert.ToHexStringLower(MD5.HashData(Encoding.UTF8.GetBytes(text)));
    }
}
