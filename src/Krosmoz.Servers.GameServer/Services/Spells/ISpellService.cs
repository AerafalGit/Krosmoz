// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Krosmoz.Protocol.Datacenter.Spells;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Services.Spells;

/// <summary>
/// Defines the contract for a service that provides spell-related operations.
/// </summary>
public interface ISpellService
{
    /// <summary>
    /// Attempts to retrieve a spell by its unique identifier.
    /// </summary>
    /// <param name="spellId">The unique identifier of the spell.</param>
    /// <param name="spell">When the method returns, contains the spell if found; otherwise, null.</param>
    /// <returns>True if the spell is found; otherwise, false.</returns>
    bool TryGetSpell(int spellId, [NotNullWhen(true)] out Spell? spell);

    /// <summary>
    /// Attempts to retrieve a spell level by its unique identifier.
    /// </summary>
    /// <param name="spellLevelId">The unique identifier of the spell level.</param>
    /// <param name="spellLevel">When the method returns, contains the spell level if found; otherwise, null.</param>
    /// <returns>True if the spell level is found; otherwise, false.</returns>
    bool TryGetSpellLevel(int spellLevelId, [NotNullWhen(true)] out SpellLevel? spellLevel);

    /// <summary>
    /// Retrieves the spell level corresponding to a character's level.
    /// </summary>
    /// <param name="spellId">The unique identifier of the spell.</param>
    /// <param name="characterLevel">The level of the character.</param>
    /// <returns>The <see cref="SpellLevel"/> corresponding to the character's level, or null if no match is found.</returns>
    SpellLevel? GetSpellLevelByCharacterLevel(int spellId, byte characterLevel);

    /// <summary>
    /// Asynchronously sends the list of spells available to the specified character.
    /// </summary>
    /// <param name="character">The character whose spell list is to be sent.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    ValueTask SendCharacterSpellListAsync(CharacterActor character);
}
