// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Transport;

namespace Krosmoz.Core.Network.Dispatcher.Handlers;

/// <summary>
/// Represents an abstract base class for handling network messages.
/// </summary>
/// <typeparam name="TSession">The type of the TCP session associated with the message.</typeparam>
/// <typeparam name="TMessage">The type of the network message to handle.</typeparam>
public abstract class MessageHandler<TSession, TMessage>
    where TSession : TcpSession
    where TMessage : class
{
    /// <summary>
    /// Asynchronously handles a network message for a given session.
    /// </summary>
    /// <param name="session">The TCP session associated with the message.</param>
    /// <param name="message">The network message to handle.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public abstract Task HandleAsync(TSession session, TMessage message);
}
