// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Models.Commands;

/// <summary>
/// Represents a group of commands with a name and description.
/// </summary>
public sealed class CommandGroup
{
    /// <summary>
    /// Gets or sets the name of the command group.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the description of the command group.
    /// </summary>
    public required string Description { get; init; }

    /// <summary>
    /// Gets or sets the array of commands in the group.
    /// </summary>
    public required Command[] Commands { get; init; }
}
