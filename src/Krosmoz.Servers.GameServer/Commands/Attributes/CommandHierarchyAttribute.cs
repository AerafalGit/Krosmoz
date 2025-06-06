// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;

namespace Krosmoz.Servers.GameServer.Commands.Attributes;

/// <summary>
/// Specifies the hierarchy level required for a command method.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class CommandHierarchyAttribute : Attribute
{
    /// <summary>
    /// Gets the game hierarchy level associated with the command.
    /// </summary>
    public GameHierarchies Hierarchy { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandHierarchyAttribute"/> class with the specified hierarchy level.
    /// </summary>
    /// <param name="hierarchy">The game hierarchy level required for the command.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="hierarchy"/> is not a valid <see cref="GameHierarchies"/> value.</exception>
    public CommandHierarchyAttribute(GameHierarchies hierarchy)
    {
        if (!Enum.IsDefined(hierarchy))
            throw new ArgumentOutOfRangeException(nameof(hierarchy), "Invalid game hierarchy specified.");

        Hierarchy = hierarchy;
    }
}
