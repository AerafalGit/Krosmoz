// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Connection.Search;
using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.AuthServer.Network.Transport;
using Microsoft.EntityFrameworkCore;

namespace Krosmoz.Servers.AuthServer.Services.Acquaintance;

/// <summary>
/// Provides implementation for managing acquaintance-related operations.
/// </summary>
public sealed class AcquaintanceService : IAcquaintanceService
{
    private readonly AuthDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="AcquaintanceService"/> class.
    /// </summary>
    /// <param name="dbContext">The database context used for accessing account and character data.</param>
    public AcquaintanceService(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Sends the list of acquaintance servers asynchronously to the specified connection.
    /// </summary>
    /// <param name="connection">The Dofus connection to which the server list will be sent.</param>
    /// <param name="nickname">The nickname associated with the acquaintance request.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SendAcquaintanceServerListAsync(DofusConnection connection, string nickname)
    {
        var friendAccount = await _dbContext
            .Accounts
            .Include(static x => x.Characters)
            .FirstOrDefaultAsync(x => x.Nickname != null && x.Nickname.Equals(nickname), connection.ConnectionClosed);

        if (friendAccount is null)
        {
            await SendAcquaintanceSearchErrorAsync(connection, AcquaintanceSearchErrorReasons.NoResult);
            return;
        }

        if (friendAccount.Id == connection.Account.Id)
        {
            await SendAcquaintanceSearchErrorAsync(connection, AcquaintanceSearchErrorReasons.Unavailable);
            return;
        }

        await connection.SendAsync(new AcquaintanceServerListMessage { Servers = friendAccount.Characters.Select(static x => x.ServerId).Distinct() });
    }

    /// <summary>
    /// Sends an acquaintance search error message asynchronously to the specified connection.
    /// </summary>
    /// <param name="connection">The Dofus connection to which the error message will be sent.</param>
    /// <param name="reason">The reason for the acquaintance search error.</param>
    /// <returns>A value task that represents the asynchronous operation.</returns>
    private static ValueTask SendAcquaintanceSearchErrorAsync(DofusConnection connection, AcquaintanceSearchErrorReasons reason)
    {
        return connection.SendAsync(new AcquaintanceSearchErrorMessage { Reason = (sbyte)reason });
    }
}
