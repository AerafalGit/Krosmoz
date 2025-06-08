// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Network.Transport;

namespace Krosmoz.Servers.GameServer.Services.Characters.Selection;

/// <summary>
/// Defines the contract for a service that handles character selection operations.
/// </summary>
public interface ICharacterSelectionService
{
    /// <summary>
    /// Sends the list of characters to the specified connection asynchronously.
    /// </summary>
    /// <param name="connection">The connection to send the character list to.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendCharacterListAsync(DofusConnection connection);

    /// <summary>
    /// Selects a character asynchronously based on the provided parameters.
    /// </summary>
    /// <param name="connection">The connection associated with the character selection.</param>
    /// <param name="firstSelection">Indicates whether this is the first character selection.</param>
    /// <param name="doTutorial">Indicates whether the tutorial should be started.</param>
    /// <param name="characterId">The ID of the character to select.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SelectCharacterAsync(DofusConnection connection, bool firstSelection, bool doTutorial, int characterId);
}
