// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Game.Almanach;
using Krosmoz.Protocol.Messages.Game.Basic;
using Krosmoz.Protocol.Messages.Game.Character.Stats;
using Krosmoz.Protocol.Messages.Game.Character.Status;
using Krosmoz.Protocol.Messages.Game.Context.Roleplay.Emote;
using Krosmoz.Protocol.Messages.Game.Initialization;
using Krosmoz.Protocol.Messages.Game.Tinsel;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;
using Krosmoz.Servers.GameServer.Services.Characteristics;
using Krosmoz.Servers.GameServer.Services.Commands;
using Krosmoz.Servers.GameServer.Services.InfoMessages;
using Krosmoz.Servers.GameServer.Services.Inventory;
using Krosmoz.Servers.GameServer.Services.Maps;
using Krosmoz.Servers.GameServer.Services.Shortcuts;
using Krosmoz.Servers.GameServer.Services.Spells;

namespace Krosmoz.Servers.GameServer.Services.Characters.Loading;

/// <summary>
/// Represents the service responsible for loading character data and initializing their state in the game.
/// </summary>
public sealed class CharacterLoadingService : ICharacterLoadingService
{
    // TODO: Implement experience rate and other game settings
    private const int ExperienceRate = 1;

    private readonly IInventoryService _inventoryService;
    private readonly ISpellService _spellService;
    private readonly ICharacteristicService _characteristicService;
    private readonly IMapService _mapService;
    private readonly IInfoMessageService _infoMessageService;
    private readonly IShortcutService _shortcutService;
    private readonly ICommandService _commandService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterLoadingService"/> class with the specified services.
    /// </summary>
    /// <param name="inventoryService">The service responsible for managing inventory-related operations.</param>
    /// <param name="spellService">The service responsible for managing spell-related operations.</param>
    /// <param name="characteristicService">The service responsible for managing character characteristics.</param>
    /// <param name="mapService">The service responsible for managing map-related operations.</param>
    /// <param name="infoMessageService">The service responsible for sending informational messages to the client.</param>
    /// <param name="shortcutService">The service responsible for managing shortcuts.</param>
    /// <param name="commandService">The service responsible for managing commands.</param>
    public CharacterLoadingService(
        IInventoryService inventoryService,
        ISpellService spellService,
        ICharacteristicService characteristicService,
        IMapService mapService,
        IInfoMessageService infoMessageService,
        IShortcutService shortcutService,
        ICommandService commandService)
    {
        _inventoryService = inventoryService;
        _spellService = spellService;
        _characteristicService = characteristicService;
        _mapService = mapService;
        _infoMessageService = infoMessageService;
        _shortcutService = shortcutService;
        _commandService = commandService;
    }

    /// <summary>
    /// Loads the specified character asynchronously, initializing their state and sending relevant data to the client.
    /// </summary>
    /// <param name="character">The character to be loaded.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task LoadCharacterAsync(CharacterActor character)
    {
        character.ConnectedAt = DateTime.UtcNow;
        character.ExperiencePercent = ExperienceRate * 100;

        var tasks = new[]
        {
            // TODO: Handle connection, dungeon key check, and other connection-related logic
            _inventoryService.SendInventoryContentAsync(character).AsTask(),
            _inventoryService.SendInventoryWeightAsync(character).AsTask(),
            _shortcutService.SendShortcutBarContentAsync(character, ShortcutBars.GeneralShortcutBar).AsTask(),
            _shortcutService.SendShortcutBarContentAsync(character, ShortcutBars.SpellShortcutBar).AsTask(),
            character.Connection.SendAsync(new CharacterCapabilitiesMessage { GuildEmblemSymbolCategories = 4095 }).AsTask(),
            _spellService.SendCharacterSpellListAsync(character).AsTask(),
            character.Connection.SendAsync(new TitlesAndOrnamentsListMessage { ActiveOrnament = 0, ActiveTitle = 0, Ornaments = [], Titles = [] }).AsTask(),
            character.Connection.SendAsync(new EmoteListMessage { EmoteIds = character.Record.Emotes.Select(static emote => (byte)emote) }).AsTask(),
            character.Connection.SendAsync(new ServerExperienceModificatorMessage { ExperiencePercent = character.ExperiencePercent }).AsTask(),
            character.Connection.SendAsync<SequenceNumberRequestMessage>().AsTask(),
            character.Connection.SendAsync(new AlmanachCalendarDateMessage { Date = 1 }).AsTask(),
            // TODO: Job messages,
            // TODO: Guild messages
            SendConnectionMessagesAsync(character),
            _commandService.SendConsoleCommandsListAsync(character.Connection).AsTask(),
            _characteristicService.SendCharacterCharacteristicsAsync(character).AsTask(),
            SendOnlineFriendsAsync(character),
            SendPlayerStatusUpdateAsync(character)
            // TODO: Send player status update
            // TODO: Send bidhouses messages
            // TODO: Send presets lists
            // TODO: ListMapNpcsQuestStatusUpdate
        };

        await Task.WhenAll(tasks);

        // TODO: rejoin party if applicable

        await character.Connection.SendAsync<CharacterLoadingCompleteMessage>();

        // TODO: Send followed quests
        // TODO: Send followed achievements

        // TODO: reconnect fight if applicable

        // TODO: mount riding

        await character.Connection.SendAsync(new LifePointsRegenBeginMessage { RegenRate = 4 });

        await _mapService.AddActorAsync(character);
        await _mapService.SendMapInformationsAsync(character, character.Map.Id);

        // TODO: refresh mount
    }

    /// <summary>
    /// Sends a message to the character about the number of online friends asynchronously.
    /// </summary>
    /// <param name="character">The character to whom the message will be sent.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task SendOnlineFriendsAsync(CharacterActor character)
    {
        var onlineFriends = character.Friends.Count(static x => x.Character is not null);

        if (onlineFriends > 0)
            await _infoMessageService.SendTextInformationAsync(character, TextInformationTypes.TextInformationMessage, 197, onlineFriends.ToString(CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// Sends a player status update message asynchronously if the character's status is not available.
    /// </summary>
    /// <param name="character">The character whose status is to be updated.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static async Task SendPlayerStatusUpdateAsync(CharacterActor character)
    {
        if (character.Status > PlayerStatuses.PlayerStatusAvailable)
            await character.Connection.SendAsync(new PlayerStatusUpdateMessage
            {
                AccountId = character.Account.Id,
                PlayerId = (uint)character.Id,
                Status = character.GetPlayerStatus()
            });
    }

    private async Task SendConnectionMessagesAsync(CharacterActor character)
    {
        // Bienvenue dans DOFUS !
        // Pour ne pas te faire voler ton compte, n'entre jamais tes identifiants ailleurs que sur le Launcher Ankama, la page de connexion du jeu DOFUS ou le site https://www.dofus.com.
        // <u><a href="https://www.dofus.com/fr/mmorpg/communaute/phishing" target="_blank">En savoir plus.</a></u>
        await _infoMessageService.SendTextInformationAsync(character, TextInformationTypes.TextInformationError, 89);

        // TODO: If the IP address is different, we send the current ip of the player
    }
}
