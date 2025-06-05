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
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the port number is less than or equal to zero, or greater than 65535.
    /// </exception>
    public CompositeServerBuilder ListenLocalhost(int port, Action<IConnectionBuilder> configure)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(port);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(port, ushort.MaxValue);

        var connectionBuilder = new ConnectionBuilder(ApplicationServices);

        configure(connectionBuilder);

        Bindings.Add(new ServerBinding(port, connectionBuilder.Build(), new SocketTransportFactory(Options.Create(TransportOptions), ApplicationServices.GetLoggerFactory())));

        return this;
    }

    /// <summary>
    /// Configures the server to listen on a port specified by an environment variable.
    /// </summary>
    /// <param name="environmentVariable">The name of the environment variable containing the port number.</param>
    /// <param name="configure">A delegate to configure the connection builder.</param>
    /// <returns>The current instance of <see cref="CompositeServerBuilder"/>.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the environment variable is not set or does not contain a valid port number.
    /// </exception>
    public CompositeServerBuilder ListenFromEnvironment(string environmentVariable, Action<IConnectionBuilder> configure)
    {
        if (!int.TryParse(Environment.GetEnvironmentVariable(environmentVariable), out var port))
            throw new ArgumentException($"Environment variable '{environmentVariable}' is not set or is not a valid port number.", nameof(environmentVariable));

        return ListenLocalhost(port, configure);
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
