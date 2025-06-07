// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Servers.GameServer.Network.Transport;

namespace Krosmoz.Servers.GameServer.Services.Shortcuts;

/// <summary>
/// Defines the interface for a shortcut service that manages shortcut bar.
/// </summary>
public interface IShortcutService
{
    /// <summary>
    /// Sends the content of a shortcut bar to the specified connection asynchronously.
    /// </summary>
    /// <param name="connection">The connection to which the shortcut bar content will be sent.</param>
    /// <param name="barType">The type of the shortcut bar to send.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask SendShortcutBarContentAsync(DofusConnection connection, ShortcutBars barType);
}
