// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Krosmoz.Servers.GameServer.Network.Transport;

namespace Krosmoz.Servers.GameServer.Services.Chat;

/// <summary>
/// Defines the contract for a chat service that handles sending messages.
/// </summary>
public interface IChatService
{
    /// <summary>
    /// Sends a server message to a specified connection asynchronously.
    /// </summary>
    /// <param name="connection">The connection to which the message will be sent.</param>
    /// <param name="message">The message to send, which supports composite formatting.</param>
    /// <param name="args">An array of objects to format into the message.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask SendServerMessageAsync(DofusConnection connection, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string message, params object[] args);
}
