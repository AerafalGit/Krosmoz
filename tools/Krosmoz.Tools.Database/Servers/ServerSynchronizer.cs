// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Servers;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Serialization.I18N;
using Krosmoz.Servers.AuthServer.Database.Models.Servers;
using Krosmoz.Tools.Database.Base;
using Microsoft.EntityFrameworkCore;

namespace Krosmoz.Tools.Database.Servers;

/// <summary>
/// Represents a synchronizer for server data, responsible for synchronizing
/// server information between the datacenter repository and the authentication database.
/// </summary>
public sealed class ServerSynchronizer : Synchronizer
{
    /// <summary>
    /// Synchronizes server data asynchronously by clearing the existing server records
    /// and repopulating them with data from the datacenter repository.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous synchronization operation.</returns>
    public override async Task SynchronizeAsync(CancellationToken cancellationToken)
    {
        await AuthDbContext.Servers.ExecuteDeleteAsync(cancellationToken);

        var i18N = DatacenterRepository.GetI18N();

        var servers = DatacenterRepository
            .GetObjects<Server>()
            .Select(server => new ServerRecord
            {
                Id = server.Id,
                Name = i18N.GetText(Languages.French, server.NameId),
                Type = (ServerGameTypeIds)server.GameTypeId,
                Status = ServerStatus.Offline
            });

        await AuthDbContext.Servers.AddRangeAsync(servers, cancellationToken);
        await AuthDbContext.SaveChangesAsync(cancellationToken);
    }
}
