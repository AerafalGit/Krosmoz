// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;

namespace Krosmoz.Servers.GameServer.Commands.Arguments;

/// <summary>
/// Converts a string argument into a 32-bit floating-point number (float).
/// </summary>
public sealed class Float32ArgumentConverter : IArgumentConverter<float>
{
    /// <summary>
    /// Converts a string argument from the command context into a 32-bit floating-point number (float).
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> containing the converted float value if successful,
    /// or 0 if the conversion fails.
    /// </returns>
    public ValueTask<float> ConvertAsync(CommandContext context, CancellationToken cancellationToken)
    {
        return float.TryParse(context.Argument, CultureInfo.InvariantCulture, out var value)
            ? new ValueTask<float>(value)
            : new ValueTask<float>(0);
    }
}
