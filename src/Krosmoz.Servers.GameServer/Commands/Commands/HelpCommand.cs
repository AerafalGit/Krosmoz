// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Commands.Attributes;
using Krosmoz.Servers.GameServer.Network.Transport;

namespace Krosmoz.Servers.GameServer.Commands.Commands;

/// <summary>
/// Represents the help command that provides a list of available commands.
/// </summary>
public sealed class HelpCommand
{
    /// <summary>
    /// Executes the help command asynchronously.
    /// </summary>
    /// <param name="connection">The connection to the Dofus client.</param>
    /// <returns>A completed task representing the asynchronous operation.</returns>
    [Command("help")]
    [CommandDescription("Shows the list of available commands.")]
    public Task HelpAsync(DofusConnection connection)
    {
        return Task.CompletedTask;
    }
}
