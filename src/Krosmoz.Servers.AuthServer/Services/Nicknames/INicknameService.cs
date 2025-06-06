// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Network.Transport;

namespace Krosmoz.Servers.AuthServer.Services.Nicknames;

/// <summary>
/// Defines the contract for a service that handles nickname-related operations in the authentication server.
/// </summary>
public interface INicknameService
{
    /// <summary>
    /// Processes the nickname choice request for a client connection asynchronously.
    /// </summary>
    /// <param name="connection">The client connection making the request.</param>
    /// <param name="nickname">The nickname chosen by the client.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ChoiceNicknameAsync(DofusConnection connection, string nickname);
}
