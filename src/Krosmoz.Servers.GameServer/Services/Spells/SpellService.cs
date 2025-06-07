// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using Krosmoz.Core.Services;
using Krosmoz.Protocol.Datacenter.Spells;
using Krosmoz.Protocol.Messages.Game.Inventory.Spells;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;
using Krosmoz.Servers.GameServer.Services.Datacenter;

namespace Krosmoz.Servers.GameServer.Services.Spells;

/// <summary>
/// Provides services related to spells, including initialization and retrieval of spell data.
/// Implements <see cref="ISpellService"/> and <see cref="IInitializableService"/>.
/// </summary>
public sealed class SpellService : ISpellService, IInitializableService
{
    private readonly IDatacenterService _datacenterService;

    private FrozenDictionary<int, Spell> _spells;
    private FrozenDictionary<int, SpellLevel> _spellLevels;
    private FrozenDictionary<int, SpellBomb> _spellBombs;
    private FrozenDictionary<int, SpellPair> _spellPairs;
    private FrozenDictionary<int, SpellType> _spellTypes;
    private FrozenDictionary<int, SpellState> _spellStates;
    private FrozenDictionary<int, FrozenSet<SpellLevel>> _spellLevelsBySpellId;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpellService"/> class.
    /// </summary>
    /// <param name="datacenterService">The datacenter service used to retrieve spell-related data.</param>
    public SpellService(IDatacenterService datacenterService)
    {
        _datacenterService = datacenterService;
        _spells = FrozenDictionary<int, Spell>.Empty;
        _spellLevels = FrozenDictionary<int, SpellLevel>.Empty;
        _spellBombs = FrozenDictionary<int, SpellBomb>.Empty;
        _spellPairs = FrozenDictionary<int, SpellPair>.Empty;
        _spellTypes = FrozenDictionary<int, SpellType>.Empty;
        _spellStates = FrozenDictionary<int, SpellState>.Empty;
        _spellLevelsBySpellId = FrozenDictionary<int, FrozenSet<SpellLevel>>.Empty;
    }

    /// <summary>
    /// Attempts to retrieve a spell by its unique identifier.
    /// </summary>
    /// <param name="spellId">The unique identifier of the spell.</param>
    /// <param name="spell">When the method returns, contains the spell if found; otherwise, null.</param>
    /// <returns>True if the spell is found; otherwise, false.</returns>
    public bool TryGetSpell(int spellId, [NotNullWhen(true)] out Spell? spell)
    {
        return _spells.TryGetValue(spellId, out spell);
    }

    /// <summary>
    /// Attempts to retrieve a spell level by its unique identifier.
    /// </summary>
    /// <param name="spellLevelId">The unique identifier of the spell level.</param>
    /// <param name="spellLevel">When the method returns, contains the spell level if found; otherwise, null.</param>
    /// <returns>True if the spell level is found; otherwise, false.</returns>
    public bool TryGetSpellLevel(int spellLevelId, [NotNullWhen(true)] out SpellLevel? spellLevel)
    {
        return _spellLevels.TryGetValue(spellLevelId, out spellLevel);
    }

    /// <summary>
    /// Retrieves the spell level corresponding to a character's level.
    /// </summary>
    /// <param name="spellId">The unique identifier of the spell.</param>
    /// <param name="characterLevel">The level of the character.</param>
    /// <returns>The <see cref="SpellLevel"/> corresponding to the character's level, or null if no match is found.</returns>
    public SpellLevel? GetSpellLevelByCharacterLevel(int spellId, byte characterLevel)
    {
        return _spellLevelsBySpellId
            .GetValueOrDefault(spellId, [])
            .OrderByDescending(static spellLevel => spellLevel.MinPlayerLevel)
            .FirstOrDefault(spellLevel => spellLevel.MinPlayerLevel <= characterLevel);
    }

    /// <summary>
    /// Sends the list of spells available to the specified character asynchronously.
    /// </summary>
    /// <param name="character">The character whose spell list is to be sent.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public ValueTask SendCharacterSpellListAsync(CharacterActor character)
    {
        return character.Connection.SendAsync(new SpellListMessage
        {
            Spells = character.Spells.Select(static spell => spell.GetSpellItem()),
            SpellPrevisualization = true
        });
    }

    /// <summary>
    /// Initializes the service by loading spell-related data from the datacenter.
    /// </summary>
    public void Initialize()
    {
        _spells = _datacenterService.GetObjects<Spell>().ToFrozenDictionary(static x => x.Id);
        _spellLevels = _datacenterService.GetObjects<SpellLevel>().ToFrozenDictionary(static x => x.Id);
        _spellBombs = _datacenterService.GetObjects<SpellBomb>().ToFrozenDictionary(static x => x.Id);
        _spellPairs = _datacenterService.GetObjects<SpellPair>().ToFrozenDictionary(static x => x.Id);
        _spellTypes = _datacenterService.GetObjects<SpellType>().ToFrozenDictionary(static x => x.Id);
        _spellStates = _datacenterService.GetObjects<SpellState>().ToFrozenDictionary(static x => x.Id);
        _spellLevelsBySpellId = _spells.ToFrozenDictionary(static x => x.Key, x => x.Value.SpellLevels.Select(spellLevelId => _spellLevels[(int)spellLevelId]).ToFrozenSet());
    }
}
