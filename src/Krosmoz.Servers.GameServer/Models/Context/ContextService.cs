// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Game.Context;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;
using Krosmoz.Servers.GameServer.Services.Characters.Loading;

namespace Krosmoz.Servers.GameServer.Models.Context;

/// <summary>
/// Provides services for managing game contexts for character.
/// </summary>
public sealed class ContextService : IContextService
{
    private readonly ICharacterLoadingService _characterLoadingService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContextService"/> class.
    /// </summary>
    /// <param name="characterLoadingService">The service used to load character data.</param>
    public ContextService(ICharacterLoadingService characterLoadingService)
    {
        _characterLoadingService = characterLoadingService;
    }

    /// <summary>
    /// Asynchronously creates a game context for the specified character with the given player state.
    /// </summary>
    /// <param name="character">The character for whom the game context is to be created.</param>
    /// <param name="state">The state of the player in the game context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task CreateContextAsync(CharacterActor character, PlayerStates state)
    {
        character.State = state;

        await SendGameContextDestroyAsync(character);
        await SendGameContextCreateAsync(character, state);

        // TODO: implements end fight context

        if (!character.IsLoaded)
        {
            character.IsLoaded = true;
            await _characterLoadingService.LoadCharacterAsync(character);
        }
    }

    /// <summary>
    /// Asynchronously sends a message to create a game context for the specified character.
    /// </summary>
    /// <param name="character">The character for whom the game context is to be created.</param>
    /// <param name="state">The state of the player in the game context.</param>
    /// <returns>A value task representing the asynchronous operation.</returns>
    private static ValueTask SendGameContextCreateAsync(CharacterActor character, PlayerStates state)
    {
        return character.Connection.SendAsync(new GameContextCreateMessage
        {
            Context = (sbyte)state
        });
    }

    /// <summary>
    /// Asynchronously sends a message to destroy the current game context for the specified character.
    /// </summary>
    /// <param name="character">The character for whom the game context is to be destroyed.</param>
    /// <returns>A value task representing the asynchronous operation.</returns>
    private static ValueTask SendGameContextDestroyAsync(CharacterActor character)
    {
        return character.Connection.SendAsync<GameContextDestroyMessage>();
    }
}
