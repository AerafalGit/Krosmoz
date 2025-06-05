// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Network.Transport;

namespace Krosmoz.Servers.AuthServer.Services.Queue;

/// <summary>
/// Defines the contract for a queue service that manages authentication connections.
/// </summary>
public interface IQueueService
{
    /// <summary>
    /// Adds an authentication connection to the queue.
    /// </summary>
    /// <param name="connection">The authentication connection to enqueue.</param>
    void Enqueue(DofusConnection connection);

    /// <summary>
    /// Removes an authentication connection from the queue.
    /// </summary>
    /// <param name="connection">The authentication connection to dequeue.</param>
    void Dequeue(DofusConnection connection);

    /// <summary>
    /// Sends the current queue status to a specific authentication connection.
    /// </summary>
    /// <param name="connection">The authentication connection to send the status to.</param>
    /// <param name="position">The position of the connection in the queue.</param>
    /// <param name="total">The total number of connections in the queue.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask SendQueueStatusAsync(DofusConnection connection, ushort position, ushort total);
}
