// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Commands;

/// <summary>
/// Represents the context in which a command is executed.
/// </summary>
public class CommandContext
{
    /// <summary>
    /// Gets or sets the service provider used to resolve dependencies for the command.
    /// </summary>
    public required IServiceProvider Services { get; set; }

    /// <summary>
    /// Gets or sets the string argument associated with the command.
    /// </summary>
    public required string Argument { get; set; }

    /// <summary>
    /// Gets or sets the custom index of the current argument, if applicable.
    /// </summary>
    public int ArgumentLength { get; set; } = -1;
}
