// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Shortcut;
using MemoryPack;

namespace Krosmoz.Servers.GameServer.Models.Shortcuts;

/// <summary>
/// Represents a shortcut bar object for a smiley.
/// </summary>
[MemoryPackable]
public sealed partial class ShortcutBarObjectSmiley : ShortcutBarObject
{
    /// <summary>
    /// Gets or sets the ID of the smiley associated with this shortcut bar object.
    /// </summary>
    public sbyte SmileyId { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShortcutBarObjectSmiley"/> class.
    /// </summary>
    /// <param name="slot">The slot number of the shortcut bar object.</param>
    /// <param name="smileyId">The ID of the smiley associated with this shortcut bar object.</param>
    public ShortcutBarObjectSmiley(sbyte slot, sbyte smileyId)
    {
        Slot = slot;
        SmileyId = smileyId;
    }

    /// <summary>
    /// Converts the shortcut bar object into a <see cref="ShortcutSmiley"/> instance.
    /// </summary>
    /// <returns>
    /// A <see cref="ShortcutSmiley"/> object with the slot and smiley ID information set.
    /// </returns>
    public override Shortcut GetShortcut()
    {
        return new ShortcutSmiley
        {
            Slot = Slot,
            SmileyId = SmileyId
        };
    }
}
