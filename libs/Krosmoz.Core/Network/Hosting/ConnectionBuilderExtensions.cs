// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Middleware;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.DependencyInjection;

namespace Krosmoz.Core.Network.Hosting;

/// <summary>
/// Provides extension methods for configuring connection builders in the Boufbowl network framework.
/// </summary>
public static class ConnectionBuilderExtensions
{
    /// <summary>
    /// Adds telemetry middleware to the connection pipeline.
    /// This middleware captures telemetry data such as connection metrics and logs activity.
    /// </summary>
    /// <param name="builder">The connection builder to configure.</param>
    /// <returns>The connection builder with telemetry middleware added.</returns>
    public static IConnectionBuilder UseTelemetry(this IConnectionBuilder builder)
    {
        return builder.Use(context => ActivatorUtilities.CreateInstance<TelemetryMiddleware>(builder.ApplicationServices, context).InvokeAsync);
    }
}
