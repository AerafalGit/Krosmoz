// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Commands;

/// <summary>
/// Defines the possible error types that can occur during command execution.
/// </summary>
public enum CommandErrors
{
    /// <summary>
    /// Indicates that no error occurred.
    /// </summary>
    None,

    /// <summary>
    /// Indicates that the command failed due to a cooldown restriction.
    /// </summary>
    BadCooldown,

    /// <summary>
    /// Indicates that the command failed due to a hierarchy mismatch.
    /// </summary>
    BadHierarchy,

    /// <summary>
    /// Indicates that the command was not found.
    /// </summary>
    CommandNotFound
}
