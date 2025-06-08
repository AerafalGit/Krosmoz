// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Dispatcher.Attributes;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Game.Context;
using Krosmoz.Servers.GameServer.Models.Context;
using Krosmoz.Servers.GameServer.Network.Transport;

namespace Krosmoz.Servers.GameServer.Network.Handlers.Context;

/// <summary>
/// Handles context-related operations.
/// </summary>
public sealed class ContextHandler
{
    private readonly IContextService _contextService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContextHandler"/> class.
    /// </summary>
    /// <param name="contextService">The service used for context-related operations.</param>
    public ContextHandler(IContextService contextService)
    {
        _contextService = contextService;
    }

    /// <summary>
    /// Handles a game context creation request asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the client making the request.</param>
    /// <param name="_">The message containing the game context creation request.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleGameContextCreateRequestAsync(DofusConnection connection, GameContextCreateRequestMessage _)
    {
        // TODO: Implement the logic for handling game context creation requests.

        return _contextService.CreateContextAsync(connection.Heroes.Master, PlayerStates.GameTypeRoleplay);
    }
}
