// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Metadata;

namespace Krosmoz.Core.Network.Dispatcher;

/// <summary>
/// Defines the contract for a message dispatcher that handles the dispatching of messages
/// to connections in the network.
/// </summary>
/// <typeparam name="TConnection">The type of the connection.</typeparam>
public interface IMessageDispatcher<in TConnection> : IAsyncDisposable
    where TConnection : class
{
    /// <summary>
    /// Dispatches a message asynchronously to the specified connection.
    /// </summary>
    /// <param name="connection">The connection to which the message will be dispatched.</param>
    /// <param name="message">The message to be dispatched.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DispatchMessageAsync(TConnection connection, DofusMessage message);
}
