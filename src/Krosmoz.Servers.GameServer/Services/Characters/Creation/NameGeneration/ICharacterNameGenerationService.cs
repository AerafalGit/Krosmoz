// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Services.Characters.Creation.NameGeneration;

/// <summary>
/// Defines the contract for a service that generates character names.
/// </summary>
public interface ICharacterNameGenerationService
{
    /// <summary>
    /// Generates a new character name.
    /// </summary>
    /// <returns>A randomly generated character name as a string.</returns>
    string GenerateName();
}
