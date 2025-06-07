// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Services.InfoMessages;

/// <summary>
/// Defines a service for sending informational messages to characters.
/// </summary>
public interface IInfoMessageService
{
    /// <summary>
    /// Sends a text information message asynchronously to the specified character.
    /// </summary>
    /// <param name="character">The character to whom the message will be sent.</param>
    /// <param name="type">The type of the text information message.</param>
    /// <param name="messageId">The unique identifier of the message.</param>
    /// <param name="parameters">The parameters to include in the message.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask SendTextInformationAsync(CharacterActor character, TextInformationTypes type, ushort messageId, params IEnumerable<string> parameters);

    /// <summary>
    /// Sends a plain text message asynchronously to the specified character.
    /// </summary>
    /// <param name="character">The character to whom the message will be sent.</param>
    /// <param name="message">The plain text message to send.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask SendTextInformationAsync(CharacterActor character, string message);
}
