// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Network.Transport;

namespace Krosmoz.Servers.GameServer.Services.Characters.Deletion;

/// <summary>
/// Defines the contract for a service that handles character deletion operations.
/// </summary>
public interface ICharacterDeletionService
{
    /// <summary>
    /// Deletes a character asynchronously based on the provided parameters.
    /// </summary>
    /// <param name="connection">The connection associated with the character deletion.</param>
    /// <param name="characterId">The ID of the character to delete.</param>
    /// <param name="secretAnswerHash">The hash of the secret answer used for verification.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteCharacterAsync(DofusConnection connection, int characterId, string secretAnswerHash);
}
