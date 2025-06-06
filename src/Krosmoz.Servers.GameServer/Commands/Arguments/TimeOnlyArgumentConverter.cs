// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;

namespace Krosmoz.Servers.GameServer.Commands.Arguments;

/// <summary>
/// Converts a string argument into a <see cref="TimeOnly"/>.
/// </summary>
public sealed class TimeOnlyArgumentConverter : IArgumentConverter<TimeOnly>
{
    /// <summary>
    /// Converts a string argument from the command context into a <see cref="TimeOnly"/>.
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <returns>
    /// The converted <see cref="TimeOnly"/> value if successful,
    /// or a <see cref="TimeOnly"/> created from <see cref="DateTime.UnixEpoch"/> if the conversion fails.
    /// </returns>
    public TimeOnly Convert(CommandContext context)
    {
        return TimeOnly.TryParse(context.Argument, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var value)
            ? value
            : TimeOnly.FromDateTime(DateTime.UnixEpoch);
    }
}
