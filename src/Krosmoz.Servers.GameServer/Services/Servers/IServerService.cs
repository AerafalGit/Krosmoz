// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Servers.GameServer.Network.Transport;

namespace Krosmoz.Servers.GameServer.Services.Servers;

/// <summary>
/// Represents a service for managing server-related operations.
/// </summary>
public interface IServerService
{
    /// <summary>
    /// Gets the unique identifier of the server.
    /// </summary>
    ushort ServerId { get; }

    /// <summary>
    /// Gets the name of the server.
    /// </summary>
    string ServerName { get; }

    /// <summary>
    /// Gets the game type of the server.
    /// </summary>
    ServerGameTypeIds ServerGameType { get; }

    /// <summary>
    /// Sends server information asynchronously to the specified game connection.
    /// </summary>
    /// <param name="connection">The game connection to send the server information to.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendServerInformationsAsync(DofusConnection connection);
}
