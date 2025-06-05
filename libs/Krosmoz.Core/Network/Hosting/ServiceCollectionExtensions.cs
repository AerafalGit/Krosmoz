// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Krosmoz.Core.Network.Hosting;

/// <summary>
/// Provides extension methods for configuring services related to the composite server.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures the host builder to use a composite server.
    /// </summary>
    /// <param name="builder">The host builder to configure.</param>
    /// <param name="configure">An action to configure the composite server builder.</param>
    /// <returns>The configured <see cref="IHostBuilder"/>.</returns>
    public static IHostBuilder UseCompositeServer(this IHostBuilder builder, Action<CompositeServerBuilder> configure)
    {
        builder.ConfigureServices(services =>
        {
            ConfigureServices(services, configure);
        });

        return builder;
    }

    /// <summary>
    /// Configures the application builder to use a composite server.
    /// </summary>
    /// <param name="builder">The application builder to configure.</param>
    /// <param name="configure">An action to configure the composite server builder.</param>
    /// <returns>The configured <see cref="IHostApplicationBuilder"/>.</returns>
    public static IHostApplicationBuilder UseCompositeServer(this IHostApplicationBuilder builder, Action<CompositeServerBuilder> configure)
    {
        ConfigureServices(builder.Services, configure);

        return builder;
    }

    /// <summary>
    /// Configures the service collection with the composite server and its options.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configure">An action to configure the composite server builder.</param>
    private static void ConfigureServices(IServiceCollection services, Action<CompositeServerBuilder> configure)
    {
        services.AddHostedService(provider =>
        {
            var builder = new CompositeServerBuilder(provider);
            configure(builder);
            return new ServerHostedService(builder.Build());
        });
    }
}
