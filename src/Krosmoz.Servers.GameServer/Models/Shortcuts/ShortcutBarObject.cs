// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Shortcut;
using MemoryPack;

namespace Krosmoz.Servers.GameServer.Models.Shortcuts;

/// <summary>
/// Represents an abstract base class for objects in the shortcut bar.
/// </summary>
[MemoryPackUnion(0, typeof(ShortcutBarObjectEmote))]
[MemoryPackUnion(1, typeof(ShortcutBarObjectItem))]
[MemoryPackUnion(2, typeof(ShortcutBarObjectPreset))]
[MemoryPackUnion(3, typeof(ShortcutBarObjectSmiley))]
[MemoryPackUnion(4, typeof(ShortcutBarObjectSpell))]
[MemoryPackable]
public abstract partial class ShortcutBarObject
{
    /// <summary>
    /// Gets or sets the slot number of the shortcut bar object.
    /// </summary>
    /// <remarks>
    /// The slot determines the position of the object in the shortcut bar.
    /// </remarks>
    public virtual sbyte Slot { get; set; }

    /// <summary>
    /// Converts the shortcut bar object into a <see cref="Shortcut"/> instance.
    /// </summary>
    /// <returns>
    /// A <see cref="Shortcut"/> object with the slot information set.
    /// </returns>
    public virtual Shortcut GetShortcut()
    {
        return new Shortcut { Slot = Slot };
    }
}
