// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.Network.Internal;

/// <summary>
/// Represents an empty disposable object.
/// Provides a no-op implementation of the <see cref="IDisposable"/> interface.
/// </summary>
internal sealed class EmptyDisposable : IDisposable
{
    private static readonly Lazy<IDisposable> s_instance = new(() => new EmptyDisposable(), LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>
    /// Gets the singleton instance of <see cref="EmptyDisposable"/>.
    /// </summary>
    public static IDisposable Instance =>
        s_instance.Value;

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// This implementation does nothing as there are no resources to dispose.
    /// </summary>
    public void Dispose()
    {
        // No resources to dispose
    }
}
