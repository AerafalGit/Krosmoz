// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Dispatcher.Attributes;
using Krosmoz.Protocol.Messages.Game.Character.Deletion;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Characters.Deletion;

namespace Krosmoz.Servers.GameServer.Network.Handlers.Characters.Deletion;

/// <summary>
/// Handles character deletion-related operations.
/// </summary>
public sealed class CharacterDeletionHandler
{
    private readonly ICharacterDeletionService _characterDeletionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterDeletionHandler"/> class.
    /// </summary>
    /// <param name="characterDeletionService">The service used for character deletion operations.</param>
    public CharacterDeletionHandler(ICharacterDeletionService characterDeletionService)
    {
        _characterDeletionService = characterDeletionService;
    }

    /// <summary>
    /// Handles a character deletion request asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client making the request.</param>
    /// <param name="message">The message containing the character deletion details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleCharacterDeletionRequestAsync(DofusConnection connection, CharacterDeletionRequestMessage message)
    {
        return _characterDeletionService.DeleteCharacterAsync(connection, message.CharacterId, message.SecretAnswerHash);
    }
}
