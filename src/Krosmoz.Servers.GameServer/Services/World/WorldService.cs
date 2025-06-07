// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Services.World;

/// <summary>
/// Represents a service for managing the world state, including characters.
/// Provides dictionary-like access to characters by their unique identifiers.
/// </summary>
public sealed class WorldService : IWorldService
{
    private readonly ConcurrentDictionary<int, CharacterActor> _characters;

    /// <summary>
    /// Gets or sets the character associated with the specified unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the character.</param>
    /// <returns>The character associated with the specified identifier.</returns>
    public CharacterActor this[int id]
    {
        get => _characters[id];
        set => _characters[id] = value;
    }

    /// <summary>
    /// Gets a collection of all keys (unique identifiers) in the world service.
    /// </summary>
    public ICollection<int> Keys =>
        _characters.Keys;

    /// <summary>
    /// Gets a collection of all characters in the world service.
    /// </summary>
    public ICollection<CharacterActor> Values =>
        _characters.Values;

    /// <summary>
    /// Gets the number of characters in the world service.
    /// </summary>
    public int Count =>
        _characters.Count;

    /// <summary>
    /// Gets a value indicating whether the world service is read-only.
    /// </summary>
    public bool IsReadOnly =>
        false;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorldService"/> class.
    /// </summary>
    public WorldService()
    {
        _characters = [];
    }

    /// <summary>
    /// Determines whether the world service contains a character with the specified key.
    /// </summary>
    /// <param name="id">The unique identifier of the character.</param>
    /// <returns><c>true</c> if the character exists; otherwise, <c>false</c>.</returns>
    public bool ContainsKey(int id)
    {
        return _characters.ContainsKey(id);
    }

    /// <summary>
    /// Adds a character to the world service with the specified key.
    /// </summary>
    /// <param name="id">The unique identifier of the character.</param>
    /// <param name="character">The character to add.</param>
    public void Add(int id, CharacterActor character)
    {
        _characters.TryAdd(id, character);
    }

    /// <summary>
    /// Removes the character associated with the specified key from the world service.
    /// </summary>
    /// <param name="id">The unique identifier of the character.</param>
    /// <returns><c>true</c> if the character was successfully removed; otherwise, <c>false</c>.</returns>
    public bool Remove(int id)
    {
        return _characters.Remove(id, out _);
    }

    /// <summary>
    /// Attempts to retrieve the character associated with the specified key.
    /// </summary>
    /// <param name="id">The unique identifier of the character.</param>
    /// <param name="character">
    /// When this method returns, contains the character associated with the key, if found;
    /// otherwise, <c>null</c>.
    /// </param>
    /// <returns><c>true</c> if the character was found; otherwise, <c>false</c>.</returns>
    public bool TryGetValue(int id, [NotNullWhen(true)] out CharacterActor? character)
    {
        return _characters.TryGetValue(id, out character);
    }

    /// <summary>
    /// Adds a character to the world service.
    /// </summary>
    /// <param name="character">The character to add.</param>
    public void Add(CharacterActor character)
    {
        _characters.TryAdd(character.Id, character);
    }

    /// <summary>
    /// Removes all characters from the world service.
    /// </summary>
    public void Clear()
    {
        _characters.Clear();
    }

    /// <summary>
    /// Determines whether the world service contains the specified character.
    /// </summary>
    /// <param name="character">The character to check.</param>
    /// <returns><c>true</c> if the character exists; otherwise, <c>false</c>.</returns>
    public bool Contains(CharacterActor character)
    {
        return _characters.ContainsKey(character.Id);
    }

    /// <summary>
    /// Copies the characters in the world service to the specified array, starting at the specified index.
    /// </summary>
    /// <param name="array">The array to copy characters to.</param>
    /// <param name="arrayIndex">The index in the array at which copying begins.</param>
    public void CopyTo(CharacterActor[] array, int arrayIndex)
    {
        _characters.Values.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Removes the specified character from the world service.
    /// </summary>
    /// <param name="character">The character to remove.</param>
    /// <returns><c>true</c> if the character was successfully removed; otherwise, <c>false</c>.</returns>
    public bool Remove(CharacterActor character)
    {
        return _characters.Remove(character.Id, out _);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the characters in the world service.
    /// </summary>
    /// <returns>An enumerator for the characters in the world service.</returns>
    public IEnumerator<CharacterActor> GetEnumerator()
    {
        return _characters.Values.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the characters in the world service.
    /// </summary>
    /// <returns>An enumerator for the characters in the world service.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
