// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Network.Transport;

namespace Krosmoz.Servers.AuthServer.Services.Acquaintance;

/// <summary>
/// Defines a service for managing acquaintance-related operations.
/// </summary>
public interface IAcquaintanceService
{
    /// <summary>
    /// Sends the list of acquaintance servers asynchronously to the specified connection.
    /// </summary>
    /// <param name="connection">The Dofus connection to which the server list will be sent.</param>
    /// <param name="nickname">The nickname associated with the acquaintance request.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendAcquaintanceServerListAsync(DofusConnection connection, string nickname);
}
