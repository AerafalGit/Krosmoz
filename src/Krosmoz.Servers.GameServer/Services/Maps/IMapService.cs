// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Krosmoz.Servers.GameServer.Models.Actors;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;
using Krosmoz.Servers.GameServer.Models.World.Maps;

namespace Krosmoz.Servers.GameServer.Services.Maps;

/// <summary>
/// Defines the contract for a service that provides map-related operations.
/// </summary>
public interface IMapService
{
    /// <summary>
    /// Attempts to retrieve a map by its identifier.
    /// </summary>
    /// <param name="mapId">The unique identifier of the map.</param>
    /// <param name="map">
    /// When this method returns, contains the map associated with the specified identifier,
    /// if the map is found; otherwise, null. This parameter is passed uninitialized.
    /// </param>
    /// <returns>True if the map is found; otherwise, false.</returns>
    bool TryGetMap(int mapId, [NotNullWhen(true)] out Map? map);

    /// <summary>
    /// Adds an actor to the map asynchronously.
    /// </summary>
    /// <param name="actor">The actor to add to the map.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddActorAsync(Actor actor);

    /// <summary>
    /// Adds an actor to a specific map asynchronously.
    /// </summary>
    /// <param name="actor">The actor to add to the map.</param>
    /// <param name="map">The map to which the actor will be added.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddActorAsync(Actor actor, Map map);

    /// <summary>
    /// Removes an actor from the map asynchronously.
    /// </summary>
    /// <param name="actor">The actor to remove from the map.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RemoveActorAsync(Actor actor);

    /// <summary>
    /// Removes an actor from a specific map asynchronously.
    /// </summary>
    /// <param name="actor">The actor to remove from the map.</param>
    /// <param name="map">The map from which the actor will be removed.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RemoveActorAsync(Actor actor, Map map);

    /// <summary>
    /// Refreshes the state of an actor on the map asynchronously.
    /// </summary>
    /// <param name="actor">The actor whose state will be refreshed.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RefreshActorAsync(Actor actor);

    /// <summary>
    /// Refreshes the state of an actor on a specific map asynchronously.
    /// </summary>
    /// <param name="actor">The actor whose state will be refreshed.</param>
    /// <param name="map">The map on which the actor's state will be refreshed.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RefreshActorAsync(Actor actor, Map map);

    /// <summary>
    /// Sends map information to a character asynchronously.
    /// </summary>
    /// <param name="character">The character to whom the map information will be sent.</param>
    /// <param name="mapId">The unique identifier of the map.</param>
    /// <param name="refreshActors">
    /// A value indicating whether to refresh the actors on the map. Defaults to true.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendMapInformationsAsync(CharacterActor character, int mapId, bool refreshActors = true);

    /// <summary>
    /// Sends the current map information to a character asynchronously.
    /// </summary>
    /// <param name="character">The character to whom the current map information will be sent.</param>
    /// <param name="map">The current map to send to the character.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendCurrentMapAsync(CharacterActor character, Map map);
}
