// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace Krosmoz.Tools.Protocol.Extensions;

/// <summary>
/// Provides extension methods for string manipulation.
/// </summary>
public static partial class StringExtensions
{
    /// <summary>
    /// Matches strings ending with "is" and captures the preceding part of the string.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching and capturing the "is" ending rule.</returns>
    [GeneratedRegex(@"(?<keep>.*)(is)$", RegexOptions.Compiled)]
    private static partial Regex IsToEsRule();

    /// <summary>
    /// Matches strings ending with "f" or "fe" and captures the preceding part of the string.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching and capturing the "f" or "fe" ending rule.</returns>
    [GeneratedRegex(@"(?<keep>.*[^f])(f|fe)$", RegexOptions.Compiled)]
    private static partial Regex FToVesRule();

    /// <summary>
    /// Matches strings ending with a consonant followed by "y" and captures the preceding part of the string.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching and capturing the consonant + "y" ending rule.</returns>
    [GeneratedRegex(@"(?<keep>.*[^aeiou])(y)$", RegexOptions.Compiled)]
    private static partial Regex ConsonantYToIesRule();

    /// <summary>
    /// Matches strings ending with a consonant followed by "o" and captures the preceding part of the string.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching and capturing the consonant + "o" ending rule.</returns>
    [GeneratedRegex(@"(?<keep>.*[^aeiou])(o)$", RegexOptions.Compiled)]
    private static partial Regex ConsonantOToOesRule();

    /// <summary>
    /// Matches strings ending with "us" and captures the preceding part of the string.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching and capturing the "us" ending rule.</returns>
    [GeneratedRegex(@"(?<keep>.*)(us)$", RegexOptions.Compiled)]
    private static partial Regex UsToIRule();

    /// <summary>
    /// Matches strings ending with "ex" or "ix" and captures the preceding part of the string.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching and capturing the "ex" or "ix" ending rule.</returns>
    [GeneratedRegex(@"(?<keep>.*)(ex|ix)$", RegexOptions.Compiled)]
    private static partial Regex ExIxToIcesRule();

    /// <summary>
    /// Matches strings ending with "on" and captures the preceding part of the string.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching and capturing the "on" ending rule.</returns>
    [GeneratedRegex(@"(?<keep>.*)(on)$", RegexOptions.Compiled)]
    private static partial Regex OnToARule();

    /// <summary>
    /// Matches strings ending with "um" and captures the preceding part of the string.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching and capturing the "um" ending rule.</returns>
    [GeneratedRegex(@"(?<keep>.*)(um)$", RegexOptions.Compiled)]
    private static partial Regex UmToARule();

    /// <summary>
    /// Matches strings ending with special cases like "ch", "sh", "ss", "x", or "z" and captures the preceding part of the string.
    /// </summary>
    /// <returns>A <see cref="Regex"/> for matching and capturing special endings.</returns>
    [GeneratedRegex(@"(?<keep>.*)(ch|sh|ss|x|z)$", RegexOptions.Compiled)]
    private static partial Regex SpecialEndingsToEsRule();

    /// <summary>
    /// Converts a singular noun to its plural form based on predefined rules.
    /// </summary>
    /// <param name="text">The input string to pluralize.</param>
    /// <returns>The pluralized form of the input string.</returns>
    [Pure]
    public static string Pluralize(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        if (text.Length <= 1)
            return text + "s";

        if (text.EndsWith("s", StringComparison.OrdinalIgnoreCase) && !text.EndsWith("ss", StringComparison.OrdinalIgnoreCase) && text.Length > 2)
            return text;

        var match = IsToEsRule().Match(text);
        if (match.Success)
            return match.Groups["keep"].Value + "es";

        match = FToVesRule().Match(text);
        if (match.Success)
            return match.Groups["keep"].Value + "ves";

        match = ConsonantYToIesRule().Match(text);
        if (match.Success)
            return match.Groups["keep"].Value + "ies";

        match = ConsonantOToOesRule().Match(text);
        if (match.Success)
            return match.Groups["keep"].Value + "oes";

        match = UsToIRule().Match(text);
        if (match.Success)
            return match.Groups["keep"].Value + "i";

        match = ExIxToIcesRule().Match(text);
        if (match.Success)
            return match.Groups["keep"].Value + "ices";

        match = OnToARule().Match(text);
        if (match.Success)
            return match.Groups["keep"].Value + "a";

        match = UmToARule().Match(text);
        if (match.Success)
            return match.Groups["keep"].Value + "a";

        match = SpecialEndingsToEsRule().Match(text);
        if (match.Success)
            return match.Groups["keep"].Value + match.Groups[2].Value + "es";

        return text + "s";
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

        return char.ToUpper(text[0]) + text[1..].ToLowerInvariant();
    }
}
