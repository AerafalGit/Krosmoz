// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Dispatcher.Attributes;
using Krosmoz.Protocol.Messages.Game.Character.Choice;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Characters.Selection;

namespace Krosmoz.Servers.GameServer.Network.Handlers.Characters.Selection;

/// <summary>
/// Handles character selection-related operations.
/// </summary>
public sealed class CharacterSelectionHandler
{
    private readonly ICharacterSelectionService _characterSelectionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterSelectionHandler"/> class.
    /// </summary>
    /// <param name="characterSelectionService">The service used for character selection operations.</param>
    public CharacterSelectionHandler(ICharacterSelectionService characterSelectionService)
    {
        _characterSelectionService = characterSelectionService;
    }

    /// <summary>
    /// Handles a request to retrieve the list of characters asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client making the request.</param>
    /// <param name="message">The message containing the request for the character list.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleCharactersListRequestAsync(DofusConnection connection, CharactersListRequestMessage message)
    {
        return _characterSelectionService.SendCharacterListAsync(connection);
    }

    /// <summary>
    /// Handles the first character selection request asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client making the request.</param>
    /// <param name="message">The message containing the first character selection details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleCharacterFirstSelectionAsync(DofusConnection connection, CharacterFirstSelectionMessage message)
    {
        return _characterSelectionService.SelectCharacterAsync(connection, true, message.DoTutorial, message.Id);
    }

    /// <summary>
    /// Handles a character selection request asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client making the request.</param>
    /// <param name="message">The message containing the character selection details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleCharacterSelectionAsync(DofusConnection connection, CharacterSelectionMessage message)
    {
        return _characterSelectionService.SelectCharacterAsync(connection, false, false, message.Id);
    }
}
