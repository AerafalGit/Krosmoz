// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Dispatcher.Attributes;
using Krosmoz.Protocol.Messages.Connection;
using Krosmoz.Protocol.Messages.Connection.Register;
using Krosmoz.Servers.AuthServer.Network.Transport;
using Krosmoz.Servers.AuthServer.Services.Authentication;
using Krosmoz.Servers.AuthServer.Services.Nicknames;

namespace Krosmoz.Servers.AuthServer.Network.Handlers.Authentication;

/// <summary>
/// Handles authentication-related messages and nickname choice requests for Dofus connections.
/// </summary>
public sealed class AuthenticationHandler
{
    private readonly IAuthenticationService _authenticationService;
    private readonly INicknameService _nicknameService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationHandler"/> class.
    /// </summary>
    /// <param name="authenticationService">The service responsible for handling authentication.</param>
    /// <param name="nicknameService">The service responsible for handling nickname choices.</param>
    public AuthenticationHandler(IAuthenticationService authenticationService, INicknameService nicknameService)
    {
        _authenticationService = authenticationService;
        _nicknameService = nicknameService;
    }

    /// <summary>
    /// Handles the identification message asynchronously by delegating to the authentication service.
    /// </summary>
    /// <param name="connection">The Dofus connection associated with the message.</param>
    /// <param name="message">The identification message to process.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleIdentificationAsync(DofusConnection connection, IdentificationMessage message)
    {
        return _authenticationService.AuthenticateAsync(connection, message);
    }

    /// <summary>
    /// Handles the nickname choice request message asynchronously by delegating to the nickname service.
    /// </summary>
    /// <param name="connection">The Dofus connection associated with the message.</param>
    /// <param name="message">The nickname choice request message to process.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleNicknameChoiceRequestAsync(DofusConnection connection, NicknameChoiceRequestMessage message)
    {
        return _nicknameService.ChoiceNicknameAsync(connection, message.Nickname);
    }
}
