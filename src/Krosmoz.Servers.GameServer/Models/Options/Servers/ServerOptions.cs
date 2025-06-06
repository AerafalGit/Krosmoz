// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Models.Options.Servers;

/// <summary>
/// Represents the configuration options for a game server.
/// </summary>
public sealed class ServerOptions
{
    /// <summary>
    /// Gets or sets the unique identifier of the server.
    /// </summary>
    public ushort ServerId { get; set; }
}
