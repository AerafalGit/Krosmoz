// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Dispatcher.Attributes;
using Krosmoz.Protocol.Messages.Common.Basic;
using Krosmoz.Servers.GameServer.Network.Transport;

namespace Krosmoz.Servers.GameServer.Network.Handlers.Basic;

/// <summary>
/// Handles basic network operations.
/// </summary>
public sealed class BasicHandler
{
    /// <summary>
    /// Handles the BasicPingMessage by responding with a BasicPongMessage.
    /// </summary>
    /// <param name="connection">The connection to the Dofus client.</param>
    /// <param name="message">The BasicPingMessage received from the client.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public async Task HandleBasicPingAsync(DofusConnection connection, BasicPingMessage message)
    {
        await connection.SendAsync(new BasicPongMessage { Quiet = message.Quiet });
    }
}
