// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Dispatcher.Attributes;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Messages.Game.Character.Creation;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Characters.Creation;

namespace Krosmoz.Servers.GameServer.Network.Handlers.Characters;

/// <summary>
/// Handles character creation-related operations.
/// </summary>
public sealed class CharacterCreationHandler
{
    private readonly ICharacterCreationService _characterCreationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterCreationHandler"/> class.
    /// </summary>
    /// <param name="characterCreationService">The service used for character creation operations.</param>
    public CharacterCreationHandler(ICharacterCreationService characterCreationService)
    {
        _characterCreationService = characterCreationService;
    }

    /// <summary>
    /// Handles a request for a character name suggestion asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client making the request.</param>
    /// <param name="_">The message containing the request for a name suggestion.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public async Task HandleCharacterNameSuggestionRequestAsync(DofusConnection connection, CharacterNameSuggestionRequestMessage _)
    {
        await _characterCreationService.SendCharacterNameSuggestionAsync(connection);
    }

    /// <summary>
    /// Handles a character creation request asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client making the request.</param>
    /// <param name="message">The message containing the character creation details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleCharacterCreationRequestAsync(DofusConnection connection, CharacterCreationRequestMessage message)
    {
        return _characterCreationService.CreateCharacterAsync(
            connection,
            message.Name,
            (BreedIds)message.Breed,
            message.Sex,
            message.Colors.ToList(),
            message.CosmeticId
        );
    }
}
