// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Messages.Connection;
using Krosmoz.Servers.AuthServer.Network.Dispatcher.Handlers;
using Krosmoz.Servers.AuthServer.Network.Transport;

namespace Krosmoz.Servers.AuthServer.Handlers.Connection;

public sealed class IdentificationHandler : AuthMessageHandler<IdentificationMessage>
{
    public override Task HandleAsync(AuthSession session, IdentificationMessage message)
    {
        return Task.CompletedTask;
    }
}
