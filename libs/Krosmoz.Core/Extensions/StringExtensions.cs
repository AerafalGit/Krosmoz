// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
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
    /// Converts the specified string to its MD5 hash representation in lowercase hexadecimal format.
    /// </summary>
    /// <param name="text">The input string to hash.</param>
    /// <returns>A string containing the MD5 hash of the input, represented in lowercase hexadecimal format.</returns>
    [Pure]
    public static string ToMd5(this string text)
    {
        return Convert.ToHexStringLower(MD5.HashData(Encoding.UTF8.GetBytes(text)));
    }

    /// <summary>
    /// Creates a new string by concatenating the specified string a given number of times.
    /// </summary>
    /// <param name="text">The string to be repeated.</param>
    /// <param name="times">The number of times to repeat the string.</param>
    /// <returns>A new string consisting of the input string repeated the specified number of times.</returns>
    [Pure]
    public static string ConcatCopy(this string text, int times)
    {
        var builder = new StringBuilder(text.Length * times);

        for (var i = 0; i < times; i++)
            builder.Append(text);

        return builder.ToString();
    }

    /// <summary>
    /// Counts the occurrences of a specified character in a substring of the given string.
    /// </summary>
    /// <param name="text">The string to search within.</param>
    /// <param name="chr">The character to count occurrences of.</param>
    /// <param name="startIndex">The starting index of the substring to search.</param>
    /// <param name="count">The number of characters to include in the substring.</param>
    /// <returns>The number of times the specified character occurs in the substring.</returns>
    [Pure]
    public static int CountOccurences(this string text, char chr, int startIndex, int count)
    {
        var occurences = 0;

        for (var i = startIndex; i < startIndex + count; i++)
        {
            if (text[i] == chr)
                occurences++;
        }

        return occurences;
    }

    /// <summary>
    /// Extracts the next argument from the specified string starting at the given position.
    /// </summary>
    /// <param name="text">The string to extract the argument from.</param>
    /// <param name="position">
    /// A reference to the current position in the string. This will be updated to the position
    /// after the extracted argument or the end of the string.
    /// </param>
    /// <param name="argument">
    /// When this method returns, contains the extracted argument if one was found; otherwise, <c>null</c>.
    /// </param>
    /// <returns>
    /// <c>true</c> if an argument was successfully extracted; otherwise, <c>false</c>.
    /// </returns>
    public static bool ExtractNextArgument(this string text, ref int position, [NotNullWhen(true)] out string? argument)
    {
        argument = null;

        if (string.IsNullOrWhiteSpace(text))
            return false;

        var i = position;

        for (; i < text.Length; i++)
        {
            if (!char.IsWhiteSpace(text[i]))
                break;
        }

        position = i;

        var endPos   = -1;
        var startPos = position;

        for (i = startPos; i < text.Length; i++)
        {
            if (char.IsWhiteSpace(text[i]))
                endPos = i;

            if (endPos is -1)
                continue;

            position = endPos;

            if (startPos == endPos)
                return false;

            argument = text.Substring(startPos, endPos - startPos);
            return true;
        }

        position = text.Length;

        if (startPos == position)
            return false;

        argument = text[startPos..];
        return true;
    }
}
