// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Services.World;

/// <summary>
/// Represents a service for managing the world state, including characters.
/// Provides dictionary-like access to characters by their unique identifiers.
/// </summary>
public interface IWorldService : ICollection<CharacterActor>
{
    /// <summary>
    /// Gets or sets the character associated with the specified key.
    /// </summary>
    /// <param name="id">The unique identifier of the character.</param>
    /// <returns>The character associated with the specified key.</returns>
    CharacterActor this[int id] { get; set; }

    /// <summary>
    /// Gets a collection of all keys (unique identifiers) in the world service.
    /// </summary>
    ICollection<int> Keys { get; }

    /// <summary>
    /// Gets a collection of all characters in the world service.
    /// </summary>
    ICollection<CharacterActor> Values { get; }

    /// <summary>
    /// Determines whether the world service contains a character with the specified key.
    /// </summary>
    /// <param name="id">The unique identifier of the character.</param>
    /// <returns><c>true</c> if the character exists; otherwise, <c>false</c>.</returns>
    bool ContainsKey(int id);

    /// <summary>
    /// Adds a character to the world service with the specified key.
    /// </summary>
    /// <param name="id">The unique identifier of the character.</param>
    /// <param name="value">The character to add.</param>
    void Add(int id, CharacterActor value);

    /// <summary>
    /// Removes the character associated with the specified key from the world service.
    /// </summary>
    /// <param name="id">The unique identifier of the character.</param>
    /// <returns><c>true</c> if the character was successfully removed; otherwise, <c>false</c>.</returns>
    bool Remove(int id);

    /// <summary>
    /// Attempts to retrieve the character associated with the specified key.
    /// </summary>
    /// <param name="id">The unique identifier of the character.</param>
    /// <param name="value">
    /// When this method returns, contains the character associated with the key, if found;
    /// otherwise, <c>null</c>.
    /// </param>
    /// <returns><c>true</c> if the character was found; otherwise, <c>false</c>.</returns>
    bool TryGetValue(int id, [NotNullWhen(true)] out CharacterActor? value);
}
