// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;

namespace Krosmoz.Core.Commands.Arguments;

/// <summary>
/// Converts a string argument into a <see cref="DateTimeOffset"/>.
/// </summary>
public sealed class DateTimeOffsetConverter : IArgumentConverter<DateTimeOffset>
{
    /// <summary>
    /// Converts a string argument from the command context into a <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> containing the converted <see cref="DateTimeOffset"/> value if successful,
    /// or <see cref="DateTimeOffset.UnixEpoch"/> if the conversion fails.
    /// </returns>
    public ValueTask<DateTimeOffset> ConvertAsync(CommandContext context, CancellationToken cancellationToken)
    {
        return DateTimeOffset.TryParse(context.Argument, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var value)
            ? new ValueTask<DateTimeOffset>(value)
            : new ValueTask<DateTimeOffset>(DateTimeOffset.UnixEpoch);
    }
}
