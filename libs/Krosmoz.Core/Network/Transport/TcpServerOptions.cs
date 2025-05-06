// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.Network.Transport;

/// <summary>
/// Represents the configuration options for a TCP server.
/// </summary>
public sealed class TcpServerOptions
{
    /// <summary>
    /// Gets or sets the host address of the TCP server.
    /// </summary>
    public required string Host { get; set; }

    /// <summary>
    /// Gets or sets the port number on which the TCP server listens.
    /// </summary>
    public required int Port { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of concurrent connections allowed by the TCP server.
    /// </summary>
    public required int MaxConnections { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of concurrent connections allowed per IP address.
    /// </summary>
    public required int MaxConnectionsPerIp { get; set; }
}
