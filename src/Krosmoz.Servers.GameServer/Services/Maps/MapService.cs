// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using Krosmoz.Core.Services;
using Krosmoz.Protocol.Datacenter.World;
using Krosmoz.Protocol.Messages.Game.Context;
using Krosmoz.Protocol.Messages.Game.Context.Roleplay;
using Krosmoz.Servers.GameServer.Database;
using Krosmoz.Servers.GameServer.Models.Actors;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;
using Krosmoz.Servers.GameServer.Models.World.Maps;
using Krosmoz.Servers.GameServer.Services.Datacenter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Krosmoz.Servers.GameServer.Services.Maps;

/// <summary>
/// Represents the map service responsible for managing maps, actors, and related operations.
/// </summary>
public sealed class MapService : IMapService, IAsyncInitializableService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IDatacenterService _datacenterService;

    private FrozenDictionary<int, Area> _areas;
    private FrozenDictionary<int, SubArea> _subAreas;
    private FrozenDictionary<int, Map> _maps;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapService"/> class.
    /// </summary>
    /// <param name="serviceScopeFactory">The factory for creating service scopes.</param>
    /// <param name="datacenterService">The service for accessing datacenter objects.</param>
    public MapService(IServiceScopeFactory serviceScopeFactory, IDatacenterService datacenterService)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _datacenterService = datacenterService;
        _areas = FrozenDictionary<int, Area>.Empty;
        _subAreas = FrozenDictionary<int, SubArea>.Empty;
        _maps = FrozenDictionary<int, Map>.Empty;
    }

    /// <summary>
    /// Attempts to retrieve a map by its identifier.
    /// </summary>
    /// <param name="mapId">The unique identifier of the map.</param>
    /// <param name="map">
    /// When this method returns, contains the map associated with the specified identifier,
    /// if the map is found; otherwise, null. This parameter is passed uninitialized.
    /// </param>
    /// <returns>True if the map is found; otherwise, false.</returns>
    public bool TryGetMap(int mapId, [NotNullWhen(true)] out Map? map)
    {
        return _maps.TryGetValue(mapId, out map);
    }

    /// <summary>
    /// Adds an actor to the map asynchronously.
    /// </summary>
    /// <param name="actor">The actor to add to the map.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task AddActorAsync(Actor actor)
    {
        return AddActorAsync(actor, actor.Map);
    }

    /// <summary>
    /// Adds an actor to a specific map asynchronously.
    /// </summary>
    /// <param name="actor">The actor to add to the map.</param>
    /// <param name="map">The map to which the actor will be added.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task AddActorAsync(Actor actor, Map map)
    {
        map.AddActor(actor);

        if (actor is CharacterActor characterActor)
        {
            await SendCurrentMapAsync(characterActor, map);

            if (map.ContainsZaap)
            {
                // TODO: Discover the Zaap
            }

            // TODO: Send current position to party followers
        }

        await RefreshActorAsync(actor);
    }

    /// <summary>
    /// Removes an actor from the map asynchronously.
    /// </summary>
    /// <param name="actor">The actor to remove from the map.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task RemoveActorAsync(Actor actor)
    {
        return RemoveActorAsync(actor, actor.Map);
    }

    /// <summary>
    /// Removes an actor from a specific map asynchronously.
    /// </summary>
    /// <param name="actor">The actor to remove from the map.</param>
    /// <param name="map">The map from which the actor will be removed.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task RemoveActorAsync(Actor actor, Map map)
    {
        map.RemoveActor(actor);
        return SendGameContextRemoveElementAsync(map, actor);
    }

    /// <summary>
    /// Refreshes the state of an actor on the map asynchronously.
    /// </summary>
    /// <param name="actor">The actor whose state will be refreshed.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task RefreshActorAsync(Actor actor)
    {
        return RefreshActorAsync(actor, actor.Map);
    }

    /// <summary>
    /// Refreshes the state of an actor on a specific map asynchronously.
    /// </summary>
    /// <param name="actor">The actor whose state will be refreshed.</param>
    /// <param name="map">The map on which the actor's state will be refreshed.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task RefreshActorAsync(Actor actor, Map map)
    {
        return Parallel.ForEachAsync(map.GetCharacters(), async (character, _) =>
        {
            if (actor.CanBeeSeen(character))
                return;

            await character.Connection.SendAsync(new GameRolePlayShowActorMessage { Informations = actor.GetGameRolePlayActorInformations() });
        });
    }

    /// <summary>
    /// Sends map information to a character asynchronously.
    /// </summary>
    /// <param name="character">The character to whom the map information will be sent.</param>
    /// <param name="mapId">The unique identifier of the map.</param>
    /// <param name="refreshActors">
    /// A value indicating whether to refresh the actors on the map. Defaults to true.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SendMapInformationsAsync(CharacterActor character, int mapId, bool refreshActors = true)
    {
        if (character.Map.Id != mapId)
            return;

        await SendMapComplementaryInformationsDataAsync(character.Map, character);
        await SendMapFightCountAsync(character.Map, character);

        if (/* !character.IsInFight && */ refreshActors)
            await RefreshActorAsync(character);
    }

    /// <summary>
    /// Sends the current map information to a character asynchronously.
    /// </summary>
    /// <param name="character">The character to whom the current map information will be sent.</param>
    /// <param name="map">The current map to send to the character.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task SendCurrentMapAsync(CharacterActor character, Map map) => throw new NotImplementedException();

    /// <summary>
    /// Initializes the map service asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous initialization operation.</returns>
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        _areas = _datacenterService.GetObjects<Area>().ToFrozenDictionary(static x => x.Id);
        _subAreas = _datacenterService.GetObjects<SubArea>().ToFrozenDictionary(static x => x.Id);

        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<GameDbContext>();

        var mapRecords = await dbContext
            .Maps
            .Include(static x => x.Interactives)
            .ThenInclude(static x => x.InteractiveActions)
            .ToListAsync(cancellationToken);

        var maps = new Dictionary<int, Map>();

        foreach (var mapRecord in mapRecords)
        {
            if (!_subAreas.TryGetValue(mapRecord.SubAreaId, out var subArea))
                continue;

            if (!_areas.TryGetValue(subArea.AreaId, out var area))
                continue;

            maps.Add(mapRecord.Id, new Map(mapRecord, area, subArea));
        }

        _maps = maps.ToFrozenDictionary();
    }

    /// <summary>
    /// Sends the map complementary information data to a character asynchronously.
    /// </summary>
    /// <param name="map">The map for which the complementary information data will be sent.</param>
    /// <param name="character">The character to whom the information will be sent.</param>
    /// <returns>A value task that represents the asynchronous operation.</returns>
    private static ValueTask SendMapComplementaryInformationsDataAsync(Map map, CharacterActor character)
    {
        return character.Connection.SendAsync(map.GetMapComplementaryInformationsDataMessage(character));

        // TODO: paddocks
    }

    /// <summary>
    /// Sends the fight count information of a map to a character asynchronously.
    /// </summary>
    /// <param name="_">The map whose fight count information will be sent (currently unused).</param>
    /// <param name="character">The character to whom the fight count information will be sent.</param>
    /// <returns>A value task that represents the asynchronous operation.</returns>
    private static ValueTask SendMapFightCountAsync(Map _, CharacterActor character)
    {
        return character.Connection.SendAsync(new MapFightCountMessage
        {
            FightCount = 0
        });
    }

    /// <summary>
    /// Sends a game context remove element message to all characters on the map asynchronously.
    /// </summary>
    /// <param name="map">The map from which the actor is being removed.</param>
    /// <param name="actor">The actor being removed from the map.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private static Task SendGameContextRemoveElementAsync(Map map, Actor actor)
    {
        return Parallel.ForEachAsync(map.GetCharacters(), async (character, _) =>
        {
            await character.Connection.SendAsync(new GameContextRemoveElementMessage { Id = actor.Id });
        });
    }
}
