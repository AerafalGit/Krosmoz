// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;

namespace Krosmoz.Servers.GameServer.Commands.Arguments;

/// <summary>
/// Converts a string argument into a <see cref="DateOnly"/>.
/// </summary>
public sealed class DateOnlyConverter : IArgumentConverter<DateOnly>
{
    /// <summary>
    /// Converts a string argument from the command context into a <see cref="DateOnly"/>.
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> containing the converted <see cref="DateOnly"/> value if successful,
    /// or a <see cref="DateOnly"/> created from <see cref="DateTime.UnixEpoch"/> if the conversion fails.
    /// </returns>
    public ValueTask<DateOnly> ConvertAsync(CommandContext context, CancellationToken cancellationToken)
    {
        return DateOnly.TryParse(context.Argument, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var value)
            ? new ValueTask<DateOnly>(value)
            : new ValueTask<DateOnly>(DateOnly.FromDateTime(DateTime.UnixEpoch));
    }
}
