// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Commands.Arguments;

/// <summary>
/// Represents a base interface for argument converters.
/// </summary>
public interface IArgumentConverter;

/// <summary>
/// Defines a contract for converting a string argument into a specific type.
/// </summary>
/// <typeparam name="TArgument">The type of the argument to convert to.</typeparam>
public interface IArgumentConverter<out TArgument> : IArgumentConverter
{
    /// <summary>
    /// Converts a string argument into the specified type asynchronously.
    /// </summary>
    /// <param name="context">The context in which the command is executed.</param>
    /// <returns>
    /// The converted argument of type <typeparamref name="TArgument"/>.
    /// </returns>
    TArgument Convert(CommandContext context);
}
