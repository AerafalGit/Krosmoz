// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Shortcut;
using MemoryPack;

namespace Krosmoz.Servers.GameServer.Models.Shortcuts;

/// <summary>
/// Represents a shortcut bar object for a preset.
/// </summary>
[MemoryPackable]
public sealed partial class ShortcutBarObjectPreset : ShortcutBarObject
{
    /// <summary>
    /// Gets or sets the ID of the preset associated with this shortcut bar object.
    /// </summary>
    public sbyte PresetId { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShortcutBarObjectPreset"/> class.
    /// </summary>
    /// <param name="slot">The slot number of the shortcut bar object.</param>
    /// <param name="presetId">The ID of the preset associated with this shortcut bar object.</param>
    public ShortcutBarObjectPreset(sbyte slot, sbyte presetId)
    {
        Slot = slot;
        PresetId = presetId;
    }

    /// <summary>
    /// Converts the shortcut bar object into a <see cref="ShortcutObjectPreset"/> instance.
    /// </summary>
    /// <returns>
    /// A <see cref="ShortcutObjectPreset"/> object with the slot and preset ID information set.
    /// </returns>
    public override Shortcut GetShortcut()
    {
        return new ShortcutObjectPreset
        {
            Slot = Slot,
            PresetId = PresetId
        };
    }
}
