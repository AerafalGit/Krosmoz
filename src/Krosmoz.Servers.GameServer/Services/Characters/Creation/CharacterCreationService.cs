// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Text.RegularExpressions;
using Krosmoz.Protocol.Constants;
using Krosmoz.Protocol.Datacenter.Breeds;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Ipc;
using Krosmoz.Protocol.Ipc.Messages.Accounts;
using Krosmoz.Protocol.Ipc.Types.Accounts;
using Krosmoz.Protocol.Messages.Game.Character.Creation;
using Krosmoz.Servers.GameServer.Database;
using Krosmoz.Servers.GameServer.Factories.Characters;
using Krosmoz.Servers.GameServer.Models.Appearances;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Breeds;
using Krosmoz.Servers.GameServer.Services.Characters.Creation.NameGeneration;
using Krosmoz.Servers.GameServer.Services.Characters.Selection;
using Krosmoz.Servers.GameServer.Services.Servers;
using Microsoft.EntityFrameworkCore;
using NATS.Client.Core;

namespace Krosmoz.Servers.GameServer.Services.Characters.Creation;

/// <summary>
/// Service responsible for character creation operations.
/// Implements <see cref="ICharacterCreationService"/>.
/// </summary>
public sealed partial class CharacterCreationService : ICharacterCreationService
{
    private readonly GameDbContext _dbContext;
    private readonly INatsConnection _natsConnection;
    private readonly ICharacterNameGenerationService _characterNameGenerationService;
    private readonly ICharacterSelectionService _characterSelectionService;
    private readonly IBreedService _breedService;
    private readonly IServerService _serverService;
    private readonly ICharacterFactory _characterFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterCreationService"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="natsConnection">The NATS connection.</param>
    /// <param name="characterNameGenerationService">The character name generation service.</param>
    /// <param name="characterSelectionService">The character selection service.</param>
    /// <param name="breedService">The breed service.</param>
    /// <param name="serverService">The server service.</param>
    /// <param name="characterFactory">The character factory.</param>
    public CharacterCreationService(
        GameDbContext dbContext,
        INatsConnection natsConnection,
        ICharacterNameGenerationService characterNameGenerationService,
        ICharacterSelectionService characterSelectionService,
        IBreedService breedService,
        IServerService serverService,
        ICharacterFactory characterFactory)
    {
        _dbContext = dbContext;
        _natsConnection = natsConnection;
        _characterNameGenerationService = characterNameGenerationService;
        _characterSelectionService = characterSelectionService;
        _breedService = breedService;
        _serverService = serverService;
        _characterFactory = characterFactory;
    }

    /// <summary>
    /// Sends a character name suggestion to the specified connection asynchronously.
    /// </summary>
    /// <param name="connection">The connection to send the suggestion to.</param>
    public ValueTask SendCharacterNameSuggestionAsync(DofusConnection connection)
    {
        return connection.SendAsync(new CharacterNameSuggestionSuccessMessage { Suggestion = _characterNameGenerationService.GenerateName() });
    }

    /// <summary>
    /// Creates a new character asynchronously based on the provided parameters.
    /// </summary>
    /// <param name="connection">The connection associated with the character creation.</param>
    /// <param name="name">The name of the character.</param>
    /// <param name="breedId">The breed ID of the character.</param>
    /// <param name="sex">The gender of the character.</param>
    /// <param name="colors">The colors representing the character's appearance.</param>
    /// <param name="cosmeticId">The cosmetic ID for the character's head.</param>
    public async Task CreateCharacterAsync(DofusConnection connection, string name, BreedIds breedId, bool sex, IList<int> colors, ushort cosmeticId)
    {
        if (!CanCreateNewCharacter(connection.Heroes.Account))
        {
            await SendCharacterCreationResultAsync(connection, CharacterCreationResults.ErrTooManyCharacters);
            return;
        }

        if (!CanRetrieveBreed(breedId, out var breed))
        {
            await SendCharacterCreationResultAsync(connection, CharacterCreationResults.ErrNotAllowed);
            return;
        }

        if (!CanRetrieveHead(cosmeticId, out var head))
        {
            await SendCharacterCreationResultAsync(connection, CharacterCreationResults.ErrNotAllowed);
            return;
        }

        if (!CanUseHead(head, breedId, sex))
        {
            await SendCharacterCreationResultAsync(connection, CharacterCreationResults.ErrNotAllowed);
            return;
        }

        if (!CanUseName(name))
        {
            await SendCharacterCreationResultAsync(connection, CharacterCreationResults.ErrInvalidName);
            return;
        }

        var characterWithSameNameExists = await _dbContext
            .Characters
            .AsNoTracking()
            .AnyAsync(x => x.Name.Equals(name), connection.ConnectionClosed);

        if (characterWithSameNameExists)
        {
            await SendCharacterCreationResultAsync(connection, CharacterCreationResults.ErrNameAlreadyExists);
            return;
        }

        var actorLook = GetActorLook(sex, breed, head, colors);

        var characterRecord = _characterFactory.CreateCharacterRecord(connection.Heroes.Account, name, breed, head, sex, actorLook);

        _dbContext.Characters.Add(characterRecord);
        await _dbContext.SaveChangesAsync(connection.ConnectionClosed);

        var createCharacterRequest = new IpcAccountAddCharacterRequest
        {
            AccountId = connection.Heroes.Account.Id,
            ServerId = _serverService.ServerId,
            CharacterId = characterRecord.Id
        };

        var createCharacterResponse = await _natsConnection.RequestAsync<IpcAccountAddCharacterRequest, IpcAccountAddCharacterResponse>(
            IpcSubjects.AccountAddCharacter,
            createCharacterRequest,
            cancellationToken: connection.ConnectionClosed);

        if (!createCharacterResponse.Data?.Success ?? false)
        {
            _dbContext.Characters.Remove(characterRecord);
            await _dbContext.SaveChangesAsync(connection.ConnectionClosed);
            await SendCharacterCreationResultAsync(connection, CharacterCreationResults.ErrNotAllowed);
            return;
        }

        connection.Heroes.Account.Characters.Add(new IpcAccountCharacter
        {
            ServerId = _serverService.ServerId,
            CharacterId = characterRecord.Id
        });

        await SendCharacterCreationResultAsync(connection, CharacterCreationResults.Ok);

        await _characterSelectionService.SendCharacterListAsync(connection);
    }

