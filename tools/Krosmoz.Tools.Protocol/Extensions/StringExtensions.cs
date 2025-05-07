// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;

namespace Krosmoz.Tools.Protocol.Extensions;

/// <summary>
/// Provides extension methods for string manipulation.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Converts a string to PascalCase by capitalizing the first letter of each word
    /// and removing delimiters such as underscores, hyphens, and spaces.
    /// </summary>
    /// <param name="text">The input string to convert.</param>
    /// <returns>
    /// A PascalCase version of the input string, or the original string if it is null or empty.
    /// </returns>
    public static string ToPascalCase(this string text)
    {
        return string.IsNullOrEmpty(text)
            ? text
            : string.Join(string.Empty, text.Split(['_', '-', ' '], StringSplitOptions.RemoveEmptyEntries).Select(static x => x.Capitalize()));
    }
}
