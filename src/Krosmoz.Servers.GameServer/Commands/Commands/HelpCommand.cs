// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Text;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Authorized;
using Krosmoz.Servers.GameServer.Commands.Attributes;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Commands;

namespace Krosmoz.Servers.GameServer.Commands.Commands;

/// <summary>
/// Represents the help command that provides a list of available commands.
/// </summary>
[CommandGroup("commands")]
public sealed class HelpCommand
{
    private readonly ICommandService _commandService;

    /// <summary>
    /// Initializes a new instance of the <see cref="HelpCommand"/> class.
    /// </summary>
    /// <param name="commandService">The service responsible for managing commands.</param>
    public HelpCommand(ICommandService commandService)
    {
        _commandService = commandService;
    }

    /// <summary>
    /// Executes the help command asynchronously.
    /// </summary>
    /// <param name="connection">The connection to the Dofus client.</param>
    /// <returns>A completed task representing the asynchronous operation.</returns>
    [Command("help")]
    [CommandDescription("Shows the list of available commands.")]
    public async Task HelpAsync(DofusConnection connection)
    {
        var builder = new StringBuilder();

        foreach (var commandGroup in _commandService.CommandGroups)
        {
            builder.AppendLine($"---- {commandGroup.Name} ----");

            foreach (var command in commandGroup.Commands)
                builder.AppendLine($"- {command.Name}: {command.Description}");

            builder.AppendLine($"----{new string('-', commandGroup.Name.Length + 2)}----");
        }

        await connection.SendAsync(new ConsoleMessage { Type = (sbyte)ConsoleMessageTypes.ConsoleTextMessage, Content = builder.ToString() });
    }
}
