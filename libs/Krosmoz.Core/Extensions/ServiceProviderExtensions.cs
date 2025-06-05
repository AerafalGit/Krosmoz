// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Krosmoz.Core.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="IServiceProvider"/> interface.
/// </summary>
internal static class ServiceProviderExtensions
{
    /// <summary>
    /// Retrieves an <see cref="ILoggerFactory"/> instance from the service provider.
    /// If no logger factory is registered, returns a <see cref="NullLoggerFactory"/> instance.
    /// </summary>
    /// <param name="provider">The service provider to retrieve the logger factory from.</param>
    /// <returns>An <see cref="ILoggerFactory"/> instance.</returns>
    public static ILoggerFactory GetLoggerFactory(this IServiceProvider provider)
    {
        return provider.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
    }
}
