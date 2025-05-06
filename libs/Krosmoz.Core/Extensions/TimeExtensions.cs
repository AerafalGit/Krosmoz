// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.Contracts;

namespace Krosmoz.Core.Extensions;

/// <summary>
/// Provides extension methods for working with Unix timestamps and <see cref="DateTime"/> objects.
/// </summary>
public static class TimeExtensions
{
    /// <summary>
    /// Represents the Unix epoch, which is January 1, 1970, 00:00:00 UTC.
    /// </summary>
    private static readonly DateTime s_unixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Converts the specified <see cref="DateTime"/> to a Unix timestamp in seconds.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> to convert.</param>
    /// <returns>The Unix timestamp in seconds.</returns>
    [Pure]
    public static int GetUnixTimestampSeconds(this DateTime dateTime)
    {
        return (int)(dateTime - s_unixEpoch.ToLocalTime()).TotalSeconds;
    }

    /// <summary>
    /// Converts the specified <see cref="DateTime"/> to a Unix timestamp in milliseconds.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> to convert.</param>
    /// <returns>The Unix timestamp in milliseconds.</returns>
    [Pure]
    public static long GetUnixTimestampMilliseconds(this DateTime dateTime)
    {
        return (long)(dateTime - s_unixEpoch.ToLocalTime()).TotalMilliseconds;
    }

    /// <summary>
    /// Converts a Unix timestamp in seconds to a <see cref="DateTime"/> object.
    /// </summary>
    /// <param name="unixTimestamp">The Unix timestamp in seconds.</param>
    /// <returns>A <see cref="DateTime"/> object representing the specified Unix timestamp.</returns>
    [Pure]
    public static DateTime FromUnixTimestampSeconds(this int unixTimestamp)
    {
        return s_unixEpoch.AddSeconds(unixTimestamp).ToLocalTime();
    }

    /// <summary>
    /// Converts a Unix timestamp in milliseconds to a <see cref="DateTime"/> object.
    /// </summary>
    /// <param name="unixTimestamp">The Unix timestamp in milliseconds.</param>
    /// <returns>A <see cref="DateTime"/> object representing the specified Unix timestamp.</returns>
    [Pure]
    public static DateTime FromUnixTimestampMilliseconds(this long unixTimestamp)
    {
        return s_unixEpoch.AddMilliseconds(unixTimestamp).ToLocalTime();
    }
}
