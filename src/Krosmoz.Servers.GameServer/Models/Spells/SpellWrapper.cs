// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Spells;
using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Servers.GameServer.Models.Spells;

/// <summary>
/// Represents a wrapper for a spell, containing its template, level information, and position.
/// </summary>
public sealed class SpellWrapper
{
    /// <summary>
    /// Gets the spell template associated with this wrapper.
    /// </summary>
    public Spell Template { get; }

    /// <summary>
    /// Gets the level information of the spell.
    /// </summary>
    public SpellLevel Level { get; }

    /// <summary>
    /// Gets the position of the spell.
    /// </summary>
    public byte Position { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SpellWrapper"/> class.
    /// </summary>
    /// <param name="template">The spell template.</param>
    /// <param name="level">The level information of the spell.</param>
    /// <param name="position">The position of the spell.</param>
    public SpellWrapper(Spell template, SpellLevel level, byte position)
    {
        Template = template;
        Level = level;
        Position = position;
    }

    /// <summary>
    /// Creates a <see cref="SpellItem"/> instance based on the spell's template, level, and position.
    /// </summary>
    /// <returns>A new <see cref="SpellItem"/> instance.</returns>
    public SpellItem GetSpellItem()
    {
        return new SpellItem
        {
            SpellId = Template.Id,
            SpellLevel = (sbyte)Level.Grade,
            Position = Position
        };
    }
}
