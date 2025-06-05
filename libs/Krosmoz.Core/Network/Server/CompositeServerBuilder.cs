// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
using Krosmoz.Core.Network.Internal;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets;
using Microsoft.Extensions.Options;

namespace Krosmoz.Core.Network.Server;

/// <summary>
/// Represents a builder for creating a composite server.
/// Provides functionality to configure server bindings, heartbeat intervals, and shutdown timeouts.
/// </summary>
public sealed class CompositeServerBuilder
{
    /// <summary>
    /// Gets the transport options for socket-based connections.
    /// </summary>
    public SocketTransportOptions TransportOptions { get; }

    /// <summary>
    /// Gets the list of server bindings associated with the composite server.
    /// </summary>
    public IList<ServerBinding> Bindings { get; }

    /// <summary>
    /// Gets or sets the timeout duration for server shutdown.
    /// </summary>
    public TimeSpan ShutdownTimeout { get; set; }

    /// <summary>
    /// Gets or sets the interval for server heartbeat checks.
    /// </summary>
    public TimeSpan HeartBeatInterval { get; set; }

    /// <summary>
    /// Gets the service provider for application services.
    /// </summary>
    public IServiceProvider ApplicationServices { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeServerBuilder"/> class with default settings.
    /// </summary>
    public CompositeServerBuilder() : this(EmptyServiceProvider.Instance)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeServerBuilder"/> class.
    /// </summary>
    /// <param name="services">The service provider for application services.</param>
    public CompositeServerBuilder(IServiceProvider services)
    {
        ApplicationServices = services;
        Bindings = [];
        ShutdownTimeout = TimeSpan.FromSeconds(5);
        HeartBeatInterval = TimeSpan.FromSeconds(1);
        TransportOptions = new SocketTransportOptions();
    }

    /// <summary>
    /// Configures the server to listen on localhost at the specified port.
    /// </summary>
    /// <param name="port">The port number to listen on.</param>
    /// <param name="configure">A delegate to configure the connection builder.</param>
    /// <returns>The current instance of <see cref="CompositeServerBuilder"/>.</returns>
    public CompositeServerBuilder ListenLocalhost(int port, Action<IConnectionBuilder> configure)
    {
        var connectionBuilder = new ConnectionBuilder(ApplicationServices);

        configure(connectionBuilder);

        Bindings.Add(new ServerBinding(port, connectionBuilder.Build(), new SocketTransportFactory(Options.Create(TransportOptions), ApplicationServices.GetLoggerFactory())));

        return this;
    }

    /// <summary>
    /// Builds and returns a new instance of the <see cref="CompositeServer"/> class.
    /// </summary>
    /// <returns>A new composite server instance.</returns>
    public CompositeServer Build()
    {
        return new CompositeServer(this);
    }
}
