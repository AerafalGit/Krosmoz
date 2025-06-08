// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Dispatcher.Attributes;
using Krosmoz.Protocol.Messages.Game.Approach;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Authentication;

namespace Krosmoz.Servers.GameServer.Network.Handlers.Authentication;

/// <summary>
/// Handles authentication-related operations.
/// </summary>
public sealed class AuthenticationHandler
{
    private readonly IAuthenticationService _authenticationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationHandler"/> class.
    /// </summary>
    /// <param name="authenticationService">The authentication service used to handle authentication operations.</param>
    public AuthenticationHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    /// <summary>
    /// Handles the authentication ticket message received from a client asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client.</param>
    /// <param name="message">The authentication ticket message containing the ticket information.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleAuthenticationTicketAsync(DofusConnection connection, AuthenticationTicketMessage message)
    {
        return _authenticationService.AuthenticateByTicketAsync(connection, message.Ticket);
    }
}
