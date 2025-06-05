// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.GameServer.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Tools.Migrations;

/// <summary>
/// A background service that handles database migrations for the Auth and Game databases.
/// </summary>
public sealed class MigrationWorker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly ILogger<MigrationWorker> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MigrationWorker"/> class.
    /// </summary>
    /// <param name="serviceScopeFactory">The factory to create service scopes.</param>
    /// <param name="applicationLifetime">The application lifetime manager.</param>
    /// <param name="logger">The logger instance for logging messages.</param>
    public MigrationWorker(IServiceScopeFactory serviceScopeFactory, IHostApplicationLifetime applicationLifetime, ILogger<MigrationWorker> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _applicationLifetime = applicationLifetime;
        _logger = logger;
    }

    /// <summary>
    /// Executes the migration process asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();

            var authDbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
            var gameDbContext = scope.ServiceProvider.GetRequiredService<GameDbContext>();

            var authDbExists = await EnsureDatabaseExistsAsync(authDbContext, cancellationToken);

            if (!authDbExists)
                _logger.LogWarning("Auth database does not exist. It will be created.");

            var gameDbExists = await EnsureDatabaseExistsAsync(gameDbContext, cancellationToken);

            if (!gameDbExists)
                _logger.LogWarning("Game database does not exist. It will be created.");

            _logger.LogInformation("Starting migrations for Auth database");
            await MigrateDatabaseAsync(authDbContext, cancellationToken);
            _logger.LogInformation("Auth database migrations completed successfully");

            _logger.LogInformation("Starting migrations for Game database");
            await MigrateDatabaseAsync(gameDbContext, cancellationToken);
            _logger.LogInformation("Game database migrations completed successfully");

            Environment.SetEnvironmentVariable("KROSMOZ_DB_NEED_SEED", !authDbExists && !gameDbExists ? "true" : "false", EnvironmentVariableTarget.User);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while executing migrations");
        }
        finally
        {
            _applicationLifetime.StopApplication();
        }
    }

    /// <summary>
    /// Ensures that the database exists by creating it if it does not already exist.
    /// </summary>
    /// <param name="context">The database context to ensure the database for.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private static Task<bool> EnsureDatabaseExistsAsync(DbContext context, CancellationToken cancellationToken)
    {
        return context.Database.CreateExecutionStrategy().ExecuteAsync(context.Database.EnsureCreatedAsync, cancellationToken);
    }

    /// <summary>
    /// Applies pending migrations to the database.
    /// </summary>
    /// <param name="dbContext">The database context to apply migrations for.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private static Task MigrateDatabaseAsync(DbContext dbContext, CancellationToken cancellationToken)
    {
        return dbContext.Database.CreateExecutionStrategy().ExecuteAsync(dbContext.Database.MigrateAsync, cancellationToken);
    }
}
