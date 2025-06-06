// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;

namespace Krosmoz.Core.Commands.Arguments;

/// <summary>
/// Converts a string argument into a <see cref="DateTime"/>.
/// </summary>
public sealed class DateTimeConverter : IArgumentConverter<DateTime>
{
    /// <summary>
    /// Converts a string argument from the command context into a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> containing the converted <see cref="DateTime"/> value in UTC if successful,
    /// or <see cref="DateTime.UnixEpoch"/> if the conversion fails.
    /// </returns>
    public ValueTask<DateTime> ConvertAsync(CommandContext context, CancellationToken cancellationToken)
    {
        return DateTime.TryParse(context.Argument, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var value)
            ? new ValueTask<DateTime>(DateTime.SpecifyKind(value, DateTimeKind.Utc))
            : new ValueTask<DateTime>(DateTime.UnixEpoch);
    }
}
