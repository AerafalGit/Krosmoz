// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Text.Json.Serialization;
using Krosmoz.Core.Network.Framing;
using Krosmoz.Core.Network.Server;
using Krosmoz.Core.Network.Transport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Krosmoz.Core.Extensions;

/// <summary>
/// Provides extension methods for configuring application services and middleware.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// The endpoint path for the health check.
    /// </summary>
    private const string HealthEndpointPath = "/health";

    /// <summary>
    /// The endpoint path for the aliveness check.
    /// </summary>
    private const string AlivenessEndpointPath = "/alive";

    /// <summary>
    /// Configures the application builder to use an Npgsql DbContext with the specified connection name.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the DbContext to be configured.</typeparam>
    /// <param name="builder">The application builder to configure.</param>
    /// <param name="connectionName">The name of the connection string to use.</param>
    /// <exception cref="ArgumentException">Thrown if the connection name is null or empty.</exception>
    /// <returns>The configured <see cref="IHostApplicationBuilder"/> instance.</returns>
    public static IHostApplicationBuilder UseNpgsqlDbContext<TDbContext>(this IHostApplicationBuilder builder, string connectionName)
        where TDbContext : DbContext
    {
        ArgumentException.ThrowIfNullOrEmpty(connectionName);

        builder.AddNpgsqlDbContext<TDbContext>(connectionName, configureDbContextOptions: dbContextOptions =>
        {
            dbContextOptions.UseNpgsql(static npgsql =>
            {
                npgsql
                    .MigrationsAssembly(typeof(TDbContext).Assembly)
                    .MigrationsHistoryTable("migrations");
            });
        });

        return builder;
    }

    /// <summary>
    /// Configures the application builder to use a NATS client with the specified connection name and JSON serializer contexts.
    /// </summary>
    /// <param name="builder">The application builder to configure.</param>
    /// <param name="connectionName">The name of the connection string to use.</param>
    /// <param name="contexts">An array of JSON serializer contexts to register with the NATS client.</param>
    /// <exception cref="ArgumentException">Thrown if the connection name is null or empty.</exception>
    /// <returns>The configured <see cref="IHostApplicationBuilder"/> instance.</returns>
    public static IHostApplicationBuilder UseNats(this IHostApplicationBuilder builder, string connectionName, params JsonSerializerContext[] contexts)
    {
        ArgumentException.ThrowIfNullOrEmpty(connectionName);

        builder.AddNatsClient(connectionName, configureOptions: options => options with { SerializerRegistry = new NatsJsonContextSerializerRegistry(contexts) });

        return builder;
    }

    /// <summary>
    /// Adds default service configurations to the application builder.
    /// </summary>
    /// <param name="builder">The application builder to configure.</param>
    /// <returns>The configured <see cref="IHostApplicationBuilder"/> instance.</returns>
    public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
    {
        builder.ConfigureOpenTelemetry();
        builder.AddDefaultHealthChecks();

        builder.Services.AddServiceDiscovery();

        builder.Services.ConfigureHttpClientDefaults(static http =>
        {
            http.AddStandardResilienceHandler();
            http.AddServiceDiscovery();
        });

        builder.Logging.UseSerilog();

        return builder;
    }

    /// <summary>
    /// Configures OpenTelemetry for logging, metrics, and tracing.
    /// </summary>
    /// <param name="builder">The application builder to configure.</param>
    private static void ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
    {
        builder.Logging.AddOpenTelemetry(static logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        builder.Services
            .AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddMeter(ServerMetrics.MeterName)
                    .AddMeter(SocketConnectionMetrics.MeterName);
            })
            .WithTracing(tracing =>
            {
                tracing
                    .AddSource(builder.Environment.ApplicationName)
                    .AddAspNetCoreInstrumentation(static options => options.Filter = static context => !context.Request.Path.StartsWithSegments(HealthEndpointPath) && !context.Request.Path.StartsWithSegments(AlivenessEndpointPath))
                    .AddHttpClientInstrumentation()
                    .AddSource(FrameReader.ActivitySource.Name)
                    .AddSource(FrameWriter.ActivitySource.Name);
            });

        builder.AddOpenTelemetryExporters();
    }

    /// <summary>
    /// Adds OpenTelemetry exporters based on the configuration.
    /// </summary>
    /// <param name="builder">The application builder to configure.</param>
    private static void AddOpenTelemetryExporters(this IHostApplicationBuilder builder)
    {
        var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

        if (useOtlpExporter)
            builder.Services
                .AddOpenTelemetry()
                .UseOtlpExporter();
    }

    /// <summary>
    /// Adds default health checks to the application builder.
    /// </summary>
    /// <param name="builder">The application builder to configure.</param>
    private static void AddDefaultHealthChecks(this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);
    }
}
