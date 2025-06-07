// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Game.Shortcut;
using Krosmoz.Servers.GameServer.Network.Transport;

namespace Krosmoz.Servers.GameServer.Services.Shortcuts;

/// <summary>
/// Provides services for managing shortcut bar content.
/// </summary>
public sealed class ShortcutService : IShortcutService
{
    /// <summary>
    /// Sends the content of a shortcut bar to the specified connection asynchronously.
    /// </summary>
    /// <param name="connection">The connection to which the shortcut bar content will be sent.</param>
    /// <param name="barType">The type of the shortcut bar to send.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask SendShortcutBarContentAsync(DofusConnection connection, ShortcutBars barType)
    {
        return connection.SendAsync(new ShortcutBarContentMessage
        {
            BarType = (sbyte)barType,
            Shortcuts = barType is ShortcutBars.GeneralShortcutBar
                ? connection.Heroes.Master.GeneralShortcutBar.Select(static s => s.GetShortcut())
                : connection.Heroes.Master.SpellShortcutBar.Select(static s => s.GetShortcut())
        });
    }
}
