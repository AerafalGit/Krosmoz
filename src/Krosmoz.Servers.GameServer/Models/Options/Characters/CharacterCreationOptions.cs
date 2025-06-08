// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Models.Options.Characters;

/// <summary>
/// Represents the options for character creation in the game server.
/// </summary>
public sealed class CharacterCreationOptions
{
    /// <summary>
    /// Gets or sets the initial level of the character being created.
    /// </summary>
    public required byte Level { get; set; }
}
