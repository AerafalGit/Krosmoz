// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Shortcut;
using MemoryPack;

namespace Krosmoz.Servers.GameServer.Models.Shortcuts;

/// <summary>
/// Represents a shortcut bar object for an item.
/// </summary>
[MemoryPackable]
public sealed partial class ShortcutBarObjectItem : ShortcutBarObject
{
    /// <summary>
    /// Gets or sets the unique identifier of the item associated with this shortcut bar object.
    /// </summary>
    public int ItemUid { get; set; }

    /// <summary>
    /// Gets or sets the generic identifier of the item associated with this shortcut bar object.
    /// </summary>
    public int ItemGid { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShortcutBarObjectItem"/> class.
    /// </summary>
    /// <param name="slot">The slot number of the shortcut bar object.</param>
    /// <param name="itemUid">The unique identifier of the item.</param>
    /// <param name="itemGid">The generic identifier of the item.</param>
    public ShortcutBarObjectItem(sbyte slot, int itemUid, int itemGid)
    {
        Slot = slot;
        ItemUid = itemUid;
        ItemGid = itemGid;
    }

    /// <summary>
    /// Converts the shortcut bar object into a <see cref="ShortcutObjectItem"/> instance.
    /// </summary>
    /// <returns>
    /// A <see cref="ShortcutObjectItem"/> object with the slot, item UID, and item GID information set.
    /// </returns>
    public override Shortcut GetShortcut()
    {
        return new ShortcutObjectItem
        {
            Slot = Slot,
            ItemGID = ItemGid,
            ItemUID = ItemUid,
        };
    }
}
