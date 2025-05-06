// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Transport;

namespace Krosmoz.Core.Network.Dispatcher;

/// <summary>
/// Defines a dispatcher for handling and processing network messages.
/// </summary>
/// <typeparam name="TMessage">The type of the network message to dispatch.</typeparam>
public interface IMessageDispatcher<TMessage>
    where TMessage : class
{
    /// <summary>
    /// Asynchronously dispatches a network message to the appropriate handler.
    /// </summary>
    /// <param name="session">The TCP session associated with the message.</param>
    /// <param name="message">The network message to dispatch.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DispatchMessageAsync(TcpSession<TMessage> session, TMessage message);
}
