// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.GameServer.Database.Repositories.Datacenter;
using Krosmoz.Tools.Database.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Tools.Database.Hosting;

/// <summary>
/// Represents a hosted service responsible for synchronizing data using a collection of synchronizers.
/// </summary>
public sealed class SynchronizerHostedService : IHostedService
{
    private readonly IDbContextFactory<AuthDbContext> _authDbContextFactory;
    private readonly IDatacenterRepository _datacenterRepository;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IHostApplicationLifetime _lifetime;
    private readonly IEnumerable<Synchronizer> _synchronizers;

    /// <summary>
    /// Initializes a new instance of the <see cref="SynchronizerHostedService"/> class.
    /// </summary>
    /// <param name="authDbContextFactory">The factory for creating instances of <see cref="AuthDbContext"/>.</param>
    /// <param name="datacenterRepository">The repository for accessing datacenter data.</param>
    /// <param name="loggerFactory">The factory for creating logger instances.</param>
    /// <param name="lifetime">The application lifetime manager.</param>
    /// <param name="synchronizers">The collection of synchronizers to execute.</param>
    public SynchronizerHostedService(
        IDbContextFactory<AuthDbContext> authDbContextFactory,
        IDatacenterRepository datacenterRepository,
        ILoggerFactory loggerFactory,
        IHostApplicationLifetime lifetime,
        IEnumerable<Synchronizer> synchronizers)
    {
        _authDbContextFactory = authDbContextFactory;
        _datacenterRepository = datacenterRepository;
        _loggerFactory = loggerFactory;
        _synchronizers = synchronizers;
        _lifetime = lifetime;
    }

    /// <summary>
    /// Starts the hosted service and executes the synchronization process for each synchronizer.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous start operation.</returns>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var synchronizer in _synchronizers)
        {
            var synchronizerName = synchronizer.GetType().Name;

            synchronizer.AuthDbContext = await _authDbContextFactory.CreateDbContextAsync(cancellationToken);
            synchronizer.DatacenterRepository = _datacenterRepository;

            var logger = _loggerFactory.CreateLogger(synchronizerName);

            try
            {
                logger.LogInformation("Synchronizing {SynchronizerName}", synchronizerName);

                await synchronizer.SynchronizeAsync(cancellationToken);

                logger.LogInformation("{SynchronizerName} synchronized successfully", synchronizerName);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error during synchronization of {SynchronizerName}", synchronizerName);
            }
            finally
            {
                await synchronizer.AuthDbContext.DisposeAsync();
                _lifetime.StopApplication();
            }
        }
    }

    /// <summary>
    /// Stops the hosted service and signals the application to stop.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A completed task representing the stop operation.</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _lifetime.StopApplication();
        return Task.CompletedTask;
    }
}
