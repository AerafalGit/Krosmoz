// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
using Krosmoz.Protocol.Ipc;
using Krosmoz.Protocol.Ipc.Messages.Accounts;
using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.AuthServer.Database.Models.Accounts.Characters;
using Krosmoz.Servers.AuthServer.Database.Models.Accounts.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;

namespace Krosmoz.Servers.AuthServer.Services.Ipc.Accounts;

/// <summary>
/// Represents the IPC account service for managing account-related operations.
/// </summary>
public sealed class IpcAccountService : BackgroundService
{
    private readonly ILogger<IpcAccountService> _logger;
    private readonly INatsConnection _natsConnection;
    private readonly AuthDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="IpcAccountService"/> class.
    /// </summary>
    /// <param name="logger">The logger for logging IPC account service operations.</param>
    /// <param name="natsConnection">The NATS connection for IPC communication.</param>
    /// <param name="dbContext">The authentication database context.</param>
    public IpcAccountService(ILogger<IpcAccountService> logger, INatsConnection natsConnection, AuthDbContext dbContext)
    {
        _logger = logger;
        _natsConnection = natsConnection;
        _dbContext = dbContext;
    }

    /// <summary>
    /// Executes the background service tasks for handling IPC account operations.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        return Task.WhenAll(
        [
            AccountByTicket(cancellationToken),
            AccountById(cancellationToken),
            AccountByUsernameAndPassword(cancellationToken),
            AddCharacterToAccount(cancellationToken),
            RemoveCharacterFromAccount(cancellationToken),
            AddRelationToAccount(cancellationToken),
            RemoveRelationFromAccount(cancellationToken),
            UpdateRelationToAccount(cancellationToken)
        ]);
    }

    /// <summary>
    /// Handles requests to retrieve an account by its ticket.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    private async Task AccountByTicket(CancellationToken cancellationToken)
    {
        await foreach (var request in _natsConnection.SubscribeAsync<IpcAccountByTicketRequest>(IpcSubjects.AccountByTicket, cancellationToken: cancellationToken))
        {
            try
            {
                if (request.Data is null)
                {
                    await request.ReplyAsync(new IpcAccountByTicketResponse(), cancellationToken: cancellationToken);
                    continue;
                }

                var account = await _dbContext
                    .Accounts
                    .Include(static x => x.Characters.Where(static c => c.DeletedAt == null))
                    .Include(static x => x.Relations)
                    .Include(static x => x.Activity)
                    .FirstOrDefaultAsync(x => x.Ticket == request.Data.Ticket, cancellationToken);

                var response = account is null
                    ? new IpcAccountByTicketResponse()
                    : new IpcAccountByTicketResponse {Account = account.ToIpcAccount()};

                await request.ReplyAsync(response, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error processing account by ticket request: {Ticket}", request.Data?.Ticket);
            }
        }
    }

    /// <summary>
    /// Handles requests to retrieve an account by its id.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    private async Task AccountById(CancellationToken cancellationToken)
    {
        await foreach (var request in _natsConnection.SubscribeAsync<IpcAccountByIdRequest>(IpcSubjects.AccountById, cancellationToken: cancellationToken))
        {
            try
            {
                if (request.Data is null)
                {
                    await request.ReplyAsync(new IpcAccountByTicketResponse(), cancellationToken: cancellationToken);
                    continue;
                }

                var account = await _dbContext
                    .Accounts
                    .Include(static x => x.Characters.Where(static c => c.DeletedAt == null))
                    .Include(static x => x.Relations)
                    .Include(static x => x.Activity)
                    .FirstOrDefaultAsync(x => x.Id == request.Data.AccountId, cancellationToken);

                var response = account is null
                    ? new IpcAccountByIdResponse()
                    : new IpcAccountByIdResponse {Account = account.ToIpcAccount()};

                await request.ReplyAsync(response, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error processing account by id request: {AccountId}", request.Data?.AccountId);
            }
        }
    }

    /// <summary>
    /// Handles requests to retrieve an account by username and password.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    private async Task AccountByUsernameAndPassword(CancellationToken cancellationToken)
    {
        await foreach (var request in _natsConnection.SubscribeAsync<IpcAccountByUsernameAndPasswordRequest>(IpcSubjects.AccountByUsernameAndPassword, cancellationToken: cancellationToken))
        {
            try
            {
                if (request.Data is null)
                {
                    await request.ReplyAsync(new IpcAccountByUsernameAndPasswordResponse(), cancellationToken: cancellationToken);
                    continue;
                }

                var account = await _dbContext
                    .Accounts
                    .Include(static x => x.Characters.Where(static c => c.DeletedAt == null))
                    .Include(static x => x.Relations)
                    .Include(static x => x.Activity)
                    .FirstOrDefaultAsync(x => x.Username == request.Data.Username, cancellationToken);

                var response = account is null || !account.Password.Equals(request.Data.Password.ToMd5())
                    ? new IpcAccountByUsernameAndPasswordResponse()
                    : new IpcAccountByUsernameAndPasswordResponse { Account = account.ToIpcAccount() };

                await request.ReplyAsync(response, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error processing account by username and password request: {Username}", request.Data?.Username);
            }
        }

    }

    /// <summary>
    /// Handles requests to add a character to an account.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    private async Task AddCharacterToAccount(CancellationToken cancellationToken)
    {
        await foreach (var request in _natsConnection.SubscribeAsync<IpcAccountAddCharacterRequest>(IpcSubjects.AccountAddCharacter, cancellationToken: cancellationToken))
        {
            try
            {
                if (request.Data is null)
                {
                    await request.ReplyAsync(new IpcAccountAddCharacterResponse { Success = false }, cancellationToken: cancellationToken);
                    continue;
                }

                var account = await _dbContext
                    .Accounts
                    .Include(static x => x.Characters.Where(static c => c.DeletedAt == null))
                    .FirstOrDefaultAsync(x => x.Id == request.Data.AccountId, cancellationToken);

                IpcAccountAddCharacterResponse response;

                if (account is null || account.Characters.Any(x => x.CharacterId == request.Data.CharacterId && x.ServerId == request.Data.ServerId))
                    response = new IpcAccountAddCharacterResponse { Success = false };
                else
                {
                    account.Characters.Add(
                        new AccountCharacterRecord {AccountId = request.Data.AccountId, ServerId = request.Data.ServerId, CharacterId = request.Data.CharacterId});

                    _dbContext.Accounts.Update(account);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    response = new IpcAccountAddCharacterResponse { Success = true };
                }

                await request.ReplyAsync(response, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error processing account add character request: {AccountId}, {CharacterId}, {ServerId}", request.Data?.AccountId, request.Data?.CharacterId, request.Data?.ServerId);
            }
        }
    }

    /// <summary>
    /// Handles requests to remove a character from an account.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    private async Task RemoveCharacterFromAccount(CancellationToken cancellationToken)
    {
        await foreach (var request in _natsConnection.SubscribeAsync<IpcAccountRemoveCharacterRequest>(IpcSubjects.AccountRemoveCharacter, cancellationToken: cancellationToken))
        {
            try
            {
                if (request.Data is null)
                {
                    await request.ReplyAsync(new IpcAccountRemoveCharacterResponse { Success = false }, cancellationToken: cancellationToken);
                    continue;
                }

                var account = await _dbContext
                    .Accounts
                    .Include(static x => x.Characters.Where(static c => c.DeletedAt == null))
                    .FirstOrDefaultAsync(x => x.Id == request.Data.AccountId, cancellationToken);

                IpcAccountRemoveCharacterResponse response;

                if (account is null)
                    response = new IpcAccountRemoveCharacterResponse { Success = false };
                else
                {
                    var characterToRemove = account.Characters.FirstOrDefault(x => x.CharacterId == request.Data.CharacterId && x.ServerId == request.Data.ServerId);

                    if (characterToRemove is null)
                        response = new IpcAccountRemoveCharacterResponse { Success = false };
                    else
                    {
                        characterToRemove.DeletedAt = DateTime.UtcNow;
                        _dbContext.Accounts.Update(account);
                        await _dbContext.SaveChangesAsync(cancellationToken);

                        response = new IpcAccountRemoveCharacterResponse { Success = true };
                    }
                }

                await request.ReplyAsync(response, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error processing account remove character request: {AccountId}, {CharacterId}, {ServerId}", request.Data?.AccountId, request.Data?.CharacterId, request.Data?.ServerId);
            }
        }
    }

    /// <summary>
    /// Handles requests to add a relation to an account.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    private async Task AddRelationToAccount(CancellationToken cancellationToken)
    {
        await foreach (var request in _natsConnection.SubscribeAsync<IpcAccountAddRelationRequest>(IpcSubjects.AccountAddRelation, cancellationToken: cancellationToken))
        {
            try
            {
                if (request.Data is null)
                {
                    await request.ReplyAsync(new IpcAccountAddRelationResponse { Success = false }, cancellationToken: cancellationToken);
                    continue;
                }

                var account = await _dbContext
                    .Accounts
                    .Include(static x => x.Relations)
                    .FirstOrDefaultAsync(x => x.Id == request.Data.AccountId, cancellationToken);

                IpcAccountAddRelationResponse response;

                if (account is null || account.Relations.Any(x => x.ToAccountId == request.Data.TargetId))
                    response = new IpcAccountAddRelationResponse { Success = false };
                else
                {
                    account.Relations.Add(new AccountRelationRecord
                    {
                        FromAccountId = request.Data.AccountId, ToAccountId = request.Data.TargetId, RelationType = request.Data.RelationType, CreatedAt = DateTime.UtcNow
                    });

                    _dbContext.Accounts.Update(account);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    response = new IpcAccountAddRelationResponse { Success = true };
                }

                await request.ReplyAsync(response, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error processing account add relation request: {AccountId}, {TargetId}, {RelationType}", request.Data?.AccountId, request.Data?.TargetId, request.Data?.RelationType);
            }
        }
    }

    /// <summary>
    /// Handles requests to remove a relation from an account.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    private async Task RemoveRelationFromAccount(CancellationToken cancellationToken)
    {
        await foreach (var request in _natsConnection.SubscribeAsync<IpcAccountRemoveRelationRequest>(IpcSubjects.AccountRemoveRelation, cancellationToken: cancellationToken))
        {
            try
            {
                if (request.Data is null)
                {
                    await request.ReplyAsync(new IpcAccountRemoveRelationResponse { Success = false }, cancellationToken: cancellationToken);
                    continue;
                }

                var account = await _dbContext
                    .Accounts
                    .Include(static x => x.Relations)
                    .FirstOrDefaultAsync(x => x.Id == request.Data.AccountId, cancellationToken);

                IpcAccountRemoveRelationResponse response;

                if (account is null)
                    response = new IpcAccountRemoveRelationResponse { Success = false };
                else
                {
                    var relationToRemove = account.Relations.FirstOrDefault(x => x.ToAccountId == request.Data.TargetId);

                    if (relationToRemove is null)
                    {
                        response = new IpcAccountRemoveRelationResponse { Success = false };
                    }
                    else
                    {
                        account.Relations.Remove(relationToRemove);
                        _dbContext.AccountRelations.Remove(relationToRemove);
                        _dbContext.Accounts.Update(account);
                        await _dbContext.SaveChangesAsync(cancellationToken);

                        response = new IpcAccountRemoveRelationResponse { Success = true };
                    }
                }

                await request.ReplyAsync(response, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error processing account remove relation request: {AccountId}, {TargetId}", request.Data?.AccountId, request.Data?.TargetId);
            }
        }
    }

    /// <summary>
    /// Handles requests to update a relation in an account.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    private async Task UpdateRelationToAccount(CancellationToken cancellationToken)
    {
        await foreach (var request in _natsConnection.SubscribeAsync<IpcAccountUpdateRelationRequest>(IpcSubjects.AccountUpdateRelation, cancellationToken: cancellationToken))
        {
            try
            {
                if (request.Data is null)
                {
                    await request.ReplyAsync(new IpcAccountUpdateRelationResponse { Success = false }, cancellationToken: cancellationToken);
                    continue;
                }

                var account = await _dbContext
                    .Accounts
                    .Include(static x => x.Relations)
                    .FirstOrDefaultAsync(x => x.Id == request.Data.AccountId, cancellationToken);

                IpcAccountUpdateRelationResponse response;

                if (account is null)
                    response = new IpcAccountUpdateRelationResponse { Success = false };
                else
                {
                    var relationToUpdate = account.Relations.FirstOrDefault(x => x.ToAccountId == request.Data.TargetId);

                    if (relationToUpdate is null)
                    {
                        response = new IpcAccountUpdateRelationResponse { Success = false };
                    }
                    else
                    {
                        relationToUpdate.RelationType = request.Data.RelationType;
                        _dbContext.AccountRelations.Update(relationToUpdate);
                        _dbContext.Accounts.Update(account);
                        await _dbContext.SaveChangesAsync(cancellationToken);

                        response = new IpcAccountUpdateRelationResponse { Success = true };
                    }
                }

                await request.ReplyAsync(response, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error processing account update relation request: {AccountId}, {TargetId}, {RelationType}", request.Data?.AccountId, request.Data?.TargetId, request.Data?.RelationType);
            }
        }
    }
}
