// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Commands.Arguments;

/// <summary>
/// Converts a nullable reference type argument of type <typeparamref name="TArgument"/>.
/// </summary>
/// <typeparam name="TArgument">The reference type of the argument to convert. Must be a class.</typeparam>
public sealed class NullableArgumentConverter<TArgument> : IArgumentConverter<TArgument?>
    where TArgument : class
{
    /// <summary>
    /// Converts a nullable argument of type <typeparamref name="TArgument"/> from the command context.
    /// </summary>
    /// <param name="context">The context in which the command is executed, containing the argument to convert.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> containing the converted nullable argument of type <typeparamref name="TArgument"/>
    /// if successful, or <c>null</c> if the conversion fails.
    /// </returns>
    public async ValueTask<TArgument?> ConvertAsync(CommandContext context, CancellationToken cancellationToken)
    {
        TArgument? result = null;

        if (context.Converters.TryGetValue(typeof(TArgument).Name, out var converter) && converter is IArgumentConverter<TArgument> typedConverter)
            result = await typedConverter.ConvertAsync(context, cancellationToken).ConfigureAwait(false);

        return result;
    }
}
