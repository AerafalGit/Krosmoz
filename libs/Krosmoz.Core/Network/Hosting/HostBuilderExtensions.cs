// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Krosmoz.Core.Network.Hosting;

/// <summary>
/// Provides extension methods for configuring the host builder in the Boufbowl network framework.
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Configures the host builder to use a custom server with the specified configuration.
    /// </summary>
    /// <param name="builder">The host builder to configure.</param>
    /// <param name="configure">An action to configure the <see cref="ServerBuilder"/>.</param>
    /// <returns>The configured host builder.</returns>
    public static IHostBuilder UseServer(this IHostBuilder builder, Action<ServerBuilder> configure)
    {
        return builder.ConfigureServices(services =>
        {
            services.AddHostedService(provider =>
            {
                var serverBuilder = new ServerBuilder(provider);
                configure(serverBuilder);
                return new ServerHostedService(serverBuilder.Build());
            });
        });
    }
}
