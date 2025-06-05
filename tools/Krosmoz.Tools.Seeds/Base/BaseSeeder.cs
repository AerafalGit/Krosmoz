// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.GameServer.Database;
using Krosmoz.Servers.GameServer.Services.Datacenter;

namespace Krosmoz.Tools.Seeds.Base;

/// <summary>
/// Represents a base class for seeding data.
/// </summary>
public abstract class BaseSeeder
{
    /// <summary>
    /// Gets the datacenter service used for accessing datacenter files and objects.
    /// </summary>
    protected IDatacenterService DatacenterService { get; }

    /// <summary>
    /// Gets the authentication database context.
    /// </summary>
    protected AuthDbContext AuthDbContext { get; }

    /// <summary>
    /// Gets the game database context.
    /// </summary>
    protected GameDbContext GameDbContext { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseSeeder"/> class.
    /// </summary>
    /// <param name="datacenterService">The datacenter service instance.</param>
    /// <param name="authDbContext">The authentication database context instance.</param>
    /// <param name="gameDbContext">The game database context instance.</param>
    protected BaseSeeder(IDatacenterService datacenterService, AuthDbContext authDbContext, GameDbContext gameDbContext)
    {
        DatacenterService = datacenterService;
        AuthDbContext = authDbContext;
        GameDbContext = gameDbContext;
    }

    /// <summary>
    /// Seeds data asynchronously using the provided cancellation token.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous seeding operation.</returns>
    public abstract Task SeedAsync(CancellationToken cancellationToken);
}
