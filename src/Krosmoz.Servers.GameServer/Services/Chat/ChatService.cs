// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Game.Chat;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Servers;

namespace Krosmoz.Servers.GameServer.Services.Chat;

/// <summary>
/// Provides chat-related services for the game server, including sending server messages to clients.
/// </summary>
public sealed class ChatService : IChatService
{
    private readonly IServerService _serverService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatService"/> class.
    /// </summary>
    /// <param name="serverService">The server service used to retrieve server-related information.</param>
    public ChatService(IServerService serverService)
    {
        _serverService = serverService;
    }

    /// <summary>
    /// Sends a server message to a specified connection asynchronously.
    /// </summary>
    /// <param name="connection">The connection to which the message will be sent.</param>
    /// <param name="message">The message to send, which supports composite formatting.</param>
    /// <param name="args">An array of objects to format into the message.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    public ValueTask SendServerMessageAsync(DofusConnection connection, string message, params object[] args)
    {
        return connection.SendAsync(new ChatServerMessage
        {
            SenderId = 0,
            SenderAccountId = 0,
            Channel = (sbyte)ChatActivableChannels.PseudoChannelInfo,
            Fingerprint = string.Empty,
            SenderName = _serverService.ServerName,
            Timestamp = DateTime.UtcNow.GetUnixTimestampSeconds(),
            Content = string.Format(message, args)
        });
    }
}
