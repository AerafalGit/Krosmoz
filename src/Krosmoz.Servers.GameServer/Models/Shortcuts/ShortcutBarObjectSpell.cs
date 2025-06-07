// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Types.Game.Shortcut;
using MemoryPack;

namespace Krosmoz.Servers.GameServer.Models.Shortcuts;

/// <summary>
/// Represents a shortcut bar object for a spell.
/// </summary>
[MemoryPackable]
public sealed partial class ShortcutBarObjectSpell : ShortcutBarObject
{
    /// <summary>
    /// Gets or sets the ID of the spell associated with this shortcut bar object.
    /// </summary>
    public SpellIds SpellId { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShortcutBarObjectSpell"/> class.
    /// </summary>
    /// <param name="slot">The slot number of the shortcut bar object.</param>
    /// <param name="spellId">The ID of the spell associated with this shortcut bar object.</param>
    public ShortcutBarObjectSpell(sbyte slot, SpellIds spellId)
    {
        Slot = slot;
        SpellId = spellId;
    }

    /// <summary>
    /// Converts the shortcut bar object into a <see cref="ShortcutSpell"/> instance.
    /// </summary>
    /// <returns>
    /// A <see cref="ShortcutSpell"/> object with the slot and spell ID information set.
    /// </returns>
    public override Shortcut GetShortcut()
    {
        return new ShortcutSpell
        {
            Slot = Slot,
            SpellId = (ushort)SpellId
        };
    }
}
