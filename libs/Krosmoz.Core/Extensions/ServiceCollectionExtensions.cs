// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Krosmoz.Core.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="IServiceCollection"/> to simplify service registration.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds a scoped hosted service to the service collection.
    /// This method registers the specified hosted service type as both a scoped service
    /// and a hosted service, ensuring it can be resolved and managed by the dependency injection container.
    /// </summary>
    /// <typeparam name="TService">The type of the hosted service to add.</typeparam>
    /// <param name="services">The service collection to which the hosted service will be added.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddScopedHostedService<TService>(this IServiceCollection services)
        where TService : class, IHostedService
    {
        return services
            .AddScoped<TService>()
            .AddHostedService(provider =>
            {
                using var scope = provider.CreateScope();
                return scope.ServiceProvider.GetRequiredService<TService>();
            });
    }
}
