// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Servers;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.AuthServer.Database.Models.Servers;
using Krosmoz.Servers.GameServer.Database;
using Krosmoz.Servers.GameServer.Services.Datacenter;
using Krosmoz.Tools.Seeds.Base;
using Microsoft.EntityFrameworkCore;

namespace Krosmoz.Tools.Seeds.Servers;

/// <summary>
/// Represents a seeder for server data.
/// </summary>
public sealed class ServerSeeder : BaseSeeder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServerSeeder"/> class.
    /// </summary>
    /// <param name="datacenterService">The datacenter service instance used to retrieve server data.</param>
    /// <param name="authDbContext">The authentication database context used to store server records.</param>
    /// <param name="gameDbContext">The game database context used to store game-related data.</param>
    public ServerSeeder(IDatacenterService datacenterService, AuthDbContext authDbContext, GameDbContext gameDbContext)
        : base(datacenterService, authDbContext, gameDbContext)
    {
    }

    /// <summary>
    /// Seeds server data asynchronously by clearing existing records and adding new ones.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous seeding operation.</returns>
    public override async Task SeedAsync(CancellationToken cancellationToken)
    {
        await AuthDbContext.Servers.ExecuteDeleteAsync(cancellationToken);

        var servers = DatacenterService
            .GetObjects<Server>()
            .Where(static x => x.Id > 0)
            .Select(server => new ServerRecord
            {
                Id = (ushort)server.Id,
                Name = server.Name,
                Type = (ServerGameTypeIds)server.GameTypeId,
                Status = ServerStatuses.Offline,
                JoinableHierarchy = GameHierarchies.Moderator,
                VisibleHierarchy = GameHierarchies.Moderator,
                Community = (ServerCommunityIds)server.CommunityId
            });

        AuthDbContext.Servers.AddRange(servers);

        await AuthDbContext.SaveChangesAsync(cancellationToken);
    }
}
