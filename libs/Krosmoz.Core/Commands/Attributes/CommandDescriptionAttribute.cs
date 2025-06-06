// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.Commands.Attributes;

/// <summary>
/// Specifies a description for a command method.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class CommandDescriptionAttribute : Attribute
{
    /// <summary>
    /// Gets the description of the command.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandDescriptionAttribute"/> class with the specified description.
    /// </summary>
    /// <param name="description">The description of the command. Cannot be null or empty.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="description"/> is null or empty.</exception>
    public CommandDescriptionAttribute(string description)
    {
        ArgumentException.ThrowIfNullOrEmpty(description);

        Description = description;
    }
}
