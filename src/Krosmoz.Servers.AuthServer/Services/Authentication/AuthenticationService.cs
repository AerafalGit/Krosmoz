// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;
using System.Net.NetworkInformation;
using Krosmoz.Core.Extensions;
using Krosmoz.Protocol.Constants;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Messages.Connection;
using Krosmoz.Protocol.Messages.Connection.Register;
using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.AuthServer.Database.Models.Accounts;
using Krosmoz.Servers.AuthServer.Network.Transport;
using Krosmoz.Servers.AuthServer.Services.Banishments;
using Krosmoz.Servers.AuthServer.Services.Servers;
using Microsoft.EntityFrameworkCore;

namespace Krosmoz.Servers.AuthServer.Services.Authentication;

/// <summary>
/// Provides services for managing connection-related operations in the authentication server.
/// </summary>
public sealed class AuthenticationService : IAuthenticationService
{
    private readonly AuthDbContext _dbContext;
    private readonly IServerService _serverService;
    private readonly IBanishmentService _banishmentService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
    /// </summary>
    /// <param name="dbContext">The database context for accessing authentication-related data.</param>
    /// <param name="serverService">The service for managing server-related operations.</param>
    /// <param name="banishmentService">The service for managing banishment-related operations.</param>
    public AuthenticationService(AuthDbContext dbContext, IServerService serverService, IBanishmentService banishmentService)
    {
        _dbContext = dbContext;
        _serverService = serverService;
        _banishmentService = banishmentService;
    }

    /// <summary>
    /// Authenticates a connection asynchronously using the provided identification message.
    /// </summary>
    /// <param name="connection">The connection to authenticate.</param>
    /// <param name="message">The identification message containing authentication details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task AuthenticateAsync(DofusConnection connection, IdentificationMessage message)
    {
        if (!BuildInfosConstants.BuildVersion.Equals(message.Version))
        {
            await SendIdentificationFailedForBadVersionAsync(connection);
            await connection.AbortAsync("Bad version.", TimeSpan.FromMilliseconds(500));
            return;
        }

        if (string.IsNullOrEmpty(message.Username) || string.IsNullOrEmpty(message.Password))
        {
            await SendIdentificationFailedAsync(connection, IdentificationFailureReasons.WrongCredentials);
            await connection.AbortAsync("Wrong credentials.", TimeSpan.FromMilliseconds(500));
            return;
        }

        await connection.SendAsync<CredentialsAcknowledgementMessage>();

        var account = await _dbContext
            .Accounts
            .Include(static x => x.Characters)
            .Include(static x => x.Relations)
            .FirstOrDefaultAsync(x => x.Username.Equals(message.Username), connection.ConnectionClosed);

        if (account is null || !account.Password.Equals(message.Password.ToMd5()))
        {
            await SendIdentificationFailedAsync(connection, IdentificationFailureReasons.WrongCredentials);
            await connection.AbortAsync("Wrong credentials.", TimeSpan.FromMilliseconds(500));
            return;
        }

        if (!string.IsNullOrEmpty(message.MacAddress))
            account.MacAddress = PhysicalAddress.Parse(message.MacAddress);

        account.IpAddress = connection.RemoteEndPoint!.Address;
        account.Ticket = Guid.NewGuid().ToString("N");

        if (await _banishmentService.IsAccountBannedAsync(connection, account))
        {
            await connection.AbortAsync("Account is banned.", TimeSpan.FromMilliseconds(500));
            return;
        }

        await OnSuccessfullyAuthenticatedAsync(connection, account, (ushort)message.ServerId);
    }

    /// <summary>
    /// Handles post-authentication operations for a successfully authenticated connection.
    /// </summary>
    /// <param name="connection">The authenticated connection.</param>
    /// <param name="account">The account associated with the connection.</param>
    /// <param name="serverId">The ID of the server to auto-select for the connection.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task OnSuccessfullyAuthenticatedAsync(DofusConnection connection, AccountRecord account, ushort serverId)
    {
        connection.Account = account;
        connection.ServerIdAutoSelect = serverId;

        if (string.IsNullOrEmpty(account.Nickname))
        {
            await connection.SendAsync<NicknameRegistrationMessage>();
            return;
        }

        _dbContext.Update(account);

        await _dbContext.SaveChangesAsync(connection.ConnectionClosed);

        Debug.Assert(!string.IsNullOrEmpty(account.Nickname));

        var accountCreation = DateTime.UtcNow.GetUnixTimestampMilliseconds() - account.CreatedAt.GetUnixTimestampMilliseconds();

        var hasRights = account.Hierarchy >= GameHierarchies.Moderator;

        var subscriptionEndDate = hasRights
            ? DateTime.UtcNow.AddYears(100)
            : account.SubscriptionExpiredAt.GetValueOrDefault(DateTime.UtcNow);

        var subscriptionElapsedDuration = hasRights
            ? DateTime.UtcNow.GetUnixTimestampMilliseconds() - account.CreatedAt.GetUnixTimestampMilliseconds()
            : account.SubscriptionStartedAt.HasValue
                ? DateTime.UtcNow.GetUnixTimestampMilliseconds() - account.SubscriptionStartedAt.Value.GetUnixTimestampMilliseconds()
                : 0;

        await connection.SendAsync(new IdentificationSuccessMessage
        {
            AccountId = account.Id,
            Login = account.Username,
            Nickname = account.Nickname,
            CommunityId = (sbyte)ServerCommunityIds.Francophone,
            SecretQuestion = account.SecretQuestion,
            WasAlreadyConnected = false,
            AccountCreation = accountCreation,
            SubscriptionEndDate = subscriptionEndDate.GetUnixTimestampMilliseconds(),
            HasRights = hasRights,
            SubscriptionElapsedDuration = subscriptionElapsedDuration
        });

        await _serverService.OnSuccessfullyAuthenticatedAsync(connection);
    }

    /// <summary>
    /// Sends an identification failure message asynchronously to the specified connection.
    /// </summary>
    /// <param name="connection">The connection to which the failure message will be sent.</param>
    /// <param name="reason">The reason for the identification failure.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private static ValueTask SendIdentificationFailedAsync(DofusConnection connection, IdentificationFailureReasons reason)
    {
        return connection.SendAsync(new IdentificationFailedMessage { Reason = (sbyte)reason });
    }

    /// <summary>
    /// Sends an identification failure message for a bad version asynchronously to the specified connection.
    /// </summary>
    /// <param name="connection">The connection to which the failure message will be sent.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private static ValueTask SendIdentificationFailedForBadVersionAsync(DofusConnection connection)
    {
        return connection.SendAsync(new IdentificationFailedForBadVersionMessage
        {
            Reason = (sbyte)IdentificationFailureReasons.BadVersion,
            RequiredVersion = BuildInfosConstants.BuildVersion
        });
    }
}
