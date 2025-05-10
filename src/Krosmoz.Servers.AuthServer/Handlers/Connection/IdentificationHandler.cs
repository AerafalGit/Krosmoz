// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Connection;
using Krosmoz.Protocol.Messages.Connection.Register;
using Krosmoz.Protocol.Types.Version;
using Krosmoz.Servers.AuthServer.Database.Repositories.Accounts;
using Krosmoz.Servers.AuthServer.Network.Dispatcher.Handlers;
using Krosmoz.Servers.AuthServer.Network.Transport;
using Krosmoz.Servers.AuthServer.Services.Connection;

namespace Krosmoz.Servers.AuthServer.Handlers.Connection;

/// <summary>
/// Handles the identification process for authentication messages.
/// </summary>
public sealed class IdentificationHandler : AuthMessageHandler<IdentificationMessage>
{
    /// <summary>
    /// Specifies the required version for the client to connect.
    /// </summary>
    private static readonly VersionExtended s_requiredVersion = new()
    {
        Major = 2,
        Minor = 14,
        Release = 1,
        Revision = 35100,
        Patch = 0,
        BuildType = 5,
        Install = 1,
        Technology = 1
    };

    private readonly IAccountRepository _accountRepository;
    private readonly IConnectionService _connectionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentificationHandler"/> class.
    /// </summary>
    /// <param name="accountRepository">The repository for managing account-related operations.</param>
    /// <param name="connectionService">The service for managing connection-related operations.</param>
    public IdentificationHandler(IAccountRepository accountRepository, IConnectionService connectionService)
    {
        _accountRepository = accountRepository;
        _connectionService = connectionService;
    }

    /// <summary>
    /// Handles the identification message asynchronously.
    /// </summary>
    /// <param name="session">The session associated with the client.</param>
    /// <param name="message">The identification message sent by the client.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public override async Task HandleAsync(AuthSession session, IdentificationMessage message)
    {
        if (!message.Version.Equals(s_requiredVersion))
        {
            await session.SendAsync(new IdentificationFailedForBadVersionMessage
            {
                Reason = (sbyte)IdentificationFailureReasa.BadVersion,
                RequiredVersion = s_requiredVersion
            });

            await session.DisconnectAsync();
            return;
        }

        if (string.IsNullOrEmpty(message.Username) || string.IsNullOrEmpty(message.Password))
        {
            await _connectionService.SendIdentificationFailedMessageAsync(session, IdentificationFailureReasa.WrongCredentials);
            await session.DisconnectAsync();
            return;
        }

        await session.SendAsync(new CredentialsAcknowledgementMessage());

        var account = await _accountRepository.GetAccountByUsernameAsync(message.Username, session.ConnectionClosed);

        if (account is null || !account.Password.Equals(message.Password.ToMd5()))
        {
            await _connectionService.SendIdentificationFailedMessageAsync(session, IdentificationFailureReasa.WrongCredentials);
            await session.DisconnectAsync();
            return;
        }

        session.Account = account;
        session.Account.IpAddress = account.IpAddress;
        session.Account.Token = Guid.NewGuid().ToString().ToMd5();
        session.GameServerIdAutoSelect = message.ServerId;

        if (string.IsNullOrEmpty(session.Account.Nickname))
        {
            await session.SendAsync(new NicknameRegistrationMessage());
            return;
        }
    }
}
