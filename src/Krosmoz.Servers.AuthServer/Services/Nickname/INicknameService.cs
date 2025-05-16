﻿// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Network.Transport;

namespace Krosmoz.Servers.AuthServer.Services.Nickname;

/// <summary>
/// Defines the contract for a service that handles nickname-related operations in the authentication server.
/// </summary>
public interface INicknameService
{
    /// <summary>
    /// Processes the nickname choice request for a client session asynchronously.
    /// </summary>
    /// <param name="session">The client session making the request.</param>
    /// <param name="nickname">The nickname chosen by the client.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ChoiceNicknameAsync(AuthSession session, string nickname);
}
