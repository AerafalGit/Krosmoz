// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Commands.Arguments;

/// <summary>
/// Converts a string argument into a boolean value.
/// </summary>
public sealed class BooleanArgumentConverter : IArgumentConverter<bool>
{
    /// <summary>
    /// Converts a string argument from the command context into a boolean value.
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> containing true if the argument matches common truthy values
    /// (e.g., "true", "yes", "y", "1", "on", "enable", "enabled", "t"), otherwise false.
    /// </returns>
    public ValueTask<bool> ConvertAsync(CommandContext context, CancellationToken cancellationToken)
    {
        return new ValueTask<bool>(context.Argument is "true" or "yes" or "y" or "1" or "on" or "enable" or "enabled" or "t");
    }
}
