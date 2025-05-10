// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Dispatcher.Handlers;
using Krosmoz.Core.Network.Protocol.Dofus;
using Krosmoz.Servers.AuthServer.Network.Transport;

namespace Krosmoz.Servers.AuthServer.Network.Dispatcher.Handlers;

/// <summary>
/// Represents an abstract base class for handling authentication-related messages.
/// </summary>
/// <typeparam name="TMessage">The type of the Dofus message being handled.</typeparam>
public abstract class AuthMessageHandler<TMessage> : MessageHandler<AuthSession, TMessage, DofusMessage>
    where TMessage : DofusMessage;
