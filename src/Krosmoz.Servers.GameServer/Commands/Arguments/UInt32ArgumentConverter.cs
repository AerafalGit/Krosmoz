﻿// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;

namespace Krosmoz.Servers.GameServer.Commands.Arguments;

/// <summary>
/// Converts a string argument into a 32-bit unsigned integer (uint).
/// </summary>
public sealed class UInt32ArgumentConverter : IArgumentConverter<uint>
{
    /// <summary>
    /// Converts a string argument from the command context into a 32-bit unsigned integer (uint).
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <returns>
    /// The converted uint value if successful,
    /// or 0 if the conversion fails.
    /// </returns>
    public uint Convert(CommandContext context)
    {
        return uint.TryParse(context.Argument, CultureInfo.InvariantCulture, out var value)
            ? value
            : default;
    }
}
