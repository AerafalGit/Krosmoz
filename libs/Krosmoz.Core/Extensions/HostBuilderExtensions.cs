// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Krosmoz.Core.Extensions;

/// <summary>
/// Provides extension methods for configuring an <see cref="IHostBuilder"/> to use Serilog for logging.
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Configures the <see cref="IHostBuilder"/> to use Serilog as the logging provider with predefined settings.
    /// </summary>
    /// <param name="builder">The <see cref="IHostBuilder"/> to configure.</param>
    /// <returns>The configured <see cref="IHostBuilder"/>.</returns>
    /// <remarks>
    /// This method sets up Serilog with the following configurations:
    /// <list type="bullet">
    /// <item><description>Minimum log level is set to Debug.</description></item>
    /// <item><description>Overrides log levels for specific namespaces such as Microsoft and Microsoft.AspNetCore.</description></item>
    /// <item><description>Logs are written to the console with a custom output template.</description></item>
    /// </list>
    /// </remarks>
    public static IHostBuilder UseSerilogLogging(this IHostBuilder builder)
    {
        return builder.ConfigureLogging(static logging => logging.ClearProviders().AddSerilog(new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Error)
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss}] {Level:u3}: {Message:lj}{NewLine}{Exception}")
            .CreateLogger()));
    }
}
