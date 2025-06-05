// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.Network.Internal;

/// <summary>
/// Represents an empty implementation of the <see cref="IServiceProvider"/> interface.
/// Provides a singleton instance that always returns null for service requests.
/// </summary>
internal sealed class EmptyServiceProvider : IServiceProvider
{
    private static readonly Lazy<IServiceProvider> s_instance = new(() => new EmptyServiceProvider(), LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>
    /// Gets the singleton instance of <see cref="EmptyServiceProvider"/>.
    /// </summary>
    public static IServiceProvider Instance =>
        s_instance.Value;

    /// <summary>
    /// Always returns null, indicating no service is provided for the specified type.
    /// </summary>
    /// <param name="serviceType">The type of service requested.</param>
    /// <returns>Always null.</returns>
    public object? GetService(Type serviceType)
    {
        return null;
    }
}
