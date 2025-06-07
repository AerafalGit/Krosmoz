// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Game.Shortcut;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Services.Shortcuts;

/// <summary>
/// Provides services for managing shortcut bar content.
/// </summary>
public sealed class ShortcutService : IShortcutService
{
    public ValueTask SendShortcutBarContentAsync(CharacterActor character, ShortcutBars barType)
    {
        return character.Connection.SendAsync(new ShortcutBarContentMessage
        {
            BarType = (sbyte)barType,
            Shortcuts = barType is ShortcutBars.GeneralShortcutBar
                ? character.GeneralShortcutBar.Select(static s => s.GetShortcut())
                : character.SpellShortcutBar.Select(static s => s.GetShortcut())
        });
    }
}
