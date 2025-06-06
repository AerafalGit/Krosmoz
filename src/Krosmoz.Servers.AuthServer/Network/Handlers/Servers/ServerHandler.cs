// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Dispatcher.Attributes;
using Krosmoz.Protocol.Messages.Connection;
using Krosmoz.Protocol.Messages.Connection.Search;
using Krosmoz.Servers.AuthServer.Network.Transport;
using Krosmoz.Servers.AuthServer.Services.Acquaintance;
using Krosmoz.Servers.AuthServer.Services.Servers;

namespace Krosmoz.Servers.AuthServer.Network.Handlers.Servers;

/// <summary>
/// Handles server-related messages for Dofus connections, including server selection
/// and acquaintance search requests.
/// </summary>
public sealed class ServerHandler
{
    private readonly IServerService _serverService;
    private readonly IAcquaintanceService _acquaintanceService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServerHandler"/> class.
    /// </summary>
    /// <param name="serverService">The service responsible for managing server-related operations.</param>
    /// <param name="acquaintanceService">The service responsible for managing acquaintance-related operations.</param>
    public ServerHandler(IServerService serverService, IAcquaintanceService acquaintanceService)
    {
        _serverService = serverService;
        _acquaintanceService = acquaintanceService;
    }

    /// <summary>
    /// Handles the server selection message asynchronously by delegating to the server service.
    /// </summary>
    /// <param name="connection">The Dofus connection associated with the message.</param>
    /// <param name="message">The server selection message to process.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleServerSelectionAsync(DofusConnection connection, ServerSelectionMessage message)
    {
        return _serverService.SelectGameServerAsync(connection, message.ServerId);
    }

    /// <summary>
    /// Handles the acquaintance search message asynchronously by delegating to the acquaintance service.
    /// </summary>
    /// <param name="connection">The Dofus connection associated with the message.</param>
    /// <param name="message">The acquaintance search message to process.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleAcquaintanceSearchAsync(DofusConnection connection, AcquaintanceSearchMessage message)
    {
        return _acquaintanceService.SendAcquaintanceServerListAsync(connection, message.Nickname);
    }
}
