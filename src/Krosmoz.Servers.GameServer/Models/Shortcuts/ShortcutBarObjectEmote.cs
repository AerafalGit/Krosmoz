// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Types.Game.Shortcut;
using MemoryPack;

namespace Krosmoz.Servers.GameServer.Models.Shortcuts;

/// <summary>
/// Represents a shortcut bar object for an emote.
/// </summary>
[MemoryPackable]
public sealed partial class ShortcutBarObjectEmote : ShortcutBarObject
{
    /// <summary>
    /// Gets or sets the ID of the emote associated with this shortcut bar object.
    /// </summary>
    public EmoticonIds EmoteId { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShortcutBarObjectEmote"/> class.
    /// </summary>
    /// <param name="slot">The slot number of the shortcut bar object.</param>
    /// <param name="emoteId">The ID of the emote associated with this shortcut bar object.</param>
    public ShortcutBarObjectEmote(sbyte slot, EmoticonIds emoteId)
    {
        Slot = slot;
        EmoteId = emoteId;
    }

    /// <summary>
    /// Converts the shortcut bar object into a <see cref="ShortcutEmote"/> instance.
    /// </summary>
    /// <returns>
    /// A <see cref="ShortcutEmote"/> object with the slot and emote ID information set.
    /// </returns>
    public override Shortcut GetShortcut()
    {
        return new ShortcutEmote
        {
            Slot = Slot,
            EmoteId = (byte)EmoteId
        };
    }
}
