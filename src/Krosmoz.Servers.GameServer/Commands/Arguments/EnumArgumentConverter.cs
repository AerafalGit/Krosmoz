// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Commands.Arguments;

/// <summary>
/// Converts a string argument into an enumeration value of type <typeparamref name="TEnum"/>.
/// </summary>
/// <typeparam name="TEnum">The enumeration type to convert to. Must be a struct and an enum.</typeparam>
public sealed class EnumArgumentConverter<TEnum> : IArgumentConverter<TEnum>
    where TEnum : struct, Enum
{
    /// <summary>
    /// Converts a string argument from the command context into an enumeration value of type <typeparamref name="TEnum"/>.
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <returns>
    /// The converted enumeration value of type <typeparamref name="TEnum"/> if successful,
    /// or the default value of <typeparamref name="TEnum"/> if the conversion fails.
    /// </returns>
    public TEnum Convert(CommandContext context)
    {
        return Enum.TryParse<TEnum>(context.Argument, true, out var value)
            ? value
            : default;
    }
}
