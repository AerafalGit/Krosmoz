﻿// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Serialization.Repository;
using Krosmoz.Servers.AuthServer.Database;

namespace Krosmoz.Tools.Database.Base;

/// <summary>
/// Represents a base class for synchronizing data between the datacenter repository
/// and the authentication database context.
/// </summary>
public abstract class BaseSynchronizer
{
    /// <summary>
    /// Gets or sets the repository for accessing datacenter data.
    /// This property is required and must be initialized before use.
    /// </summary>
    public IDatacenterRepository DatacenterRepository { get; set; } = null!;

    /// <summary>
    /// Gets or sets the database context for the authentication server.
    /// This property is required and must be initialized before use.
    /// </summary>
    public AuthDbContext AuthDbContext { get; set; } = null!;

    /// <summary>
    /// Synchronizes data asynchronously using the provided cancellation token.
    /// This method must be implemented by derived classes.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous synchronization operation.</returns>
    public abstract Task SynchronizeAsync(CancellationToken cancellationToken);
}
