// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Database.Models.Accounts;

namespace Krosmoz.Servers.AuthServer.Database.Repositories.Accounts;

/// <summary>
/// Represents a repository interface for managing account-related operations in the database.
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    /// Retrieves an account record by its username asynchronously.
    /// </summary>
    /// <param name="username">The username of the account to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the
    /// <see cref="AccountRecord"/> associated with the specified username.
    /// </returns>
    Task<AccountRecord?> GetAccountByUsernameAsync(string username, CancellationToken cancellationToken);
}
