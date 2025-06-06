// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Messages.Connection;
using Krosmoz.Servers.AuthServer.Database.Models.Accounts;
using Krosmoz.Servers.AuthServer.Network.Transport;

namespace Krosmoz.Servers.AuthServer.Services.Authentication;

/// <summary>
/// Defines the contract for a connection service that handles connection-related operations.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Authenticates a connection asynchronously using the provided identification message.
    /// </summary>
    /// <param name="connection">The connection to authenticate.</param>
    /// <param name="message">The identification message containing authentication details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AuthenticateAsync(DofusConnection connection, IdentificationMessage message);

    /// <summary>
    /// Handles post-authentication operations for a successfully authenticated connection.
    /// </summary>
    /// <param name="connection">The authenticated connection.</param>
    /// <param name="account">The account associated with the connection.</param>
    /// <param name="serverId">The ID of the server to auto-select for the connection.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task OnSuccessfullyAuthenticatedAsync(DofusConnection connection, AccountRecord account, ushort serverId);
}
