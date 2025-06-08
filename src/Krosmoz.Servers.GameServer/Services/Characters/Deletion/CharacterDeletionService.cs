// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Ipc;
using Krosmoz.Protocol.Ipc.Messages.Accounts;
using Krosmoz.Protocol.Messages.Game.Character.Deletion;
using Krosmoz.Servers.GameServer.Database;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Characters.Selection;
using Microsoft.EntityFrameworkCore;
using NATS.Client.Core;

namespace Krosmoz.Servers.GameServer.Services.Characters.Deletion;

/// <summary>
/// Service responsible for character deletion operations.
/// Implements <see cref="ICharacterDeletionService"/>.
/// </summary>
public sealed class CharacterDeletionService : ICharacterDeletionService
{
    /// <summary>
    /// Maximum experience threshold to delete a character without requiring a secret question.
    /// </summary>
    private const long MaxExperienceToDeleteCharacterWithoutSecretQuestion = 202_000;

    private readonly GameDbContext _dbContext;
    private readonly INatsConnection _natsConnection;
    private readonly ICharacterSelectionService _characterSelectionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterDeletionService"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="natsConnection">The NATS connection.</param>
    /// <param name="characterSelectionService">The character selection service.</param>
    public CharacterDeletionService(GameDbContext dbContext, INatsConnection natsConnection, ICharacterSelectionService characterSelectionService)
    {
        _dbContext = dbContext;
        _natsConnection = natsConnection;
        _characterSelectionService = characterSelectionService;
    }

    /// <summary>
    /// Deletes a character asynchronously based on the provided parameters.
    /// </summary>
    /// <param name="connection">The connection associated with the character deletion.</param>
    /// <param name="characterId">The ID of the character to delete.</param>
    /// <param name="secretAnswerHash">The hash of the secret answer used for verification.</param>
    public async Task DeleteCharacterAsync(DofusConnection connection, int characterId, string secretAnswerHash)
    {
        var characterRecord = await _dbContext
            .Characters
            .FirstOrDefaultAsync(x => x.Id == characterId, connection.ConnectionClosed);

        if (characterRecord is null)
        {
            await SendCharacterDeletionErrorAsync(connection, CharacterDeletionErrors.DelErrNoReason);
            return;
        }

        var characterToRemove = connection.Heroes.Account.Characters.FirstOrDefault(c => c.CharacterId == characterId);

        if (characterToRemove is null)
        {
            await SendCharacterDeletionErrorAsync(connection, CharacterDeletionErrors.DelErrNoReason);
            return;
        }

        if (characterRecord.Experience >= MaxExperienceToDeleteCharacterWithoutSecretQuestion && !string.Equals(secretAnswerHash, $"{characterId}~{connection.Heroes.Account.SecretAnswer}".ToMd5()))
        {
            await SendCharacterDeletionErrorAsync(connection, CharacterDeletionErrors.DelErrBadSecretAnswer);
            return;
        }

        var removeCharacterRequest = new IpcAccountRemoveCharacterRequest
        {
            AccountId = connection.Heroes.Account.Id,
            CharacterId = characterToRemove.CharacterId,
            ServerId = characterToRemove.ServerId
        };

        var removeCharacterResponse = await _natsConnection.RequestAsync<IpcAccountRemoveCharacterRequest, IpcAccountRemoveCharacterResponse>(
            IpcSubjects.AccountRemoveCharacter,
            removeCharacterRequest,
            cancellationToken: connection.ConnectionClosed);

        if (removeCharacterResponse.Data?.Success ?? false)
        {
            await SendCharacterDeletionErrorAsync(connection, CharacterDeletionErrors.DelErrNoReason);
            return;
        }

        connection.Heroes.Account.Characters.Remove(characterToRemove);

        characterRecord.DeletedAt = DateTime.UtcNow;

        _dbContext.Characters.Update(characterRecord);

        await _dbContext.SaveChangesAsync(connection.ConnectionClosed);

        await _characterSelectionService.SendCharacterListAsync(connection);
    }

    /// <summary>
    /// Sends a character deletion error message to the specified connection asynchronously.
    /// </summary>
    /// <param name="connection">The connection to send the error message to.</param>
    /// <param name="error">The error reason for the character deletion failure.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private static ValueTask SendCharacterDeletionErrorAsync(DofusConnection connection, CharacterDeletionErrors error)
    {
        return connection.SendAsync(new CharacterDeletionErrorMessage { Reason = (sbyte)error });
    }
}
