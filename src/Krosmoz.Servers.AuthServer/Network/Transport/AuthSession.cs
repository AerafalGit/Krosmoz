// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Net.Sockets;
using Krosmoz.Core.Network.Dispatcher;
using Krosmoz.Core.Network.Factory;
using Krosmoz.Core.Network.Framing;
using Krosmoz.Core.Network.Protocol.Dofus;
using Krosmoz.Core.Network.Transport;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Messages.Connection;
using Krosmoz.Protocol.Messages.Handshake;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Servers.AuthServer.Network.Transport;

/// <summary>
/// Represents an authentication session for handling Dofus messages over a TCP connection.
/// </summary>
public sealed class AuthSession : TcpSession<DofusMessage>
{
    private readonly IMessageFactory<DofusMessage> _messageFactory;

    public string Salt { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthSession"/> class.
    /// </summary>
    /// <param name="socket">The socket used for the TCP connection.</param>
    /// <param name="messageDecoder">The decoder for Dofus messages.</param>
    /// <param name="messageEncoder">The encoder for Dofus messages.</param>
    /// <param name="dispatcher">The dispatcher for handling Dofus messages.</param>
    /// <param name="logger">The logger for logging session-related information.</param>
    /// <param name="messageFactory">The factory for creating message names based on protocol IDs.</param>
    public AuthSession(
        Socket socket,
        IMessageDecoder<DofusMessage> messageDecoder,
        IMessageEncoder<DofusMessage> messageEncoder,
        IMessageDispatcher<DofusMessage> dispatcher,
        ILogger<TcpSession<DofusMessage>> logger,
        IMessageFactory<DofusMessage> messageFactory)
        : base(socket, messageDecoder, messageEncoder, dispatcher, logger)
    {
        _messageFactory = messageFactory;

        Salt = Guid.NewGuid().ToString().Replace("-", string.Empty);
    }

    /// <summary>
    /// Retrieves the name of a Dofus message based on its protocol ID.
    /// </summary>
    /// <param name="message">The Dofus message whose name is to be retrieved.</param>
    /// <returns>The name of the message.</returns>
    protected override string GetMessageName(DofusMessage message)
    {
        return _messageFactory.CreateMessageName(message.ProtocolId);
    }

    /// <summary>
    /// Handles actions to be performed when the session is connected.
    /// Sends the required protocol version and a HelloConnect message to the client.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected override async Task OnConnectedAsync()
    {
        await SendAsync(new ProtocolRequired { RequiredVersion = (int)Metadata.ProtocolRequiredBuild, CurrentVersion = (int)Metadata.ProtocolBuild });
        await SendAsync(new HelloConnectMessage { Key = [], Salt = Salt });
    }
}
