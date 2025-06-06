// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Commands.Attributes;

/// <summary>
/// Specifies that a class represents a command group.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class CommandGroupAttribute : Attribute
{
    /// <summary>
    /// Gets the name of the command group.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandGroupAttribute"/> class with the specified name.
    /// </summary>
    /// <param name="name">The name of the command group. Cannot be null or empty.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is null or empty.</exception>
    public CommandGroupAttribute(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        Name = name;
    }
}
