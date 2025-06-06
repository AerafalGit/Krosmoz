// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Commands.Attributes;

/// <summary>
/// Specifies a cooldown period for a command method.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class CommandCooldownAttribute : Attribute
{
    /// <summary>
    /// Gets the cooldown duration for the command.
    /// </summary>
    public TimeSpan Cooldown { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandCooldownAttribute"/> class with the specified cooldown duration.
    /// </summary>
    /// <param name="cooldown">The cooldown duration as a <see cref="TimeSpan"/>. Must be non-negative.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="cooldown"/> is negative.</exception>
    public CommandCooldownAttribute(TimeSpan cooldown)
    {
        if (cooldown < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(cooldown), "Cooldown must be a non-negative timespan.");

        Cooldown = cooldown;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandCooldownAttribute"/> class with the specified cooldown in seconds.
    /// </summary>
    /// <param name="seconds">The cooldown duration in seconds. Must be non-negative.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="seconds"/> is negative.</exception>
    public CommandCooldownAttribute(int seconds)
    {
        if (seconds < 0)
            throw new ArgumentOutOfRangeException(nameof(seconds), "Cooldown must be a non-negative integer.");

        Cooldown = TimeSpan.FromSeconds(seconds);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandCooldownAttribute"/> class with the specified cooldown in minutes and seconds.
    /// </summary>
    /// <param name="minutes">The cooldown duration in minutes. Must be non-negative.</param>
    /// <param name="seconds">The cooldown duration in seconds. Must be non-negative.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="minutes"/> or <paramref name="seconds"/> is negative.</exception>
    public CommandCooldownAttribute(int minutes, int seconds)
    {
        if (minutes < 0 || seconds < 0)
            throw new ArgumentException("Cooldown must be a non-negative integer.");

        Cooldown = new TimeSpan(0, minutes, seconds);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandCooldownAttribute"/> class with the specified cooldown in hours, minutes, and seconds.
    /// </summary>
    /// <param name="hours">The cooldown duration in hours. Must be non-negative.</param>
    /// <param name="minutes">The cooldown duration in minutes. Must be non-negative.</param>
    /// <param name="seconds">The cooldown duration in seconds. Must be non-negative.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="hours"/>, <paramref name="minutes"/>, or <paramref name="seconds"/> is negative.</exception>
    public CommandCooldownAttribute(int hours, int minutes, int seconds)
    {
        if (hours < 0 || minutes < 0 || seconds < 0)
            throw new ArgumentException("Cooldown must be a non-negative integer.");

        Cooldown = new TimeSpan(hours, minutes, seconds);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandCooldownAttribute"/> class with the specified cooldown in days, hours, minutes, and seconds.
    /// </summary>
    /// <param name="days">The cooldown duration in days. Must be non-negative.</param>
    /// <param name="hours">The cooldown duration in hours. Must be non-negative.</param>
    /// <param name="minutes">The cooldown duration in minutes. Must be non-negative.</param>
    /// <param name="seconds">The cooldown duration in seconds. Must be non-negative.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="days"/>, <paramref name="hours"/>, <paramref name="minutes"/>, or <paramref name="seconds"/> is negative.</exception>
    public CommandCooldownAttribute(int days, int hours, int minutes, int seconds)
    {
        if (days < 0 || hours < 0 || minutes < 0 || seconds < 0)
            throw new ArgumentException("Cooldown must be a non-negative integer.");

        Cooldown = new TimeSpan(days, hours, minutes, seconds);
    }
}
