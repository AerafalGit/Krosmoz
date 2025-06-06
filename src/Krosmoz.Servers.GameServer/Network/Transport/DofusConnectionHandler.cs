// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Framing;
using Krosmoz.Core.Network.Metadata;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Servers.GameServer.Network.Transport;

/// <summary>
/// Handles Dofus client connections, managing message reading, writing, and dispatching.
/// </summary>
public sealed class DofusConnectionHandler : ConnectionHandler
{
    private readonly ObjectFactory<DofusConnection> _connectionFactory;
    private readonly IServiceProvider _provider;
    private readonly IMessageFactory _messageFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="DofusConnectionHandler"/> class.
    /// </summary>
    /// <param name="provider">The service provider for dependency injection.</param>
    /// <param name="messageFactory">The factory for creating message instances.</param>
    public DofusConnectionHandler(IServiceProvider provider, IMessageFactory messageFactory)
    {
        _provider = provider;
        _messageFactory = messageFactory;
        _connectionFactory = ActivatorUtilities.CreateFactory<DofusConnection>(
        [
            typeof(ConnectionContext),
            typeof(FrameWriter<DofusMessage>),
            typeof(IMessageFactory),
            typeof(ILogger<DofusConnection>)
        ]);
    }

    /// <summary>
    /// Handles a new connection asynchronously.
    /// </summary>
    /// <param name="connection">The connection context for the new connection.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public override async Task OnConnectedAsync(ConnectionContext connection)
    {
        await using var reader = connection.CreateReader(new DofusMessageDecoder(_messageFactory));

        var writer = connection.CreateWriter(new DofusMessageEncoder());
        var logger = _provider.GetRequiredService<ILogger<DofusConnection>>();
        var dispatcher = _provider.GetRequiredService<IMessageDispatcher<DofusConnection>>();

        await using var dofusConnection = _connectionFactory(_provider, [connection, writer, _messageFactory, logger]);

        logger.LogDebug("DofusConnection {ConnectionName} established", dofusConnection);

        try
        {
            await foreach (var message in reader.ReadAllAsync(dofusConnection.ConnectionClosed))
            {
                var messageName = _messageFactory.CreateMessageName(message.ProtocolId);

                logger.LogDebug("DofusConnection {ConnectionName} received message {MessageName} ({MessageId})", dofusConnection, messageName, message.ProtocolId);

                await dispatcher.DispatchMessageAsync(dofusConnection, message);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "DofusConnection {ConnectionName} encountered an error", dofusConnection);
        }
        finally
        {
            logger.LogDebug("DofusConnection {ConnectionName} closed", dofusConnection);
        }
    }
}
