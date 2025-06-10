// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Metadata;

namespace Krosmoz.Core.Network.Framing.Factory;

/// <summary>
/// Defines the contract for a factory that creates Dofus messages.
/// </summary>
public interface IMessageFactory
{
    /// <summary>
    /// Creates a Dofus message instance based on the specified message identifier.
    /// </summary>
    /// <param name="messageId">The unique identifier of the message.</param>
    /// <returns>A <see cref="DofusMessage"/> instance corresponding to the given identifier.</returns>
    DofusMessage CreateMessage(uint messageId);

    /// <summary>
    /// Retrieves the name of a Dofus message based on the specified message identifier.
    /// </summary>
    /// <param name="messageId">The unique identifier of the message.</param>
    /// <returns>A string representing the name of the message.</returns>
    string CreateMessageName(uint messageId);
}
