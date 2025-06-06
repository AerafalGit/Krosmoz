// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;

namespace Krosmoz.Servers.GameServer.Commands.Arguments;

/// <summary>
/// Converts a string argument into a 16-bit unsigned integer (ushort).
/// </summary>
public sealed class UInt16ArgumentConverter : IArgumentConverter<ushort>
{
    /// <summary>
    /// Converts a string argument from the command context into a 16-bit unsigned integer (ushort).
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <returns>
    /// The converted ushort value if successful,
    /// or 0 if the conversion fails.
    /// </returns>
    public ushort Convert(CommandContext context)
    {
        return ushort.TryParse(context.Argument, CultureInfo.InvariantCulture, out var value)
            ? value
            : default;
    }
}
