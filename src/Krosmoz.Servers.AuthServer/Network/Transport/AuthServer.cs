// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Protocol.Dofus;
using Krosmoz.Core.Network.Transport;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Krosmoz.Servers.AuthServer.Network.Transport;

/// <summary>
/// Represents the authentication server for handling TCP connections and Dofus messages.
/// </summary>
public sealed class AuthServer : TcpServer<AuthSession, DofusMessage>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthServer"/> class.
    /// </summary>
    /// <param name="services">The service provider used for dependency injection.</param>
    /// <param name="logger">The logger used for logging server-related information.</param>
    /// <param name="options">The options for configuring the TCP server.</param>
    public AuthServer(IServiceProvider services, ILogger<TcpServer<AuthSession, DofusMessage>> logger, IOptions<TcpServerOptions> options)
        : base(services, logger, options)
    {
    }
}
