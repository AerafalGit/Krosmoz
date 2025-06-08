// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Messages.Game.Character.Choice;
using Krosmoz.Protocol.Types.Game.Character.Choice;
using Krosmoz.Servers.GameServer.Database;
using Krosmoz.Servers.GameServer.Database.Models.Characters;
using Krosmoz.Servers.GameServer.Factories.Characters;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Experiences;
using Krosmoz.Servers.GameServer.Services.Servers;
using Krosmoz.Servers.GameServer.Services.Social;
using Krosmoz.Servers.GameServer.Services.World;
using Microsoft.EntityFrameworkCore;

namespace Krosmoz.Servers.GameServer.Services.Characters.Selection;

/// <summary>
/// Service responsible for handling character selection operations.
/// </summary>
public sealed class CharacterSelectionService : ICharacterSelectionService
{
    private readonly GameDbContext _dbContext;
    private readonly IServerService _serverService;
    private readonly IExperienceService _experienceService;
    private readonly ICharacterFactory _characterFactory;
    private readonly ISocialService _socialService;
    private readonly IWorldService _worldService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterSelectionService"/> class.
    /// </summary>
    /// <param name="dbContext">The database context for accessing character data.</param>
    /// <param name="serverService">The server service for managing server-related operations.</param>
    /// <param name="experienceService">The experience service for managing experience-related operations.</param>
    /// <param name="characterFactory">The factory for creating character instances.</param>
    /// <param name="socialService">The social service for managing social interactions.</param>
    /// <param name="worldService">The world service for managing world-related operations.</param>
    public CharacterSelectionService(
        GameDbContext dbContext,
        IServerService serverService,
        IExperienceService experienceService,
        ICharacterFactory characterFactory,
        ISocialService socialService,
        IWorldService worldService)
    {
        _dbContext = dbContext;
        _serverService = serverService;
        _experienceService = experienceService;
        _characterFactory = characterFactory;
        _socialService = socialService;
        _worldService = worldService;
    }

    /// <summary>
    /// Sends the list of characters to the client.
    /// </summary>
    /// <param name="connection">The connection to the client.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SendCharacterListAsync(DofusConnection connection)
    {
        var characters = await _dbContext
            .Characters
            .Where(static x => x.DeletedAt == null)
            .OrderByDescending(static x => x.UpdatedAt)
            .ToListAsync(connection.ConnectionClosed);

        if (characters.Count is 0)
        {
            await connection.SendAsync(new CharactersListMessage { Characters = [], HasStartupActions = false });
            return;
        }

        // TODO: search existing character in memory and handle reconnection in fight if needed

        await connection.SendAsync(new CharactersListWithRemodelingMessage
        {
            Characters = GetCharacterBaseInformations(characters),
            CharactersToRemodel = GetCharacterToRemodelInformations(characters),
            HasStartupActions = false // TODO: Startup Actions
        });
    }

    /// <summary>
    /// Handles the selection of a character by the client.
    /// </summary>
    /// <param name="connection">The connection to the client.</param>
    /// <param name="firstSelection">Indicates whether this is the first character selection.</param>
    /// <param name="doTutorial">Indicates whether the tutorial should be started.</param>
    /// <param name="characterId">The ID of the character to select.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SelectCharacterAsync(DofusConnection connection, bool firstSelection, bool doTutorial, int characterId)
    {
        var characterRecord = await _dbContext
            .Characters
            .FirstOrDefaultAsync(x => x.Id == characterId && x.DeletedAt == null, connection.ConnectionClosed);

        if (characterRecord is null || characterRecord.AccountId != connection.Heroes.Account.Id)
        {
            await SendCharacterSelectedErrorAsync(connection);
            return;
        }

        connection.Heroes.Master = !_worldService.TryGetValue(characterId, out var character)
                ? _characterFactory.CreateCharacter(connection, characterRecord)
                : character;

        await _socialService.LoadRelationsAsync(connection.Heroes.Master);

        await connection.SendAsync(new CharacterSelectedSuccessMessage
        {
            Infos = connection.Heroes.Master.GetCharacterBaseInformations(),
            IsCollectingStats = false
        });
    }

    /// <summary>
    /// Retrieves the base information of characters.
    /// </summary>
    /// <param name="characters">The list of character records.</param>
    /// <returns>A collection of character base information.</returns>
    private IEnumerable<CharacterBaseInformations> GetCharacterBaseInformations(IEnumerable<CharacterRecord> characters)
    {
        return _serverService.ServerGameType is ServerGameTypeIds.ServeurClassique or ServerGameTypeIds.CombatKolizeum or ServerGameTypeIds.ServeurTournoi
            ? characters.Select(character => new CharacterBaseInformations
            {
                Id = (uint)character.Id,
                Breed = (sbyte)character.Breed,
                Name = character.Name,
                Sex = character.Sex,
                EntityLook = character.Look.GetEntityLook(),
                Level = _experienceService.GetCharacterLevelByExperience(character.Experience)
            })
            : characters.Select(character => new CharacterHardcoreOrEpicInformations
            {
                Id = (uint)character.Id,
                Breed = (sbyte)character.Breed,
                Name = character.Name,
                Sex = character.Sex,
                EntityLook = character.Look.GetEntityLook(),
                Level = _experienceService.GetCharacterLevelByExperience(character.Experience),
                DeathCount = character.DeathCount,
                DeathMaxLevel = character.DeathMaxLevel,
                DeathState = (sbyte)character.DeathState
            });
    }

    /// <summary>
    /// Retrieves the remodeling information of characters.
    /// </summary>
    /// <param name="characters">The list of character records.</param>
    /// <returns>A collection of character remodeling information.</returns>
    private static IEnumerable<CharacterToRemodelInformations> GetCharacterToRemodelInformations(IEnumerable<CharacterRecord> characters)
    {
        return characters
            .Where(static character =>
                character.MandatoryChanges.HasFlag(CharacterRemodelings.CharacterRemodelingBreed) ||
                character.MandatoryChanges.HasFlag(CharacterRemodelings.CharacterRemodelingColors) ||
                character.MandatoryChanges.HasFlag(CharacterRemodelings.CharacterRemodelingCosmetic) ||
                character.MandatoryChanges.HasFlag(CharacterRemodelings.CharacterRemodelingGender) ||
                character.MandatoryChanges.HasFlag(CharacterRemodelings.CharacterRemodelingName))
            .Select(static character => new CharacterToRemodelInformations
            {
                Id = (uint)character.Id,
                Breed = (sbyte)character.Breed,
                Sex = character.Sex,
                Name = character.Name,
                Colors = character.Look.Colors.Values.Select(static x => x.ToArgb()),
                CosmeticId = character.Head,
                MandatoryChangeMask = (sbyte)character.MandatoryChanges,
                PossibleChangeMask = (sbyte)character.PossibleChanges
            });
    }

    /// <summary>
    /// Sends an error message indicating that character selection failed.
    /// </summary>
    /// <param name="connection">The connection to the client.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static ValueTask SendCharacterSelectedErrorAsync(DofusConnection connection)
    {
        return connection.SendAsync<CharacterSelectedErrorMessage>();
    }
}
