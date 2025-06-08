// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Servers.GameServer.Network.Transport;

namespace Krosmoz.Servers.GameServer.Services.Characters.Creation;

/// <summary>
/// Defines the contract for character creation services.
/// Provides methods for suggesting character names and creating new characters.
/// </summary>
public interface ICharacterCreationService
{
    /// <summary>
    /// Sends a character name suggestion to the specified connection.
    /// </summary>
    /// <param name="connection">The connection to which the name suggestion will be sent.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    ValueTask SendCharacterNameSuggestionAsync(DofusConnection connection);

    /// <summary>
    /// Creates a new character with the specified attributes.
    /// </summary>
    /// <param name="connection">The connection associated with the character creation.</param>
    /// <param name="name">The name of the character to be created.</param>
    /// <param name="breedId">The breed identifier of the character.</param>
    /// <param name="sex">The gender of the character (true for male, false for female).</param>
    /// <param name="colors">A list of color identifiers for the character's appearance.</param>
    /// <param name="cosmeticId">The cosmetic identifier for the character.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CreateCharacterAsync(DofusConnection connection, string name, BreedIds breedId, bool sex, IList<int> colors, ushort cosmeticId);
}
