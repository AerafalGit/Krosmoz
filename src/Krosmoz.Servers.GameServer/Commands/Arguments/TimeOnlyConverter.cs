// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;

namespace Krosmoz.Servers.GameServer.Commands.Arguments;

/// <summary>
/// Converts a string argument into a <see cref="TimeOnly"/>.
/// </summary>
public sealed class TimeOnlyConverter : IArgumentConverter<TimeOnly>
{
    /// <summary>
    /// Converts a string argument from the command context into a <see cref="TimeOnly"/>.
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> containing the converted <see cref="TimeOnly"/> value if successful,
    /// or a <see cref="TimeOnly"/> created from <see cref="DateTime.UnixEpoch"/> if the conversion fails.
    /// </returns>
    public ValueTask<TimeOnly> ConvertAsync(CommandContext context, CancellationToken cancellationToken)
    {
        return TimeOnly.TryParse(context.Argument, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var value)
            ? new ValueTask<TimeOnly>(value)
            : new ValueTask<TimeOnly>(TimeOnly.FromDateTime(DateTime.UnixEpoch));
    }
}
