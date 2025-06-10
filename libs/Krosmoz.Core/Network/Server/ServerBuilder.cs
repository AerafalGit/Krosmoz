// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
using Krosmoz.Core.Network.Internal;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Krosmoz.Core.Network.Server;

/// <summary>
/// Represents a builder for creating a server.
/// Provides functionality to configure server binding, heartbeat intervals, and shutdown timeouts.
/// </summary>
public sealed class ServerBuilder
{
    /// <summary>
    /// Gets the transport options for socket-based connections.
    /// </summary>
    public SocketTransportOptions TransportOptions { get; }

    /// <summary>
    /// Gets the server bindings for the composite server.
    /// </summary>
    public ServerBinding? Binding { get; set; }

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
    /// Initializes a new instance of the <see cref="ServerBuilder"/> class with default settings.
    /// </summary>
    public ServerBuilder() : this(EmptyServiceProvider.Instance)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServerBuilder"/> class.
    /// </summary>
    /// <param name="services">The service provider for application services.</param>
    public ServerBuilder(IServiceProvider services)
    {
        ApplicationServices = services;
        ShutdownTimeout = TimeSpan.FromSeconds(5);
        HeartBeatInterval = TimeSpan.FromSeconds(1);
        TransportOptions = new SocketTransportOptions();
    }

    /// <summary>
    /// Configures the server to listen on localhost at the specified port.
    /// </summary>
    /// <param name="port">The port number to listen on.</param>
    /// <param name="configure">A delegate to configure the connection builder.</param>
    /// <returns>The current instance of <see cref="ServerBuilder"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the port number is less than or equal to zero, or greater than 65535.
    /// </exception>
    public ServerBuilder ListenLocalhost(int port, Action<IConnectionBuilder> configure)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(port);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(port, ushort.MaxValue);

        var connectionBuilder = new ConnectionBuilder(ApplicationServices);

        configure(connectionBuilder);

        Binding = new ServerBinding(port, connectionBuilder.Build(), new SocketTransportFactory(Options.Create(TransportOptions), ApplicationServices.GetLoggerFactory()));

        return this;
    }

    /// <summary>
    /// Configures the server to listen on a port specified by an environment variable.
    /// </summary>
    /// <param name="configure">A delegate to configure the connection builder.</param>
    /// <returns>The current instance of <see cref="ServerBuilder"/>.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the environment variable is not set or does not contain a valid port number.
    /// </exception>
    public ServerBuilder ListenFromEnvironment(Action<IConnectionBuilder> configure)
    {
        var configuration = ApplicationServices.GetRequiredService<IConfiguration>();

        var connectionString = configuration.GetConnectionString("socket-server");

        var uri = new Uri(connectionString!);

        return ListenLocalhost(uri.Port, configure);
    }

    /// <summary>
    /// Builds and returns a new instance of the <see cref="Server"/> class.
    /// </summary>
    /// <returns>A new composite server instance.</returns>
    public Server Build()
    {
        if (Binding is null)
            throw new InvalidOperationException("Server binding must be configured before building the server.");

        return new Server(this);
    }
}
