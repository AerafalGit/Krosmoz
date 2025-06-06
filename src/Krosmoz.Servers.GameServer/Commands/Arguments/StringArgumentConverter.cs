// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Commands.Arguments;

/// <summary>
/// Converts a string argument into a string.
/// </summary>
public sealed class StringArgumentConverter : IArgumentConverter<string>
{
    /// <summary>
    /// Converts a string argument from the command context into a string.
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <returns>
    /// The string value of the argument.
    /// </returns>
    public string Convert(CommandContext context)
    {
        if (context.Argument is not ['"', .. var argument, '"'])
        {
            context.ArgumentLength = 0;
            return string.Empty;
        }

        context.ArgumentLength = context.Argument.Length - (argument.Length + 2);

        return argument;
    }
}
