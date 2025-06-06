// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;
using System.Text.RegularExpressions;

namespace Krosmoz.Servers.GameServer.Commands.Arguments;

/// <summary>
/// Converts a string argument into a <see cref="TimeSpan"/>.
/// </summary>
public sealed partial class TimeSpanArgumentConverter : IArgumentConverter<TimeSpan>
{
    /// <summary>
    /// Converts a string argument from the command context into a <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <returns>
    /// The converted <see cref="TimeSpan"/> value if successful,
    /// or <see cref="TimeSpan.Zero"/> if the conversion fails or the argument is invalid.
    /// </returns>
    public TimeSpan Convert(CommandContext context)
    {
        if (string.IsNullOrWhiteSpace(context.Argument) || context.Argument is "0" or "0s" or "0ms" or "0µs" or "0ns")
            return TimeSpan.Zero;

        if (TimeSpan.TryParse(context.Argument, CultureInfo.InvariantCulture, out var timeSpan))
            return timeSpan;

        var match = GetTimeSpanRegex().Match(context.Argument);

        if (!match.Success)
            return TimeSpan.Zero;

        var years = match.Groups["years"].Success ? int.Parse(match.Groups["years"].Value, CultureInfo.InvariantCulture) : 0;
        var months = match.Groups["months"].Success ? int.Parse(match.Groups["months"].Value, CultureInfo.InvariantCulture) : 0;
        var weeks = match.Groups["weeks"].Success ? int.Parse(match.Groups["weeks"].Value, CultureInfo.InvariantCulture) : 0;
        var days = match.Groups["days"].Success ? int.Parse(match.Groups["days"].Value, CultureInfo.InvariantCulture) : 0;
        var hours = match.Groups["hours"].Success ? int.Parse(match.Groups["hours"].Value, CultureInfo.InvariantCulture) : 0;
        var minutes = match.Groups["minutes"].Success ? int.Parse(match.Groups["minutes"].Value, CultureInfo.InvariantCulture) : 0;
        var seconds = match.Groups["seconds"].Success ? int.Parse(match.Groups["seconds"].Value, CultureInfo.InvariantCulture) : 0;
        var milliseconds = match.Groups["milliseconds"].Success ? int.Parse(match.Groups["milliseconds"].Value, CultureInfo.InvariantCulture) : 0;
        var microseconds = match.Groups["microseconds"].Success ? int.Parse(match.Groups["microseconds"].Value, CultureInfo.InvariantCulture) : 0;
        var nanoseconds = match.Groups["nanoseconds"].Success ? int.Parse(match.Groups["nanoseconds"].Value, CultureInfo.InvariantCulture) : 0;

        return new TimeSpan(
            ticks: years * TimeSpan.TicksPerDay * 365
                   + months * TimeSpan.TicksPerDay * 30
                   + weeks * TimeSpan.TicksPerDay * 7
                   + days * TimeSpan.TicksPerDay
                   + hours * TimeSpan.TicksPerHour
                   + minutes * TimeSpan.TicksPerMinute
                   + seconds * TimeSpan.TicksPerSecond
                   + milliseconds * TimeSpan.TicksPerMillisecond
                   + microseconds * TimeSpan.TicksPerMicrosecond
                   + nanoseconds * TimeSpan.NanosecondsPerTick
        );
    }

    /// <summary>
    /// Gets the regular expression used to parse a string into a <see cref="TimeSpan"/>.
    /// </summary>
    /// <returns>A compiled <see cref="Regex"/> for parsing time span strings.</returns>
    [GeneratedRegex(@"^((?<years>\d+)y\s*)?((?<months>\d+)mo\s*)?((?<weeks>\d+)w\s*)?((?<days>\d+)d\s*)?((?<hours>\d+)h\s*)?((?<minutes>\d+)m\s*)?((?<seconds>\d+)s\s*)?((?<milliseconds>\d+)ms\s*)?((?<microseconds>\d+)(µs|us)\s*)?((?<nanoseconds>\d+)ns\s*)?$", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.RightToLeft | RegexOptions.CultureInvariant)]
    private static partial Regex GetTimeSpanRegex();
}
