// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Models.Commands;
using Krosmoz.Servers.GameServer.Network.Transport;

namespace Krosmoz.Servers.GameServer.Services.Commands;

/// <summary>
/// Defines the contract for a service that manages and executes commands.
/// </summary>
public interface ICommandService
{
    /// <summary>
    /// Gets the collection of command groups available in the service.
    /// </summary>
    IEnumerable<CommandGroup> CommandGroups { get; }

    /// <summary>
    /// Sends the list of available console commands to the specified connection asynchronously.
    /// </summary>
    /// <param name="connection">The connection to which the command list will be sent.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask SendConsoleCommandsListAsync(DofusConnection connection);

    /// <summary>
    /// Executes a command asynchronously based on the provided command text and connection.
    /// </summary>
    /// <param name="connection">The connection to the Dofus client.</param>
    /// <param name="commandText">The text of the command to execute.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task ExecuteCommandAsync(DofusConnection connection, string commandText);
}