    /// <summary>
    /// Determines whether a new character can be created for the specified connection.
    /// </summary>
    /// <param name="account">The account associated with the connection.</param>
    /// <returns>True if a new character can be created; otherwise, false.</returns>
    private bool CanCreateNewCharacter(IpcAccount account)
    {
        return account.Characters.Count(x => x.ServerId == _serverService.ServerId) < ProtocolConstants.MaxMembersPerParty;
    }

    /// <summary>
    /// Attempts to retrieve breed data for the specified breed ID.
    /// </summary>
    /// <param name="breedId">The ID of the breed to retrieve.</param>
    /// <param name="breed">
    /// When this method returns, contains the breed data if the breed ID exists; otherwise, null.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns>True if the breed data was successfully retrieved; otherwise, false.</returns>
    private bool CanRetrieveBreed(BreedIds breedId, [NotNullWhen(true)] out Breed? breed)
    {
        return _breedService.TryGetBreed(breedId, out breed);
    }

    /// <summary>
    /// Attempts to retrieve head data for the specified cosmetic ID.
    /// </summary>
    /// <param name="cosmeticId">The ID of the cosmetic to retrieve the head data for.</param>
    /// <param name="head">
    /// When this method returns, contains the head data if the cosmetic ID exists; otherwise, null.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns>True if the head data was successfully retrieved; otherwise, false.</returns>
    private bool CanRetrieveHead(int cosmeticId, [NotNullWhen(true)] out Head? head)
    {
        return _breedService.TryGetHead(cosmeticId, out head);
    }

    /// <summary>
    /// Determines whether the specified head can be used with the given breed and gender.
    /// </summary>
    /// <param name="head">The head to check.</param>
    /// <param name="breedId">The breed ID to check against.</param>
    /// <param name="sex">The sex to check against.</param>
    /// <returns>True if the head can be used; otherwise, false.</returns>
    private static bool CanUseHead(Head head, BreedIds breedId, bool sex)
    {
        return head.Gender == (sex ? 1 : 0) && head.Breed == (int)breedId;
    }

    /// <summary>
    /// Determines whether the specified name is valid for a character.
    /// </summary>
    /// <param name="name">The name to validate.</param>
    /// <returns>True if the name is valid; otherwise, false.</returns>
    private static bool CanUseName(string name)
    {
        return NameRegex().IsMatch(name);
    }

    /// <summary>
    /// Constructs the visual appearance of a character based on the specified attributes.
    /// </summary>
    /// <param name="sex">The sex of the character.</param>
    /// <param name="breed">The breed of the character.</param>
    /// <param name="head">The head appearance of the character.</param>
    /// <param name="colors">An array of colors representing the character's appearance.</param>
    /// <returns>An <see cref="ActorLook"/> object representing the character's appearance.</returns>
    private static ActorLook GetActorLook(bool sex, Breed breed, Head head, IList<int> colors)
    {
        var lookStr = sex ? breed.FemaleLook : breed.MaleLook;
        var defaultColors = sex ? breed.FemaleColors : breed.MaleColors;

        var look = ActorLook.Parse(lookStr);

        for (var i = 0; i < colors.Count; i++)
        {
            if (defaultColors.Count <= i)
                continue;

            var color = colors[i];

            look.AddColor(i + 1, color is -1 ? Color.FromArgb((int)defaultColors[i]) : Color.FromArgb(color));
        }

        foreach (var skin in head.Skins.Split(','))
            look.AddSkin(ushort.Parse(skin));

        return look;
    }

    /// <summary>
    /// Sends the result of a character creation operation to the specified game connection asynchronously.
    /// </summary>
    /// <param name="connection">The game connection to send the result to.</param>
    /// <param name="result">The result of the character creation operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private static ValueTask SendCharacterCreationResultAsync(DofusConnection connection, CharacterCreationResults result)
    {
        return connection.SendAsync(new CharacterCreationResultMessage { Result = (sbyte)result });
    }

    /// <summary>
    /// Gets the regular expression used to validate character names.
    /// </summary>
    /// <returns>A <see cref="Regex"/> object for validating character names.</returns>
    [GeneratedRegex("^[A-Z][a-z]{2,9}(?:-[A-Za-z]{2,9}|[a-zA-Z]{1,16})$", RegexOptions.Multiline)]
    private static partial Regex NameRegex();
}
