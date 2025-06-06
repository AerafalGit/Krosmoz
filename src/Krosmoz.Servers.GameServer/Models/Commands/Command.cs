// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Models.Commands;

/// <summary>
/// Represents a command with a name, description, and help text.
/// </summary>
public sealed class Command
{
    /// <summary>
    /// Gets or sets the name of the command.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the description of the command.
    /// </summary>
    public required string Description { get; init; }

    /// <summary>
    /// Gets or sets the help text for the command.
    /// </summary>
    public required string Help { get; init; }
}
