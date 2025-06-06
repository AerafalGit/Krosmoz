// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;

namespace Krosmoz.Servers.GameServer.Models.Options.Breeds;

/// <summary>
/// Represents the configuration options for breeds in the game server.
/// </summary>
public sealed class BreedOptions
{
    /// <summary>
    /// Gets or sets the collection of breeds that are visible to players.
    /// </summary>
    public required IEnumerable<PlayableBreeds> VisibleBreeds { get; set; }

    /// <summary>
    /// Gets or sets the collection of breeds that are playable by players.
    /// </summary>
    public required IEnumerable<PlayableBreeds> PlayableBreeds { get; set; }
}
