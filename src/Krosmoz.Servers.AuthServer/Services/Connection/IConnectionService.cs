// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Servers.AuthServer.Network.Transport;

namespace Krosmoz.Servers.AuthServer.Services.Connection;

/// <summary>
/// Defines the contract for a connection service that handles connection-related operations.
/// </summary>
public interface IConnectionService
{
    /// <summary>
    /// Sends an identification failure message asynchronously to the specified session.
    /// </summary>
    /// <param name="session">The session to which the failure message will be sent.</param>
    /// <param name="reason">The reason for the identification failure.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendIdentificationFailedMessageAsync(AuthSession session, IdentificationFailureReasa reason);
}
