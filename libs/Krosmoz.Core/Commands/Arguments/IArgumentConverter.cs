// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.Commands.Arguments;

/// <summary>
/// Represents a base interface for argument converters.
/// </summary>
public interface IArgumentConverter;

/// <summary>
/// Defines a contract for converting a string argument into a specific type.
/// </summary>
/// <typeparam name="TArgument">The type of the argument to convert to.</typeparam>
public interface IArgumentConverter<TArgument> : IArgumentConverter
{
    /// <summary>
    /// Converts a string argument into the specified type asynchronously.
    /// </summary>
    /// <param name="context">The context in which the command is executed.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> containing the converted argument of type <typeparamref name="TArgument"/>.
    /// </returns>
    ValueTask<TArgument> ConvertAsync(CommandContext context, CancellationToken cancellationToken);
}

/// <summary>
/// Defines a contract for converting a string argument into a specific type
/// with additional context information.
/// </summary>
/// <typeparam name="TArgument">The type of the argument to convert to.</typeparam>
/// <typeparam name="TContext">The type of the context in which the command is executed.</typeparam>
public interface IArgumentConverter<TArgument, in TContext> : IArgumentConverter
    where TContext : CommandContext
{
    /// <summary>
    /// Converts a string argument into the specified type asynchronously,
    /// using the provided context information.
    /// </summary>
    /// <param name="context">The specific context in which the command is executed.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> containing the converted argument of type <typeparamref name="TArgument"/>.
    /// </returns>
    ValueTask<TArgument> ConvertAsync(TContext context, CancellationToken cancellationToken);
}
