// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.Commands.Arguments;

/// <summary>
/// Converts a nullable value type argument of type <typeparamref name="TArgument"/>.
/// </summary>
/// <typeparam name="TArgument">The value type of the argument to convert. Must be a struct.</typeparam>
public sealed class NullableStructArgumentConverter<TArgument> : IArgumentConverter<TArgument?>
    where TArgument : struct
{
    /// <summary>
    /// Converts a nullable value type argument from the command context.
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> containing the converted nullable value type argument of type <typeparamref name="TArgument"/>
    /// if successful, or <c>null</c> if the conversion fails.
    /// </returns>
    public async ValueTask<TArgument?> ConvertAsync(CommandContext context, CancellationToken cancellationToken)
    {
        var type = Nullable.GetUnderlyingType(typeof(TArgument)) ?? typeof(TArgument);

        TArgument? result = null;

        if (context.Converters.TryGetValue(type.Name, out var converter) && converter is IArgumentConverter<TArgument> typedConverter)
            result = await typedConverter.ConvertAsync(context, cancellationToken).ConfigureAwait(false);

        return result;
    }
}

/// <summary>
/// Converts a nullable value type argument of type <typeparamref name="TArgument"/>
/// using a specific context of type <typeparamref name="TContext"/>.
/// </summary>
/// <typeparam name="TArgument">The value type of the argument to convert.</typeparam>
/// <typeparam name="TContext">The type of the command context.</typeparam>
public sealed class NullableStructArgumentConverter<TArgument, TContext> : IArgumentConverter<TArgument?, TContext>
    where TArgument : struct
    where TContext : CommandContext
{
    /// <summary>
    /// Converts a nullable value type argument from the command context.
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> containing the converted nullable value type argument of type <typeparamref name="TArgument"/>
    /// if successful, or <c>null</c> if the conversion fails.
    /// </returns>
    public async ValueTask<TArgument?> ConvertAsync(TContext context, CancellationToken cancellationToken)
    {
        var type = Nullable.GetUnderlyingType(typeof(TArgument)) ?? typeof(TArgument);

        TArgument? result = null;

        if (context.Converters.TryGetValue(type.Name, out var converter) && converter is IArgumentConverter<TArgument, TContext> typedConverter)
            result = await typedConverter.ConvertAsync(context, cancellationToken).ConfigureAwait(false);

        return result;
    }
}
