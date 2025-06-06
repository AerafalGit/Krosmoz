// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Connection;
using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.AuthServer.Database.Models.Accounts;
using Krosmoz.Servers.AuthServer.Network.Transport;
using Microsoft.EntityFrameworkCore;

namespace Krosmoz.Servers.AuthServer.Services.Banishments;

/// <summary>
/// Provides functionality to check and handle account banishments.
/// </summary>
public sealed class BanishmentService : IBanishmentService
{
    private readonly AuthDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="BanishmentService"/> class.
    /// </summary>
    /// <param name="dbContext">The database context used to access banishment data.</param>
    public BanishmentService(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Checks if the specified account is banned and handles the banishment logic.
    /// </summary>
    /// <param name="connection">The authentication connection associated with the account.</param>
    /// <param name="account">The account record to check for banishment.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains
    /// <c>true</c> if the account is banned; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> IsAccountBannedAsync(DofusConnection connection, AccountRecord account, CancellationToken cancellationToken)
    {
        var banishment = await _dbContext.Banishments.FirstOrDefaultAsync(x =>
            x.AccountId == account.Id ||
            x.IpAddress == account.IpAddress ||
            x.MacAddress == account.MacAddress, cancellationToken);

        switch (banishment)
        {
            case null:
                return false;
            case { AccountId: not null, ExpiredAt: not null } when banishment.ExpiredAt.Value < DateTime.UtcNow:
                _dbContext.Banishments.Remove(banishment);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return false;
            case { ExpiredAt: not null, AccountId: not null }:
                await SendIdentificationFailedBannedAsync(connection, banishment.ExpiredAt.Value.GetUnixTimestampMilliseconds());
                return true;
            case { MacAddress: not null } or { IpAddress: not null }:
                await SendIdentificationFailedAsync(connection);
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// Sends a message to the connection indicating the account is banned with an expiration date.
    /// </summary>
    /// <param name="connection">The authentication connection to send the message to.</param>
    /// <param name="banEndDate">The timestamp indicating when the ban ends.</param>
    /// <returns>A task representing the asynchronous send operation.</returns>
    private static ValueTask SendIdentificationFailedBannedAsync(DofusConnection connection, double banEndDate)
    {
        return connection.SendAsync(new IdentificationFailedBannedMessage
        {
            BanEndDate = banEndDate,
            Reason = (sbyte)IdentificationFailureReasons.Banned
        });
    }

    /// <summary>
    /// Sends a message to the connection indicating the account is banned without an expiration date.
    /// </summary>
    /// <param name="connection">The authentication connection to send the message to.</param>
    /// <returns>A task representing the asynchronous send operation.</returns>
    private static ValueTask SendIdentificationFailedAsync(DofusConnection connection)
    {
        return connection.SendAsync(new IdentificationFailedMessage
        {
            Reason = (sbyte)IdentificationFailureReasons.Banned
        });
    }
}
