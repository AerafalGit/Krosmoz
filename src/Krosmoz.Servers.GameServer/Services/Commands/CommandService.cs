// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Concurrent;
using Krosmoz.Core.Extensions;
using Krosmoz.Protocol.Messages.Authorized;
using Krosmoz.Servers.GameServer.Commands;
using Krosmoz.Servers.GameServer.Commands.Arguments;
using Krosmoz.Servers.GameServer.Models.Commands;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Chat;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Servers.GameServer.Services.Commands;

/// <summary>
/// Provides services for managing and executing commands in the game server.
/// </summary>
public sealed partial class CommandService : ICommandService
{
    private static readonly ConcurrentDictionary<string, IArgumentConverter> s_argumentConverters = new()
    {
        ["uint8"] = new UInt8ArgumentConverter(),
        ["uint16"] = new UInt16ArgumentConverter(),
        ["uint32"] = new UInt32ArgumentConverter(),
        ["uint64"] = new UInt64ArgumentConverter(),
        ["int8"] = new Int8ArgumentConverter(),
        ["int16"] = new Int16ArgumentConverter(),
        ["int32"] = new Int32ArgumentConverter(),
        ["int64"] = new Int64ArgumentConverter(),
        ["string"] = new StringArgumentConverter(),
        ["bool"] = new BooleanArgumentConverter(),
        ["float"] = new Float32ArgumentConverter(),
        ["double"] = new Float64ArgumentConverter(),
        ["system.datetime"] = new DateTimeArgumentConverter(),
        ["system.datetimeoffset"] = new DateTimeOffsetArgumentConverter(),
        ["system.dateonly"] = new DateOnlyArgumentConverter(),
        ["system.timeonly"] = new TimeOnlyArgumentConverter(),
        ["system.timespan"] = new TimeSpanArgumentConverter()
    };

    private readonly ConcurrentDictionary<string, DateTime> _commandCooldowns;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<CommandService> _logger;

    /// <summary>
    /// Gets the collection of command groups available in the service.
    /// </summary>
    public IEnumerable<CommandGroup> CommandGroups =>
        s_commandGroups;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandService"/> class.
    /// </summary>
    /// <param name="serviceScopeFactory">The service scope factory used to create service scopes.</param>
    /// <param name="logger">The logger used to log information and errors.</param>
    public CommandService(IServiceScopeFactory serviceScopeFactory, ILogger<CommandService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _commandCooldowns = [];
    }

    /// <summary>
    /// Sends the list of available console commands to the specified connection asynchronously.
    /// </summary>
    /// <param name="connection">The connection to which the command list will be sent.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    public ValueTask SendConsoleCommandsListAsync(DofusConnection connection)
    {
        return connection.SendAsync(new ConsoleCommandsListMessage
        {
            Aliases = s_commandGroups.SelectMany(static x => x.Commands.Select(static c => c.Name)),
            Descriptions = s_commandGroups.SelectMany(static x => x.Commands.Select(static c => c.Description)),
            Args = []
        });
    }

    /// <summary>
    /// Executes a command asynchronously based on the provided command text and connection.
    /// </summary>
    /// <param name="connection">The connection to the Dofus client.</param>
    /// <param name="commandText">The text of the command to execute.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task ExecuteCommandAsync(DofusConnection connection, string commandText)
    {
        if (string.IsNullOrEmpty(commandText))
            return;

        var index = 0;

        if (!commandText.ExtractNextArgument(ref index, out var commandGroupName) || !commandText.ExtractNextArgument(ref index, out var commandName))
            return;

        var scope = _serviceScopeFactory.CreateAsyncScope();

        var commandContext = new CommandContext { Services = scope.ServiceProvider, Argument = string.Empty };

        try
        {
            var commandResult = await ExecuteCommandAsync(commandContext, connection, scope.ServiceProvider, index, commandText, commandGroupName, commandName);

            if (!commandResult.IsSuccess)
            {
                var chatService = scope.ServiceProvider.GetRequiredService<IChatService>();

                switch (commandResult)
                {
                    case { IsBadHierarchy: true }:
                        await chatService.SendServerMessageAsync(connection, "You must be at least {0} to execute this command. Your hierarchy is {1}.", commandResult.RequiredHierarchy, commandResult.PlayerHierarchy);
                        break;

                    case { IsBadCooldown: true }:
                        await chatService.SendServerMessageAsync(connection, "You must wait {0} before executing this command again.", commandResult.Cooldown.Value.ToHumanReadableTime());
                        break;

                    case { IsCommandNotFound: true }:
                        await chatService.SendServerMessageAsync(connection, "Command '{0} {1}' not found.", commandGroupName, commandName);
                        break;
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while executing command '{CommandGroupName} {CommandName}' for connection {ConnectionName}.", commandGroupName, commandName, connection);
        }
        finally
        {
            await scope.DisposeAsync();
        }
    }
}
