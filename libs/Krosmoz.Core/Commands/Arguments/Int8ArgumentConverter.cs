// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;

namespace Krosmoz.Core.Commands.Arguments;

/// <summary>
/// Converts a string argument into an 8-bit signed integer (sbyte).
/// </summary>
public sealed class Int8ArgumentConverter : IArgumentConverter<sbyte>
{
    /// <summary>
    /// Converts a string argument from the command context into an 8-bit signed integer (sbyte).
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> containing the converted sbyte value if successful,
    /// or 0 if the conversion fails.
    /// </returns>
    public ValueTask<sbyte> ConvertAsync(CommandContext context, CancellationToken cancellationToken)
    {
        return sbyte.TryParse(context.Argument, CultureInfo.InvariantCulture, out var value)
            ? new ValueTask<sbyte>(value)
            : new ValueTask<sbyte>(0);
    }
}
