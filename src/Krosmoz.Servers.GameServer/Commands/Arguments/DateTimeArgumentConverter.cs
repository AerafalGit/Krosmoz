// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;

namespace Krosmoz.Servers.GameServer.Commands.Arguments;

/// <summary>
/// Converts a string argument into a <see cref="DateTime"/>.
/// </summary>
public sealed class DateTimeArgumentConverter : IArgumentConverter<DateTime>
{
    /// <summary>
    /// Converts a string argument from the command context into a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <returns>
    /// The converted <see cref="DateTime"/> value in UTC if successful,
    /// or <see cref="DateTime.UnixEpoch"/> if the conversion fails.
    /// </returns>
    public DateTime Convert(CommandContext context)
    {
        return DateTime.TryParse(context.Argument, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var value)
            ? DateTime.SpecifyKind(value, DateTimeKind.Utc)
            : DateTime.UnixEpoch;
    }
}
