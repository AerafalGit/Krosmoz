// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;

namespace Krosmoz.Core.Commands.Arguments;

/// <summary>
/// Converts a string argument into a 64-bit signed integer (long).
/// </summary>
public sealed class Int64ArgumentConverter : IArgumentConverter<long>
{
    /// <summary>
    /// Converts a string argument from the command context into a 64-bit signed integer (long).
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> containing the converted long value if successful,
    /// or 0 if the conversion fails.
    /// </returns>
    public ValueTask<long> ConvertAsync(CommandContext context, CancellationToken cancellationToken)
    {
        return long.TryParse(context.Argument, CultureInfo.InvariantCulture, out var value)
            ? new ValueTask<long>(value)
            : new ValueTask<long>(0);
    }
}
