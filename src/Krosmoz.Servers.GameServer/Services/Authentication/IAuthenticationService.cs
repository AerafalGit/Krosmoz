// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Network.Transport;

namespace Krosmoz.Servers.GameServer.Services.Authentication;

/// <summary>
/// Defines the contract for an authentication service.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Authenticates a game connection using a ticket asynchronously.
    /// </summary>
    /// <param name="connection">The game connection to authenticate.</param>
    /// <param name="ticket">The authentication ticket.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AuthenticateByTicketAsync(DofusConnection connection, string ticket);
}
