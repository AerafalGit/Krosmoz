// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Krosmoz.Protocol.Enums;

namespace Krosmoz.Servers.GameServer.Commands;

/// <summary>
/// Represents the result of a command execution, including error details, hierarchy requirements, cooldowns, and exceptions.
/// </summary>
/// <param name="Error">The error type associated with the command execution.</param>
/// <param name="RequiredHierarchy">The hierarchy required to execute the command, if applicable.</param>
/// <param name="PlayerHierarchy">The player's hierarchy during the command execution, if applicable.</param>
/// <param name="Cooldown">The cooldown duration for the command, if applicable.</param>
public record struct CommandResult(
    CommandErrors Error,
    GameHierarchies? RequiredHierarchy,
    GameHierarchies? PlayerHierarchy,
    TimeSpan? Cooldown)
{
    /// <summary>
    /// Gets a value indicating whether the command execution was successful.
    /// </summary>
    public bool IsSuccess =>
        Error is CommandErrors.None && RequiredHierarchy is null && PlayerHierarchy is null && Cooldown is null;

    /// <summary>
    /// Gets a value indicating whether the command failed due to a hierarchy mismatch.
    /// </summary>
    [MemberNotNullWhen(true, nameof(RequiredHierarchy), nameof(PlayerHierarchy))]
    public bool IsBadHierarchy =>
        Error is CommandErrors.BadHierarchy && RequiredHierarchy is not null && PlayerHierarchy is not null;

    /// <summary>
    /// Gets a value indicating whether the command failed due to a cooldown restriction.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Cooldown))]
    public bool IsBadCooldown =>
        Error is CommandErrors.BadCooldown && Cooldown is not null;

    /// <summary>
    /// Gets a value indicating whether the command was not found.
    /// </summary>
    public bool IsCommandNotFound =>
        Error is CommandErrors.CommandNotFound;

    /// <summary>
    /// Creates a successful command result.
    /// </summary>
    /// <returns>A <see cref="CommandResult"/> indicating success.</returns>
    public static CommandResult Success()
    {
        return new CommandResult(CommandErrors.None, null, null, null);
    }

    /// <summary>
    /// Creates a command result indicating a hierarchy mismatch.
    /// </summary>
    /// <param name="requiredHierarchy">The required hierarchy for the command.</param>
    /// <param name="playerHierarchy">The player's hierarchy during the command execution.</param>
    /// <returns>A <see cref="CommandResult"/> indicating a bad hierarchy error.</returns>
    public static CommandResult BadHierarchy(GameHierarchies requiredHierarchy, GameHierarchies playerHierarchy)
    {
        return new CommandResult(CommandErrors.BadHierarchy, requiredHierarchy, playerHierarchy, null);
    }

    /// <summary>
    /// Creates a command result indicating a cooldown restriction.
    /// </summary>
    /// <param name="cooldown">The cooldown duration for the command.</param>
    /// <returns>A <see cref="CommandResult"/> indicating a bad cooldown error.</returns>
    public static CommandResult BadCooldown(TimeSpan cooldown)
    {
        return new CommandResult(CommandErrors.BadCooldown, null, null, cooldown);
    }

    /// <summary>
    /// Creates a command result indicating that the command was not found.
    /// </summary>
    /// <returns>A <see cref="CommandResult"/> indicating a command not found error.</returns>
    public static CommandResult CommandNotFound()
    {
        return new CommandResult(CommandErrors.CommandNotFound, null, null, null);
    }
}
