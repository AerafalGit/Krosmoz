// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.Extensions;

/// <summary>
/// Provides extension methods for <see cref="Task"/> to add functionality such as cancellation and timeout handling.
/// </summary>
internal static class TaskExtensions
{
    /// <summary>
    /// Awaits the specified <see cref="Task"/> with support for cancellation.
    /// </summary>
    /// <param name="task">The task to await.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that resolves to <c>true</c> if the task completes successfully,
    /// or <c>false</c> if the operation is canceled.
    /// </returns>
    public static async Task<bool> WithCancellation(this Task task, CancellationToken cancellationToken)
    {
        try
        {
            await task.WaitAsync(cancellationToken).ConfigureAwait(false);
            return true;
        }
        catch (OperationCanceledException)
        {
            return false;
        }
    }

    /// <summary>
    /// Awaits the specified <see cref="Task"/> with a timeout.
    /// </summary>
    /// <param name="task">The task to await.</param>
    /// <param name="timeout">The maximum duration to wait before timing out.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that resolves to <c>true</c> if the task completes within the timeout,
    /// or <c>false</c> if the operation times out.
    /// </returns>
    public static async Task<bool> TimeoutAfter(this Task task, TimeSpan timeout)
    {
        using var cts = new CancellationTokenSource(timeout);

        try
        {
            await task.WaitAsync(cts.Token).ConfigureAwait(false);
            return true;
        }
        catch (OperationCanceledException)
        {
            return false;
        }
    }
}
