// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;

namespace Krosmoz.Core.Commands.Arguments;

/// <summary>
/// Converts a string argument into a 16-bit signed integer (short).
/// </summary>
public sealed class Int16ArgumentConverter : IArgumentConverter<short>
{
    /// <summary>
    /// Converts a string argument from the command context into a 16-bit signed integer (short).
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> containing the converted short value if successful,
    /// or 0 if the conversion fails.
    /// </returns>
    public ValueTask<short> ConvertAsync(CommandContext context, CancellationToken cancellationToken)
    {
        return short.TryParse(context.Argument, CultureInfo.InvariantCulture, out var value)
            ? new ValueTask<short>(value)
            : new ValueTask<short>(0);
    }
}
