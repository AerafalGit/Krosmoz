// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Database.Models.Accounts;
using Krosmoz.Servers.AuthServer.Network.Transport;

namespace Krosmoz.Servers.AuthServer.Services.Banishments;

/// <summary>
/// Defines the contract for a service that handles account banishment checks.
/// </summary>
public interface IBanishmentService
{
    /// <summary>
    /// Determines whether the specified account is banned.
    /// </summary>
    /// <param name="connection">The authentication connection associated with the account.</param>
    /// <param name="account">The account record to check for banishment.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains
    /// <c>true</c> if the account is banned; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> IsAccountBannedAsync(DofusConnection connection, AccountRecord account);
}
