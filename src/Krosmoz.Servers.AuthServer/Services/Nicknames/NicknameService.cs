// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Constants;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Connection.Register;
using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.AuthServer.Network.Transport;
using Krosmoz.Servers.AuthServer.Services.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Krosmoz.Servers.AuthServer.Services.Nicknames;

/// <summary>
/// Provides services for handling nickname-related operations in the authentication server.
/// </summary>
public sealed class NicknameService : INicknameService
{
    private readonly AuthDbContext _dbContext;
    private readonly IAuthenticationService _authenticationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="NicknameService"/> class.
    /// </summary>
    /// <param name="dbContext">The database context for accessing account data.</param>
    /// <param name="authenticationService">The service for managing client connections.</param>
    public NicknameService(AuthDbContext dbContext, IAuthenticationService authenticationService)
    {
        _dbContext = dbContext;
        _authenticationService = authenticationService;
    }

    /// <summary>
    /// Processes the nickname choice request for a client connection.
    /// </summary>
    /// <param name="connection">The client connection making the request.</param>
    /// <param name="nickname">The nickname chosen by the client.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task ChoiceNicknameAsync(DofusConnection connection, string nickname)
    {
        if (string.IsNullOrEmpty(nickname))
        {
            await SendNicknameRefusedAsync(connection, NicknameErrors.InvalidNick);
            return;
        }

        if (nickname.Length is < ProtocolConstants.MinNickLen or > ProtocolConstants.MaxNickLen)
        {
            await SendNicknameRefusedAsync(connection, NicknameErrors.InvalidNick);
            return;
        }

        if (connection.Account.Username.Equals(nickname, StringComparison.InvariantCultureIgnoreCase))
        {
            await SendNicknameRefusedAsync(connection, NicknameErrors.SameAsLogin);
            return;
        }

        if (connection.Account.Username.Contains(nickname, StringComparison.InvariantCultureIgnoreCase))
        {
            await SendNicknameRefusedAsync(connection, NicknameErrors.TooSimilarToLogin);
            return;
        }

        if (await _dbContext.Accounts.AnyAsync(x => x.Nickname != null && x.Nickname.Equals(nickname)))
        {
            await SendNicknameRefusedAsync(connection, NicknameErrors.AlreadyUsed);
            return;
        }

        connection.Account.Nickname = nickname;

        await connection.SendAsync<NicknameAcceptedMessage>();

        await _authenticationService.OnSuccessfullyAuthenticatedAsync(connection, connection.Account, 0);
    }

    /// <summary>
    /// Sends a nickname refusal message to the connection.
    /// </summary>
    /// <param name="connection">The connection to send the message to.</param>
    /// <param name="error">The reason for the nickname refusal.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private static async Task SendNicknameRefusedAsync(DofusConnection connection, NicknameErrors error)
    {
        await connection.SendAsync(new NicknameRefusedMessage { Reason = (sbyte)error });
    }
}
