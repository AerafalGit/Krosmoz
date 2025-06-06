// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Models.Commands;
using Krosmoz.Servers.GameServer.Network.Transport;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Servers.GameServer.Services.Commands;

public sealed partial class CommandService : ICommandService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<CommandService> _logger;

    public partial IEnumerable<CommandGroup> CommandGroups { get; }

    public CommandService(IServiceScopeFactory serviceScopeFactory, ILogger<CommandService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public partial Task ExecuteCommandAsync(DofusConnection connection, string commandText);
}
