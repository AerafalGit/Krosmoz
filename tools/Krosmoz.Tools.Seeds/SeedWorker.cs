// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Tools.Seeds.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Tools.Seeds;

/// <summary>
/// A background service that handles database seeding operations.
/// </summary>
public sealed class SeedWorker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IConfiguration _configuration;
    private readonly ILogger<SeedWorker> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SeedWorker"/> class.
    /// </summary>
    /// <param name="serviceScopeFactory">The factory to create service scopes.</param>
    /// <param name="applicationLifetime">The application lifetime manager.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="logger">The logger instance for logging messages.</param>
    public SeedWorker(IServiceScopeFactory serviceScopeFactory, IHostApplicationLifetime applicationLifetime, IConfiguration configuration, ILogger<SeedWorker> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _applicationLifetime = applicationLifetime;
        _configuration = configuration;
        _logger = logger;
    }

    /// <summary>
    /// Executes the database seeding process asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!_configuration.GetValue<bool>("KROSMOZ_DB_NEED_SEED"))
        {
            _logger.LogInformation("Database seeding is not required. Exiting the worker.");
            _applicationLifetime.StopApplication();
            return;
        }

        try
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();

            var seedsCount = 0;

            foreach (var seeder in scope.ServiceProvider.GetServices<BaseSeeder>())
            {
                var synchronizerName = seeder.GetType().Name;

                _logger.LogInformation("Starting seeding for {SynchronizerName}", synchronizerName);

                await seeder.SeedAsync(cancellationToken);

                _logger.LogInformation("Finished seeding for {SynchronizerName}", synchronizerName);

                seedsCount++;
            }

            _logger.LogInformation("Total seeds executed: {SeedsCount}", seedsCount);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while seeding the database");
        }
        finally
        {
            _applicationLifetime.StopApplication();
        }
    }
}
