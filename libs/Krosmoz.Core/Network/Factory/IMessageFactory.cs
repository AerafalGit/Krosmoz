// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.Network.Factory;

/// <summary>
/// Defines a factory for creating network messages based on a message ID.
/// </summary>
/// <typeparam name="TMessage">The type of the network message to create.</typeparam>
public interface IMessageFactory<out TMessage>
    where TMessage : class
{
    /// <summary>
    /// Creates a network message based on the specified message ID.
    /// </summary>
    /// <param name="messageId">The ID of the message to create.</param>
    /// <returns>The created network message.</returns>
    TMessage CreateMessage(uint messageId);

    /// <summary>
    /// Creates the name of a network message based on the specified message ID.
    /// </summary>
    /// <param name="messageId">The ID of the message to create the name for.</param>
    /// <returns>The name of the created network message.</returns>
    string CreateMessageName(uint messageId);
}
