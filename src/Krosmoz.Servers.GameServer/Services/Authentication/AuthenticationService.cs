// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Ipc;
using Krosmoz.Protocol.Ipc.Messages.Accounts;
using Krosmoz.Protocol.Messages.Game.Approach;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Servers;
using Microsoft.AspNetCore.Connections;
using NATS.Client.Core;

namespace Krosmoz.Servers.GameServer.Services.Authentication;

/// <summary>
/// Provides functionality for authenticating game connections.
/// </summary>
public sealed class AuthenticationService : IAuthenticationService
{
    private readonly IServerService _serverService;
    private readonly INatsConnection _natsConnection;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
    /// </summary>
    /// <param name="serverService">The server service for managing server-related operations.</param>
    /// <param name="natsConnection">The NATS connection for messaging and communication.</param>
    public AuthenticationService(IServerService serverService, INatsConnection natsConnection)
    {
        _serverService = serverService;
        _natsConnection = natsConnection;
    }

    /// <summary>
    /// Authenticates a game connection using a ticket asynchronously.
    /// </summary>
    /// <param name="connection">The game connection to authenticate.</param>
    /// <param name="ticket">The authentication ticket.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AuthenticateByTicketAsync(DofusConnection connection, string ticket)
    {
        try
        {
            var response = await _natsConnection.RequestAsync<IpcAccountByTicketRequest, IpcAccountByTicketResponse>(
                IpcSubjects.AccountByTicket,
                new IpcAccountByTicketRequest { Ticket = ticket },
                cancellationToken: connection.ConnectionClosed);

            if (response.Data?.Account is null)
            {
                await SendAuthenticationTicketRefusedAsync(connection);
                connection.Abort("Account with the provided ticket does not exist.");
                return;
            }

            connection.Heroes.Account = response.Data.Account;

            await _serverService.SendServerInformationsAsync(connection);

            await connection.SendAsync<AuthenticationTicketAcceptedMessage>();
        }
        catch (Exception e)
        {
            await SendAuthenticationTicketRefusedAsync(connection);
            connection.Abort(new ConnectionAbortedException("An error occurred while authenticating the connection.", e));
        }
    }

    /// <summary>
    /// Sends an authentication ticket refused message asynchronously to the specified game connection.
    /// </summary>
    /// <param name="connection">The game connection to which the message will be sent.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static ValueTask SendAuthenticationTicketRefusedAsync(DofusConnection connection)
    {
        return connection.SendAsync<AuthenticationTicketRefusedMessage>();
    }
}
