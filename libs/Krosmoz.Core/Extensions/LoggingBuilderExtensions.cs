// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Krosmoz.Core.Extensions;

/// <summary>
/// Provides extension methods for configuring logging with Serilog.
/// </summary>
public static class LoggingBuilderExtensions
{
    /// <summary>
    /// Configures the logging builder to use Serilog as the logging provider.
    /// </summary>
    /// <param name="builder">The logging builder to configure.</param>
    /// <returns>The configured <see cref="ILoggingBuilder"/> instance.</returns>
    public static ILoggingBuilder UseSerilog(this ILoggingBuilder builder)
    {
        return builder.ClearProviders().AddSerilog(new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss}] {Level:u3}: {Message:lj}{NewLine}{Exception}")
            .CreateLogger());
    }
}
