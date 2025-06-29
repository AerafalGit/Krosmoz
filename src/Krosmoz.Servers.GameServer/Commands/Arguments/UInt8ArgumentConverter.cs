﻿// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;

namespace Krosmoz.Servers.GameServer.Commands.Arguments;

/// <summary>
/// Converts a string argument into an 8-bit unsigned integer (byte).
/// </summary>
public sealed class UInt8ArgumentConverter : IArgumentConverter<byte>
{
    /// <summary>
    /// Converts a string argument from the command context into an 8-bit unsigned integer (byte).
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <returns>
    /// The converted byte value if successful,
    /// or 0 if the conversion fails.
    /// </returns>
    public byte Convert(CommandContext context)
    {
        return byte.TryParse(context.Argument, CultureInfo.InvariantCulture, out var value)
            ? value
            : default;
    }
}
