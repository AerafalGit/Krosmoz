// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Dispatcher;
using Krosmoz.Core.Network.Framing;
using Krosmoz.Core.Network.Framing.Factory;
using Krosmoz.Core.Network.Framing.Serialization;
using Krosmoz.Protocol.Constants;
using Krosmoz.Protocol.Messages.Connection;
using Krosmoz.Protocol.Messages.Handshake;
using Krosmoz.Servers.AuthServer.Services.Queue;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Servers.AuthServer.Network.Transport;

/// <summary>
/// Handles Dofus client connections, managing message reading, writing, and dispatching.
/// </summary>
public sealed class DofusConnectionHandler : ConnectionHandler
{
    private readonly ObjectFactory<DofusConnection> _connectionFactory;
    private readonly IServiceProvider _provider;
    private readonly IMessageFactory _messageFactory;
    private readonly IQueueService _queueService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DofusConnectionHandler"/> class.
    /// </summary>
    /// <param name="provider">The service provider for dependency injection.</param>
    /// <param name="messageFactory">The factory for creating message instances.</param>
    /// <param name="queueService">The service for managing the authentication connection queue.</param>
    public DofusConnectionHandler(IServiceProvider provider, IMessageFactory messageFactory, IQueueService queueService)
    {
        _provider = provider;
        _messageFactory = messageFactory;
        _queueService = queueService;
        _connectionFactory = ActivatorUtilities.CreateFactory<DofusConnection>(
        [
            typeof(ConnectionContext),
            typeof(FrameWriter),
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
        connection.Features.Set(_messageFactory);
        connection.Features.Set(new MessageDecoder(_messageFactory));

        await using var reader = connection.CreateReader();

        var writer = connection.CreateWriter();
        var logger = _provider.GetRequiredService<ILogger<DofusConnection>>();
        var dispatcher = _provider.GetRequiredService<IMessageDispatcher<DofusConnection>>();

        await using var dofusConnection = _connectionFactory(_provider, [connection, writer, logger]);

        logger.LogDebug("DofusConnection {ConnectionName} established", dofusConnection);

        await OnConnectionEstablishedAsync(dofusConnection);

        try
        {
            await foreach (var readResult in reader.ReadAllAsync(dofusConnection.ConnectionClosed))
            {
                var messageName = _messageFactory.CreateMessageName(readResult.Message!.ProtocolId);

                logger.LogDebug("DofusConnection {ConnectionName} received message {MessageName} ({MessageId})", dofusConnection, messageName, readResult.Message.ProtocolId);

                await dispatcher.DispatchMessageAsync(dofusConnection, readResult.Message);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "DofusConnection {ConnectionName} encountered an error", dofusConnection);
        }
        finally
        {
            await OnConnectionClosedAsync(dofusConnection);

            logger.LogDebug("DofusConnection {ConnectionName} closed", dofusConnection);
        }
    }

    /// <summary>
    /// Handles the establishment of a new Dofus connection asynchronously.
    /// Sends the required protocol version and a hello connect message to the client,
    /// and enqueues the connection in the authentication queue.
    /// </summary>
    /// <param name="connection">The Dofus connection to be established.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task OnConnectionEstablishedAsync(DofusConnection connection)
    {
        await connection.SendAsync(new ProtocolRequired { RequiredVersion = MetadataConstants.ProtocolRequiredBuild, CurrentVersion = MetadataConstants.ProtocolBuild });

        await connection.SendAsync(new HelloConnectMessage { Key = [], Salt = string.Empty });

        _queueService.Enqueue(connection);
    }

    /// <summary>
    /// Handles the closure of a Dofus connection asynchronously.
    /// Removes the connection from the authentication queue and sends the queue status
    /// if the connection is still active.
    /// </summary>
    /// <param name="connection">The Dofus connection to be closed.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task OnConnectionClosedAsync(DofusConnection connection)
    {
        _queueService.Dequeue(connection);

        if (!connection.IsConnected)
            return;

        await _queueService.SendQueueStatusAsync(connection, 0, 0);
    }
}
